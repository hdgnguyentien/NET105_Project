﻿using Data.ModelsClass;
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
            var lstSP = await _services.GetAll<SanPham>(Connection.api + "SanPhams/Get-All");
            var lstMS = await _services.GetAll<MauSac>(Connection.api + "MauSacs/Get-All");
            var lstKC = await _services.GetAll<KichCo>(Connection.api + "KichCos/Get-All");
            var lstTL = await _services.GetAll<TheLoai>(Connection.api + "TheLoais/Get-All");
            foreach (var item in lstSPCT)
            {
                item.sanPham = lstSP.FirstOrDefault(x => x.Id == item.IdSP);
                item.mauSac = lstMS.FirstOrDefault(x => x.Id == item.IdMauSac);
                item.kichCo = lstKC.FirstOrDefault(x => x.Id == item.IdKichCo);
            }
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
            spct.kichCo = lstKC.FirstOrDefault(x => x.Id == spct.IdKichCo);
            return View(spct);
        }
        public async Task<IActionResult> SearchSanPham(decimal min, decimal max, string name)
        {
            var lstSanphamChitiet = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");

            decimal temp;
            if (min > max)
            {
                temp = min;
                min = max;
                max = temp;
            }

            var result = lstSanphamChitiet.Where(p => p.GiaBan >= min && p.GiaBan <= max && p.sanPham.Ten.Contains(name)).ToList();
            if (result.Count > 0)
            {
                ViewData["result"] = "Tìm thấy" + result.Count + "sản phẩm";
                return View("Index", result);
            }
            else
            {
                ViewData["thongbao"] = "Không tìm thấy sản phẩm nào phù hợp";
                return View("Index");
            }
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
            spct.kichCo = lstKC.FirstOrDefault(x => x.Id == spct.IdKichCo);
            return spct;
        }
    }
}