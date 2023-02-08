using Data.ModelsClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class MagiamgiaConfiguration : IEntityTypeConfiguration<MaGiamGia>
    {
        public void Configure(EntityTypeBuilder<MaGiamGia> builder)
        {
            builder.ToTable("MaGiamGia");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Ma).IsRequired();
            builder.Property(x=>x.SoLuong).IsRequired();
            builder.Property(x=>x.NgayBatdau).IsRequired();
            builder.Property(x=>x.NgayKetthuc).IsRequired();
            builder.Property(x=>x.TrangThai).IsRequired();
            builder.Property(x=>x.PhanTramGiam).IsRequired();
        }
    }
}
