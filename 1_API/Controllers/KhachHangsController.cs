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
        public async Task<IActionResult> GetAllKhachHang()
        {
            var result = await _repo.GetAllAsync();
            if (result == null) return Ok("Không có khách hàng");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetKhachHangById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return Ok("Không tìm thấy khách hàng");
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateKhachHang([FromForm] CreateKhachHang ckh)
        {
            KhachHang kh = new KhachHang()
            {
                Id = Guid.NewGuid(),
                Ten = ckh.Ten,
                Email = ckh.Email,
                MatKhau = ckh.MatKhau,
                GioiTinh = ckh.GioiTinh,
                DiaChi = ckh.DiaChi,
                NgaySinh = ckh.NgaySinh,
                Sdt = ckh.Sdt,
            };
            try
            {
                var result = await _repo.AddOneAsyn(kh);
                return Ok(kh);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Tạo mới không thành công");
            }

        }

        [HttpPost]
        [Route("Update/id")]
        public async Task<IActionResult> UpdateKhachHang(Guid id, [FromForm] UpdateKhachHang ukh)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy khách hàng");
            }
            else
            {
                result.Ten = ukh.Ten;
                result.Email = ukh.Email;
                result.MatKhau = ukh.MatKhau;
                result.GioiTinh = ukh.GioiTinh;
                result.DiaChi = ukh.DiaChi;
                result.NgaySinh = ukh.NgaySinh;
                result.Sdt = ukh.Sdt;
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
