using Data.ModelsClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SanphamChitiet>().HasData(
                new SanphamChitiet() {
                    Id = new Guid("7AAF5675-683D-4608-9534-EA737A4247B3"), 
                    IdSP = new Guid("01BF8A6C-D89A-4DB2-9068-AF277BE488AE"),
                    IdKichCo = new Guid("BD3AEB4B-DCDB-445B-9669-F5A1CE0C29D7"),
                    IdMauSac = new Guid("7A6C0C50-FB67-44EA-9C62-AD0E0F67AB3C"),
                    
                    SoLuong = 10, 
                    GiaBan = 100000,
                    GiaNhap = 80000,
                    TrangThai = 1,
                    },
                new SanphamChitiet()
                {
                    Id = new Guid("51A13AFD-08D1-4A20-A0E0-FB4447BD215D") ,
                    IdSP = new Guid("9A1B3786-D0D0-402A-BFD8-10DB04F8E6ED") ,
                    IdKichCo = new Guid("670AD56F-8D6A-478E-8CA5-BEF8D0E38AF5") ,
                    IdMauSac = new Guid("9A1B3786-D0D0-402A-BFD8-10DB04F8E6ED") ,

                    SoLuong = 10,
                    GiaBan = 100000,
                    GiaNhap = 80000,
                    TrangThai = 1
                });

            modelBuilder.Entity<SanPham>().HasData(
                new SanPham() {
                    Id = new Guid("01BF8A6C-D89A-4DB2-9068-AF277BE488AE"),
                    Ten = "Giay 1",
                    IdHang = new Guid("B9DB8C0C-BF87-4E16-BDD2-A9FEE4B14B4A"),
                    TrangThai = 1
                    },
                new SanPham()
                {
                    Id = new Guid("9A1B3786-D0D0-402A-BFD8-10DB04F8E6ED"),
                    Ten = "Giay 2",
                    IdHang = new Guid("DA7A593F-20E5-407E-85C6-4BB0A97F1A73"),
                    TrangThai = 1
                });
            modelBuilder.Entity<Hang>().HasData(
                new Hang() {
                    Id = new Guid("B9DB8C0C-BF87-4E16-BDD2-A9FEE4B14B4A"),
                    TenHang = "Adidas"
                },
                new Hang()
                {
                    Id = new Guid("DA7A593F-20E5-407E-85C6-4BB0A97F1A73"),
                    TenHang = "Nike"
                }
                );
            modelBuilder.Entity<MauSac>().HasData(
                new MauSac() 
                {
                    Id = new Guid("7A6C0C50-FB67-44EA-9C62-AD0E0F67AB3C"),
                    TenMau = "Xanh"
                },
                new MauSac()
                {
                    Id = new Guid("9A1B3786-D0D0-402A-BFD8-10DB04F8E6ED"),
                    TenMau = "Do"
                }
                );
            modelBuilder.Entity<KichCo>().HasData(
                new KichCo() 
                {
                    Id = new Guid("BD3AEB4B-DCDB-445B-9669-F5A1CE0C29D7"),
                    Size = 38
                },
                new KichCo()
                {
                    Id = new Guid("670AD56F-8D6A-478E-8CA5-BEF8D0E38AF5"),
                    Size = 39
                }
                );
        }
    } 
}
