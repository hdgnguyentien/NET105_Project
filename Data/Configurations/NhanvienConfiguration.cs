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
    public class NhanvienConfiguration : IEntityTypeConfiguration<NhanVien>
    {
        public void Configure(EntityTypeBuilder<NhanVien> builder)
        {
            builder.ToTable("NhanVien");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Ten);
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.MatKhau).IsRequired();
            builder.Property(x => x.DiaChi);
            builder.Property(x => x.GioiTinh).HasColumnType("bit");

            builder.HasOne(x => x.chucVu).WithMany(x => x.nhanViens).HasForeignKey(x => x.IdCvu);
        }
    }
}
