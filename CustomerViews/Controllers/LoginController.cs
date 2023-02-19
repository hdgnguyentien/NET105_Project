using CustomerViews.IServices;
using CustomerViews.Models;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;

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
                        HttpContext.Session.SetString("ten", kh.Ten);
                        if (kh.GioHangs != null && kh.GioHangs.Any())
                            HttpContext.Session.SetString("idgh", kh.GioHangs.FirstOrDefault().Id.ToString());
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
    }
}
