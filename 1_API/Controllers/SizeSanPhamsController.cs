using _1_API.ViewModel.Hang;
using _1_API.ViewModel.SizeSanPham;
using Data.IRepositories;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;

namespace _1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeSanPhamsController : ControllerBase
    {
        private IAllRepositories<SizeSanPham> _repo;


        public SizeSanPhamsController(IAllRepositories<SizeSanPham> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAllSizeSanPham()
        {
            var result = await _repo.GetAllAsync();
            if (result == null) return Ok("Không có Size sp nào");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetSizeSanPhamById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return Ok("Không tìm thấy Size sp nào");
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateSizeSanPham(CreateSizeSanPham ch)
        {
            SizeSanPham h = new SizeSanPham()
            {
                Id = Guid.NewGuid(),
                IdSize = ch.IdSize,
                IdSanPhamChiTiet = ch.IdSanPhamChiTiet,
                SoLuong = ch.SoLuong,
            };
            try
            {
                var result = await _repo.AddOneAsyn(h);
                return Ok(h);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Tạo mới không thành công");
            }

        }

        [HttpPost]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateSizeSanPham(Guid id, UpdateSizeSanPham uh)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy Size sp nào");
            }
            else
            {
                result.IdSize = uh.IdSize;
                result.IdSanPhamChiTiet = uh.IdSanPhamChiTiet;
                result.SoLuong = uh.SoLuong;
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy Size sp nào");
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
