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
            string idgh = HttpContext.Session.GetString("idgh");

            var lstSPCT = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            var lstGHCT = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");
            var lstKC = await _services.GetAll<KichCo>(Connection.api + "KichCos/Get-All");
            var lstMauSac = await _services.GetAll<MauSac>(Connection.api + "MauSacs/Get-All");
            ViewData["lstMauSac"] = lstMauSac.ToList();
            ViewData["lstKC"] = lstKC.ToList();
            ViewData["lstSPCT"] = lstSPCT.ToList();
            decimal tongtien = 0;
            var ls = lstGHCT.Where(x => x.IdGioHang.ToString() == idgh);
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

            var lstSizeSP = await _services.GetAll<SizeSanPham>(Connection.api + "SizeSanPhams/Get-All");
            var lstSanphamChitiet = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            var lstKichCo = await _services.GetAll<KichCo>(Connection.api + "KichCos/Get-All");
            var respons = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");


            var respon = respons.FirstOrDefault(x => x.IdGioHang.ToString() == idgh && x.Id == id);
            var kc = lstKichCo.FirstOrDefault(x => x.Id == respon.IdKichCo);
            var sizesp = lstSizeSP.FirstOrDefault(x => x.IdSize == kc.Id);

            var lstsl = (from a in lstSanphamChitiet.ToList()
                         join b in lstSizeSP.ToList() on a.Id equals b.IdSanPhamChiTiet
                         join c in lstKichCo.ToList().Where(x => x.Id == kc.Id) on b.IdSize equals c.Id
                         join d in respons.ToList() on c.Id equals d.IdKichCo
                         select new { a, b, c, d }).FirstOrDefault();

            respon.SoLuong++;
            if (respon.SoLuong > sizesp.SoLuong)
            {
                return Ok("Vượt quá số lượng trong kho");
            }
            else
            {
                await _services.Update(Connection.api + "GioHangChiTiets/Update/", respon, respon.Id);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GiamSL(Guid id)
        {
            string idgh = HttpContext.Session.GetString("idgh");
            var respons = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");
            var lstSizeSP = await _services.GetAll<SizeSanPham>(Connection.api + "SizeSanPhams/Get-All");
            var lstKichCo = await _services.GetAll<KichCo>(Connection.api + "KichCos/Get-All");
            var respon = respons.FirstOrDefault(x => x.IdGioHang.ToString() == idgh && x.Id == id);
            var kc = lstKichCo.FirstOrDefault(x => x.Id == respon.IdKichCo);
            var sizesp = lstSizeSP.FirstOrDefault(x => x.IdSize == kc.Id);

            --respon.SoLuong;
            if (respon.SoLuong <= 0)
            {
                await _services.Remove<GiohangChitiet>(Connection.api + "GioHangChiTiets/GetById/", Connection.api + "GioHangChiTiets/Delete/", respon.Id);
                return RedirectToAction("Index");
            }
            await _services.Update(Connection.api + "GioHangChiTiets/Update/", respon, respon.Id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Add(Guid ma, string idSize, string soluong)
        {
            string idkh = HttpContext.Session.GetString("idkh");
            if (idkh == null)
            {
                return RedirectToAction("DangNhap", "Login");
            }
            else
            {
                if (idSize == null)
                {
                    return Ok("Vui lòng chọn size");
                }
                else if (int.Parse(soluong) <= 0)
                {
                    return Ok("Vui lòng nhập số lượng");
                }
                else
                {
                    var maND = await _services.GetAll<KhachHang>(Connection.api + "KhachHangs/Get-All");
                    var maKH = maND.FirstOrDefault(p => p.Id.ToString() == idkh);

                    var laymaGH = await _services.GetAll<GioHang>(Connection.api + "GioHangs/Get-All");
                    var maGH = laymaGH.FirstOrDefault(p => p.IdKH.ToString() == idkh);

                    var lstSanphamChitiet = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
                    var spct = lstSanphamChitiet.FirstOrDefault(x => x.Id == ma);


                    var lstSizeSP = await _services.GetAll<SizeSanPham>(Connection.api + "SizeSanPhams/Get-All");
                    var sizesp = lstSizeSP.FirstOrDefault(x => x.IdSize == Guid.Parse(idSize) && x.IdSanPhamChiTiet == ma);
                    var lstKichCo = await _services.GetAll<KichCo>(Connection.api + "KichCos/Get-All");
                    var dataa = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");
                    var data = dataa.FirstOrDefault(p => p.IdSPChitiet == spct.Id && p.IdGioHang == maGH.Id && p.IdKichCo.ToString() == idSize);
                    if (data == null)
                    {
                        if (int.Parse(soluong) > sizesp.SoLuong)
                        {
                            return Ok("Vượt quá số lượng trong kho");
                        }
                        else
                        {
                            CreateGioHangChiTiet ghct = new CreateGioHangChiTiet()
                            {
                                IdGioHang = maGH.Id,
                                IdSPChitiet = spct.Id,
                                GiaBan = spct.GiaBan,
                                SoLuong = int.Parse(soluong),
                                IdKichCo = Guid.Parse(idSize),
                            };
                            await _services.Add(Connection.api + "GioHangChiTiets/", ghct);
                            return RedirectToAction("Index");
                        }

                    }
                    else
                    {
                        var respon = (from a in lstSanphamChitiet.ToList()
                                      join b in lstSizeSP.ToList() on a.Id equals b.IdSanPhamChiTiet
                                      join c in lstKichCo.ToList() on b.IdSize equals c.Id
                                      where a.Id == ma
                                      select new { a, b, c });

                        var sl = respon.FirstOrDefault(x => x.a.Id == ma);
                        data.SoLuong++;
                        if (data.SoLuong > sl.b.SoLuong)
                        {
                            return Ok("Vượt quá số lượng trong kho");
                        }
                        else
                        {
                            await _services.Update(Connection.api + "GioHangChiTiets/Update/", data, data.Id);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _services.Remove<GiohangChitiet>(Connection.api + "GioHangChiTiets/GetById/", Connection.api + "GioHangChiTiets/Delete/", id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> MuaHang()
        {
            string idgh = HttpContext.Session.GetString("idgh");
            var lstSPCT = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            var lstKC = await _services.GetAll<KichCo>(Connection.api + "KichCos/Get-All");
            ViewData["lstKC"] = lstKC.ToList();
            ViewData["lstSPCT"] = lstSPCT.ToList();
            var lstGHCT = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");
            var ls = lstGHCT.Where(x => x.IdGioHang.ToString() == idgh);
            decimal tongtien = 0;
            foreach (var item in ls)
            {
                tongtien += (item.SoLuong * item.GiaBan);
            }
            ViewBag.tt = tongtien;
            return View(ls);
        }

        public async Task<IActionResult> CheckMaGiamGia(string magiamgia)
        {

            string idgh = HttpContext.Session.GetString("idgh");

            var lstMa = await _services.GetAll<MaGiamGia>(Connection.api + "MaGiamGias/Get-All");
            var lstSPCT = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            var lstKC = await _services.GetAll<KichCo>(Connection.api + "KichCos/Get-All");
            var lstGHCT = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");

            var ls = lstGHCT.Where(x => x.IdGioHang.ToString() == idgh);
            ViewData["lstKC"] = lstKC.ToList();
            ViewData["lstSPCT"] = lstSPCT.ToList();
            var ma = lstMa.FirstOrDefault(x => x.Ma == magiamgia);
            decimal tongtien = 0;
            if (ma == null)
            {
                ViewData["thongbao"] = "Không có mã giảm giá này";
                foreach (var item in ls)
                {
                    tongtien += (item.SoLuong * item.GiaBan);
                }
                ViewBag.tt = tongtien;
                return View("MuaHang", ls);
            }
            else
            {
                if (ma.NgayKetthuc < DateTime.Now)
                {
                    ViewData["thongbao"] = "Mã giảm giá đã hết hạn";
                    foreach (var item in ls)
                    {
                        tongtien += (item.SoLuong * item.GiaBan);
                    }
                    ViewBag.tt = tongtien;
                    return View("MuaHang", ls);
                }
                else if (ma.SoLuong <= 0)
                {
                    ViewData["thongbao"] = "Mã giảm giá đã hết.";
                    foreach (var item in ls)
                    {
                        tongtien += (item.SoLuong * item.GiaBan);
                    }
                    ViewBag.tt = tongtien;
                    return View("MuaHang", ls);
                }
                else if (ma.TrangThai == 0)
                {
                    ViewData["thongbao"] = "Mã giảm giá không khả dụng";
                    foreach (var item in ls)
                    {
                        tongtien += (item.SoLuong * item.GiaBan);
                    }
                    ViewBag.tt = tongtien;
                    return View("MuaHang", ls);
                }
                else
                {
                    ViewData["thongbao"] = "Đã áp dụng mã thành công";
                    foreach (var item in ls)
                    {
                        tongtien += (item.SoLuong * item.GiaBan) * (100 - ma.PhanTramGiam) / 100;
                    }
                    --ma.SoLuong;
                    if (ma.SoLuong <= 0)
                    {
                        ma.TrangThai = 0;
                    }
                    await _services.Update(Connection.api + "MaGiamGias/Update/", ma, ma.Id);
                    ViewBag.tt = tongtien;
                    HttpContext.Session.SetString("idmgg", ma.Id.ToString());
                    return View("MuaHang", ls);
                }

            }

        }

        public async Task<IActionResult> CheckOut(string tongtien)
        {
            string idgh = HttpContext.Session.GetString("idgh");
            string idkh = HttpContext.Session.GetString("idkh");
            string idmgg = HttpContext.Session.GetString("idmgg");

            var maND = await _services.GetAll<KhachHang>(Connection.api + "KhachHangs/Get-All");
            var maKH = maND.FirstOrDefault(p => p.Id.ToString() == idkh);
            var lstGHCT = await _services.GetAll<GiohangChitiet>(Connection.api + "GioHangChiTiets/Get-All");
            var ls = lstGHCT.Where(x => x.IdGioHang.ToString() == idgh);
            var lsgh = lstGHCT.FirstOrDefault(x => x.IdGioHang.ToString() == idgh);
            var lstSizeSP = await _services.GetAll<SizeSanPham>(Connection.api + "SizeSanPhams/Get-All");
            var lstKichCo = await _services.GetAll<KichCo>(Connection.api + "KichCos/Get-All");
            var lstSanphamChitiet = await _services.GetAll<SanphamChitiet>(Connection.api + "SanphamChitiets/Get-All");
            CreateHoaDon hd = new CreateHoaDon()
            {
                Id = Guid.NewGuid(),
                IdMaGiamGia = idmgg != null ? Guid.Parse(idmgg) : null,
                IdKH = Guid.Parse(idkh),
                NgayTao = DateTime.Now,
                TrangThai = 1,
                TongTien = Convert.ToDecimal(tongtien),
                DiaChi = maKH.DiaChi,
            };
            await _services.Add(Connection.api + "HoaDons/", hd);
            foreach (var item in ls)
            {
                var sls = lstSizeSP.FirstOrDefault(x => x.IdSanPhamChiTiet == item.IdSPChitiet);
                CreateHoaDonChiTiet hdct = new CreateHoaDonChiTiet()
                {
                    Id = Guid.NewGuid(),
                    IdHoaDon = hd.Id,
                    IdKichCo = item.IdKichCo,
                    IdSPChitiet = item.IdSPChitiet,
                    SoLuong = item.SoLuong,
                    GiaBan = item.GiaBan,
                };
                await _services.Add(Connection.api + "HoaDonChiTiets/", hdct);

                sls.SoLuong -= item.SoLuong;
                await _services.Update(Connection.api + "SizeSanPhams/Update/", sls, sls.Id);

                await _services.Remove<GiohangChitiet>(Connection.api + "GioHangChiTiets/GetById/", Connection.api + "GioHangChiTiets/Delete/", item.Id);
            }
            return View();
        }
    }
}
