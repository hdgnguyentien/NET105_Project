using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1_API.ViewModel.SanphamChitiet
{
    public class UpdateSanphamChitiet
    {
        public UpdateSanphamChitiet()
        {
            Selected = new List<string>();
            TheLoai = new List<SelectListItem>();
        }
        public Guid IdSPCT { get; set; }
        public Guid? IdSP { get; set; }
        public Guid? IdMauSac { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên chi tiết")]
        public string? TenSPChiTiet { get; set; }
        public string? AnhDaiDien { get; set; }
        public string? AnhPhu1 { get; set; }
        public string? AnhPhu3 { get; set; }
        public string? AnhPhu2 { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá nhập")]
        public decimal? GiaNhap { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá bán")]
        public decimal GiaBan { get; set; }
        public int? TrangThai { get; set; }
        public IList<SelectListItem> TheLoai { get; set; }
        public IList<string> Selected { get; set; }


        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile? ImageFile { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile? ImageFile1 { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile? ImageFile2 { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile? ImageFile3 { get; set; }
    }
}
