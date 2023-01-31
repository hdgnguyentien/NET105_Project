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
    public class SanphamChitietConfiguration : IEntityTypeConfiguration<SanphamChitiet>
    {
        public void Configure(EntityTypeBuilder<SanphamChitiet> builder)
        {
            builder.ToTable("SanphamChitiet");
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.SoLuong).IsRequired().HasColumnType("int");
            builder.Property(x=>x.GiaNhap).IsRequired().HasColumnType("decimal");
            builder.Property(x=>x.GiaBan).IsRequired().HasColumnType("decimal");
            builder.Property(x=>x.TrangThai).IsRequired().HasColumnType("bit");

            builder.HasOne(x => x.sanPham).WithMany(x => x.sanphamChitiets).HasForeignKey(x => x.IdSP);
            builder.HasOne(x => x.mauSac).WithMany(x => x.sanphamChitiets).HasForeignKey(x => x.IdMauSac);
            builder.HasOne(x => x.kichCo).WithMany(x => x.sanphamChitiets).HasForeignKey(x => x.IdKichCo);

        }
    }
}
