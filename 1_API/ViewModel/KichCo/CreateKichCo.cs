using System.ComponentModel.DataAnnotations;

namespace _1_API.ViewModel.KichCo
{
    public class CreateKichCo
    {
        [Required(ErrorMessage = "Vui lòng nhập Size")]
        public float? Size { get; set; }
    }
}
