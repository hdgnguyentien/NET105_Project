using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class MaGiamGia
    {
        public Guid Id { get; set; }
        public int Ma { get; set; }
        public DateTime NgayBatdau { get; set; }
        public DateTime NgayKetthuc { get; set; }
        public int SoLuong { get; set; }
        public bool TrangThai { get; set; }
        public List<HoaDon> hoaDons { get; set; }
    }
}
