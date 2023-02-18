using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hang",
                columns: new[] { "Id", "TenHang" },
                values: new object[,]
                {
                    { new Guid("b9db8c0c-bf87-4e16-bdd2-a9fee4b14b4a"), "Adidas" },
                    { new Guid("da7a593f-20e5-407e-85c6-4bb0a97f1a73"), "Nike" }
                });

            migrationBuilder.InsertData(
                table: "KichCo",
                columns: new[] { "Id", "Size" },
                values: new object[,]
                {
                    { new Guid("670ad56f-8d6a-478e-8ca5-bef8d0e38af5"), 39f },
                    { new Guid("bd3aeb4b-dcdb-445b-9669-f5a1ce0c29d7"), 38f }
                });

            migrationBuilder.InsertData(
                table: "MauSac",
                columns: new[] { "Id", "TenMau" },
                values: new object[,]
                {
                    { new Guid("7a6c0c50-fb67-44ea-9c62-ad0e0f67ab3c"), "Xanh" },
                    { new Guid("9a1b3786-d0d0-402a-bfd8-10db04f8e6ed"), "Do" }
                });

            migrationBuilder.InsertData(
                table: "SanPham",
                columns: new[] { "Id", "IdHang", "Ten", "TrangThai" },
                values: new object[] { new Guid("01bf8a6c-d89a-4db2-9068-af277be488ae"), new Guid("b9db8c0c-bf87-4e16-bdd2-a9fee4b14b4a"), "Giay 1", 1 });

            migrationBuilder.InsertData(
                table: "SanPham",
                columns: new[] { "Id", "IdHang", "Ten", "TrangThai" },
                values: new object[] { new Guid("9a1b3786-d0d0-402a-bfd8-10db04f8e6ed"), new Guid("da7a593f-20e5-407e-85c6-4bb0a97f1a73"), "Giay 2", 1 });

            migrationBuilder.InsertData(
                table: "SanphamChitiet",
                columns: new[] { "Id", "GiaBan", "GiaNhap", "IdKichCo", "IdMauSac", "IdSP", "SoLuong", "TrangThai" },
                values: new object[] { new Guid("51a13afd-08d1-4a20-a0e0-fb4447bd215d"), 100000m, 80000m, new Guid("670ad56f-8d6a-478e-8ca5-bef8d0e38af5"), new Guid("9a1b3786-d0d0-402a-bfd8-10db04f8e6ed"), new Guid("9a1b3786-d0d0-402a-bfd8-10db04f8e6ed"), 10, 1 });

            migrationBuilder.InsertData(
                table: "SanphamChitiet",
                columns: new[] { "Id", "GiaBan", "GiaNhap", "IdKichCo", "IdMauSac", "IdSP", "SoLuong", "TrangThai" },
                values: new object[] { new Guid("7aaf5675-683d-4608-9534-ea737a4247b3"), 100000m, 80000m, new Guid("bd3aeb4b-dcdb-445b-9669-f5a1ce0c29d7"), new Guid("7a6c0c50-fb67-44ea-9c62-ad0e0f67ab3c"), new Guid("01bf8a6c-d89a-4db2-9068-af277be488ae"), 10, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SanphamChitiet",
                keyColumn: "Id",
                keyValue: new Guid("51a13afd-08d1-4a20-a0e0-fb4447bd215d"));

            migrationBuilder.DeleteData(
                table: "SanphamChitiet",
                keyColumn: "Id",
                keyValue: new Guid("7aaf5675-683d-4608-9534-ea737a4247b3"));

            migrationBuilder.DeleteData(
                table: "KichCo",
                keyColumn: "Id",
                keyValue: new Guid("670ad56f-8d6a-478e-8ca5-bef8d0e38af5"));

            migrationBuilder.DeleteData(
                table: "KichCo",
                keyColumn: "Id",
                keyValue: new Guid("bd3aeb4b-dcdb-445b-9669-f5a1ce0c29d7"));

            migrationBuilder.DeleteData(
                table: "MauSac",
                keyColumn: "Id",
                keyValue: new Guid("7a6c0c50-fb67-44ea-9c62-ad0e0f67ab3c"));

            migrationBuilder.DeleteData(
                table: "MauSac",
                keyColumn: "Id",
                keyValue: new Guid("9a1b3786-d0d0-402a-bfd8-10db04f8e6ed"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "Id",
                keyValue: new Guid("01bf8a6c-d89a-4db2-9068-af277be488ae"));

            migrationBuilder.DeleteData(
                table: "SanPham",
                keyColumn: "Id",
                keyValue: new Guid("9a1b3786-d0d0-402a-bfd8-10db04f8e6ed"));

            migrationBuilder.DeleteData(
                table: "Hang",
                keyColumn: "Id",
                keyValue: new Guid("b9db8c0c-bf87-4e16-bdd2-a9fee4b14b4a"));

            migrationBuilder.DeleteData(
                table: "Hang",
                keyColumn: "Id",
                keyValue: new Guid("da7a593f-20e5-407e-85c6-4bb0a97f1a73"));
        }
    }
}
