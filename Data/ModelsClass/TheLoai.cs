using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class TheLoai
    {
        public TheLoai()
        {
            theloaiSanPhams = new HashSet<TheLoaiSanPham>();
        }

        public Guid Id { get; set; }
        public string? TenTheLoai { get; set; }
        public virtual ICollection<TheLoaiSanPham> theloaiSanPhams { get; set; }
    }
}
