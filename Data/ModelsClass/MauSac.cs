using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class MauSac
    {
        public MauSac()
        {
            sanphamChitiets = new HashSet<SanphamChitiet>();
        }
        public Guid Id { get; set; }
        public string? TenMau { get; set; }
        public virtual ICollection<SanphamChitiet> sanphamChitiets { get; set; }
    }
}
