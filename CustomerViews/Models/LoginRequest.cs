using System.ComponentModel.DataAnnotations;
using CustomerViews.IServices;
using Data.ModelsClass;
using Microsoft.AspNetCore.Mvc;

namespace CustomerViews.Models
{
    public class LoginRequest : ValidationAttribute
    {
        private readonly IAllServices _services;
        public LoginRequest()
        {
        }
        public LoginRequest(IAllServices services)
        {
            _services = services;
        }

        [Required(ErrorMessage = "Vui lòng không để trống")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Mật khẩu ít nhất 6 ký tự")]
        //[CustomValidation(typeof(LoginRequest), "IsUserExists")]
        public string? MatKhau { get; set; }
        //public async Task<ValidationResult> IsUserExists(string? MatKhau)
        //{
        //    var lstKH = await _services.GetAll<KhachHang>(Connection.api + "KhachHangs/Get-All");
        //    var kh = lstKH.FirstOrDefault(p => p.Email == Email && p.MatKhau == MatKhau);
        //    if (kh != null)
        //    {
        //        return ValidationResult.Success;
        //    }
        //    return new ValidationResult("Sai tài khoản hoặc mật khẩu");

        //}
    }
}
