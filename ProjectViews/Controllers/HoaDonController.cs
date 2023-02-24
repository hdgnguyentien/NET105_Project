using _1_API.ViewModel.HoaDon;
using _1_API.ViewModel.NhanVien;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using ProjectViews.IServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProjectViews.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly ILogger<HoaDonController> _logger;
        private readonly IAllServices _services;
        public HoaDonController(ILogger<HoaDonController> logger, IAllServices services)
        {
            _logger = logger;
            _services = services;
        }
        public async Task<IActionResult> Index()
        {
            var hd = from a in await _services.GetAll<HoaDon>("https://localhost:7203/api/HoaDons/Get-All")
                     where a.TrangThai == 1
                     select new ViewHoaDon()
                     {
                         Id = a.Id,
                         MaHD = "a",
                         NgayTao = a.NgayTao.Value.Day + "" + "/" + a.NgayTao.Value.Month + "" + "/" + a.NgayTao.Value.Year + ""
                     };
            return View(hd);
        }

        

        
    }
}
