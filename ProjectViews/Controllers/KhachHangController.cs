using _1_API.ViewModel.KhachHang;
using _1_API.ViewModel.MaGiamGia;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using ProjectViews.IServices;

namespace ProjectViews.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly ILogger<KhachHangController> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly IAllServices _services;
        public KhachHangController(ILogger<KhachHangController> logger, IWebHostEnvironment webHost, IAllServices services)
        {
            _logger = logger;
            _webHost = webHost;
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lstKhachHang = await _services.GetAll<KhachHang>("https://localhost:7203/api/KhachHangs/Get-All");
            var kh = from a in lstKhachHang
                      select new ViewKhachHang()
                      {
                          Id = a.Id,
                          Ten = a.Ten,
                          DiaChi = a.DiaChi,
                          Email = a.Email,
                          GioiTinh = a.GioiTinh,
                          MatKhau = a.MatKhau,
                          NgaySinh = a.NgaySinh,
                          Sdt = a.Sdt,

                      };
            return View(kh);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ModelKhachHang model)
        {
            if (ModelState.IsValid)
            {
                CreateKhachHang kh = new CreateKhachHang()
                {
                    Ten= model.Ten,
                    GioiTinh= model.GioiTinh,
                    Email = model.Email,
                    MatKhau= model.MatKhau,
                    Sdt= model.Sdt,
                    NgaySinh= model.NgaySinh,
                    DiaChi = model.DiaChi
                };
                await _services.Add("https://localhost:7203/api/KhachHangs/", kh);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _services.Remove<KhachHang>("https://localhost:7203/api/KhachHangs/GetById/", "https://localhost:7203/api/KhachHangs/Delete/", id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var kh = await _services.GetById<KhachHang>("https://localhost:7203/api/KhachHangs/GetById/", id);
            UpdateKhachHang up = new UpdateKhachHang()
            {
                Id = id,
                Ten = kh.Ten,
                DiaChi = kh.DiaChi,
                Sdt = kh.Sdt,
                Email = kh.Email ,
                NgaySinh = kh.NgaySinh,
                GioiTinh = kh.GioiTinh,
                MatKhau = kh.MatKhau
            };
            return View(up);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateKhachHang kh)
        {
            if (ModelState.IsValid)
            {
                UpdateKhachHang up = new UpdateKhachHang()
                {
                    Ten = kh.Ten,
                    DiaChi = kh.DiaChi,
                    Sdt = kh.Sdt,
                    Email = kh.Email,
                    NgaySinh = kh.NgaySinh,
                    GioiTinh = kh.GioiTinh,
                    MatKhau = kh.MatKhau
                };
                await _services.Update<UpdateKhachHang>("https://localhost:7203/api/KhachHangs/Update/", up, kh.Id);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {

            var kh = await _services.GetById<KhachHang>("https://localhost:7203/api/KhachHangs/GetById/", id);
            DetailsKhachHang up = new DetailsKhachHang()
            {
                Id = id,
                Ten = kh.Ten,
                DiaChi = kh.DiaChi,
                Sdt = kh.Sdt,
                Email = kh.Email,
                NgaySinh = kh.NgaySinh,
                GioiTinh = kh.GioiTinh,
                MatKhau = kh.MatKhau
            };
            return View(up);
        }

    }
}
