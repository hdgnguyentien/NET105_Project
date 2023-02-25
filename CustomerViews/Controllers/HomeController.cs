using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using CustomerViews.IServices;
using CustomerViews.Models;
using System.Diagnostics;
using _1_API.ViewModel.GioHangChiTiet;
using System.Drawing;
using _1_API.ViewModel.SizeSanPham;

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

            ViewData["lstSP"] = lstSP.ToList();
            ViewData["lstTL"] = lstTL.ToList();
            return View(lstSPCT.ToList());
        }
        public async Task<IActionResult> SanPhamChiTiet(Guid spct_id)
        {
            if (HttpContext.Session.GetString("ten") != null)
            {
                var khid = HttpContext.Session.GetString("idkh");
                Guid guidID = Guid.Parse(khid);
                var lstKH = await _services.GetAll<KhachHang>(Connection.api + "KhachHangs/Get-All");
                var diachi = lstKH.FirstOrDefault(p => p.Id == guidID).DiaChi;
                ViewData["diachi"] = diachi;
            }
           

            var spct = await _services.GetById<SanphamChitiet>(Connection.api + "SanphamChitiets/GetById/", spct_id);
            var lstSP = await _services.GetAll<SanPham>(Connection.api + "SanPhams/Get-All");
            var lstSPCT = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            var lstMS = await _services.GetAll<MauSac>(Connection.api + "MauSacs/Get-All");
            var lstKC = await _services.GetAll<KichCo>(Connection.api + "KichCos/Get-All");
            var lstTL = await _services.GetAll<TheLoai>(Connection.api + "TheLoais/Get-All");
            var lstsizesp = await _services.GetAll<SizeSanPham>(Connection.api + "SizeSanPhams/Get-All");
            var listsize = (from a in lstsizesp.ToList().Where(x => x.IdSanPhamChiTiet == spct_id)
                           join b in lstSPCT.ToList() on a.IdSanPhamChiTiet equals b.Id
                           join c in lstKC.ToList() on a.IdSize equals c.Id
                           where a.SoLuong >= 1
                           orderby c.Size ascending
                           select new SizeSanPhamModel()
                           {
                               Id = a.IdSize,
                               Size = c.Size.ToString(),
                               SoLuong = a.SoLuong
                           });
            ViewData["listSize"] = listsize.ToList();

            spct.sanPham = lstSP.FirstOrDefault(x => x.Id == spct.IdSP);
            spct.mauSac = lstMS.FirstOrDefault(x => x.Id == spct.IdMauSac);
            return View(spct);
        }
        public async Task<IActionResult> SearchSanPham(string ten,Guid idTheLoai)
        {
            var lstSPCT = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            var lstTL = await _services.GetAll<TheLoaiSanPham>(Connection.api + "TheLoaiSanPhams/Get-All");
            if (!string.IsNullOrEmpty(ten))
                lstSPCT = lstSPCT.Where(x => x.TenSPChiTiet.ToLower().Contains(ten.ToLower())).ToList();
            if (idTheLoai != Guid.Empty)
            {
                var lstSPCT_id = lstTL.Where(x => x.IdTheLoai == idTheLoai).Select(x => x.IdChiTietSP);
                lstSPCT = lstSPCT.Where(x => lstSPCT_id.Contains(x.Id)).ToList();
            }
            ViewData["lstTL"] = lstTL.ToList();
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