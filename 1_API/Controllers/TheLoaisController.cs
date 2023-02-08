using _1_API.ViewModel.MauSac;
using _1_API.ViewModel.TheLoai;
using Data.IRepositories;
using Data.ModelsClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheLoaisController : ControllerBase
    {
        private IAllRepositories<TheLoai> _repo;


        public TheLoaisController(IAllRepositories<TheLoai> repo)
        {
            _repo = repo;

        }

        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAllTheLoai()
        {
            var result = await _repo.GetAllAsync();
            if (result == null) return Ok("Không có thể loại");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetTheLoaiById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return Ok("Không tìm thấy thể loại");
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateTheLoai([FromForm] CreateTheLoai ctl)
        {
            TheLoai tl = new TheLoai()
            {
                Id = Guid.NewGuid(),
                TenTheLoai = ctl.TenTheLoai
            };
            try
            {
                var result = await _repo.AddOneAsyn(tl);
                return Ok(tl);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Tạo mới không thành công");
            }

        }

        [HttpPost]
        [Route("Update/id")]
        public async Task<IActionResult> UpdateTheLoai(Guid id, [FromForm] UpdateTheLoai utl)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy thể loại");
            }
            else
            {
                result.TenTheLoai = utl.TenTheLoai;
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy thể loại");
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
