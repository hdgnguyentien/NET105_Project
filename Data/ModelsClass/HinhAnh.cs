using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class HinhAnh
    {
        public Guid Id { get; set; }
        public Guid? IdChiTietSP { get; set; }
        public string? LinkAnh { get; set; }
        public SanphamChitiet? sanphamChitiet { get; set; }
    }
}
