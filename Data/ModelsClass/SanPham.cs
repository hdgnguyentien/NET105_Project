using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class SanPham
    {
        public Guid Id { get; set; }
        public string Ten { get; set; }
        public Guid IdHang { get; set; }
        public bool TrangThai { get; set; }
        public Hang hang { get; set; }
        public List<SanphamChitiet> sanphamChitiets { get; set; }
        public List<TheLoaiSanPham> theloaiSanPhams { get; set; }

    }
}
