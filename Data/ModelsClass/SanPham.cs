using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class SanPham
    {
        public SanPham()
        {
            sanphamChitiets = new HashSet<SanphamChitiet>();
        }

        public Guid Id { get; set; }
        public string? Ten { get; set; }
        public Guid? IdHang { get; set; }
        public int? TrangThai { get; set; }
        public Hang? hang { get; set; }
        public virtual ICollection<SanphamChitiet> sanphamChitiets { get; set; }


    }
}
