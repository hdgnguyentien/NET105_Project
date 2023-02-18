using _1_API.ViewModel.MauSac;
using _1_API.ViewModel.TheLoai;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using ProjectViews.IServices;

namespace ProjectViews.Controllers
{
    public class MauSacController : Controller
    {
        private readonly ILogger<MauSacController> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly IAllServices _services;

        public MauSacController(ILogger<MauSacController> logger, IWebHostEnvironment webHost, IAllServices services)
        {
            _logger = logger;
            _webHost = webHost;
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _services.GetAll<MauSac>("https://localhost:7203/api/MauSacs/Get-All");
            var mausac = from a in result.ToList()
                          select new ViewMauSac()
                          {
                              Id = a.Id,
                              Ten=a.TenMau
                          };
            return View(mausac);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MauSacModel model)
        {
            if (ModelState.IsValid)
            {
                CreateMauSac ms = new CreateMauSac()
                {
                    Ten = model.Ten,
                };
                await _services.Add("https://localhost:7203/api/MauSacs/", ms);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _services.Remove<MauSac>("https://localhost:7203/api/MauSacs/GetById",
                "https://localhost:7203/api/MauSacs/Delete/", id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _services.GetById<MauSac>("https://localhost:7203/api/MauSacs/GetById/", id);
            UpdateMauSac ms = new UpdateMauSac()
            {
                Id = id,
                Ten = result.TenMau,
            };
            return View(ms);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateMauSac updateMauSac)
        {
            if (ModelState.IsValid)
            {
                UpdateMauSac mausac = new UpdateMauSac()
                {
                    Ten = updateMauSac.Ten,

                };
                await _services.Update<UpdateMauSac>("https://localhost:7203/api/MauSacs/Update/", mausac, updateMauSac.Id);
                return RedirectToAction("Index", "MauSac");
            }
            return View();
        }
    }
}
