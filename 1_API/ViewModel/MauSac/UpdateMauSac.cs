using System.ComponentModel.DataAnnotations;

namespace _1_API.ViewModel.MauSac
{
    public class UpdateMauSac
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập màu sắc")]
        public string? Ten { get; set; }
    }
}
