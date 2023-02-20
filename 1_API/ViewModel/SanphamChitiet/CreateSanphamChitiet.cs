using System.ComponentModel.DataAnnotations;

namespace _1_API.ViewModel.SanphamChitiet
{
    public class CreateSanphamChitiet
    {
        public Guid? IdSP { get; set; }
        public Guid? IdMauSac { get; set; }
        public Guid? IdKichCo { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]

        public int SoLuong { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Giá nhập")]

        public decimal? GiaNhap { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Giá bán")]

        public decimal GiaBan { get; set; }
        public int? TrangThai { get; set; }
    }
}
