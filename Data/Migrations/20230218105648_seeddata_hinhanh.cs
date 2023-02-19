using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class seeddata_hinhanh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HinhAnh",
                columns: new[] { "Id", "IdChiTietSP", "LinkAnh" },
                values: new object[] { new Guid("b596d1db-4113-4ed7-99e2-fa96a945d23c"), new Guid("51a13afd-08d1-4a20-a0e0-fb4447bd215d"), "C:\\Users\\Admin\\source\\repos\\NET105_Project\\ProjectViews\\wwwroot\\AnhNhanVien\\5752c6ba-f2e2-4331-95f2-2abaad283f46_nature-3082832.jpg" });

            migrationBuilder.InsertData(
                table: "HinhAnh",
                columns: new[] { "Id", "IdChiTietSP", "LinkAnh" },
                values: new object[] { new Guid("e508ba75-5185-4ef4-b533-2099fff74a84"), new Guid("7aaf5675-683d-4608-9534-ea737a4247b3"), "C:\\Users\\Admin\\source\\repos\\NET105_Project\\ProjectViews\\wwwroot\\AnhNhanVien\\307ec916-d15c-41b9-aeb8-ebfbf7c58033_nature-3082832.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HinhAnh",
                keyColumn: "Id",
                keyValue: new Guid("b596d1db-4113-4ed7-99e2-fa96a945d23c"));

            migrationBuilder.DeleteData(
                table: "HinhAnh",
                keyColumn: "Id",
                keyValue: new Guid("e508ba75-5185-4ef4-b533-2099fff74a84"));
        }
    }
}
