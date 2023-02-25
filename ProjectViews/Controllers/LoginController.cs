using _1_API.ViewModel.Login;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using ProjectViews.IServices;

namespace ProjectViews.Controllers
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
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LoginToAdmin(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var lstNhanvien = await _services.GetAll<NhanVien>("https://localhost:7203/api/NhanViens/Get-All");
                var login = lstNhanvien.ToList().FirstOrDefault(p => p.Email == model.Email && p.MatKhau == model.Password);
                if (login != null)
                {
                    HttpContext.Session.SetString("name", login.Ten);
                    HttpContext.Session.SetString("idnv", login.Id.ToString());
                    return RedirectToAction("Index", "SanPham");
                }
                else
                {
                    ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu");
                }
            }
            return View("Index");
        }
    }
}
