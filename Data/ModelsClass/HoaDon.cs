using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class HoaDon
    {
        public HoaDon()
        {
            hoadonChitiets = new HashSet<HoadonChitiet>();
        }

        public Guid Id { get; set; }
        public Guid? IdMaGiamGia { get; set; }
        public Guid? IdKH { get; set; }
        public Guid? IdNV { get; set; }
        public DateTime? NgayTao { get; set; }
        public int? TrangThai { get; set; }
        public decimal? TongTien { get; set; }
        public string? DiaChi { get; set; }
        public string? MaHD { get; set; }

        public MaGiamGia? maGiamGia { get; set; }
        public KhachHang? khachHang { get; set; }
        public NhanVien? nhanVien { get; set; }
        public virtual ICollection<HoadonChitiet> hoadonChitiets { get; set; }
    }
}
