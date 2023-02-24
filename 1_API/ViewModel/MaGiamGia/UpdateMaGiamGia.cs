using System.ComponentModel.DataAnnotations;

namespace _1_API.ViewModel.MaGiamGia
{
    public class UpdateMaGiamGia
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mã giảm giá")]
        public string? Ma { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày bắt đầu")]
        public DateTime? NgayBatdau { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày kết thúc")]
        public DateTime? NgayKetthuc { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        public int? SoLuong { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập trạng thái")]
        public int? TrangThai { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập phần trăm ")]
        public int PhanTramGiam { get; set; }
    }
}
