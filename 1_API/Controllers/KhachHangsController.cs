using _1_API.ViewModel.KhachHang;
using _1_API.ViewModel.NhanVien;
using Data.IRepositories;
using Data.ModelsClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangsController : ControllerBase
    {
        private IAllRepositories<KhachHang> _repo;

        public KhachHangsController(IAllRepositories<KhachHang> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAllNhanVien()
        {
            var result = await _repo.GetAllAsync();
            if (result == null) return Ok("Không có khách hàng");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetNhanVienById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return Ok("Không tìm thấy khách hàng");
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateNhanVien([FromForm] CreateKhachHang cnv)
        {
            KhachHang nv = new KhachHang()
            {
                Id = Guid.NewGuid(),
                Ten = cnv.Ten,
                Email = cnv.Email,
                MatKhau = cnv.MatKhau,
                GioiTinh = cnv.GioiTinh,
                DiaChi = cnv.DiaChi,
                NgaySinh = cnv.NgaySinh,
                Sdt = cnv.Sdt,
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
        public async Task<IActionResult> UpdateNhanVien(Guid id, [FromForm] UpdateKhachHang unv)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy khách hàng");
            }
            else
            {
                result.Ten = unv.Ten;
                result.Email = unv.Email;
                result.MatKhau = unv.MatKhau;
                result.GioiTinh = unv.GioiTinh;
                result.DiaChi = unv.DiaChi;
                result.NgaySinh = unv.NgaySinh;
                result.Sdt = unv.Sdt;
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy khách hàng");
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
