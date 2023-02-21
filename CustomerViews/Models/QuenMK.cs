using System.ComponentModel.DataAnnotations;

namespace CustomerViews.Models
{
    public class QuenMK
    {
        [Required(ErrorMessage = "Vui lòng không để trống số điện thoại")]

        public string SDT { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống Email")]

        public string Email { get; set; }
    }
}
