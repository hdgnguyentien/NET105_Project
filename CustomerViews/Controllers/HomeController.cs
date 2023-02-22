using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using CustomerViews.IServices;
using CustomerViews.Models;
using System.Diagnostics;
using _1_API.ViewModel.GioHangChiTiet;
using System.Drawing;

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
            var lstSP = await _services.GetAll<SanPham>(Connection.api + "SanPhams/Get-All"); 
            var lstTL = await _services.GetAll<TheLoai>(Connection.api + "TheLoais/Get-All");

            ViewData["lstSP"] = lstSP.ToList();
            ViewData["lstTL"] = lstTL.ToList();
            return View(lstSPCT.ToList());
        }
        public async Task<IActionResult> SanPhamChiTiet(Guid spct_id)
        {
            var spct = await _services.GetById<SanphamChitiet>(Connection.api + "SanphamChitiets/GetById/", spct_id);

            var lstSP = await _services.GetAll<SanPham>(Connection.api + "SanPhams/Get-All");
            var lstMS = await _services.GetAll<MauSac>(Connection.api + "MauSacs/Get-All");
            var lstKC = await _services.GetAll<KichCo>(Connection.api + "KichCos/Get-All");
            var lstTL = await _services.GetAll<TheLoai>(Connection.api + "TheLoais/Get-All");

            spct.sanPham = lstSP.FirstOrDefault(x => x.Id == spct.IdSP);
            spct.mauSac = lstMS.FirstOrDefault(x => x.Id == spct.IdMauSac);
            return View(spct);
        }
        public async Task<IActionResult> SearchSanPham(string ten,Guid idTheLoai)
        {
            var lstSPCT = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            if (!string.IsNullOrEmpty(ten))
                lstSPCT = lstSPCT.Where(x => x.TenSPChiTiet.ToLower().Contains(ten.ToLower())).ToList();
            if (idTheLoai != Guid.Empty)
            {
                var lstTL = await _services.GetAll<TheLoaiSanPham>(Connection.api + "TheLoaiSanPhams/Get-All");
                var lstSPCT_id = lstTL.Where(x => x.IdTheLoai == idTheLoai).Select(x => x.IdChiTietSP);
                lstSPCT = lstSPCT.Where(x => lstSPCT_id.Contains(x.Id)).ToList();
            }
            return View("Index", lstSPCT);
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
        public async Task<SanphamChitiet> GetThongTinSP(SanphamChitiet spct)
        {
            var lstSP = await _services.GetAll<SanPham>(Connection.api + "SanPhams/Get-All");
            var lstMS = await _services.GetAll<MauSac>(Connection.api + "MauSacs/Get-All");
            var lstKC = await _services.GetAll<KichCo>(Connection.api + "KichCos/Get-All");
            var lstTL = await _services.GetAll<TheLoai>(Connection.api + "TheLoais/Get-All");

            spct.sanPham = lstSP.FirstOrDefault(x => x.Id == spct.IdSP);
            spct.mauSac = lstMS.FirstOrDefault(x => x.Id == spct.IdMauSac);
            return spct;
        }
    }
}