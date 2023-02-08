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
    public class HinhAnhConfiguration : IEntityTypeConfiguration<HinhAnh>
    {
        public void Configure(EntityTypeBuilder<HinhAnh> builder)
        {
            builder.ToTable("HinhAnh");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IdChiTietSP).IsRequired();
            builder.Property(x => x.LinkAnh).IsRequired();
            builder.HasOne(x => x.sanphamChitiet).WithMany(x => x.hinhAnhs).HasForeignKey(x => x.IdChiTietSP);
        }
    }
}
