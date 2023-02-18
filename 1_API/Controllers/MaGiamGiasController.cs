using _1_API.ViewModel.KhachHang;
using _1_API.ViewModel.MaGiamGia;
using Data.IRepositories;
using Data.ModelsClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaGiamGiasController : ControllerBase
    {
        private IAllRepositories<MaGiamGia> _repo;

        public MaGiamGiasController(IAllRepositories<MaGiamGia> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAllMaGiamGia()
        {
            var result = await _repo.GetAllAsync();
            if (result == null) return Ok("Không có mã giảm giá");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetMaGiamGiaById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return Ok("Không tìm thấy mã giảm giá");
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateMaGiamGia(CreateMaGiamGia cnv)
        {
            MaGiamGia nv = new MaGiamGia()
            {
                Id = Guid.NewGuid(),
                Ma = cnv.Ma,
                NgayBatdau = cnv.NgayBatdau,
                NgayKetthuc = cnv.NgayKetthuc,
                SoLuong = cnv.SoLuong,
                PhanTramGiam = cnv.PhanTramGiam,
                TrangThai = cnv.TrangThai,
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
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateMaGiamGia(Guid id,UpdateMaGiamGia unv)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy mã giảm giá");
            }
            else
            {
                result.Ma = unv.Ma;
                result.NgayKetthuc = unv.NgayKetthuc;
                result.NgayBatdau = unv.NgayBatdau;
                result.SoLuong = unv.SoLuong;
                result.PhanTramGiam = unv.PhanTramGiam;
                result.TrangThai = unv.TrangThai;
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
        public async Task<IActionResult> DeleteMaGiamGia(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy mã giảm giá");
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
