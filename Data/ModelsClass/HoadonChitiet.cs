using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class HoadonChitiet
    {
        public Guid Id { get; set; }
        public Guid? IdSPChitiet { get; set; }
        public Guid? IdHoaDon { get; set; }
        public Guid? IdKichCo { get; set; }
        public int? SoLuong { get; set; }
        public decimal? GiaBan { get; set; }
        public SanphamChitiet? sanphamChitiet { get; set; }
        public HoaDon? hoaDon { get; set; }
        public KichCo? kichCo { get; set; }
    }
}
