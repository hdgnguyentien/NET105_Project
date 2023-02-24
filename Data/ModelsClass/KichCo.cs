using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class KichCo
    {
        public KichCo()
        {
            SizeSanPhams = new HashSet<SizeSanPham>();
            giohangChitiets = new HashSet<GiohangChitiet>();
            hoadonChitiets = new HashSet<HoadonChitiet>();
        }
        public Guid Id { get; set; }
        public float? Size { get; set; }
        public virtual ICollection<SizeSanPham> SizeSanPhams { get; set; }
        public virtual ICollection<GiohangChitiet> giohangChitiets { get; set; }
        public virtual ICollection<HoadonChitiet> hoadonChitiets { get; set; }
    }
}
