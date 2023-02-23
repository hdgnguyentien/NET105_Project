using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class SizeSanPham
    {
        public Guid Id { get; set; }
        public Guid? IdSanPhamChiTiet { get; set; }
        public Guid? IdSize { get; set; }
        public int SoLuong { get; set; }
        public KichCo? KichCo { get; set; }
        public SanphamChitiet? SanPhamChitiet { get; set; }

    }
}
