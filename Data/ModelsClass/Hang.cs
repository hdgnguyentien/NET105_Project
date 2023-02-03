using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class Hang
    {
        public Guid Id { get; set; }
        public string TenHang { get; set; }
        public List<SanPham> sanPhams { get; set; }
    }
}
