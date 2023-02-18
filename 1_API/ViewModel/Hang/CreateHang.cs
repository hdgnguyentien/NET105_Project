using System.ComponentModel.DataAnnotations;

namespace _1_API.ViewModel.Hang
{
    public class CreateHang
    {
        
        [Required(ErrorMessage = "Vui lòng nhập Tên Nhà sản xuất")]
        public string? TenHang { get; set; }
    }
}
