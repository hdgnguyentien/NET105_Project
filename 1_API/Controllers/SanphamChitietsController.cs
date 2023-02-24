using _1_API.ViewModel.SanPham;
using _1_API.ViewModel.SanphamChitiet;
using Data.IRepositories;
using Data.ModelsClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanphamChitietsController : ControllerBase
    {
        private IAllRepositories<SanphamChitiet> _repo;
        private IAllRepositories<TheLoaiSanPham> _theloaisprepo;
        private IAllRepositories<HinhAnh> _hinhanhprepo;
        private IAllRepositories<MauSac> _mausacrepo;


        public SanphamChitietsController(IAllRepositories<SanphamChitiet> repo, IAllRepositories<TheLoaiSanPham> theloaisprepo, IAllRepositories<HinhAnh> hinhanhprepo, IAllRepositories<MauSac> mausacrepo)
        {
            _repo = repo;
            _theloaisprepo = theloaisprepo;
            _hinhanhprepo = hinhanhprepo;
            _mausacrepo = mausacrepo;
        }

        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAllSanPhamCt()
        {
            var result = await _repo.GetAllAsync();
            if (result == null) return Ok("Không có sản phẩm chi tiết");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetSanPhamCtById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return Ok("Không tìm thấy sản phẩm chi tiết");
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateSanPhamCt(CreateSanphamChitiet csp)
        {
            var ma = await _repo.GetAllAsync();
            
            var listmau = await _mausacrepo.GetAllAsync();
            var tenmau = listmau.ToList().FirstOrDefault(p => p.Id == csp.IdMauSac);
            SanphamChitiet spct = new SanphamChitiet()
            {
                Id = Guid.NewGuid(),
                IdSP = csp.IdSP,
                IdMauSac = csp.IdMauSac,
                GiaBan = csp.GiaBan,
                GiaNhap = csp.GiaNhap,
                TrangThai = csp.TrangThai,
                TenSPChiTiet = csp.TenChiTiet + " Màu " + tenmau.TenMau,
                AnhDaiDien = csp.AnhDaiDien,
                MaSPChiTiet = "SP" + ma.Count(),
                AnhPhu1 = csp.AnhPhu1,
                AnhPhu2 = csp.AnhPhu2,
                AnhPhu3 = csp.AnhPhu3,
                NgayTao = DateTime.Now
            };

            try
            {
                var result = await _repo.AddOneAsyn(spct);
                foreach (var item in csp.Selected)
                {
                    TheLoaiSanPham tlsp = new TheLoaiSanPham()
                    {
                        Id = Guid.NewGuid(),
                        IdChiTietSP = spct.Id,
                        IdTheLoai = Guid.Parse(item),
                    };
                    await _theloaisprepo.AddOneAsyn(tlsp);
                }


                return Ok(spct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Tạo mới không thành công");
            }

        }

        [HttpPost]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateSanPhamCt(Guid id, UpdateSanphamChitiet usp)
        {
            var result = await _repo.GetByIdAsync(id);
            var listmau = await _mausacrepo.GetAllAsync();
            var tenmau = listmau.ToList().FirstOrDefault(p => p.Id == usp.IdMauSac);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy sản phẩm chi tiết");
            }
            else
            {

                result.IdSP = usp.IdSP;
                result.IdMauSac = usp.IdMauSac;
                result.GiaBan = usp.GiaBan;
                result.GiaNhap = usp.GiaNhap;
                result.AnhDaiDien = usp.AnhDaiDien;
                result.AnhPhu1 = usp.AnhPhu1;
                result.AnhPhu2 = usp.AnhPhu2;
                result.AnhPhu3 = usp.AnhPhu3;
                result.TrangThai = usp.TrangThai;
                result.TenSPChiTiet = usp.TenSPChiTiet + " Màu " + tenmau.TenMau;
                var lsttlsp = await _theloaisprepo.GetAllAsync();
                lsttlsp = lsttlsp.Where(p => p.IdChiTietSP == result.Id);

                try
                {
                    await _repo.UpdateOneAsyn(result);
                    foreach (var item in lsttlsp)
                    {
                        await _theloaisprepo.DeleteOneAsyn(item);
                    }
                    foreach (var item in usp.Selected)
                    {
                        TheLoaiSanPham tlsp = new TheLoaiSanPham()
                        {
                            Id = Guid.NewGuid(),
                            IdChiTietSP = result.Id,
                            IdTheLoai = Guid.Parse(item),
                        };
                        await _theloaisprepo.AddOneAsyn(tlsp);
                    }
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Update không thành công");
                }


            }

        }
        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy sản phẩm chi tiết");
            }
            else
            {
                try
                {
                    await _repo.DeleteOneAsyn(result);
                    return Ok("Xóa thành công");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Xóa không thành công");
                }


            }
        }
    }
}
