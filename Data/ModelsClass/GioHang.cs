using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class GioHang
    {
        public GioHang()
        {
            giohangChitiets = new HashSet<GiohangChitiet>();
        }

        public Guid Id { get; set; }
        public Guid? IdKH { get; set; }
        public KhachHang? KhachHang { get; set; }
        public virtual ICollection<GiohangChitiet> giohangChitiets { get; set; }
    }
}
