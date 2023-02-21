using _1_API.ViewModel.SanPham;
using _1_API.ViewModel.SanphamChitiet;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using ProjectViews.IServices;

namespace ProjectViews.Controllers
{
    public class SanPhamChiTietController : Controller
    {
        private readonly ILogger<SanPhamChiTietController> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly IAllServices _services;
        public SanPhamChiTietController(ILogger<SanPhamChiTietController> logger, IWebHostEnvironment webHost, IAllServices services)
        {
            _logger = logger;
            _webHost = webHost;
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var lstSPCT = await _services.GetAll<SanphamChitiet>("https://localhost:7203/api/SanphamChitiets/Get-All");
            var lstSP = await _services.GetAll<SanPham>("https://localhost:7203/api/SanPhams/Get-All");
            var lstKichCo = await _services.GetAll<KichCo>("https://localhost:7203/api/KichCos/Get-All");
            var lstMauSac = await _services.GetAll<MauSac>("https://localhost:7203/api/MauSacs/Get-All");
            var spct = from a in lstSPCT.ToList().Where(p => p.IdSP == Guid.Parse(id))
                       join b in lstSP on a.IdSP equals b.Id
                       join d in lstMauSac on a.IdMauSac equals d.Id
                       select new ViewSanPhamChiTiet()
                       {
                           Id= a.Id,
                           GiaBan = a.GiaBan,
                           TenMauSac=d.TenMau,
                           TrangThai = a.TrangThai == 1 ? "Đang hoạt động" : "Ngưng hoạt động",
                           MaSPChiTiet = a.MaSPChiTiet,
                           TenSPChiTiet = a.TenSPChiTiet,
                           AnhDaiDien = a.AnhDaiDien
                       };
            return View(spct);
        }

        public async Task<IActionResult> Create()
        {         
            var lstSP = await _services.GetAll<SanPham>("https://localhost:7203/api/SanPhams/Get-All");
            ViewData["lstSP"] = lstSP.ToList();
            var lstKichCo = await _services.GetAll<KichCo>("https://localhost:7203/api/KichCos/Get-All");
            ViewData["lstKichCo"] = lstKichCo.ToList();
            var lstMauSac = await _services.GetAll<MauSac>("https://localhost:7203/api/MauSacs/Get-All");
            ViewData["lstMauSac"] = lstMauSac.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SanPhamChiTietModel model)
        {

            if (ModelState.IsValid)
            {
                CreateSanphamChitiet spct = new CreateSanphamChitiet()
                {
                    IdSP= model.IdSP,
                    IdKichCo= model.IdKichCo,
                    IdMauSac= model.IdMauSac,
                    GiaBan=model.GiaBan,
                    GiaNhap=model.GiaNhap,
                    SoLuong=model.SoLuong,
                    TrangThai = model.TrangThai
                };
                await _services.Add("https://localhost:7203/api/SanphamChiTiets/", spct);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _services.Remove<SanphamChitiet>("https://localhost:7203/api/SanphamChiTiets/GetById/", "https://localhost:7203/api/SanphamChiTiets/Delete/", id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var lstSP = await _services.GetAll<SanPham>("https://localhost:7203/api/SanPhams/Get-All");
            ViewData["lstSP"] = lstSP.ToList();
            var lstKichCo = await _services.GetAll<KichCo>("https://localhost:7203/api/KichCos/Get-All");
            ViewData["lstKichCo"] = lstKichCo.ToList();
            var lstMauSac = await _services.GetAll<MauSac>("https://localhost:7203/api/MauSacs/Get-All");
            ViewData["lstMauSac"] = lstMauSac.ToList();

            var spct = await _services.GetById<SanphamChitiet>("https://localhost:7203/api/SanphamChitiets/GetById/", id);

            UpdateSanphamChitiet update = new UpdateSanphamChitiet()
            {
                Id = id,
                GiaBan= spct.GiaBan,
                GiaNhap= spct.GiaNhap,
                TrangThai= spct.TrangThai,
                IdMauSac= spct.IdMauSac,
                IdSP = spct.IdSP
            };
            return View(update);
        }

        public async Task<IActionResult> Update(UpdateSanphamChitiet spct)
        {
            if (ModelState.IsValid)
            {
                UpdateSanphamChitiet updateSPCT = new UpdateSanphamChitiet()
                {
                    GiaBan = spct.GiaBan,
                    GiaNhap = spct.GiaNhap,
                    SoLuong = spct.SoLuong,
                    TrangThai = spct.TrangThai,
                    IdKichCo = spct.IdKichCo,
                    IdMauSac = spct.IdMauSac,
                    IdSP = spct.IdSP
                };
                await _services.Update<UpdateSanphamChitiet>("https://localhost:7203/api/SanphamChitiets/Update/", updateSPCT, spct.Id);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var lstSPCT = await _services.GetById<SanphamChitiet>("https://localhost:7203/api/SanphamChitiets/GetById/", id);
            SanPhamChiTietDetails details = new SanPhamChiTietDetails()
            {
                GiaBan = lstSPCT.GiaBan,
                GiaNhap = lstSPCT.GiaNhap,
                TrangThai = lstSPCT.TrangThai,
                IdMauSac = lstSPCT.IdMauSac,
                IdSP = lstSPCT.IdSP
                
            };
            return View(details);
        }
    }
}
