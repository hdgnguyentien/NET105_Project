namespace _1_API.ViewModel.GioHangChiTiet
{
    public class CreateGioHangChiTiet
    {
        public Guid IdSPChitiet { get; set; }
        public Guid? IdGioHang { get; set; }
        public Guid? IdKichCo { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
    }
}
