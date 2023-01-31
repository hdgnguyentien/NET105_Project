using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class ChucVu
    {
        public Guid Id { get; set; }
        public string Ten { get; set; }
        public List<NhanVien> nhanViens { get; set; }
    }
}
