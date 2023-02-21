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
                new SanphamChitiet()
                {
                    Id = new Guid("7AAF5675-683D-4608-9534-EA737A4247B3"),
                    IdSP = new Guid("01BF8A6C-D89A-4DB2-9068-AF277BE488AE"),
                    IdMauSac = new Guid("7A6C0C50-FB67-44EA-9C62-AD0E0F67AB3C"),
                    TenSPChiTiet = "V1",
                    MaSPChiTiet = "SP1",
                    GiaBan = 100000,
                    GiaNhap = 80000,
                    TrangThai = 1,
                    AnhDaiDien = "null"
                },
                new SanphamChitiet()
                {
                    Id = new Guid("51A13AFD-08D1-4A20-A0E0-FB4447BD215D"),
                    IdSP = new Guid("9A1B3786-D0D0-402A-BFD8-10DB04F8E6ED"),
                    IdMauSac = new Guid("9A1B3786-D0D0-402A-BFD8-10DB04F8E6ED"),
                    TenSPChiTiet = "V1",
                    MaSPChiTiet = "SP2",
                    GiaBan = 100000,
                    GiaNhap = 80000,
                    TrangThai = 1,
                    AnhDaiDien = "null"
                });
            modelBuilder.Entity<KichCo>().HasData(
                new KichCo()
                {
                    Id = new Guid("CD27319D-468D-4189-99A4-B2DA273492A3"),
                    Size = 36,
                },
                new KichCo()
                {
                    Id = new Guid("D43ED236-C9CF-4659-BA0D-F834BB316D39"),
                    Size = 37,
                });
            modelBuilder.Entity<SizeSanPham>().HasData(
                new SizeSanPham()
                {
                    Id = new Guid("5EE0FA3D-9AB6-490A-83CA-01021103412D"),
                    IdSanPhamChiTiet = new Guid("7AAF5675-683D-4608-9534-EA737A4247B3"),
                    IdSize = new Guid("CD27319D-468D-4189-99A4-B2DA273492A3"),
                    SoLuong = 10,
                },
                new SizeSanPham()
                {
                    Id = new  Guid("1D1CDE20-7455-48CA-8E25-AEA8C98A4671") ,
                    IdSanPhamChiTiet = new Guid("51A13AFD-08D1-4A20-A0E0-FB4447BD215D"),
                    IdSize = new Guid("D43ED236-C9CF-4659-BA0D-F834BB316D39"),
                    SoLuong = 10,
                }
            );
            modelBuilder.Entity<HinhAnh>().HasData(
                new HinhAnh()
                {
                    Id = new Guid("E508BA75-5185-4EF4-B533-2099FFF74A84"),
                    IdChiTietSP = new Guid("7AAF5675-683D-4608-9534-EA737A4247B3"),
                    LinkAnh = "C:\\Users\\Admin\\source\\repos\\NET105_Project\\ProjectViews\\wwwroot\\AnhNhanVien\\307ec916-d15c-41b9-aeb8-ebfbf7c58033_nature-3082832.jpg"
                },
                new HinhAnh()
                {
                    Id = new Guid("B596D1DB-4113-4ED7-99E2-FA96A945D23C"),
                    IdChiTietSP = new Guid("51A13AFD-08D1-4A20-A0E0-FB4447BD215D"),
                    LinkAnh = "C:\\Users\\Admin\\source\\repos\\NET105_Project\\ProjectViews\\wwwroot\\AnhNhanVien\\5752c6ba-f2e2-4331-95f2-2abaad283f46_nature-3082832.jpg"
                });
            modelBuilder.Entity<SanPham>().HasData(
                new SanPham()
                {
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
                new Hang()
                {
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
            modelBuilder.Entity<GioHang>().HasData(
                new GioHang()
                {
                    Id = new Guid("9779E43B-FCFC-40A0-B778-FA6F4757FD55"),
                    IdKH = new Guid("3F8C0EFF-A06A-4B07-A449-8832C622290D")
                },
                new GioHang()
                {
                    Id = new Guid("87C18DE0-9653-408E-9F7D-36B2F5A3D890"),
                    IdKH = new Guid("F572907F-4E58-4EE3-B282-8100A3A2043F")
                });
            modelBuilder.Entity<KhachHang>().HasData(
                new KhachHang()
                {
                    Id = new Guid("3F8C0EFF-A06A-4B07-A449-8832C622290D"),
                    Ten = "a",
                    Email = "a",
                    MatKhau = "1",
                    DiaChi = "1",
                    GioiTinh = true,
                    Sdt = "1",
                    NgaySinh = DateTime.Now,
                },
                new KhachHang()
                {
                    Id = new Guid("F572907F-4E58-4EE3-B282-8100A3A2043F"),
                    Ten = "b",
                    Email = "b",
                    MatKhau = "1",
                    DiaChi = "2",
                    GioiTinh = true,
                    Sdt = "2",
                    NgaySinh = DateTime.Now,
                });
            modelBuilder.Entity<GiohangChitiet>().HasData(
                new GiohangChitiet()
                {
                    Id = new Guid("040174D7-FB1C-4FFF-912C-215F1562E2EA"),
                    IdGioHang = new Guid("9779E43B-FCFC-40A0-B778-FA6F4757FD55"),
                    IdSPChitiet = new Guid("7AAF5675-683D-4608-9534-EA737A4247B3"),
                    SoLuong = 3,
                    GiaBan = 20000
                },
                new GiohangChitiet()
                {
                    Id = new Guid("E5FB1F37-454C-4AAE-968D-D19C6E8CC9FA"),
                    IdGioHang = new Guid("87C18DE0-9653-408E-9F7D-36B2F5A3D890"),
                    IdSPChitiet = new Guid("51A13AFD-08D1-4A20-A0E0-FB4447BD215D"),
                    SoLuong = 2,
                    GiaBan = 10000
                });
        }
    }
}
