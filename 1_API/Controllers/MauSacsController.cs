using _1_API.ViewModel.ChucVu;
using _1_API.ViewModel.MauSac;
using Data.IRepositories;
using Data.ModelsClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MauSacsController : ControllerBase
    {
        private IAllRepositories<MauSac> _repo;


        public MauSacsController(IAllRepositories<MauSac> repo)
        {
            _repo = repo;

        }

        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAllMauSac()
        {
            var result = await _repo.GetAllAsync();
            if (result == null) return Ok("Không có màu sắc");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetMauSacById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return Ok("Không tìm thấy màu sắc");
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateMauSac( CreateMauSac cms)
        {
            MauSac ms = new MauSac()
            {
                Id = Guid.NewGuid(),
                TenMau = cms.Ten
            };
            try
            {
                var result = await _repo.AddOneAsyn(ms);
                return Ok(ms);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Tạo mới không thành công");
            }

        }

        [HttpPost]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateMauSac(Guid id, UpdateMauSac ums)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy màu sắc");
            }
            else
            {
                result.TenMau = ums.Ten;
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy màu sắc");
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
