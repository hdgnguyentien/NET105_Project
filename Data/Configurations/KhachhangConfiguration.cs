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
    public class KhachhangConfiguration : IEntityTypeConfiguration<KhachHang>
    {
        public void Configure(EntityTypeBuilder<KhachHang> builder)
        {
            builder.ToTable("KhachHang");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Ten);
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.MatKhau).IsRequired();
            builder.Property(x => x.DiaChi);
            builder.Property(x => x.GioiTinh).HasColumnType("bit");
            builder.Property(x => x.Sdt).IsRequired();
        }
    }
}
