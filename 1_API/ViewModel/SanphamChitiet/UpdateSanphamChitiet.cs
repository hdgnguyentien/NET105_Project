namespace _1_API.ViewModel.SanphamChitiet
{
    public class UpdateSanphamChitiet
    {
        public Guid? IdSP { get; set; }
        public Guid? IdMauSac { get; set; }
        public Guid? IdKichCo { get; set; }

        public int? SoLuong { get; set; }
        public decimal? GiaNhap { get; set; }
        public decimal? GiaBan { get; set; }
        public int? TrangThai { get; set; }
    }
}
