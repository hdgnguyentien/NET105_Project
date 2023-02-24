namespace _1_API.ViewModel.HoaDonChiTiet
{
    public class HoaDonChiTietView
    {
        public Guid? Idhd { get; set; }
        public string? TenKhachHang { get; set; }
        public string? SDT { get; set; }
        public string? DiaChi { get; set; }
        public decimal? TongTien { get; set; }
        public List<ViewHoaDonChiTiet> viewHoaDonChiTiets { get; set; }
    }
}
