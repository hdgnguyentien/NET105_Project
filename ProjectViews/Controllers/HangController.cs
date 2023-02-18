using _1_API.ViewModel.Hang;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using ProjectViews.IServices;

namespace ProjectViews.Controllers
{
    public class HangController : Controller
    {
        private readonly ILogger<HangController> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly IAllServices _services;

        public HangController(ILogger<HangController> logger, IWebHostEnvironment webHost, IAllServices services)
        {
            _logger = logger;
            _webHost = webHost;
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _services.GetAll<Hang>("https://localhost:7203/api/Hangs/Get-All");
            var hang = from a in result.ToList()
                       select new ViewHang()
                       {
                           Id = a.Id,
                           TenHang = a.TenHang,
                       };
            return View(hang);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(HangModel model)
        {
            if (ModelState.IsValid)
            {
                CreateHang hang = new CreateHang()
                {
                    TenHang = model.TenHang,
                };
                await _services.Add("https://localhost:7203/api/Hangs/", hang);
                return RedirectToAction("Index");
            }

            return View();
        }
        
        public async Task<IActionResult> Delete(Guid id)
        {
            await _services.Remove<Hang>("https://localhost:7203/api/Hangs/GetById/", "https://localhost:7203/api/Hangs/Delete/", id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _services.GetById<Hang>("https://localhost:7203/api/Hangs/GetById/", id);
            UpdateHang up = new UpdateHang()
            {
                Id = id,
                TenHang = result.TenHang,
            };
            return View(up);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateHang hang)
        {
            if (ModelState.IsValid)
            {
                //API sai router Update/id => {id}
                UpdateHang update = new UpdateHang()
                {
                    TenHang = hang.TenHang,
                };
                await _services.Update<UpdateHang>("https://localhost:7203/api/Hangs/Update/", update, hang.Id);
                return RedirectToAction("Index","Hang");
            }

            return View();
        }
    }
}
