using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2_Data.Migrations
{
    public partial class _05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_NhanVien_IdNV",
                table: "HoaDon");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdNV",
                table: "HoaDon",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "KhachHang",
                keyColumn: "Id",
                keyValue: new Guid("3f8c0eff-a06a-4b07-a449-8832c622290d"),
                column: "NgaySinh",
                value: new DateTime(2023, 2, 20, 22, 56, 23, 131, DateTimeKind.Local).AddTicks(3129));

            migrationBuilder.UpdateData(
                table: "KhachHang",
                keyColumn: "Id",
                keyValue: new Guid("f572907f-4e58-4ee3-b282-8100a3a2043f"),
                column: "NgaySinh",
                value: new DateTime(2023, 2, 20, 22, 56, 23, 131, DateTimeKind.Local).AddTicks(3143));

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_NhanVien_IdNV",
                table: "HoaDon",
                column: "IdNV",
                principalTable: "NhanVien",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_NhanVien_IdNV",
                table: "HoaDon");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdNV",
                table: "HoaDon",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_NhanVien_IdNV",
                table: "HoaDon",
                column: "IdNV",
                principalTable: "NhanVien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
