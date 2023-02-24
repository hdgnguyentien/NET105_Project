using _1_API.ViewModel.SanPham;
using _1_API.ViewModel.SizeSanPham;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using ProjectViews.IServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProjectViews.Controllers
{
    public class SizeSanPhamController : Controller
    {
        private readonly ILogger<SizeSanPhamController> _logger;
        private readonly IAllServices _services;
        public SizeSanPhamController(ILogger<SizeSanPhamController> logger, IAllServices services)
        {
            _logger = logger;
            _services = services;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            var lstSize = await _services.GetAll<KichCo>("https://localhost:7203/api/KichCos/Get-All");
            ViewData["lstSize"] = lstSize.ToList();
            var lstSizeSP = await _services.GetAll<SizeSanPham>("https://localhost:7203/api/SizeSanPhams/Get-All");
            lstSizeSP = lstSizeSP.Where(p => p.IdSanPhamChiTiet == Guid.Parse(id));
            var obj = from a in lstSizeSP
                      join b in lstSize on a.IdSize equals b.Id
                      select new SizeSanPhamModel()
                      {
                          Id = a.Id,
                          Size = b.Size.ToString(),
                          SoLuong = a.SoLuong
                      };
            var objs = obj.ToList();
            ViewData["lstSizeSP"] = objs;
            CreateSizeSanPham cr = new CreateSizeSanPham();
            cr.IdSanPhamChiTiet = Guid.Parse(id);
            return View(cr);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSizeSanPham model)
        {
            var lstSize = await _services.GetAll<KichCo>("https://localhost:7203/api/KichCos/Get-All");
            ViewData["lstSize"] = lstSize.ToList();
            var lstSizeSP = await _services.GetAll<SizeSanPham>("https://localhost:7203/api/SizeSanPhams/Get-All");
            lstSizeSP = lstSizeSP.Where(p => p.IdSanPhamChiTiet == model.IdSanPhamChiTiet);
            var obj = from a in lstSizeSP
                      join b in lstSize on a.IdSize equals b.Id
                      select new SizeSanPhamModel()
                      {
                          Id = a.Id,
                          Size = b.Size.ToString(),
                          SoLuong = a.SoLuong
                      };
            ViewData["lstSizeSP"] = obj.ToList();
            if (ModelState.IsValid)
            {
                var lstSanPham = await _services.GetAll<SizeSanPham>("https://localhost:7203/api/SizeSanPhams/Get-All");
                var ten = lstSanPham.FirstOrDefault(p => p.IdSanPhamChiTiet == model.IdSanPhamChiTiet && p.IdSize == model.IdSize);
                if (ten != null)
                {
                    ModelState.AddModelError("", "Size đã có trong bảng");
                    return View(model);
                }
                else
                {
                    CreateSizeSanPham sp = new CreateSizeSanPham()
                    {
                        IdSanPhamChiTiet = model.IdSanPhamChiTiet,
                        IdSize = model.IdSize,
                        SoLuong = model.SoLuong
                    };
                    await _services.Add("https://localhost:7203/api/SizeSanPhams/", sp);
                    return RedirectToAction("Create", new { id = model.IdSanPhamChiTiet.ToString() });
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            var spid = await _services.GetById<SizeSanPham>("https://localhost:7203/api/SizeSanPhams/GetById/", Guid.Parse(id));
            var lstSize = await _services.GetAll<KichCo>("https://localhost:7203/api/KichCos/Get-All");
            ViewData["lstSize"] = lstSize.ToList();
            UpdateSizeSanPham nvmd = new UpdateSizeSanPham()
            {
                Id = Guid.Parse(id),
                IdSanPhamChiTiet = spid.IdSanPhamChiTiet,
                IdSize = spid.IdSize,
                SoLuong = spid.SoLuong
            };
            return View(nvmd);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSizeSanPham model)
        {

            await _services.Update<UpdateSizeSanPham>("https://localhost:7203/api/SizeSanPhams/Update/", model, model.Id);
            return RedirectToAction("Create",new {id = model.IdSanPhamChiTiet});
        }

    }
}
