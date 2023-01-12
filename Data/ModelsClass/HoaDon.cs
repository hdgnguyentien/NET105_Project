using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsClass
{
    public class HoaDon
    {
        public Guid Id { get; set; }
        public Guid IdMaGiamGia { get; set; }
        public Guid IdKH { get; set; }
        public Guid IdNV { get; set; }
        public DateTime NgayTao { get; set; }
        public bool TrangThai { get; set; }
        public decimal TongTien { get; set; }
        public string DiaChi { get; set; }
    }
}
