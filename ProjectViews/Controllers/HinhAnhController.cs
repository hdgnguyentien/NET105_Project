using _1_API.ViewModel.HinhAnh;
using _1_API.ViewModel.KhachHang;
using _1_API.ViewModel.NhanVien;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectViews.IServices;
using System.Security.Cryptography;

namespace ProjectViews.Controllers
{
    public class HinhAnhController : Controller
    {
        private readonly ILogger<HinhAnhController> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly IAllServices _services;
        public HinhAnhController(ILogger<HinhAnhController> logger, IWebHostEnvironment webHost, IAllServices services)
        {
            _logger = logger;
            _webHost = webHost;
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lstHinhAnh = await _services.GetAll<HinhAnh>("https://localhost:7203/api/HinhAnhs/Get-All");
            var lstSPCT = await _services.GetAll<SanphamChitiet>("https://localhost:7203/api/SanPhamChiTiets/Get-All");
            var hinhAnh = from a in lstHinhAnh
                          join b in lstSPCT on a.IdChiTietSP equals b.IdSP
                     select new ViewHinhAnh()
                     {
                        IdChiTietSP = a.Id,
                       LinkAnh = a.LinkAnh

                     };
            return View(hinhAnh);
        }

        public async Task<IActionResult> Create()
        {
            var lstHinhAnh = await _services.GetAll<HinhAnh>("https://localhost:7203/api/HinhAnhs/Get-All");
            var lstSPCT = await _services.GetAll<SanphamChitiet>("https://localhost:7203/api/SanPhamChiTiets/Get-All");
            ViewData["lstSPCT"] = lstSPCT.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ModelHinhAnh model)
        {
            var lstHinhAnh = await _services.GetAll<HinhAnh>("https://localhost:7203/api/HinhAnhs/Get-All");
            var lstSPCT = await _services.GetAll<SanphamChitiet>("https://localhost:7203/api/SanPhamChiTiets/Get-All");
            ViewData["lstSPCT"] = lstSPCT.ToList();
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.ImageFile != null)
                {
                    string uploadFolder = Path.Combine(_webHost.WebRootPath, "LinkAnh");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                    string filepath = Path.Combine(uploadFolder, uniqueFileName);
                    var filestream = new FileStream(filepath, FileMode.Create);
                    model.ImageFile.CopyTo(filestream);
                };
                CreateHinhAnh avatar = new CreateHinhAnh()
                {
                    LinkAnh = uniqueFileName,
                };
                await _services.Add("https://localhost:7203/api/HinhAnhs/", avatar);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
