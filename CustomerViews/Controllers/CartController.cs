using CustomerViews.IServices;
using CustomerViews.Models;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using _1_API.ViewModel.GioHangChiTiet;
using _1_API.ViewModel.HoaDon;
using _1_API.ViewModel.HoaDonChiTiet;

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
            ViewData["lstSPCT"] = lstSPCT.ToList();
            ViewData["idGioHang"] = idgh;

            decimal tongtien = 0;
            var lstGHCT = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");
            var ls = lstGHCT.Where(x=>x.IdGioHang.ToString() == idgh);
            foreach (var item in ls)
            {
                tongtien += (item.SoLuong * item.GiaBan);
            }
            ViewBag.tt = tongtien;
            return View(ls);
        }
        public async Task<IActionResult> TangSL(Guid id)
        {
            string idgh = HttpContext.Session.GetString("idgh");
            var lstSanphamChitiet = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            var spct = lstSanphamChitiet.FirstOrDefault(x => x.Id == id);
            var respons = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");
            var respon = respons.FirstOrDefault(x => x.IdSPChitiet == spct.Id && x.IdGioHang.ToString() == idgh);
            respon.SoLuong++;
            if (respon.SoLuong > spct.SoLuong)
            {
                return Ok("Vượt quá số lượng trong kho");
            }
            else
            {
                await _services.Update(Connection.api + "GioHangChiTiets/Update/", respon, respon.Id);
                return RedirectToAction("Index");
            }

        }
        public async Task<IActionResult> GiamSL(Guid id)
        {
            string idgh = HttpContext.Session.GetString("idgh");
            var lstSanphamChitiet = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            var spct = lstSanphamChitiet.FirstOrDefault(x => x.Id == id);
            var respons = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");
            var respon = respons.FirstOrDefault(x => x.IdSPChitiet == spct.Id && x.IdGioHang.ToString() == idgh);
            --respon.SoLuong;
            if (respon.SoLuong <= 0)
            {
                await _services.Remove<GiohangChitiet>(Connection.api + "GioHangChiTiets/GetById/", Connection.api + "GioHangChiTiets/Delete/", respon.Id);
                return RedirectToAction("Index");
            }
            else
                await _services.Update(Connection.api + "GioHangChiTiets/Update/", respon, respon.Id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Addtocard(Guid ma )
		{
			string idkh = HttpContext.Session.GetString("idkh");
			if (idkh == null)
			{
				return RedirectToAction("DangNhap","Login");
			}
			else
			{
                var maND = await _services.GetAll<KhachHang>(Connection.api + "KhachHangs/Get-All");
                var maKH = maND.FirstOrDefault(p => p.Id.ToString() == idkh);

                var laymaGH = await _services.GetAll<GioHang>(Connection.api + "GioHangs/Get-All");
                var maGH = laymaGH.FirstOrDefault(p => p.IdKH.ToString() == idkh);

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
                    if (data.SoLuong >= spct.SoLuong)
                    {
                        return Ok("Vượt quá số lượng trong kho");
                    }
                    else
                    {
                        data.SoLuong++;
                        await _services.Update(Connection.api + "GioHangChiTiets/Update/", data, data.Id);
					    return RedirectToAction("Index");
                    }
				}
			}
		}
		public async Task<IActionResult> Delete(Guid id)
		{
			await _services.Remove<GiohangChitiet>(Connection.api +"GioHangChiTiets/GetById/", Connection.api +"GioHangChiTiets/Delete/", id);
			return RedirectToAction("Index");
        }
        public async Task<IActionResult> MuaHang(Guid id)
        {
            string idgh = HttpContext.Session.GetString("idgh");
            var lstSPCT = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            ViewData["lstSPCT"] = lstSPCT.ToList();
            decimal tongtien = 0;
            var lstGHCT = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");
            var ls = lstGHCT.Where(x => x.IdGioHang.ToString() == idgh);
            foreach (var item in ls)
            {
                tongtien += (item.SoLuong * item.GiaBan);
            }
            ViewBag.tt = tongtien;
            return View(ls);
        }
        public async Task<IActionResult> CheckOut(Guid id)
        {
            string idgh = HttpContext.Session.GetString("idgh");
            string idkh = HttpContext.Session.GetString("idkh");

            var maND = await _services.GetAll<KhachHang>(Connection.api + "KhachHangs/Get-All");
            var maKH = maND.FirstOrDefault(p => p.Id.ToString() == idkh);
            var lstGHCT = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");
            var ls = lstGHCT.Where(x => x.IdGioHang.ToString() == idgh);
            CreateHoaDon hd = new CreateHoaDon()
            {
                Id = Guid.NewGuid(),
                IdKH = Guid.Parse(idkh),
                NgayTao = DateTime.Now,
                TrangThai = 1,
                TongTien = ls.Sum(x=>x.GiaBan*x.SoLuong),
                DiaChi = maKH.DiaChi,
            };
            var respon = await _services.Add(Connection.api + "HoaDons/", hd);
            foreach (var item in ls)
            {
                CreateHoaDonChiTiet hdct = new CreateHoaDonChiTiet()
                {
                    Id= Guid.NewGuid(),
                    IdHoaDon = hd.Id,
                    IdSPChitiet = item.IdSPChitiet,
                    SoLuong = item.SoLuong,
                    GiaBan = item.GiaBan,
                };
                var responHDCT = await _services.Add(Connection.api + "HoaDonChiTiets/", hdct);
                var lstSanphamChitiet = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
                var spct = lstSanphamChitiet.FirstOrDefault(x => x.Id == item.IdSPChitiet);
                spct.SoLuong -= item.SoLuong;
                await _services.Update(Connection.api + "SanphamChitiets/Update/", spct, spct.Id);
            }
            //await _services.Remove<GiohangChitiet>(Connection.api + "GioHangChiTiets/GetById/", Connection.api + "GioHangChiTiets/Delete/", id);
            return View();
        }
    }
}
