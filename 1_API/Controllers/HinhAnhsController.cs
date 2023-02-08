using _1_API.ViewModel.Hang;
using _1_API.ViewModel.HinhAnh;
using Data.IRepositories;
using Data.ModelsClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HinhAnhsController : ControllerBase
    {
        private IAllRepositories<HinhAnh> _repo;


        public HinhAnhsController(IAllRepositories<HinhAnh> repo)
        {
            _repo = repo;

        }

        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAllHinhAnh()
        {
            var result = await _repo.GetAllAsync();
            if (result == null) return Ok("Không có hình ảnh");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetHinhAnhById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return Ok("Không tìm thấy hình ảnh");
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateHinhAnh([FromForm] CreateHinhAnh cha)
        {
            HinhAnh ha = new HinhAnh()
            {
                Id = Guid.NewGuid(),
                IdChiTietSP = cha.IdChiTietSP,
                LinkAnh = cha.LinkAnh
            };
            try
            {
                var result = await _repo.AddOneAsyn(ha);
                return Ok(ha);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Tạo mới không thành công");
            }

        }

        [HttpPost]
        [Route("Update/id")]
        public async Task<IActionResult> UpdateHinhAnh(Guid id, [FromForm] UpdateHinhAnh uha)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy hình ảnh");
            }
            else
            {
                result.IdChiTietSP = uha.IdChiTietSP;
                result.LinkAnh = uha.LinkAnh;
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy hình ảnh");
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
