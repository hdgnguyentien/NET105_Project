using _1_API.ViewModel.NhanVien;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectViews.IServices;

namespace ProjectViews.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly ILogger<NhanVienController> _logger;
        private readonly INhanVienServices _nhanvienServices;

        public NhanVienController(ILogger<NhanVienController> logger, INhanVienServices nhanvienServices)
        {
            _logger = logger;
            _nhanvienServices = nhanvienServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<NhanVien> nhanviens = new List<NhanVien>();
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://localhost:7203/api/NhanViens/Get-All");
            string nhanvienResponse = await response.Content.ReadAsStringAsync();
            nhanviens =  JsonConvert.DeserializeObject<List<NhanVien>>(nhanvienResponse);
            return View(nhanviens);
        }

        public IActionResult CreateNhanVien()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNhanVien(CreateNhanVien nv)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:7203/api/NhanViens/");

            
            var postTask = client.PostAsJsonAsync<CreateNhanVien>("Create", nv);
            postTask.Wait();
            Task.FromResult(nv);
            return RedirectToAction("Index");
        }
    }
}
