using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace _1_API.ViewModel.HinhAnh
{
    public class ModelHinhAnh
    {
        public Guid? IdChiTietSP { get; set; }
        public string? LinkAnh { get; set; }
        [Required(ErrorMessage = "Vui lòng thêm ảnh sản phẩm")]
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile? ImageFile { get; set; }
    }
}
