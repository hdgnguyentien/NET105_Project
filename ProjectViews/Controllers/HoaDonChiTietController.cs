using _1_API.ViewModel.HoaDon;
using _1_API.ViewModel.HoaDonChiTiet;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using ProjectViews.IServices;

namespace ProjectViews.Controllers
{
    public class HoaDonChiTietController : Controller
    {
        private readonly ILogger<HoaDonChiTietController> _logger;
        private readonly IAllServices _services;
        public HoaDonChiTietController(ILogger<HoaDonChiTietController> logger, IAllServices services)
        {
            _logger = logger;
            _services = services;
        }
        public async Task<IActionResult> Index(string id)
        {
            var hoadon = await _services.GetById<HoaDon>("https://localhost:7203/api/HoaDons/GetById/", Guid.Parse(id));
            var lsthoadonct = await _services.GetAll<HoadonChitiet>("https://localhost:7203/api/HoaDonChiTiets/Get-All");
            var lstsanphamct = await _services.GetAll<SanphamChitiet>("https://localhost:7203/api/SanphamChitiets/Get-All");
            var lstsanpham = await _services.GetAll<SanPham>("https://localhost:7203/api/SanPhams/Get-All");
            var lsthang = await _services.GetAll<Hang>("https://localhost:7203/api/Hangs/Get-All");
            var lstsizesp = await _services.GetAll<SizeSanPham>("https://localhost:7203/api/SizeSanPhams/Get-All");
            var lstsize = await _services.GetAll<KichCo>("https://localhost:7203/api/KichCos/Get-All");
            var lstmausac = await _services.GetAll<MauSac>("https://localhost:7203/api/MauSacs/Get-All");
            var lstkhachhang = await _services.GetAll<KhachHang>("https://localhost:7203/api/KhachHangs/Get-All");

            decimal? tongtienhoadon = 0;
            
            
            var hoadonct = lsthoadonct.ToList().Where(p => p.IdHoaDon == Guid.Parse(id));
            var kh = lstkhachhang.ToList().FirstOrDefault(p => p.Id == hoadon.IdKH);
      
            foreach (var item in lsthoadonct)
            {
                tongtienhoadon += (item.SoLuong * item.GiaBan);
            }
            var view = from a in hoadonct
                       join b in lstsanphamct on a.IdSPChitiet equals b.Id
                       join g in lstmausac on b.IdMauSac equals g.Id
                       join c in lstsanpham on b.IdSP equals c.Id
                       join d in lsthang on c.IdHang equals d.Id
                       join e in lstsizesp on a.IdSPChitiet equals e.IdSanPhamChiTiet
                       join f in lstsize on e.IdSize equals f.Id
                       where a.IdKichCo == f.Id
                       select new ViewHoaDonChiTiet()
                       {
                           Id = a.Id,
                           TenSP = "["+d.TenHang +"]" + " " + c.Ten + " " + b.TenSPChiTiet,
                           TenMauSac = g.TenMau,
                           Size = f.Size.ToString(),
                           SoLuong = a.SoLuong,
                           GiaBan = a.GiaBan,
                           MaSP = b.MaSPChiTiet,
                       };
            List<ViewHoaDonChiTiet> lstob = new List<ViewHoaDonChiTiet>();
            foreach (var item in view.ToList())
            {
                lstob.Add(item);
            }
            HoaDonChiTietView viewhdct = new HoaDonChiTietView()
            {
                Idhd = Guid.Parse(id),
                TenKhachHang = kh.Ten,
                SDT = kh.Sdt,
                DiaChi = kh.DiaChi,
                TongTien = tongtienhoadon,
                viewHoaDonChiTiets = lstob
            };
            

            return View(viewhdct);
        }
        public async Task<IActionResult> Confirm(string id)
        {
            var idnv = HttpContext.Session.GetString("idnv");
            var hd = await _services.GetById<HoaDon>("https://localhost:7203/api/HoaDons/GetById/", Guid.Parse(id));
            UpdateHoaDon updateHoaDon = new UpdateHoaDon()
            {
                IdKH = hd.IdKH,
                IdNV = Guid.Parse(idnv),
                DiaChi = hd.DiaChi,
                IdMaGiamGia = hd.IdMaGiamGia,
                NgayTao = hd.NgayTao,
                TongTien = hd.TongTien,
                TrangThai = 2,
            };
            await _services.Update<UpdateHoaDon>("https://localhost:7203/api/HoaDons/Update/", updateHoaDon, Guid.Parse(id));
            return RedirectToAction("Index","HoaDon");
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _services.Remove<HoaDon>("https://localhost:7203/api/HoaDons/GetById/", "https://localhost:7203/api/HoaDons/Delete/", Guid.Parse(id));
            return RedirectToAction("Index","HoaDon");
        }

        public async Task<IActionResult> DeleteSP(string id)
        {
            var hd = _services.GetById<HoadonChitiet>("https://localhost:7203/api/HoaDonChiTiets/GetById/", Guid.Parse(id));
            await _services.Remove<HoadonChitiet>("https://localhost:7203/api/HoaDonChiTiets/GetById/", "https://localhost:7203/api/HoaDonChiTiets/Delete/", Guid.Parse(id));
            return RedirectToAction("Index", "HoaDonChiTiet", new {id = hd.Result.IdHoaDon.ToString()});
        }

    }
}
