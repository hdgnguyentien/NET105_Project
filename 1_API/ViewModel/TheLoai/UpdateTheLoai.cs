using System.ComponentModel.DataAnnotations;

namespace _1_API.ViewModel.TheLoai
{
    public class UpdateTheLoai
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thể loại")]
        public string? TenTheLoai { get; set; }
    }
}
