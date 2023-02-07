using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class TheLoaiSanPham
    {
        public Guid Id { get; set; }
        public Guid IdTheLoai { get; set; }
        public Guid IdChiTietSP { get; set; }
        public TheLoai? theLoai { get; set; }
        public SanphamChitiet? sanphamChitiet { get; set; }
    }
}
