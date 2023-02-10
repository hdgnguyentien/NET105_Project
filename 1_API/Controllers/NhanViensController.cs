using _1_API.ViewModel.ChucVu;
using _1_API.ViewModel.NhanVien;
using Data.IRepositories;
using Data.ModelsClass;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanViensController : ControllerBase
    {
        private IAllRepositories<NhanVien> _repo;

        public NhanViensController(IAllRepositories<NhanVien> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAllNhanVien()
        {
            var result = await _repo.GetAllAsync();
            if (result == null) return Ok("Không có nhân viên");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetNhanVienById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return Ok("Không tìm thấy nhân viên");
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateNhanVien(CreateNhanVien cnv)
        {
            NhanVien nv = new NhanVien()
            {
                Id = Guid.NewGuid(),
                Ten = cnv.Ten,
                IdCvu= cnv.IdCvu,
                IdGuiBaoCao  = cnv.IdGuiBaoCao,
                Email = cnv.Email,
                MatKhau = cnv.MatKhau,
                AnhNhanVien = cnv.AnhNhanVien, 
                GioiTinh = cnv.GioiTinh, 
                DiaChi = cnv.DiaChi,
                NgaySinh = cnv.NgaySinh,
                
            };
            try
            {
                var result = await _repo.AddOneAsyn(nv);
                return Ok(nv);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Tạo mới không thành công");
            }

        }

        [HttpPost]
        [Route("Update/id")]
        public async Task<IActionResult> UpdateNhanVien(Guid id, [FromForm] UpdateNhanVien unv)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy nhân viên");
            }
            else
            {
                result.Ten = unv.Ten;
                result.IdCvu = unv.IdCvu;
                result.IdGuiBaoCao = unv.IdGuiBaoCao;
                result.Email = unv.Email;
                result.MatKhau = unv.MatKhau;
                result.AnhNhanVien = unv.AnhNhanVien;
                result.GioiTinh = unv.GioiTinh;
                result.DiaChi = unv.DiaChi;
                result.NgaySinh = unv.NgaySinh;
                try
                {
                    await _repo.UpdateOneAsyn(result);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy nhân viên");
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
