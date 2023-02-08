using Data.ModelsClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class TheloaiSanphamConfiguration : IEntityTypeConfiguration<TheLoaiSanPham>
    {
        public void Configure(EntityTypeBuilder<TheLoaiSanPham> builder)
        {
            builder.ToTable("TheLoaiSanPham");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IdTheLoai).IsRequired();
            builder.Property(x => x.IdChiTietSP).IsRequired();
            builder.HasOne(x => x.theLoai).WithMany(x => x.theloaiSanPhams).HasForeignKey(x => x.IdTheLoai);
            builder.HasOne(x => x.sanphamChitiet).WithMany(x => x.theLoaiSanPhams).HasForeignKey(x => x.IdChiTietSP);
        }
    }
}
