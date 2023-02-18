using _1_API.ViewModel.KichCo;
using _1_API.ViewModel.NhanVien;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using ProjectViews.IServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProjectViews.Controllers
{
    public class KichCoController : Controller
    {
        private readonly ILogger<KichCoController> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly IAllServices _services;

        public KichCoController(ILogger<KichCoController> logger, IWebHostEnvironment webHost, IAllServices services)
        {
            _logger = logger;
            _webHost = webHost;
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            var result = await _services.GetAll<KichCo>("https://localhost:7203/api/KichCos/Get-All");
            var kichco = from a in result.ToList()
                         select new ViewKichCo()
                         {
                             Id = a.Id,
                             Size = a.Size
                         };
            return View (kichco);
        }
        public IActionResult Create () 
        {
            return View ();
        }
        [HttpPost]
        public async Task<IActionResult> Create(KichCoModel model) 
        {
            if (ModelState.IsValid)
            {
                CreateKichCo kc = new CreateKichCo()
                {
                    Size = model.Size,
                };
                await _services.Add("https://localhost:7203/api/KichCos/",kc);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _services.Remove<KichCo>("https://localhost:7203/api/KichCos/GetById",
                "https://localhost:7203/api/KichCos/Delete/",id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _services.GetById<KichCo>("https://localhost:7203/api/KichCos/GetById/", id);
            UpdateKichCo kc = new UpdateKichCo()
            {
                Id = id,
                Size = result.Size,
            };
            return View (kc);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateKichCo updateKichCo)
        {
            if (ModelState.IsValid)
            {
                UpdateKichCo kichco = new UpdateKichCo()
                {
                    Size = updateKichCo.Size,

                };
                await _services.Update<UpdateKichCo>("https://localhost:7203/api/KichCos/Update/", kichco, updateKichCo.Id);
                return RedirectToAction("Index", "KichCo");
            }
            return View();
        }
    }
}
