using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using CustomerViews.IServices;
using CustomerViews.Models;
using System.Diagnostics;

namespace CustomerViews.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAllServices _services;

        public HomeController(ILogger<HomeController> logger, IAllServices services)
        {
            _logger = logger;
            _services = services;
        }

        public async Task<IActionResult> Index()
        {
            var lstSPCT = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            return View(lstSPCT.ToList());
        }
        public async Task<IActionResult> DetailSPCT(Guid spct_id)
        {
            var spct = await _services.GetById<SanphamChitiet>(Connection.api + "SanphamChitiets/GetById/", spct_id);
            return View(spct);
        }
        [HttpGet]
        public async Task<IActionResult> SearchSanPham(decimal a, decimal b)
        {
            var lstSanphamChitiet = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            var lstSPCT = lstSanphamChitiet.ToList().ToList();

            decimal tam;
            if (a > b)
            {
                tam = a;
                a = b;
                b = tam;
            }

            var result = lstSPCT.Where(p => p.GiaBan >= a && p.GiaBan <= b).ToList();
            if (result.Count > 0)
            {
                ViewData["result"] = "Tìm thấy" + result.Count + "sản phẩm";
                return View("Index", result);
            }
            else
            {
                ViewData["thongbao"] = "Không thấy sản phẩm nào trong khoảng bạn vừa nhập";

            }

            return View("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}