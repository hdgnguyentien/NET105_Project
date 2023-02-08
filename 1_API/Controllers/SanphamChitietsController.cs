using _1_API.ViewModel.SanPham;
using _1_API.ViewModel.SanphamChitiet;
using Data.IRepositories;
using Data.ModelsClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanphamChitietsController : ControllerBase
    {
        private IAllRepositories<SanphamChitiet> _repo;


        public SanphamChitietsController(IAllRepositories<SanphamChitiet> repo)
        {
            _repo = repo;

        }

        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAllSanPhamCt()
        {
            var result = await _repo.GetAllAsync();
            if (result == null) return Ok("Không có sản phẩm chi tiết");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetSanPhamCtById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return Ok("Không tìm thấy sản phẩm chi tiết");
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateSanPhamCt([FromForm] CreateSanphamChitiet csp)
        {
            SanphamChitiet spct = new SanphamChitiet()
            {
                Id = Guid.NewGuid(),
                IdSP = csp.IdSP,
                IdKichCo = csp.IdKichCo,
                IdMauSac= csp.IdMauSac,
                SoLuong = csp.SoLuong,
                GiaBan = csp.GiaBan,
                GiaNhap= csp.GiaNhap,
                TrangThai = csp.TrangThai,
            };
            try
            {
                var result = await _repo.AddOneAsyn(spct);
                return Ok(spct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Tạo mới không thành công");
            }

        }

        [HttpPost]
        [Route("Update/id")]
        public async Task<IActionResult> UpdateSanPhamCt(Guid id, [FromForm] UpdateSanphamChitiet usp)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy sản phẩm chi tiết");
            }
            else
            {
                result.IdSP = usp.IdSP;
                result.IdKichCo = usp.IdKichCo;
                result.IdMauSac = usp.IdMauSac;
                result.SoLuong= usp.SoLuong;
                result.GiaBan = usp.GiaBan;
                result.GiaNhap = usp.GiaNhap;
                result.TrangThai = usp.TrangThai;
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy sản phẩm chi tiết");
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
