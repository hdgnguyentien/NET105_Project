using _1_API.ViewModel.NhanVien;
using _1_API.ViewModel.SanPham;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using ProjectViews.IServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProjectViews.Controllers
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
            var lstSanPham = await _services.GetAll<SanPham>("https://localhost:7203/api/SanPhams/Get-All");           
            var lstHang = await _services.GetAll<Hang>("https://localhost:7203/api/Hangs/Get-All");
            ViewData["ListHang"] = lstHang.ToList();
            var sp = from a in lstSanPham.ToList()
                     join b in lstHang.ToList() on a.IdHang equals b.Id
                     select new ViewSanPham()
                     {
                         Id = a.Id,
                         Ten = a.Ten,
                         TrangThai = a.TrangThai == 1 ? "Đang hoạt động" : "Ngưng hoạt động",
                         TenHang = b.TenHang  
                     };

            return View(sp);
        }

        public async Task<IActionResult> Create()
        {
            var lstSanPham = await _services.GetAll<SanPham>("https://localhost:7203/api/SanPhams/Get-All");
            ViewData["ListSanPham"] = lstSanPham.ToList();
            var lstHang = await _services.GetAll<Hang>("https://localhost:7203/api/Hangs/Get-All");
            ViewData["ListHang"] = lstHang.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SanPhamModel model)
        {
            var lstHang = await _services.GetAll<Hang>("https://localhost:7203/api/Hangs/Get-All");
            ViewData["ListHang"] = lstHang.ToList();

            if (ModelState.IsValid)
            {
                var lstSanPham = await _services.GetAll<SanPham>("https://localhost:7203/api/SanPhams/Get-All");
                var ten = lstSanPham.FirstOrDefault(p => p.Ten.ToLower() == model.Ten.ToLower());
                if(ten != null)
                {
                    ModelState.AddModelError("", "Tên sản phẩm đã được sử dụng");
                    return View();
                }
                else
                {
                    CreateSanPham sp = new CreateSanPham()
                    {
                        IdHang = model.IdHang,
                        Ten = model.Ten,
                        TrangThai = model.TrangThai
                    };
                    await _services.Add("https://localhost:7203/api/SanPhams/", sp);
                    return RedirectToAction("Index");
                }               
            }
            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _services.Remove<SanPham>("https://localhost:7203/api/SanPhams/GetById/", "https://localhost:7203/api/SanPhams/Delete/", id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var lstSanPham = await _services.GetAll<SanPham>("https://localhost:7203/api/SanPhams/Get-All");
            ViewData["ListSanPham"] = lstSanPham.ToList();
            var lstHang = await _services.GetAll<Hang>("https://localhost:7203/api/Hangs/Get-All");
            ViewData["ListHang"] = lstHang.ToList();

            var spid = await _services.GetById<SanPham>("https://localhost:7203/api/SanPhams/GetById/", id);

            UpdateSanPham nvmd = new UpdateSanPham()
            {
                Id = id,
                Ten = spid.Ten,
                TrangThai= spid.TrangThai,
                IdHang= spid.IdHang,
            };
            return View(nvmd);
        }

        public async Task<IActionResult> Update(UpdateSanPham sp)
        {
            if (ModelState.IsValid)
            {
                UpdateSanPham updateSP = new UpdateSanPham()
                {
                    Id= sp.Id,
                    IdHang = sp.IdHang,
                    Ten = sp.Ten,
                    TrangThai = sp.TrangThai,
                };
                await _services.Update<UpdateSanPham>("https://localhost:7203/api/SanPhams/Update/", updateSP, sp.Id);
                return RedirectToAction("Index");
            }
            return View();
            
            
        }
        public async Task<IActionResult> Search(string ten, string idHang)
        {
            var lstHang = await _services.GetAll<Hang>("https://localhost:7203/api/hangs/Get-All");
            ViewData["ListHang"] = lstHang.ToList();
            var lstSanPham = await _services.GetAll<SanPham>("https://localhost:7203/api/SanPhams/Get-All");

            if (!string.IsNullOrEmpty(ten) || !string.IsNullOrEmpty(idHang))
            {
                if (!string.IsNullOrEmpty(idHang))
                {
                    var id = Guid.Parse(idHang);
                    lstHang = lstHang.Where(p => p.Id == id);
                    var viewSP = from a in lstSanPham.ToList()
                              join b in lstHang.ToList() on a.IdHang equals b.Id
                              select new ViewSanPham()
                              {
                                  Id = a.Id,
                                  Ten = a.Ten,
                                  TenHang = b.TenHang,
                                  TrangThai = a.TrangThai == 1 ? "Đang hoạt động" : "Ngưng hoạt động"
                              };
                    if (!string.IsNullOrEmpty(ten))
                    {
                        viewSP = viewSP.Where(p => p.Ten.ToLower().Contains(ten.ToLower()));
                        return View("Index", viewSP);
                    }
                    return View("Index", viewSP);

                }
                else
                {
                    var sp = from a in lstSanPham.ToList()
                             join b in lstHang.ToList() on a.IdHang equals b.Id
                             select new ViewSanPham()
                             {
                                 Id = a.Id,
                                 Ten = a.Ten,
                                 TenHang = b.TenHang,
                                 TrangThai = a.TrangThai == 1 ? "Đang hoạt động" : "Ngưng hoạt động"
                             };
                    sp = sp.Where(p => p.Ten.ToLower().Contains(ten.ToLower()));
                    return View("Index", sp);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
