using _1_API.ViewModel.MaGiamGia;
using _1_API.ViewModel.NhanVien;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using ProjectViews.IServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProjectViews.Controllers
{
    public class MaGiamGiaController : Controller
    {
        private readonly ILogger<MaGiamGiaController> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly IAllServices _services;

        public MaGiamGiaController(ILogger<MaGiamGiaController> logger, IWebHostEnvironment webHost, IAllServices services)
        {
            _logger = logger;
            _webHost = webHost;
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lstMaGiamGia = await _services.GetAll<MaGiamGia>("https://localhost:7203/api/MaGiamGias/Get-All");
            var mgg = from a in lstMaGiamGia
                      select new ViewMaGiamGia()
                      {
                          Id = a.Id,
                          Ma = a.Ma,
                          NgayBatdau = a.NgayBatdau,
                          NgayKetthuc = a.NgayKetthuc,
                          SoLuong = a.SoLuong,
                          TrangThai = a.TrangThai,
                          PhanTramGiam = a.PhanTramGiam,
                      };
            return View(mgg);
        }
         public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ModelMaGiamGia model)
        {
            if (ModelState.IsValid)
            {
                CreateMaGiamGia mgg = new CreateMaGiamGia() 
                { 
                    Ma= model.Ma,
                    NgayBatdau= model.NgayBatdau,
                    NgayKetthuc= model.NgayKetthuc,
                    PhanTramGiam= model.PhanTramGiam,
                    SoLuong= model.SoLuong,
                    TrangThai = model.TrangThai
                };
                await _services.Add("https://localhost:7203/api/MaGiamGias/", mgg);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _services.Remove<MaGiamGia>("https://localhost:7203/api/MaGiamGias/GetById/", "https://localhost:7203/api/MaGiamGias/Delete/", id);
            return RedirectToAction("Index", "MaGiamGia");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var mgg = await _services.GetById<MaGiamGia>("https://localhost:7203/api/MaGiamGias/GetById/", id);
            UpdateMaGiamGia up = new UpdateMaGiamGia()
            {
                Id= id,
                Ma = mgg.Ma,
                NgayBatdau= mgg.NgayBatdau,
                NgayKetthuc = mgg.NgayKetthuc,
                PhanTramGiam= mgg.PhanTramGiam,
                SoLuong= mgg.SoLuong,
                TrangThai = mgg.TrangThai
            };
            return View(up);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateMaGiamGia mgg)
        {
            if (ModelState.IsValid)
            {
                UpdateMaGiamGia up = new UpdateMaGiamGia()
                {
                    Ma = mgg.Ma,
                    NgayBatdau = mgg.NgayBatdau,
                    NgayKetthuc = mgg.NgayKetthuc,
                    PhanTramGiam = mgg.PhanTramGiam,
                    SoLuong = mgg.SoLuong,
                    TrangThai = mgg.TrangThai
                };
                await _services.Update<UpdateMaGiamGia>("https://localhost:7203/api/MaGiamGias/Update/", up, mgg.Id);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {

            var mgg = await _services.GetById<MaGiamGia>("https://localhost:7203/api/MaGiamGias/GetById/", id);
            DetailMaGiamGia  up = new DetailMaGiamGia()
            {
                Id = id,
                Ma = mgg.Ma,
                NgayBatdau = mgg.NgayBatdau,
                NgayKetthuc = mgg.NgayKetthuc,
                PhanTramGiam = mgg.PhanTramGiam,
                SoLuong = mgg.SoLuong,
                TrangThai = mgg.TrangThai
            };
            return View(up);
        }
    }
}
