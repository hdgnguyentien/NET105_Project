using _1_API.ViewModel.KichCo;
using _1_API.ViewModel.TheLoai;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using ProjectViews.IServices;

namespace ProjectViews.Controllers
{
    public class TheLoaiController : Controller
    {
        private readonly ILogger<TheLoaiController> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly IAllServices _services;

        public TheLoaiController(ILogger<TheLoaiController> logger, IWebHostEnvironment webHost, IAllServices services)
        {
            _logger = logger;
            _webHost = webHost;
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _services.GetAll<TheLoai>("https://localhost:7203/api/Theloais/Get-All");
            var theloai = from a in result.ToList()
                         select new ViewTheLoai()
                         {
                             Id = a.Id,
                             TenTheloai = a.TenTheLoai,
                         };
            return View(theloai);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TheLoaiModel model)
        {
            if (ModelState.IsValid)
            {
                CreateTheLoai tl = new CreateTheLoai()
                {
                    TenTheLoai = model.TenTheLoai,
                };
                await _services.Add("https://localhost:7203/api/TheLoais/", tl);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _services.Remove<TheLoai>("https://localhost:7203/api/TheLoais/GetById",
                "https://localhost:7203/api/TheLoais/Delete/", id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _services.GetById<TheLoai>("https://localhost:7203/api/TheLoais/GetById/", id);
            UpdateTheLoai tl = new UpdateTheLoai()
            {
                Id = id,
                TenTheLoai = result.TenTheLoai,
            };
            return View(tl);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateTheLoai updateTheLoai)
        {
            if (ModelState.IsValid)
            {
                UpdateTheLoai theloai = new UpdateTheLoai()
                {
                    TenTheLoai = updateTheLoai.TenTheLoai,

                };
                await _services.Update<UpdateTheLoai>("https://localhost:7203/api/TheLoais/Update/", theloai, updateTheLoai.Id);
                return RedirectToAction("Index", "TheLoai");
            }
            return View();
        }
    }
}
