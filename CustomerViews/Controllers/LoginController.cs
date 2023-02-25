using System.Net.Mail;
using System.Net;
using _1_API.ViewModel.KhachHang;
using CustomerViews.IServices;
using CustomerViews.Models;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using _1_API.ViewModel.NhanVien;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CustomerViews.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IAllServices _services;
        public LoginController(ILogger<LoginController> logger, IAllServices services)
        {
            _logger = logger;
            _services = services;
        }
        public IActionResult DangNhap()
        {
            return View();
        }
        public async Task<IActionResult> CheckDangNhap(LoginRequest request)
        {
            if (ModelState.IsValid)
            {

                ViewData["loginfalse"] = "";
                if (HttpContext.Session.GetString("idkh") != null)
                {
                    ViewData["loginfalse"] = "Bạn đã đăng nhập rồi";
                    return View("DangNhap");
                }
                else
                {
                    var lstKH = await _services.GetAll<KhachHang>(Connection.api + "KhachHangs/Get-All");
                    if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.MatKhau))
                    {
                        ViewData["loginfalse"] = "Tài khoản hoặc mật khẩu không được để trống";
                        return View("DangNhap");
                    }
                    else
                    {
                        var kh = lstKH.FirstOrDefault(p => p.Email == request.Email && p.MatKhau == request.MatKhau);
                        if (kh == null)
                        {
                            ViewData["loginfalse"] = "Sai tài khoản hoặc mật khẩu";
                            return View("DangNhap");
                        }
                        else
                        {
                            HttpContext.Session.SetString("idkh", kh.Id.ToString());
                            HttpContext.Session.SetString("ten", kh.Ten ?? "");

                            var lstGH = await _services.GetAll<GioHang>(Connection.api + "GioHangs/Get-All");
                            var gh = lstGH.FirstOrDefault(x => x.IdKH == kh.Id);
                            if (gh != null)
                                HttpContext.Session.SetString("idgh", gh.Id.ToString());
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            return View("DangNhap");
        }
        public IActionResult DangXuat()
        {
            HttpContext.Session.Remove("idkh");
            HttpContext.Session.Remove("ten");
            HttpContext.Session.Remove("idgh");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult DangKy()
        {
            return View();
        }
        public async Task<IActionResult> CheckDangKy(CreateKhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                var lstKH = await _services.GetAll<KhachHang>(Connection.api + "KhachHangs/Get-All");
                if (lstKH.FirstOrDefault(x=>x.Email == khachHang.Email)!=null)
                {
                    ViewData["thongbao"] = "Email đã tồn tại";
                    return View("DangKy");
                }
                if (lstKH.FirstOrDefault(x => x.Sdt == khachHang.Sdt) != null)
                {
                    ViewData["thongbao"] = "Số điện thoại đã tồn tại";
                    return View("DangKy");
                }
                var success = await _services.Add<CreateKhachHang>(Connection.api + "KhachHangs/", khachHang);
                if (success != null)
                {
                    ViewData["dangkythanhcong"] = "Đăng ký thành công!!!";
                    return View("DangNhap");
                }
                else
                {
                    return View("DangKy");
                }
            }
            return View("DangKy");
        }
        public IActionResult QuenMK()
        {
            return View();
        }
        public async Task<IActionResult> CheckQuenMK(QuenMK request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.SDT))
            {
                ViewData["check"] = "Email hoặc số điện thoại không được để trống";
                return View("QuenMK");
            }
            var lstKH = await _services.GetAll<KhachHang>(Connection.api + "KhachHangs/Get-All");
            var tk = lstKH.FirstOrDefault(p => p.Email == request.Email && p.Sdt == request.SDT);
            if (tk != null)
            {
                var fromAddress = new MailAddress("nguyenhuukhoa5462@gmail.com");
                var toAddress = new MailAddress(tk.Email);
                const string fromPassword = "mqanjbksawuxofko";
                string subject = "Quên mật khẩu";
                string body = "Mật khẩu của bạn là: " + tk.MatKhau;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                ViewData["dangkythanhcong"] = "Lấy lại mật khẩu thành công. Vui lòng check email!!!";
                return View("DangNhap");
            }
            else
            {
                ViewData["check"] = "Email hoặc số điện thoại không đúng";
                return View("QuenMK");
            }
        }
        public IActionResult DoiMK()
        {
            return View();
        }

        public async Task<IActionResult> ThayDoiMK(ThayDoiMKRequest request)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Sdt) || string.IsNullOrEmpty(request.MatKhau))
                {
                    ViewData["check"] = "Email hoặc số điện thoại không được để trống";
                    return View("DoiMK");
                }
           
                var lstKH = await _services.GetAll<KhachHang>(Connection.api + "KhachHangs/Get-All");
                var tk = lstKH.FirstOrDefault(p => p.Email == request.Email && p.Sdt == request.Sdt && p.MatKhau == request.MatKhau);
                if (request.MatKhauMoi == request.NhapLaiMkm)
                {
                    if (tk != null)
                    {
                        tk.MatKhau = request.MatKhauMoi;

                        await _services.Update<KhachHang>(Connection.api + "KhachHangs/Update/", tk, tk.Id);
                        HttpContext.Session.Remove("idkh");
                        HttpContext.Session.Remove("ten");
                        HttpContext.Session.Remove("idgh");
                        ViewData["thanhcong"] = "Thay đổi mật khẩu thành công";
                        return View("DangNhap");
                    }
                    else
                    {
                        ViewData["loidmk"] = "Thông tin tài khoản không chính xác";
                        return View("DoiMK"); ;
                    }
                }
                else
                {
                    ViewData["loidmk"] = "Mật khẩu mới không trùng khớp, hãy nhập lại";
                    return View("DoiMK");
                }
            }
            else
            {
                return View("DoiMK");
            }
            


        }

        public async Task<IActionResult> ThongTinKhachHang()
        {
            var id = HttpContext.Session.GetString("idkh");
            Guid guidID = Guid.Parse(id);
            var kh = await _services.GetById<KhachHang>(Connection.api + "KhachHangs/GetById/", guidID);
            ViewKhachHang view = new ViewKhachHang()
            {
                Id = kh.Id,
                Ten = kh.Ten,
                DiaChi = kh.DiaChi,
                Email = kh.Email,
                GioiTinh = kh.GioiTinh,
                Sdt = kh.Sdt,
                NgaySinh = kh.NgaySinh
            };
            return View(view);
        }
        public async Task<IActionResult> UpdateKH()
        {
            var id = HttpContext.Session.GetString("idkh");
            Guid guidID = Guid.Parse(id);
            var kh = await _services.GetById<KhachHang>(Connection.api + "KhachHangs/GetById/", guidID);
            UpdateKhachHang view = new UpdateKhachHang()
            {
                Id = kh.Id,
                Ten = kh.Ten,
                DiaChi = kh.DiaChi,
                GioiTinh = kh.GioiTinh,
                Sdt = kh.Sdt,
                NgaySinh = kh.NgaySinh,
                Email = kh.Email,
                MatKhau = kh.MatKhau
            };
            return View(view);
        }

        public async Task<IActionResult> Updateing(UpdateKhachHang kh)
        {
            var id = HttpContext.Session.GetString("idkh");
            Guid guidID = Guid.Parse(id);
            UpdateKhachHang up = new UpdateKhachHang()
            {
                Id = kh.Id,
                Ten = kh.Ten,
                DiaChi = kh.DiaChi,
                Sdt = kh.Sdt,
                Email = kh.Email,
                MatKhau = kh.MatKhau,
                NgaySinh = kh.NgaySinh,
                GioiTinh = kh.GioiTinh,
            };
            var khachhang = await _services.Update<UpdateKhachHang>("https://localhost:7203/api/KhachHangs/Update/", up, guidID);
            var viewkh = new ViewKhachHang
            {
                Id = khachhang.Id,
                Ten = khachhang.Ten,
                DiaChi = khachhang.DiaChi,
                Email = khachhang.Email,
                GioiTinh = khachhang.GioiTinh,
                Sdt = khachhang.Sdt,
                NgaySinh = khachhang.NgaySinh
            };
            return View("ThongTinKhachHang", viewkh);



        }


    }
}
