using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2_Data.Migrations
{
    public partial class dao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "KhachHang",
                keyColumn: "Id",
                keyValue: new Guid("3f8c0eff-a06a-4b07-a449-8832c622290d"),
                column: "NgaySinh",
                value: new DateTime(2023, 2, 22, 16, 33, 53, 624, DateTimeKind.Local).AddTicks(6203));

            migrationBuilder.UpdateData(
                table: "KhachHang",
                keyColumn: "Id",
                keyValue: new Guid("f572907f-4e58-4ee3-b282-8100a3a2043f"),
                column: "NgaySinh",
                value: new DateTime(2023, 2, 22, 16, 33, 53, 624, DateTimeKind.Local).AddTicks(6231));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "KhachHang",
                keyColumn: "Id",
                keyValue: new Guid("3f8c0eff-a06a-4b07-a449-8832c622290d"),
                column: "NgaySinh",
                value: new DateTime(2023, 2, 21, 22, 56, 12, 383, DateTimeKind.Local).AddTicks(6304));

            migrationBuilder.UpdateData(
                table: "KhachHang",
                keyColumn: "Id",
                keyValue: new Guid("f572907f-4e58-4ee3-b282-8100a3a2043f"),
                column: "NgaySinh",
                value: new DateTime(2023, 2, 21, 22, 56, 12, 383, DateTimeKind.Local).AddTicks(6319));
        }
    }
}
