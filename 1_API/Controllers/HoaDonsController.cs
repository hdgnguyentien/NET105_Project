using _1_API.ViewModel.HoaDon;
using _1_API.ViewModel.KhachHang;
using Data.IRepositories;
using Data.ModelsClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonsController : ControllerBase
    {
        private IAllRepositories<HoaDon> _repo;

        public HoaDonsController(IAllRepositories<HoaDon> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAllHoaDon()
        {
            var result = await _repo.GetAllAsync();
            if (result == null) return Ok("Không có hóa đơn");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetHoaDonById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return Ok("Không tìm thấy hóa đơn");
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateHoaDon([FromForm] CreateHoaDon cnv)
        {
            HoaDon nv = new HoaDon()
            {
                Id = Guid.NewGuid(),
                IdKH = cnv.IdKH,
                IdMaGiamGia = cnv.IdMaGiamGia,
                IdNV = cnv.IdNV,
                DiaChi = cnv.DiaChi,
                TrangThai = cnv.TrangThai,
                TongTien = cnv.TongTien,
                NgayTao = cnv.NgayTao,
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
        public async Task<IActionResult> UpdateHoaDon(Guid id, [FromForm] UpdateHoaDon unv)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy hóa đơn");
            }
            else
            {
                result.IdMaGiamGia = unv.IdMaGiamGia;
                result.IdNV = unv.IdNV;
                result.IdKH = unv.IdKH;
                result.NgayTao = unv.NgayTao;
                result.DiaChi = unv.DiaChi;
                result.TongTien = unv.TongTien;
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
        public async Task<IActionResult> DeleteHoaDon(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Không tìm thấy hóa đơn");
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
