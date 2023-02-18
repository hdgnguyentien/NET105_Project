using System.ComponentModel.DataAnnotations;

namespace _1_API.ViewModel.KichCo
{
    public class UpdateKichCo
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Size")]
        public float? Size { get; set; }
    }
}
