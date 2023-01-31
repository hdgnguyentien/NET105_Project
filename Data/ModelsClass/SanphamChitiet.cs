using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class SanphamChitiet
    {
        public Guid Id { get; set; }
        public Guid IdSP { get; set; }
        public Guid IdMauSac { get; set; }
        public Guid IdKichCo { get; set; }

        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal GiaBan { get; set; }
        public bool TrangThai { get; set; }

            
        public SanPham sanPham { get; set; }
        public MauSac mauSac { get; set; }
        public KichCo kichCo { get; set; }

        public List<GiohangChitiet> giohangChitiets { get; set; }
        public List<HoadonChitiet> hoadonChitiets { get; set; }
    }
}
