using CustomerViews.IServices;
using CustomerViews.Models;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using _1_API.ViewModel.GioHangChiTiet;

namespace CustomerViews.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly IAllServices _services;
        public CartController(ILogger<CartController> logger, IAllServices services)
        {
            _logger = logger;
            _services = services;
        }
        public async Task<IActionResult> Index()
        {
            string idkh = HttpContext.Session.GetString("idkh");
            string idgh = HttpContext.Session.GetString("idgh");
            var lstSPCT = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            var lstSanPham = await _services.GetAll<SanPham>(Connection.api + "SanPhams/Get-All");
            ViewData["lstSP"] = lstSanPham.ToList();

            var laymaGH = await _services.GetAll<GioHang>(Connection.api + "GioHangs/Get-All");
            var maGH = laymaGH.FirstOrDefault(p => p.IdKH.ToString() == idkh);
            decimal tongtien = 0;
            var lstGHCT = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");
            var ls = lstGHCT.Where(x=>x.IdGioHang.ToString() == idgh);
            foreach (var item in ls)
            {
                tongtien += (item.SoLuong * item.GiaBan);
            }
            ViewBag.tt = tongtien;
            return View(lstSPCT.ToList());
        }
        public async Task<IActionResult> Add(Guid id)
        {
            string login = HttpContext.Session.GetString("idkh");
            if (login == null)
            {
                return RedirectToAction("ErrorKHchuadangnhap");
            }
            else
            {
                var maND = await _services.GetAll<KhachHang>(Connection.api + "KhachHangs/Get-All");
                var maKH = maND.FirstOrDefault(p => p.Id.ToString() == login);

                var laymaGH = await _services.GetAll<GioHang>(Connection.api + "GioHangs/Get-All");
                var maGH = laymaGH.FirstOrDefault(p => p.IdKH.ToString() == login);

                var lstSanphamChitiet = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
                var spct = lstSanphamChitiet.FirstOrDefault(x => x.Id == id);

                var dataa = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");
                var data = dataa.FirstOrDefault(p => p.IdSPChitiet == spct.Id && p.IdGioHang == maGH.Id);
                if (data == null)
                {
                    CreateGioHangChiTiet ghct = new CreateGioHangChiTiet()
                    {
                        IdGioHang = maGH.Id,
                        IdSPChitiet = spct.Id,
                        GiaBan = spct.GiaBan,
                        SoLuong = 1
                    };
                    await _services.Add(Connection.api + "GioHangChiTiets/", ghct);
                    return RedirectToAction("Index");
                }
                else
                {
                    data.SoLuong++;
                    await _services.Update(Connection.api + "GioHangChiTiets/", data, id);
                    return RedirectToAction("Index");
                }
            }
        }
        public async Task< IActionResult> TangSL(Guid id)
        {
            string idgh = HttpContext.Session.GetString("idgh");
            var lstSanphamChitiet = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            var spct = lstSanphamChitiet.FirstOrDefault(x => x.Id == id);
            var respons = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/GetById");
            var respon = respons.FirstOrDefault(x => x.IdSPChitiet == spct.Id && x.IdGioHang.ToString() == idgh);

            respon.SoLuong++;

            if (respon.SoLuong > spct.SoLuong)
            {
                return Ok("Vượt quá số lượng trong kho");
            }
            else
            {
                _services.Update(Connection.api + "GioHangChiTiets/", respon,id);
                return RedirectToAction("AllGioHangchitiet");
            }

        }
        public async Task<IActionResult> GiamSL(Guid id)
        {
            string idgh = HttpContext.Session.GetString("idgh");
            var lstSanphamChitiet = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            var spct = lstSanphamChitiet.FirstOrDefault(x => x.Id == id);
            var respons = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/GetById");
            var respon = respons.FirstOrDefault(x => x.IdSPChitiet == spct.Id && x.IdGioHang.ToString() == idgh);

            respon.SoLuong--;

            if (respon.SoLuong <= 0)
            {
                ViewBag.thongbao = "Số lượng mua ít nhất là 1 sản phẩm";
                return RedirectToAction("Index");
            }
            _services.Update(Connection.api + "GioHangChiTiets/", respon, id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Tru(Guid id)
        {
            string login = HttpContext.Session.GetString("idkh");
            if (login == null)
            {
                return RedirectToAction("ErrorKHchuadangnhap");
            }
            else
            {
                var maND = await _services.GetAll<KhachHang>(Connection.api + "KhachHangs/Get-All");
                var maKH = maND.FirstOrDefault(p => p.Id.ToString() == login);

                var laymaGH = await _services.GetAll<GioHang>(Connection.api + "GioHangs/Get-All");
                var maGH = laymaGH.FirstOrDefault(p => p.IdKH.ToString() == login);

                var lstSanphamChitiet = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
                var spct = lstSanphamChitiet.FirstOrDefault(x => x.Id == id);

                var dataa = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");
                var data = dataa.FirstOrDefault(p => p.IdSPChitiet == spct.Id && p.IdGioHang == maGH.Id);
                if (data == null)
                {
                    CreateGioHangChiTiet ghct = new CreateGioHangChiTiet()
                    {
                        IdGioHang = maGH.Id,
                        IdSPChitiet = spct.Id,
                        GiaBan = spct.GiaBan,
                        SoLuong = 1
                    };
                    await _services.Add(Connection.api + "GioHangChiTiets/", ghct);
                    return RedirectToAction("Index");
                }
                else
                {
                    --data.SoLuong;
                    await _services.Update(Connection.api + "GioHangChiTiets/", data, id);
                    return RedirectToAction("Index");
                }
            }
        }
        public async Task<IActionResult> Addtocard(Guid ma)
		{
			string login = HttpContext.Session.GetString("idkh");
			if (login == null)
			{
				return RedirectToAction("ErrorKHchuadangnhap");
			}
			else
			{
                var maND = await _services.GetAll<KhachHang>(Connection.api + "KhachHangs/Get-All");
                var maKH = maND.FirstOrDefault(p => p.Id.ToString() == login);

                var laymaGH = await _services.GetAll<GioHang>(Connection.api + "GioHangs/Get-All");
                var maGH = laymaGH.FirstOrDefault(p => p.IdKH.ToString() == login);

				var lstSanphamChitiet = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
				var spct = lstSanphamChitiet.FirstOrDefault(x=>x.Id == ma);

                var dataa =await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");
                var data= dataa.FirstOrDefault(p => p.IdSPChitiet == spct.Id && p.IdGioHang == maGH.Id);
				if (data == null)
				{
                    CreateGioHangChiTiet ghct = new CreateGioHangChiTiet()
                    {
						IdGioHang = maGH.Id,
						IdSPChitiet= spct.Id,
						GiaBan = spct.GiaBan,
						SoLuong = 1
					};
				    await _services.Add(Connection.api + "GioHangChiTiets/", ghct);
					return RedirectToAction("Index");
				}
				else
				{
					data.SoLuong++;
					await _services.Update(Connection.api + "GioHangChiTiets/", data, ma);
					return RedirectToAction("Index");
				}
			}
		}
        public async Task<IActionResult> MuaHang()
        {
            return View();
        }
    }
}
