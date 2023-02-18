using System.ComponentModel.DataAnnotations;

namespace _1_API.ViewModel.SanPham
{
    public class CreateSanPham
    {
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string? Ten { get; set; }
        public Guid? IdHang { get; set; }
        public int? TrangThai { get; set; }
    }
}
