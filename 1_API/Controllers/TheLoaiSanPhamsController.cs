using _1_API.ViewModel.TheLoai;
using _1_API.ViewModel.TheloaiSanpham;
using Data.IRepositories;
using Data.ModelsClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheLoaiSanPhamsController : ControllerBase
    {
        private IAllRepositories<TheLoaiSanPham> _repo;


        public TheLoaiSanPhamsController(IAllRepositories<TheLoaiSanPham> repo)
        {
            _repo = repo;

        }

        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAllTheLoaiSanPham()
        {
            var result = await _repo.GetAllAsync();
            if (result == null) return Ok("Không có thể loại sản phẩm");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetTheLoaiSanPhamById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return Ok("Không tìm thấy thể loại sản phẩm");
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateTheLoaiSanPham([FromForm] CreateTheLoaiSP ctl)
        {
            TheLoaiSanPham tlsp = new TheLoaiSanPham()
            {
                Id = Guid.NewGuid(),
                IdTheLoai = ctl.IdTheLoai,
                IdChiTietSP = ctl.IdChiTietSP,
            };
            try
            {
                var result = await _repo.AddOneAsyn(tlsp);
                return Ok(tlsp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Tạo mới không thành công");
            }

        }

        [HttpPost]
        [Route("Update/id")]
        public async Task<IActionResult> UpdateTheLoaiSanPham(Guid id, [FromForm] UpdateTheLoaiSP utl)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy thể loại sản phẩm");
            }
            else
            {
                result.IdChiTietSP = utl.IdChiTietSP;
                result.IdTheLoai = utl.IdTheLoai;
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy thể loại sản phẩm");
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
