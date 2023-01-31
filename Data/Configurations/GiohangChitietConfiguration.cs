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
    public class GiohangChitietConfiguration : IEntityTypeConfiguration<GiohangChitiet>
    {
        public void Configure(EntityTypeBuilder<GiohangChitiet> builder)
        {
            builder.ToTable("GiohangChitiet");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SoLuong).HasColumnType("int").IsRequired();
            builder.Property(x => x.GiaBan).HasColumnType("decimal").IsRequired();
            builder.HasOne(x => x.sanphamChitiet).WithMany(x => x.giohangChitiets).HasForeignKey(x => x.IdSPChitiet);
            builder.HasOne(x => x.gioHang).WithMany(x => x.giohangChitiets).HasForeignKey(x => x.IdGioHang);
        }
    }
}
