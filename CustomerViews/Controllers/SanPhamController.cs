using _1_API.ViewModel.SanPham;
using _1_API.ViewModel.SanphamChitiet;
using _1_API.ViewModel.SizeSanPham;
using _1_API.ViewModel.TheLoai;
using CustomerViews.IServices;
using CustomerViews.Models;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;

namespace CustomerViews.Controllers
{
    public class SanPhamController : Controller
{
        private readonly ILogger<SanPhamController> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly IAllServices _services;
        public SanPhamController(ILogger<SanPhamController> logger, IWebHostEnvironment webHost, IAllServices services)
        {
            _logger = logger;
            _webHost = webHost;
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lstSPCT = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            var lstSP = await _services.GetAll<SanPham>(Connection.api + "SanPhams/Get-All");
            var lstTL = await _services.GetAll<TheLoai>(Connection.api + "TheLoais/Get-All");

            ViewData["lstSP"] = lstSP.ToList();
            ViewData["lstTL"] = lstTL.ToList();
            return View(lstSPCT.ToList());
        }

        

    }
}
