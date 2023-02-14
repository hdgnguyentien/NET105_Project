using _1_API.ViewModel.NhanVien;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectViews.IServices;
using System.Security.Cryptography;

namespace ProjectViews.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly ILogger<NhanVienController> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly IAllServices _services;

        public NhanVienController(ILogger<NhanVienController> logger, IWebHostEnvironment webHost, IAllServices services)
        {
            _logger = logger;
            _webHost = webHost;
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lstNhanVien = await _services.GetAll<NhanVien>("https://localhost:7203/api/NhanViens/Get-All");

            var lstChucVu = await _services.GetAll<ChucVu>("https://localhost:7203/api/ChucVus/Get-All");

            var nv = from a in lstNhanVien.ToList()
                     join b in lstChucVu.ToList() on a.IdCvu equals b.Id
                     select new ViewNhanVien()
                     {
                         Id = a.Id,
                         Ten = a.Ten,
                         MaNV = a.MaNV,
                         AnhNhanVien = a.AnhNhanVien,
                         ChucVu = b.Ten,
                         Email = a.Email,
                         Sdt = a.Sdt,
                         TrangThai = a.TrangThai == 1 ? "Đang hoạt động" : "Ngưng hoạt động"
                     };

            return View(nv);
        }

        public async Task<IActionResult> CreateNhanVien()
        {
            var lstChucVu = await _services.GetAll<ChucVu>("https://localhost:7203/api/ChucVus/Get-All");
            ViewData["ListChucVu"] = lstChucVu.ToList();

            var lstNhanVien = await _services.GetAll<NhanVien>("https://localhost:7203/api/NhanViens/Get-All");
            ViewData["ListNhanVien"] = lstNhanVien.ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNhanVien(NhanVienModel nvmodel)
        {
            var lstChucVu = await _services.GetAll<ChucVu>("https://localhost:7203/api/ChucVus/Get-All");
            ViewData["ListChucVu"] = lstChucVu.ToList();

            var lstNhanVien = await _services.GetAll<NhanVien>("https://localhost:7203/api/NhanViens/Get-All");
            ViewData["ListNhanVien"] = lstNhanVien.ToList();

            var user = await _services.GetAll<NhanVien>("https://localhost:7203/api/NhanViens/Get-All");

            var emails = user.FirstOrDefault(p => p.Email == nvmodel.Email);
            if (emails != null)
            {
                ModelState.AddModelError("", "Email đã được sử dụng");
            }
            var sdt = user.FirstOrDefault(p => p.Sdt == nvmodel.Sdt);
            if (sdt != null)
            {
                ModelState.AddModelError("", "SDT đã được sử dụng");
            }
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (nvmodel.ImageFile != null)
                {
                    string uploadFolder = Path.Combine(_webHost.WebRootPath, "AnhNhanVien");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + nvmodel.ImageFile.FileName;
                    string filepath = Path.Combine(uploadFolder, uniqueFileName);
                    var filestream = new FileStream(filepath, FileMode.Create);
                    nvmodel.ImageFile.CopyTo(filestream);
                };
                CreateNhanVien nv = new CreateNhanVien()
                {
                    Ten = nvmodel.Ten,
                    MatKhau = nvmodel.MatKhau,
                    Email = nvmodel.Email,
                    NgaySinh = nvmodel.NgaySinh,
                    GioiTinh = nvmodel.GioiTinh,
                    DiaChi = nvmodel.DiaChi,
                    IdCvu = nvmodel.IdCvu,
                    IdGuiBaoCao = nvmodel.IdGuiBaoCao,
                    TrangThai = nvmodel.TrangThai,
                    Sdt = nvmodel.Sdt,
                    AnhNhanVien = uniqueFileName,
                };
                await _services.Add("https://localhost:7203/api/NhanViens/",nv);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _services.Remove<NhanVien>("https://localhost:7203/api/NhanViens/GetById/", "https://localhost:7203/api/NhanViens/Delete/", id);
            return RedirectToAction("Index", "NhanVien");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var lstChucVu = await _services.GetAll<ChucVu>("https://localhost:7203/api/ChucVus/Get-All");
            ViewData["ListChucVu"] = lstChucVu.ToList();

            var lstNhanVien = await _services.GetAll<NhanVien>("https://localhost:7203/api/NhanViens/Get-All");
            ViewData["ListNhanVien"] = lstNhanVien.ToList();

            var nvid = await _services.GetById<NhanVien>("https://localhost:7203/api/NhanViens/GetById/", id);

            UpdateNhanVien nvmd = new UpdateNhanVien()
            {
                Id = id,
                Ten = nvid.Ten,
                GioiTinh = nvid.GioiTinh,
                NgaySinh = nvid.NgaySinh,
                DiaChi = nvid.DiaChi,
                AnhNhanVien = nvid.AnhNhanVien,
                Sdt = nvid.Sdt,
                Email = nvid.Email,
                MatKhau = nvid.MatKhau,
                IdCvu = nvid.IdCvu,
                TrangThai = nvid.TrangThai,
                IdGuiBaoCao = nvid.IdGuiBaoCao,
            };

            ViewData["Image"] = nvid.AnhNhanVien;
            return View(nvmd);
        }

        public async Task<IActionResult> Update(UpdateNhanVien nv)
        {
            var lstChucVu = await _services.GetAll<ChucVu>("https://localhost:7203/api/ChucVus/Get-All");
            ViewData["ListChucVu"] = lstChucVu.ToList();

            var lstNhanVien = await _services.GetAll<NhanVien>("https://localhost:7203/api/NhanViens/Get-All");
            ViewData["ListNhanVien"] = lstNhanVien.ToList();

            var nvs = await _services.GetById<NhanVien>("https://localhost:7203/api/NhanViens/GetById/", nv.Id);

            var email = await _services.GetAll<NhanVien>("https://localhost:7203/api/NhanViens/Get-All");
            email = email.Where(p => p.Email != nvs.Email);

            var sdt = await _services.GetAll<NhanVien>("https://localhost:7203/api/NhanViens/Get-All");
            sdt = sdt.Where(p => p.Sdt != nvs.Sdt);

            var emails = email.FirstOrDefault(p => p.Email == nv.Email);
            var sdts = sdt.FirstOrDefault(p => p.Sdt == nv.Sdt);
            if (emails != null)
            {
                ModelState.AddModelError("", "Email đã được sử dụng");
            }
            if (sdts != null)
            {
                ModelState.AddModelError("", "SDT đã được sử dụng");
            }
            if (emails != null || sdts != null)
            {
                return View("Edit", nv);
            }
            UpdateNhanVien update = new UpdateNhanVien()
            {
                Ten = nv.Ten,
                GioiTinh = nv.GioiTinh,
                NgaySinh = nv.NgaySinh,
                DiaChi = nv.DiaChi,
                Sdt = nv.Sdt,
                Email = nv.Email,
                MatKhau = nv.MatKhau,
                IdCvu = nv.IdCvu,
                TrangThai = nv.TrangThai,
                IdGuiBaoCao = nv.IdGuiBaoCao,
                AnhNhanVien = nv.AnhNhanVien,
            };
            string uniqueFileName = null;
            if (nv.ImageFile != null)
            {
                string uploadFolder = Path.Combine(_webHost.WebRootPath, "AnhNhanVien");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + nv.ImageFile.FileName;
                string filepath = Path.Combine(uploadFolder, uniqueFileName);
                var filestream = new FileStream(filepath, FileMode.Create);
                nv.ImageFile.CopyTo(filestream);
                update.AnhNhanVien = uniqueFileName;
            };
            await _services.Update<UpdateNhanVien>("https://localhost:7203/api/NhanViens/Update/", update,nv.Id);
            return RedirectToAction("Index", "NhanVien");
        }
    }
}
