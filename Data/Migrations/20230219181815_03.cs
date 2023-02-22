using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2_Data.Migrations
{
    public partial class _03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "KhachHang",
                columns: new[] { "Id", "DiaChi", "Email", "GioiTinh", "MatKhau", "NgaySinh", "Sdt", "Ten" },
                values: new object[] { new Guid("3f8c0eff-a06a-4b07-a449-8832c622290d"), "1", "a", true, "1", new DateTime(2023, 2, 20, 1, 18, 14, 727, DateTimeKind.Local).AddTicks(9032), "1", "a" });

            migrationBuilder.InsertData(
                table: "KhachHang",
                columns: new[] { "Id", "DiaChi", "Email", "GioiTinh", "MatKhau", "NgaySinh", "Sdt", "Ten" },
                values: new object[] { new Guid("f572907f-4e58-4ee3-b282-8100a3a2043f"), "2", "b", true, "1", new DateTime(2023, 2, 20, 1, 18, 14, 727, DateTimeKind.Local).AddTicks(9044), "2", "b" });

            migrationBuilder.InsertData(
                table: "GioHang",
                columns: new[] { "Id", "IdKH" },
                values: new object[] { new Guid("87c18de0-9653-408e-9f7d-36b2f5a3d890"), new Guid("f572907f-4e58-4ee3-b282-8100a3a2043f") });

            migrationBuilder.InsertData(
                table: "GioHang",
                columns: new[] { "Id", "IdKH" },
                values: new object[] { new Guid("9779e43b-fcfc-40a0-b778-fa6f4757fd55"), new Guid("3f8c0eff-a06a-4b07-a449-8832c622290d") });

            migrationBuilder.InsertData(
                table: "GiohangChitiet",
                columns: new[] { "Id", "GiaBan", "IdGioHang", "IdSPChitiet", "SoLuong" },
                values: new object[] { new Guid("040174d7-fb1c-4fff-912c-215f1562e2ea"), 20000m, new Guid("9779e43b-fcfc-40a0-b778-fa6f4757fd55"), new Guid("7aaf5675-683d-4608-9534-ea737a4247b3"), 3 });

            migrationBuilder.InsertData(
                table: "GiohangChitiet",
                columns: new[] { "Id", "GiaBan", "IdGioHang", "IdSPChitiet", "SoLuong" },
                values: new object[] { new Guid("e5fb1f37-454c-4aae-968d-d19c6e8cc9fa"), 10000m, new Guid("87c18de0-9653-408e-9f7d-36b2f5a3d890"), new Guid("51a13afd-08d1-4a20-a0e0-fb4447bd215d"), 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GiohangChitiet",
                keyColumn: "Id",
                keyValue: new Guid("040174d7-fb1c-4fff-912c-215f1562e2ea"));

            migrationBuilder.DeleteData(
                table: "GiohangChitiet",
                keyColumn: "Id",
                keyValue: new Guid("e5fb1f37-454c-4aae-968d-d19c6e8cc9fa"));

            migrationBuilder.DeleteData(
                table: "GioHang",
                keyColumn: "Id",
                keyValue: new Guid("87c18de0-9653-408e-9f7d-36b2f5a3d890"));

            migrationBuilder.DeleteData(
                table: "GioHang",
                keyColumn: "Id",
                keyValue: new Guid("9779e43b-fcfc-40a0-b778-fa6f4757fd55"));

            migrationBuilder.DeleteData(
                table: "KhachHang",
                keyColumn: "Id",
                keyValue: new Guid("3f8c0eff-a06a-4b07-a449-8832c622290d"));

            migrationBuilder.DeleteData(
                table: "KhachHang",
                keyColumn: "Id",
                keyValue: new Guid("f572907f-4e58-4ee3-b282-8100a3a2043f"));
        }
    }
}
