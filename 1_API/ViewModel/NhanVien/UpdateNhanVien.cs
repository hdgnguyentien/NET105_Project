namespace _1_API.ViewModel.NhanVien
{
    public class UpdateNhanVien
    {
        public Guid? IdCvu { get; set; }
        public Guid? IdGuiBaoCao { get; set; }
        public string? Ten { get; set; }
        public string? Email { get; set; }
        public string? MatKhau { get; set; }
        public string? AnhNhanVien { get; set; }
        public bool? GioiTinh { get; set; }
        public string? DiaChi { get; set; }
        public DateTime? NgaySinh { get; set; }
    }
}
