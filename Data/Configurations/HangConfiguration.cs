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
    public class HangConfiguration : IEntityTypeConfiguration<Hang>
    {
        public void Configure(EntityTypeBuilder<Hang> builder)
        {
            builder.ToTable("Hang");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TenHang).IsRequired();
        }
    }
}
