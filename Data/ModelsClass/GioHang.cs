using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class GioHang
    {
        public Guid Id { get; set; }
        public Guid IdKH { get; set; }
        public KhachHang KhachHang { get; set; }
        public List<GiohangChitiet> giohangChitiets { get; set; }
    }
}
