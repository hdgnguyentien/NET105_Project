using System.Net.Mail;
using System.Net;
using _1_API.ViewModel.KhachHang;
using CustomerViews.IServices;
using CustomerViews.Models;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

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
                        HttpContext.Session.SetString("ten", kh.Ten??"");
                        var lstGH = await _services.GetAll<GioHang>(Connection.api + "GioHangs/Get-All");
                        var gh = lstGH.FirstOrDefault(x => x.IdKH == kh.Id);
                        if (gh != null)
                            HttpContext.Session.SetString("idgh", gh.Id.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
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
                ViewData["check"] = "Lấy lại mật khẩu thành công. Vui lòng check email!!!";
                return View("QuenMK");
            }
            else
            {
                ViewData["check"] = "Email hoặc số điện thoại không đúng";
                return View("QuenMK");
            }
        }
    }
}
