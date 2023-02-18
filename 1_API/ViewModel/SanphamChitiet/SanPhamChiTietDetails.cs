namespace _1_API.ViewModel.SanphamChitiet
{
    public class SanPhamChiTietDetails
    {
        public Guid Id { get; set; }
        public Guid? IdSP { get; set; }
        public Guid? IdMauSac { get; set; }
        public Guid? IdKichCo { get; set; }
        public int? SoLuong { get; set; }
        public decimal? GiaNhap { get; set; }
        public decimal? GiaBan { get; set; }
        public int? TrangThai { get; set; }
        //public string? TenSP { get; set; }
        //public string? TenMauSac { get; set; }
        //public float? TenKichCo { get; set; }
    }
}
