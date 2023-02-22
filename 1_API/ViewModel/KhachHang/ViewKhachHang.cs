namespace _1_API.ViewModel.KhachHang
{
    public class ViewKhachHang
    {
        public Guid Id { get; set; }
        public string? Ten { get; set; }

        public string? Email { get; set; }

        public string? MatKhau { get; set; }

        public string? DiaChi { get; set; }
        public bool? GioiTinh { get; set; }
        public string? Sdt { get; set; }
        public DateTime? NgaySinh { get; set; }
    }
}
