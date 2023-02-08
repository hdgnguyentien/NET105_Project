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
            sanphamChitiets = new HashSet<SanphamChitiet>();
        }
        public Guid Id { get; set; }
        public float? Size { get; set; }
        public virtual ICollection<SanphamChitiet> sanphamChitiets { get; set; }
    }
}
