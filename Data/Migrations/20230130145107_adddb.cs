using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class adddb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChucVu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenHang = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KichCo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KichCo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaGiamGia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ma = table.Column<int>(type: "int", nullable: false),
                    NgayBatdau = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayKetthuc = table.Column<DateTime>(type: "datetime", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaGiamGia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MauSac",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenMau = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MauSac", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TheLoai",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenTheLoai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCvu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhanVien_ChucVu_IdCvu",
                        column: x => x.IdCvu,
                        principalTable: "ChucVu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanPham_Hang_IdHang",
                        column: x => x.IdHang,
                        principalTable: "Hang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdKH = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHang_KhachHang_IdKH",
                        column: x => x.IdKH,
                        principalTable: "KhachHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMaGiamGia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdKH = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNV = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "DateTime", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nhanVienId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDon_KhachHang_IdNV",
                        column: x => x.IdNV,
                        principalTable: "KhachHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDon_MaGiamGia_IdMaGiamGia",
                        column: x => x.IdMaGiamGia,
                        principalTable: "MaGiamGia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDon_NhanVien_nhanVienId",
                        column: x => x.nhanVienId,
                        principalTable: "NhanVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanphamChitiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMauSac = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdKichCo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaNhap = table.Column<decimal>(type: "decimal", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanphamChitiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanphamChitiet_KichCo_IdKichCo",
                        column: x => x.IdKichCo,
                        principalTable: "KichCo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanphamChitiet_MauSac_IdMauSac",
                        column: x => x.IdMauSac,
                        principalTable: "MauSac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanphamChitiet_SanPham_IdSP",
                        column: x => x.IdSP,
                        principalTable: "SanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TheLoaiSanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTheLoai = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoaiSanPham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TheLoaiSanPham_SanPham_IdSanPham",
                        column: x => x.IdSanPham,
                        principalTable: "SanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TheLoaiSanPham_TheLoai_IdTheLoai",
                        column: x => x.IdTheLoai,
                        principalTable: "TheLoai",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiohangChitiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSPChitiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdGioHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiohangChitiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiohangChitiet_GioHang_IdGioHang",
                        column: x => x.IdGioHang,
                        principalTable: "GioHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiohangChitiet_SanphamChitiet_IdSPChitiet",
                        column: x => x.IdSPChitiet,
                        principalTable: "SanphamChitiet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoadonChitiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSPChitiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoadonChitiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoadonChitiet_HoaDon_IdHoaDon",
                        column: x => x.IdHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoadonChitiet_SanphamChitiet_IdSPChitiet",
                        column: x => x.IdSPChitiet,
                        principalTable: "SanphamChitiet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_IdKH",
                table: "GioHang",
                column: "IdKH");

            migrationBuilder.CreateIndex(
                name: "IX_GiohangChitiet_IdGioHang",
                table: "GiohangChitiet",
                column: "IdGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_GiohangChitiet_IdSPChitiet",
                table: "GiohangChitiet",
                column: "IdSPChitiet");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdMaGiamGia",
                table: "HoaDon",
                column: "IdMaGiamGia");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdNV",
                table: "HoaDon",
                column: "IdNV");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_nhanVienId",
                table: "HoaDon",
                column: "nhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_HoadonChitiet_IdHoaDon",
                table: "HoadonChitiet",
                column: "IdHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_HoadonChitiet_IdSPChitiet",
                table: "HoadonChitiet",
                column: "IdSPChitiet");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_IdCvu",
                table: "NhanVien",
                column: "IdCvu");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_IdHang",
                table: "SanPham",
                column: "IdHang");

            migrationBuilder.CreateIndex(
                name: "IX_SanphamChitiet_IdKichCo",
                table: "SanphamChitiet",
                column: "IdKichCo");

            migrationBuilder.CreateIndex(
                name: "IX_SanphamChitiet_IdMauSac",
                table: "SanphamChitiet",
                column: "IdMauSac");

            migrationBuilder.CreateIndex(
                name: "IX_SanphamChitiet_IdSP",
                table: "SanphamChitiet",
                column: "IdSP");

            migrationBuilder.CreateIndex(
                name: "IX_TheLoaiSanPham_IdSanPham",
                table: "TheLoaiSanPham",
                column: "IdSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_TheLoaiSanPham_IdTheLoai",
                table: "TheLoaiSanPham",
                column: "IdTheLoai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiohangChitiet");

            migrationBuilder.DropTable(
                name: "HoadonChitiet");

            migrationBuilder.DropTable(
                name: "TheLoaiSanPham");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "SanphamChitiet");

            migrationBuilder.DropTable(
                name: "TheLoai");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "MaGiamGia");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "KichCo");

            migrationBuilder.DropTable(
                name: "MauSac");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "ChucVu");

            migrationBuilder.DropTable(
                name: "Hang");
        }
    }
}
