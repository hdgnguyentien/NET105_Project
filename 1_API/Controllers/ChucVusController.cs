using _1_API.ViewModel.ChucVu;
using Data.DbContexts;
using Data.IRepositories;
using Data.ModelsClass;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChucVusController : ControllerBase
    {
        private IAllRepositories<ChucVu> _repo;
        

        public ChucVusController(IAllRepositories<ChucVu> repo)
        {
            _repo = repo;
            
        }

        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAllChucVu()
        {
            var result = await _repo.GetAllAsync();
            if (result == null) return Ok("Không có chức vụ");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetChucVuById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if(result == null) return Ok("Không tìm thấy chức vụ");
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateChucVu([FromForm]CreateChucVu ccv)
        {
            ChucVu cv = new ChucVu()
            {
                Id = Guid.NewGuid(),
                Ten = ccv.ten
            };
            try
            {
                var result = await _repo.AddOneAsyn(cv);
                return Ok(cv);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Tạo mới không thành công");
            }
                
        }

        [HttpPost]
        [Route("Update/id")]
        public async Task<IActionResult> UpdateChucVu(Guid id,[FromForm] UpdateChucVu ucv)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null )
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy chức vụ");
            }
            else {
                result.Ten = ucv.ten;
                try
                {
                    await _repo.UpdateOneAsyn(result);
                    return Ok(result);
                }
                catch(Exception ex)
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy chức vụ");
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
