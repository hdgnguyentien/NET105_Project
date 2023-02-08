using _1_API.ViewModel.GioHangChiTiet;
using _1_API.ViewModel.KhachHang;
using Data.IRepositories;
using Data.ModelsClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangChiTietsController : ControllerBase
    {
        private IAllRepositories<GiohangChitiet> _repo;

        public GioHangChiTietsController(IAllRepositories<GiohangChitiet> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAllGioHang()
        {
            var result = await _repo.GetAllAsync();
            if (result == null) return Ok("Không có giỏ hàng chi tiết");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetGioHangById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return Ok("Không tìm thấy giỏ hàng chi tiết");
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateGioHang([FromForm] CreateGioHangChiTiet cnv)
        {
            GiohangChitiet nv = new GiohangChitiet()
            {
                Id = Guid.NewGuid(),
                IdSPChitiet = cnv.IdSPChitiet,
                IdGioHang = cnv.IdGioHang,
                SoLuong = cnv.SoLuong,
                GiaBan = cnv.GiaBan,
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
        public async Task<IActionResult> UpdateGioHang(Guid id, [FromForm] UpdateGiohangChiTiet unv)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy giỏ hàng chi tiết");
            }
            else
            {
                result.IdSPChitiet = unv.IdSPChitiet;
                result.IdGioHang = unv.IdGioHang;
                result.SoLuong = unv.SoLuong;
                result.GiaBan = unv.GiaBan;
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
        public async Task<IActionResult> DeleteGioHang(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy giỏ hàng chi tiết");
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
