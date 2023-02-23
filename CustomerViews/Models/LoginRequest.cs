using System.ComponentModel.DataAnnotations;
using CustomerViews.IServices;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;

namespace CustomerViews.Models
{
    public class LoginRequest : ValidationAttribute
    {
        [Required(ErrorMessage = "Vui lòng không để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Mật khẩu ít nhất 6 ký tự")]
        //[CustomValidation(typeof(LoginRequest), "IsUserExists")]
        public string? MatKhau { get; set; }

    }
}
