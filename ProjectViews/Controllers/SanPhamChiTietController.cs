using _1_API.ViewModel.SanPham;
using _1_API.ViewModel.SanphamChitiet;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectViews.IServices;
using static _1_API.ViewModel.SanphamChitiet.SanPhamChiTietModel;

namespace ProjectViews.Controllers
{
    public class SanPhamChiTietController : Controller
    {
        private readonly ILogger<SanPhamChiTietController> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly IAllServices _services;
        public SanPhamChiTietController(ILogger<SanPhamChiTietController> logger, IWebHostEnvironment webHost, IAllServices services)
        {
            _logger = logger;
            _webHost = webHost;
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var lstSPCT = await _services.GetAll<SanphamChitiet>("https://localhost:7203/api/SanphamChitiets/Get-All");
            var lstSP = await _services.GetAll<SanPham>("https://localhost:7203/api/SanPhams/Get-All");
            var lstKichCo = await _services.GetAll<KichCo>("https://localhost:7203/api/KichCos/Get-All");
            var lstMauSac = await _services.GetAll<MauSac>("https://localhost:7203/api/MauSacs/Get-All");
            var spct = from a in lstSPCT.ToList().Where(p => p.IdSP == Guid.Parse(id))
                       join b in lstSP on a.IdSP equals b.Id
                       join d in lstMauSac on a.IdMauSac equals d.Id
                       select new ViewSanPhamChiTiet()
                       {
                           Id= a.Id,
                           GiaBan = a.GiaBan,
                           TenMauSac=d.TenMau,
                           TrangThai = a.TrangThai == 1 ? "Đang hoạt động" : "Ngưng hoạt động",
                           MaSPChiTiet = a.MaSPChiTiet,
                           TenSPChiTiet = a.TenSPChiTiet,
                           AnhDaiDien = a.AnhDaiDien
                       };
            ViewData["idSanPham"] = id;
            return View(spct);
        }
        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {         
            var lstSP = await _services.GetAll<SanPham>("https://localhost:7203/api/SanPhams/Get-All");
            ViewData["lstSP"] = lstSP.ToList();
            var lstMauSac = await _services.GetAll<MauSac>("https://localhost:7203/api/MauSacs/Get-All");
            ViewData["lstMauSac"] = lstMauSac.ToList();
            var lstTheLoai = await _services.GetAll<TheLoai>("https://localhost:7203/api/TheLoais/Get-All");
            ViewData["lstTheLoai"] = lstTheLoai.ToList();
            var lstHang = await _services.GetAll<Hang>("https://localhost:7203/api/Hangs/Get-All");
            var sp = from a in lstSP.ToList()
                     join b in lstHang.ToList() on a.IdHang equals b.Id
                     select new ViewSanPham()
                     {
                         Id = a.Id,
                         Ten = a.Ten,
                         TrangThai = a.TrangThai == 1 ? "Đang hoạt động" : "Ngưng hoạt động",
                         TenHang = b.TenHang
                     };
            var sp1 = sp.FirstOrDefault(p => p.Id == Guid.Parse(id));
            ViewData["SanPham"] = sp1;
            SanPhamChiTietModel spmodel = new SanPhamChiTietModel();
            spmodel.IdSP = Guid.Parse(id);
            foreach(var item in lstTheLoai)
            {
                spmodel.TheLoai.Add(new SelectListItem { Text = item.TenTheLoai, Value = item.Id.ToString() });
            }
            return View(spmodel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(SanPhamChiTietModel model)
        {
            var lstSP = await _services.GetAll<SanPham>("https://localhost:7203/api/SanPhams/Get-All");
            ViewData["lstSP"] = lstSP.ToList();
            var lstMauSac = await _services.GetAll<MauSac>("https://localhost:7203/api/MauSacs/Get-All");
            ViewData["lstMauSac"] = lstMauSac.ToList();
            var lstTheLoai = await _services.GetAll<TheLoai>("https://localhost:7203/api/TheLoais/Get-All");
            ViewData["lstTheLoai"] = lstTheLoai.ToList();
            var lstHang = await _services.GetAll<Hang>("https://localhost:7203/api/Hangs/Get-All");
            var sp = from a in lstSP.ToList()
                     join b in lstHang.ToList() on a.IdHang equals b.Id
                     select new ViewSanPham()
                     {
                         Id = a.Id,
                         Ten = a.Ten,
                         TrangThai = a.TrangThai == 1 ? "Đang hoạt động" : "Ngưng hoạt động",
                         TenHang = b.TenHang
                     };
            var sp1 = sp.FirstOrDefault(p => p.Id == (model.IdSP));
            ViewData["SanPham"] = sp1;
            SanPhamChiTietModel spmodel = new SanPhamChiTietModel();
            spmodel.IdSP = model.IdSP;
            foreach (var item in lstTheLoai)
            {
                spmodel.TheLoai.Add(new SelectListItem { Text = item.TenTheLoai, Value = item.Id.ToString() });
            }
            if (model.GiaNhap > model.GiaBan)
            {
                ModelState.AddModelError("","Giá bán phải cao hơn giá nhập");
            }
            if (model.Selected.Count == 0)
            {
                ModelState.AddModelError("", "Bạn chưa chọn thể loại cho sản phẩm");
            }
            if (ModelState.IsValid)
            {
                string uniqueFileName1 = null;
                string uniqueFileName2 = null;
                string uniqueFileName3= null;
                string uniqueFileName4= null;

                if (model.ImageFile != null && model.ImageFile1 != null && model.ImageFile2 != null && model.ImageFile3 != null)
                {
                    string uploadFolder = Path.Combine(_webHost.WebRootPath, "AnhChiTietSanPham");
                    uniqueFileName1 = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                    string filepath = Path.Combine(uploadFolder, uniqueFileName1);
                    var filestream = new FileStream(filepath, FileMode.Create);
                    model.ImageFile.CopyTo(filestream);

                    string uploadFolder1 = Path.Combine(_webHost.WebRootPath, "AnhChiTietSanPham");
                    uniqueFileName2 = Guid.NewGuid().ToString() + "_" + model.ImageFile1.FileName;
                    string filepath1 = Path.Combine(uploadFolder1, uniqueFileName2);
                    var filestream1 = new FileStream(filepath1, FileMode.Create);
                    model.ImageFile1.CopyTo(filestream1);

                    string uploadFolder2 = Path.Combine(_webHost.WebRootPath, "AnhChiTietSanPham");
                    uniqueFileName3 = Guid.NewGuid().ToString() + "_" + model.ImageFile2.FileName;
                    string filepath2 = Path.Combine(uploadFolder2, uniqueFileName3);
                    var filestream2 = new FileStream(filepath2, FileMode.Create);
                    model.ImageFile2.CopyTo(filestream2);

                    string uploadFolder3 = Path.Combine(_webHost.WebRootPath, "AnhChiTietSanPham");
                    uniqueFileName4 = Guid.NewGuid().ToString() + "_" + model.ImageFile3.FileName;
                    string filepath3 = Path.Combine(uploadFolder3, uniqueFileName4);
                    var filestream3 = new FileStream(filepath3, FileMode.Create);
                    model.ImageFile3.CopyTo(filestream3);
                };
                CreateSanphamChitiet spct = new CreateSanphamChitiet()
                {
                    IdSP= model.IdSP,
                    TenChiTiet = model.TenSPChiTiet,
                    IdMauSac= model.IdMauSac,
                    GiaBan = model.GiaBan,
                    GiaNhap=model.GiaNhap,
                    TrangThai = model.TrangThai,
                    AnhDaiDien = uniqueFileName1,
                    Selected = model.Selected,
                    AnhPhu1 = uniqueFileName2,
                    AnhPhu2 = uniqueFileName3,
                    AnhPhu3 = uniqueFileName4,
                };
                
                await _services.Add("https://localhost:7203/api/SanphamChiTiets/", spct);
                var a = model.IdSP.ToString();
                return RedirectToAction("Index", "SanphamChitiet", new {id = model.IdSP});
            }
            return View(spmodel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _services.Remove<SanphamChitiet>("https://localhost:7203/api/SanphamChiTiets/GetById/", "https://localhost:7203/api/SanphamChiTiets/Delete/", id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var lstSP = await _services.GetAll<SanPham>("https://localhost:7203/api/SanPhams/Get-All");
            ViewData["lstSP"] = lstSP.ToList();
            var lstMauSac = await _services.GetAll<MauSac>("https://localhost:7203/api/MauSacs/Get-All");
            ViewData["lstMauSac"] = lstMauSac.ToList();
            var lstTheLoai = await _services.GetAll<TheLoai>("https://localhost:7203/api/TheLoais/Get-All");
            
            ViewData["lstTheLoai"] = lstTheLoai.ToList();
            var lstHang = await _services.GetAll<Hang>("https://localhost:7203/api/Hangs/Get-All");
            var lstSPCT = await _services.GetAll<SanphamChitiet>("https://localhost:7203/api/SanphamChitiets/Get-All");
            var spct = await _services.GetById<SanphamChitiet>("https://localhost:7203/api/SanphamChitiets/GetById/",Guid.Parse(id));
            var sp = from a in lstSP.ToList()
                     join b in lstHang.ToList() on a.IdHang equals b.Id
                     select new ViewSanPham()
                     {
                         Id = a.Id,
                         Ten = a.Ten,
                         TrangThai = a.TrangThai == 1 ? "Đang hoạt động" : "Ngưng hoạt động",
                         TenHang = b.TenHang
                     };
            var sp1 = sp.FirstOrDefault(p => p.Id == spct.IdSP);
            ViewData["SanPham"] = sp1;
            var lstTheLoaiSanPham = await _services.GetAll<TheLoaiSanPham>("https://localhost:7203/api/TheLoaiSanPhams/Get-All");
            var tl = from a in lstSPCT.ToList()
                     join b in lstTheLoaiSanPham on a.Id equals b.IdChiTietSP
                     select (a,b);
            tl = tl.Where(p => p.b.IdChiTietSP == Guid.Parse(id)).ToList();
            List<string> listtl = new List<string>();
            foreach (var t in tl)
            {
                listtl.Add(t.b.IdTheLoai.ToString());
            }
            
            UpdateSanphamChitiet update = new UpdateSanphamChitiet()
            {
                IdSPCT = Guid.Parse(id),
                IdSP= spct.IdSP,
                GiaNhap= spct.GiaNhap,
                GiaBan= spct.GiaBan,
                TrangThai= spct.TrangThai,
                IdMauSac= spct.IdMauSac,
                AnhDaiDien = spct.AnhDaiDien,
                AnhPhu1 = spct.AnhPhu1,
                AnhPhu2 = spct.AnhPhu2,
                AnhPhu3 = spct.AnhPhu3,
                TenSPChiTiet = spct.TenSPChiTiet,
                Selected = listtl
            };
            foreach (var item in lstTheLoai)
            {
                update.TheLoai.Add(new SelectListItem { Text = item.TenTheLoai, Value = item.Id.ToString() });
            }
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSanphamChitiet model)
        {
            var lstSP = await _services.GetAll<SanPham>("https://localhost:7203/api/SanPhams/Get-All");
            ViewData["lstSP"] = lstSP.ToList();
            var lstMauSac = await _services.GetAll<MauSac>("https://localhost:7203/api/MauSacs/Get-All");
            ViewData["lstMauSac"] = lstMauSac.ToList();
            var lstTheLoai = await _services.GetAll<TheLoai>("https://localhost:7203/api/TheLoais/Get-All");

            ViewData["lstTheLoai"] = lstTheLoai.ToList();
            var lstHang = await _services.GetAll<Hang>("https://localhost:7203/api/Hangs/Get-All");
            var lstSPCT = await _services.GetAll<SanphamChitiet>("https://localhost:7203/api/SanphamChitiets/Get-All");
            var spct = await _services.GetById<SanphamChitiet>("https://localhost:7203/api/SanphamChitiets/GetById/", model.IdSPCT);
            var sp = from a in lstSP.ToList()
                     join b in lstHang.ToList() on a.IdHang equals b.Id
                     select new ViewSanPham()
                     {
                         Id = a.Id,
                         Ten = a.Ten,
                         TrangThai = a.TrangThai == 1 ? "Đang hoạt động" : "Ngưng hoạt động",
                         TenHang = b.TenHang
                     };
            var sp1 = sp.FirstOrDefault(p => p.Id == spct.IdSP);
            ViewData["SanPham"] = sp1;
            var lstTheLoaiSanPham = await _services.GetAll<TheLoaiSanPham>("https://localhost:7203/api/TheLoaiSanPhams/Get-All");
            var tl = from a in lstSPCT.ToList()
                     join b in lstTheLoaiSanPham on a.Id equals b.IdChiTietSP
                     select (a, b);
            tl = tl.Where(p => p.b.IdChiTietSP == model.IdSPCT).ToList();
            List<string> listtl = new List<string>();
            foreach (var t in tl)
            {
                listtl.Add(t.b.IdTheLoai.ToString());
            }
            foreach (var item in lstTheLoai)
            {
                model.TheLoai.Add(new SelectListItem { Text = item.TenTheLoai, Value = item.Id.ToString() });
            }
            if (model.GiaNhap > model.GiaBan)
            {
                ModelState.AddModelError("", "Giá bán phải cao hơn giá nhập");
            }
            if (model.Selected.Count == 0)
            {
                ModelState.AddModelError("", "Bạn chưa chọn thể loại cho sản phẩm");
            }
            if (ModelState.IsValid)
            {
                string uniqueFileName1 = null;
                string uniqueFileName2 = null;
                string uniqueFileName3 = null;
                string uniqueFileName4 = null;
                if (model.ImageFile != null)
                {
                    string uploadFolder = Path.Combine(_webHost.WebRootPath, "AnhChiTietSanPham");
                    uniqueFileName1 = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                    string filepath = Path.Combine(uploadFolder, uniqueFileName1);
                    var filestream = new FileStream(filepath, FileMode.Create);
                    model.ImageFile.CopyTo(filestream);
                };
                if (model.ImageFile1 != null)
                {
                    string uploadFolder1 = Path.Combine(_webHost.WebRootPath, "AnhChiTietSanPham");
                    uniqueFileName2 = Guid.NewGuid().ToString() + "_" + model.ImageFile1.FileName;
                    string filepath1 = Path.Combine(uploadFolder1, uniqueFileName2);
                    var filestream1 = new FileStream(filepath1, FileMode.Create);
                    model.ImageFile1.CopyTo(filestream1);
                };
                if (model.ImageFile2 != null)
                {
                    string uploadFolder2 = Path.Combine(_webHost.WebRootPath, "AnhChiTietSanPham");
                    uniqueFileName3 = Guid.NewGuid().ToString() + "_" + model.ImageFile2.FileName;
                    string filepath2 = Path.Combine(uploadFolder2, uniqueFileName3);
                    var filestream2 = new FileStream(filepath2, FileMode.Create);
                    model.ImageFile2.CopyTo(filestream2);
                };
                if (model.ImageFile3 != null)
                {
                    string uploadFolder3 = Path.Combine(_webHost.WebRootPath, "AnhChiTietSanPham");
                    uniqueFileName4 = Guid.NewGuid().ToString() + "_" + model.ImageFile3.FileName;
                    string filepath3 = Path.Combine(uploadFolder3, uniqueFileName4);
                    var filestream3 = new FileStream(filepath3, FileMode.Create);
                    model.ImageFile3.CopyTo(filestream3);
                };
                UpdateSanphamChitiet updateSPCT = new UpdateSanphamChitiet()
                {
                    TenSPChiTiet = model.TenSPChiTiet,
                    GiaBan = model.GiaBan,
                    GiaNhap = model.GiaNhap,
                    TrangThai = model.TrangThai,
                    IdMauSac = model.IdMauSac,
                    IdSP = model.IdSP,
                    IdSPCT = model.IdSPCT,
                    AnhDaiDien = uniqueFileName1 != null ? uniqueFileName1 : model.AnhDaiDien,
                    AnhPhu1 = uniqueFileName2 != null ? uniqueFileName2 : model.AnhPhu1,
                    AnhPhu2 = uniqueFileName3 != null ? uniqueFileName3 : model.AnhPhu2,
                    AnhPhu3 = uniqueFileName4 != null ? uniqueFileName4 : model.AnhPhu3,
                    Selected = model.Selected
                };
                await _services.Update<UpdateSanphamChitiet>("https://localhost:7203/api/SanphamChitiets/Update/", updateSPCT, model.IdSPCT);
                return RedirectToAction("Index", new {id = model.IdSP.ToString()});
            }
            return View("Edit",model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var lstSP = await _services.GetAll<SanPham>("https://localhost:7203/api/SanPhams/Get-All");
            var lstMauSac = await _services.GetAll<MauSac>("https://localhost:7203/api/MauSacs/Get-All");
            var lstTheLoai = await _services.GetAll<TheLoai>("https://localhost:7203/api/TheLoais/Get-All");
            var lstSPCT = await _services.GetAll<SanphamChitiet>("https://localhost:7203/api/SanphamChitiets/Get-All");
            var lstTheLoaiSanPham = await _services.GetAll<TheLoaiSanPham>("https://localhost:7203/api/TheLoaiSanPhams/Get-All");

            var lstctsp = from a in lstSP
                       join b in lstSPCT on a.Id equals b.IdSP
                       join c in lstMauSac on b.IdMauSac equals c.Id
                       select (a, b, c);
            var ctsp = lstctsp.FirstOrDefault(p => p.b.Id == id);
            SanPhamChiTietDetails details = new SanPhamChiTietDetails()
            {
                GiaBan = ctsp.b.GiaBan.ToString(),
                GiaNhap = ctsp.b.GiaNhap.ToString(),
                TrangThai = ctsp.b.TrangThai == 1 ? "Đang hoạt động" : "Ngưng hoạt động",
                MauSac = ctsp.c.TenMau,
                TenSP = ctsp.a.Ten,
                TenChiTiet = ctsp.b.TenSPChiTiet,
                AnhDaiDien = ctsp.b.AnhDaiDien,
                AnhPhu1 = ctsp.b.AnhPhu1,
                AnhPhu2 = ctsp.b.AnhPhu2,
                AnhPhu3 = ctsp.b.AnhPhu3,
            };
            var lsttl = from a in lstSPCT
                         join b in lstTheLoaiSanPham on a.Id equals b.IdChiTietSP
                         join c in lstTheLoai on b.IdTheLoai equals c.Id
                         where a.Id == id
                         select (a, b, c);
            foreach(var a in lsttl.ToList())
            {
                details.TheLoai += "" + a.c.TenTheLoai + ",";
            }
            return View(details);
        }
    }
}
