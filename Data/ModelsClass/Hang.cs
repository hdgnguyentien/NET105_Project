using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class Hang
    {
        public Hang()
        {
            sanPhams = new HashSet<SanPham>();
        }

        public Guid Id { get; set; }
        public string? TenHang { get; set; }
        public virtual ICollection<SanPham> sanPhams { get; set; }

    }
}
