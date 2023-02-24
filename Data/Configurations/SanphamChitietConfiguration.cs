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
            builder.Property(x=>x.IdSP).IsRequired();
            builder.Property(x=>x.IdMauSac).IsRequired();
            builder.Property(x=>x.GiaNhap).IsRequired();
            builder.Property(x=>x.GiaBan).IsRequired();
            builder.Property(x=>x.TrangThai).IsRequired();
            builder.Property(x=>x.TenSPChiTiet).IsRequired();
            builder.Property(x=>x.MaSPChiTiet).IsRequired();
            builder.Property(x=>x.AnhDaiDien).IsRequired();
            builder.Property(x=>x.AnhPhu1).IsRequired();
            builder.Property(x=>x.AnhPhu2).IsRequired();
            builder.Property(x=>x.AnhPhu3).IsRequired();
            builder.Property(x=>x.NgayTao).IsRequired();

            builder.HasOne(x => x.sanPham).WithMany(x => x.sanphamChitiets).HasForeignKey(x => x.IdSP);
            builder.HasOne(x => x.mauSac).WithMany(x => x.sanphamChitiets).HasForeignKey(x => x.IdMauSac);
            //builder.HasOne(x=>x.)

        }
    }
}
