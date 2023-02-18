using System.ComponentModel.DataAnnotations;

namespace _1_API.ViewModel.TheLoai
{
    public class CreateTheLoai
    {
        [Required(ErrorMessage = "Vui lòng nhập thể loại")]
        public string? TenTheLoai { get; set; }
    }
}
