using System.ComponentModel.DataAnnotations;

namespace _1_API.ViewModel.SanphamChitiet
{
    public class ViewSanPhamChiTiet
    {
        public Guid Id { get; set; }     
        public int? SoLuong { get; set; } 
        public decimal? GiaBan { get; set; }
        public string? TrangThai { get; set; }
        public string? TenMauSac { get; set; }
        public string? MaSPChiTiet { get; set; }
        public string? TenSPChiTiet { get; set; }
        public float? TenKichCo { get; set; }
        public string? AnhDaiDien { get; set; }
    }
}
