namespace _1_API.ViewModel.SizeSanPham
{
    public class UpdateSizeSanPham
    {
        public Guid Id { get; set; }
        public Guid? IdSanPhamChiTiet { get; set; }
        public Guid? IdSize { get; set; }
        public int SoLuong { get; set; }
    }
}
