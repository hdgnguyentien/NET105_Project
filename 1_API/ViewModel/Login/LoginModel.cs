using System.ComponentModel.DataAnnotations;

namespace _1_API.ViewModel.Login
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Vui lòng nhập Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }
    }
}
