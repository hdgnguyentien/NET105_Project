using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class NhanVien
    {
        public Guid Id { get; set; }
        public Guid IdCvu { get; set; }
        public string Ten { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public bool GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public List<HoaDon> hoaDons { get; set; }
        public ChucVu chucVu { get; set; }
    }
}
