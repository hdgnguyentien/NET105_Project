using _1_API.ViewModel.GioHang;
using _1_API.ViewModel.GioHangChiTiet;
using Data.IRepositories;
using Data.ModelsClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangsController : ControllerBase
    {
        private IAllRepositories<GioHang> _repo;

        public GioHangsController(IAllRepositories<GioHang> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAllGioHang()
        {
            var result = await _repo.GetAllAsync();
            if (result == null) return Ok("Không có giỏ hàng");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetGioHangById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return Ok("Không tìm thấy giỏ hàng");
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateGioHang([FromForm] GioHangModel cnv)
        {
            GioHang nv = new GioHang()
            {
                Id = Guid.NewGuid(),
                IdKH = cnv.IdKH,

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
        public async Task<IActionResult> UpdateGioHang(Guid id, [FromForm] GioHangModel unv)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy giỏ hàng");
            }
            else
            {
                result.IdKH = unv.IdKH;
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy giỏ hàng");
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
