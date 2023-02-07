using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class ChucVu
    {
        public ChucVu()
        {
            nhanViens = new HashSet<NhanVien>();
        }
        public Guid Id { get; set; }
        public string? Ten { get; set; }
        public virtual ICollection<NhanVien> nhanViens { get; set; }
    }
}
