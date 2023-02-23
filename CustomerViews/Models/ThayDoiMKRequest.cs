using System.ComponentModel.DataAnnotations;

namespace CustomerViews.Models
{
    public class ThayDoiMKRequest
{
        [Required(ErrorMessage = "Vui lòng không để trống Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống Sdt")]
        public string Sdt { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu cũ")]
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu mới")]
        public string MatKhauMoi { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập lại Mật khẩu mới")]
        public string NhapLaiMkm { get; set; }

    }
}
