using System.Linq;
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
        [HttpGet]
        public async Task<IActionResult> Index1(string idTL)
        {
            var lstSPCT = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            var lstSP = await _services.GetAll<SanPham>(Connection.api + "SanPhams/Get-All");
            var lstTL = await _services.GetAll<TheLoai>(Connection.api + "TheLoais/Get-All");
            var lstTLSP = await _services.GetAll<TheLoaiSanPham>(Connection.api + "TheLoaiSanPhams/Get-All");
            var lstIDSP = lstTLSP.Where(x => x.IdTheLoai == Guid.Parse(idTL)).Select(x => x.IdChiTietSP).ToList();

            ViewData["lstSP"] = lstSP.ToList();
            ViewData["lstTL"] = lstTL.ToList();
            ViewData["idTL"] = Guid.Parse(idTL);
            return View("Index",lstSPCT.Where(x=> lstIDSP.Contains(x.Id)).ToList());
        }

        public async Task<IActionResult> SearchSanPham(string ten, Guid idTheLoai)
        {
            var lstSPCT = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            var lstTLSP = await _services.GetAll<TheLoaiSanPham>(Connection.api + "TheLoaiSanPhams/Get-All");
            var lstTL = await _services.GetAll<TheLoai>(Connection.api + "TheLoais/Get-All");
            if (!string.IsNullOrEmpty(ten))
                lstSPCT = lstSPCT.Where(x => x.TenSPChiTiet.ToLower().Contains(ten.ToLower())).ToList();
            if (idTheLoai != Guid.Empty)
            {
                var lstSPCT_id = lstTLSP.Where(x => x.IdTheLoai == idTheLoai).Select(x => x.IdChiTietSP);
                lstSPCT = lstSPCT.Where(x => lstSPCT_id.Contains(x.Id)).ToList();
            }
            var lstNgayT = (from a in lstSPCT.ToList()
                            orderby a.NgayTao descending
                            select new SanphamChitiet()
                            {
                                Id = a.Id,
                                NgayTao = a.NgayTao,
                                GiaBan = a.GiaBan,
                                MaSPChiTiet = a.MaSPChiTiet,
                                TenSPChiTiet = a.TenSPChiTiet,
                                AnhDaiDien = a.AnhDaiDien
                            }).Take(4);
            var lstNoiBat = (from a in lstSPCT.ToList()
                             select new SanphamChitiet()
                             {
                                 Id = a.Id,
                                 NgayTao = a.NgayTao,
                                 GiaBan = a.GiaBan,
                                 MaSPChiTiet = a.MaSPChiTiet,
                                 TenSPChiTiet = a.TenSPChiTiet,
                                 AnhDaiDien = a.AnhDaiDien
                             }).Take(8);
            ViewData["lstNgayT"] = lstNgayT.ToList();
            ViewData["lstNoiBat"] = lstNoiBat.ToList();
            ViewData["lstTL"] = lstTL.ToList();
            return View("Index", lstSPCT);
        }


    }
}
