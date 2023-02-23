using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2_Data.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SanphamChitiet_KichCo_IdKichCo",
                table: "SanphamChitiet");

            migrationBuilder.DropIndex(
                name: "IX_SanphamChitiet_IdKichCo",
                table: "SanphamChitiet");

            migrationBuilder.DropColumn(
                name: "IdKichCo",
                table: "SanphamChitiet");

            migrationBuilder.DropColumn(
                name: "SoLuong",
                table: "SanphamChitiet");

            migrationBuilder.CreateTable(
                name: "SizeSanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSanPhamChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSize = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeSanPham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SizeSanPham_KichCo_IdSize",
                        column: x => x.IdSize,
                        principalTable: "KichCo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SizeSanPham_SanphamChitiet_IdSanPhamChiTiet",
                        column: x => x.IdSanPhamChiTiet,
                        principalTable: "SanphamChitiet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "KhachHang",
                keyColumn: "Id",
                keyValue: new Guid("3f8c0eff-a06a-4b07-a449-8832c622290d"),
                column: "NgaySinh",
                value: new DateTime(2023, 2, 21, 17, 46, 19, 821, DateTimeKind.Local).AddTicks(268));

            migrationBuilder.UpdateData(
                table: "KhachHang",
                keyColumn: "Id",
                keyValue: new Guid("f572907f-4e58-4ee3-b282-8100a3a2043f"),
                column: "NgaySinh",
                value: new DateTime(2023, 2, 21, 17, 46, 19, 821, DateTimeKind.Local).AddTicks(280));

            migrationBuilder.InsertData(
                table: "KichCo",
                columns: new[] { "Id", "Size" },
                values: new object[,]
                {
                    { new Guid("cd27319d-468d-4189-99a4-b2da273492a3"), 36f },
                    { new Guid("d43ed236-c9cf-4659-ba0d-f834bb316d39"), 37f }
                });

            migrationBuilder.InsertData(
                table: "SizeSanPham",
                columns: new[] { "Id", "IdSanPhamChiTiet", "IdSize", "SoLuong" },
                values: new object[] { new Guid("1d1cde20-7455-48ca-8e25-aea8c98a4671"), new Guid("51a13afd-08d1-4a20-a0e0-fb4447bd215d"), new Guid("d43ed236-c9cf-4659-ba0d-f834bb316d39"), 10 });

            migrationBuilder.InsertData(
                table: "SizeSanPham",
                columns: new[] { "Id", "IdSanPhamChiTiet", "IdSize", "SoLuong" },
                values: new object[] { new Guid("5ee0fa3d-9ab6-490a-83ca-01021103412d"), new Guid("7aaf5675-683d-4608-9534-ea737a4247b3"), new Guid("cd27319d-468d-4189-99a4-b2da273492a3"), 10 });

            migrationBuilder.CreateIndex(
                name: "IX_SizeSanPham_IdSanPhamChiTiet",
                table: "SizeSanPham",
                column: "IdSanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_SizeSanPham_IdSize",
                table: "SizeSanPham",
                column: "IdSize");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SizeSanPham");

            migrationBuilder.DeleteData(
                table: "KichCo",
                keyColumn: "Id",
                keyValue: new Guid("cd27319d-468d-4189-99a4-b2da273492a3"));

            migrationBuilder.DeleteData(
                table: "KichCo",
                keyColumn: "Id",
                keyValue: new Guid("d43ed236-c9cf-4659-ba0d-f834bb316d39"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdKichCo",
                table: "SanphamChitiet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "SoLuong",
                table: "SanphamChitiet",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.UpdateData(
                table: "SanphamChitiet",
                keyColumn: "Id",
                keyValue: new Guid("51a13afd-08d1-4a20-a0e0-fb4447bd215d"),
                columns: new[] { "IdKichCo", "SoLuong" },
                values: new object[] { new Guid("670ad56f-8d6a-478e-8ca5-bef8d0e38af5"), 10 });

            migrationBuilder.UpdateData(
                table: "SanphamChitiet",
                keyColumn: "Id",
                keyValue: new Guid("7aaf5675-683d-4608-9534-ea737a4247b3"),
                columns: new[] { "IdKichCo", "SoLuong" },
                values: new object[] { new Guid("bd3aeb4b-dcdb-445b-9669-f5a1ce0c29d7"), 10 });

            migrationBuilder.CreateIndex(
                name: "IX_SanphamChitiet_IdKichCo",
                table: "SanphamChitiet",
                column: "IdKichCo");

            migrationBuilder.AddForeignKey(
                name: "FK_SanphamChitiet_KichCo_IdKichCo",
                table: "SanphamChitiet",
                column: "IdKichCo",
                principalTable: "KichCo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
