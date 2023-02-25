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
    public class HoadonChitietConfiguration : IEntityTypeConfiguration<HoadonChitiet>
    {
        public void Configure(EntityTypeBuilder<HoadonChitiet> builder)
        {
            builder.ToTable("HoadonChitiet");
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.SoLuong).IsRequired();
            builder.Property(x=>x.GiaBan).IsRequired();
            builder.Property(x=>x.IdSPChitiet).IsRequired();
            builder.Property(x=>x.IdHoaDon).IsRequired();

            builder.HasOne(x => x.hoaDon).WithMany(x => x.hoadonChitiets).HasForeignKey(x => x.IdHoaDon);
            builder.HasOne(x => x.sanphamChitiet).WithMany(x => x.hoadonChitiets).HasForeignKey(x => x.IdSPChitiet);
            builder.HasOne(x => x.kichCo).WithMany(x => x.hoadonChitiets).HasForeignKey(x => x.IdKichCo);
        }
    }
}
