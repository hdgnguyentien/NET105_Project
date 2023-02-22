using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2_Data.Migrations
{
    public partial class _04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "KhachHang",
                keyColumn: "Id",
                keyValue: new Guid("3f8c0eff-a06a-4b07-a449-8832c622290d"),
                column: "NgaySinh",
                value: new DateTime(2023, 2, 20, 20, 10, 0, 58, DateTimeKind.Local).AddTicks(1729));

            migrationBuilder.UpdateData(
                table: "KhachHang",
                keyColumn: "Id",
                keyValue: new Guid("f572907f-4e58-4ee3-b282-8100a3a2043f"),
                column: "NgaySinh",
                value: new DateTime(2023, 2, 20, 20, 10, 0, 58, DateTimeKind.Local).AddTicks(1744));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "KhachHang",
                keyColumn: "Id",
                keyValue: new Guid("3f8c0eff-a06a-4b07-a449-8832c622290d"),
                column: "NgaySinh",
                value: new DateTime(2023, 2, 20, 1, 18, 14, 727, DateTimeKind.Local).AddTicks(9032));

            migrationBuilder.UpdateData(
                table: "KhachHang",
                keyColumn: "Id",
                keyValue: new Guid("f572907f-4e58-4ee3-b282-8100a3a2043f"),
                column: "NgaySinh",
                value: new DateTime(2023, 2, 20, 1, 18, 14, 727, DateTimeKind.Local).AddTicks(9044));
        }
    }
}
