using CustomerViews.IServices;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;

namespace CustomerViews.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly IAllServices _services;
        public CartController(ILogger<CartController> logger, IAllServices services)
        {
            _logger = logger;
            _services = services;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ThemGioHang()
        {
            return View();
        }
        public async Task<IActionResult> MuaHang()
        {
            return View();
        }
    }
}
