using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2_Data.Migrations
{
    public partial class _01 : Migration
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
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Size = table.Column<float>(type: "real", nullable: false)
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
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayBatdau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetthuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    PhanTramGiam = table.Column<int>(type: "int", nullable: false)
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
                    IdGuiBaoCao = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaNV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnhNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdGuiBcNavigationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_NhanVien_NhanVien_IdGuiBcNavigationId",
                        column: x => x.IdGuiBcNavigationId,
                        principalTable: "NhanVien",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
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
                    IdMaGiamGia = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdKH = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNV = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDon_KhachHang_IdKH",
                        column: x => x.IdKH,
                        principalTable: "KhachHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDon_MaGiamGia_IdMaGiamGia",
                        column: x => x.IdMaGiamGia,
                        principalTable: "MaGiamGia",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HoaDon_NhanVien_IdNV",
                        column: x => x.IdNV,
                        principalTable: "NhanVien",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SanphamChitiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMauSac = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenSPChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaSPChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaNhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanphamChitiet", x => x.Id);
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
                name: "GiohangChitiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSPChitiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdGioHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                name: "HinhAnh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdChiTietSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkAnh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhAnh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HinhAnh_SanphamChitiet_IdChiTietSP",
                        column: x => x.IdChiTietSP,
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
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TheLoaiSanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTheLoai = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdChiTietSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoaiSanPham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TheLoaiSanPham_SanphamChitiet_IdChiTietSP",
                        column: x => x.IdChiTietSP,
                        principalTable: "SanphamChitiet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TheLoaiSanPham_TheLoai_IdTheLoai",
                        column: x => x.IdTheLoai,
                        principalTable: "TheLoai",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Hang",
                columns: new[] { "Id", "TenHang" },
                values: new object[,]
                {
                    { new Guid("b9db8c0c-bf87-4e16-bdd2-a9fee4b14b4a"), "Adidas" },
                    { new Guid("da7a593f-20e5-407e-85c6-4bb0a97f1a73"), "Nike" }
                });

            migrationBuilder.InsertData(
                table: "KhachHang",
                columns: new[] { "Id", "DiaChi", "Email", "GioiTinh", "MatKhau", "NgaySinh", "Sdt", "Ten" },
                values: new object[,]
                {
                    { new Guid("3f8c0eff-a06a-4b07-a449-8832c622290d"), "1", "a", true, "1", new DateTime(2023, 2, 21, 22, 56, 12, 383, DateTimeKind.Local).AddTicks(6304), "1", "a" },
                    { new Guid("f572907f-4e58-4ee3-b282-8100a3a2043f"), "2", "b", true, "1", new DateTime(2023, 2, 21, 22, 56, 12, 383, DateTimeKind.Local).AddTicks(6319), "2", "b" }
                });

            migrationBuilder.InsertData(
                table: "KichCo",
                columns: new[] { "Id", "Size" },
                values: new object[,]
                {
                    { new Guid("670ad56f-8d6a-478e-8ca5-bef8d0e38af5"), 39f },
                    { new Guid("bd3aeb4b-dcdb-445b-9669-f5a1ce0c29d7"), 38f },
                    { new Guid("cd27319d-468d-4189-99a4-b2da273492a3"), 36f },
                    { new Guid("d43ed236-c9cf-4659-ba0d-f834bb316d39"), 37f }
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
                table: "GioHang",
                columns: new[] { "Id", "IdKH" },
                values: new object[,]
                {
                    { new Guid("87c18de0-9653-408e-9f7d-36b2f5a3d890"), new Guid("f572907f-4e58-4ee3-b282-8100a3a2043f") },
                    { new Guid("9779e43b-fcfc-40a0-b778-fa6f4757fd55"), new Guid("3f8c0eff-a06a-4b07-a449-8832c622290d") }
                });

            migrationBuilder.InsertData(
                table: "SanPham",
                columns: new[] { "Id", "IdHang", "Ten", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("01bf8a6c-d89a-4db2-9068-af277be488ae"), new Guid("b9db8c0c-bf87-4e16-bdd2-a9fee4b14b4a"), "Giay 1", 1 },
                    { new Guid("9a1b3786-d0d0-402a-bfd8-10db04f8e6ed"), new Guid("da7a593f-20e5-407e-85c6-4bb0a97f1a73"), "Giay 2", 1 }
                });

            migrationBuilder.InsertData(
                table: "SanphamChitiet",
                columns: new[] { "Id", "AnhDaiDien", "GiaBan", "GiaNhap", "IdMauSac", "IdSP", "MaSPChiTiet", "TenSPChiTiet", "TrangThai" },
                values: new object[] { new Guid("51a13afd-08d1-4a20-a0e0-fb4447bd215d"), "null", 100000m, 80000m, new Guid("9a1b3786-d0d0-402a-bfd8-10db04f8e6ed"), new Guid("9a1b3786-d0d0-402a-bfd8-10db04f8e6ed"), "SP2", "V1", 1 });

            migrationBuilder.InsertData(
                table: "SanphamChitiet",
                columns: new[] { "Id", "AnhDaiDien", "GiaBan", "GiaNhap", "IdMauSac", "IdSP", "MaSPChiTiet", "TenSPChiTiet", "TrangThai" },
                values: new object[] { new Guid("7aaf5675-683d-4608-9534-ea737a4247b3"), "null", 100000m, 80000m, new Guid("7a6c0c50-fb67-44ea-9c62-ad0e0f67ab3c"), new Guid("01bf8a6c-d89a-4db2-9068-af277be488ae"), "SP1", "V1", 1 });

            migrationBuilder.InsertData(
                table: "GiohangChitiet",
                columns: new[] { "Id", "GiaBan", "IdGioHang", "IdSPChitiet", "SoLuong" },
                values: new object[,]
                {
                    { new Guid("040174d7-fb1c-4fff-912c-215f1562e2ea"), 20000m, new Guid("9779e43b-fcfc-40a0-b778-fa6f4757fd55"), new Guid("7aaf5675-683d-4608-9534-ea737a4247b3"), 3 },
                    { new Guid("e5fb1f37-454c-4aae-968d-d19c6e8cc9fa"), 10000m, new Guid("87c18de0-9653-408e-9f7d-36b2f5a3d890"), new Guid("51a13afd-08d1-4a20-a0e0-fb4447bd215d"), 2 }
                });

            migrationBuilder.InsertData(
                table: "HinhAnh",
                columns: new[] { "Id", "IdChiTietSP", "LinkAnh" },
                values: new object[,]
                {
                    { new Guid("b596d1db-4113-4ed7-99e2-fa96a945d23c"), new Guid("51a13afd-08d1-4a20-a0e0-fb4447bd215d"), "C:\\Users\\Admin\\source\\repos\\NET105_Project\\ProjectViews\\wwwroot\\AnhNhanVien\\5752c6ba-f2e2-4331-95f2-2abaad283f46_nature-3082832.jpg" },
                    { new Guid("e508ba75-5185-4ef4-b533-2099fff74a84"), new Guid("7aaf5675-683d-4608-9534-ea737a4247b3"), "C:\\Users\\Admin\\source\\repos\\NET105_Project\\ProjectViews\\wwwroot\\AnhNhanVien\\307ec916-d15c-41b9-aeb8-ebfbf7c58033_nature-3082832.jpg" }
                });

            migrationBuilder.InsertData(
                table: "SizeSanPham",
                columns: new[] { "Id", "IdSanPhamChiTiet", "IdSize", "SoLuong" },
                values: new object[,]
                {
                    { new Guid("1d1cde20-7455-48ca-8e25-aea8c98a4671"), new Guid("51a13afd-08d1-4a20-a0e0-fb4447bd215d"), new Guid("d43ed236-c9cf-4659-ba0d-f834bb316d39"), 10 },
                    { new Guid("5ee0fa3d-9ab6-490a-83ca-01021103412d"), new Guid("7aaf5675-683d-4608-9534-ea737a4247b3"), new Guid("cd27319d-468d-4189-99a4-b2da273492a3"), 10 }
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
                name: "IX_HinhAnh_IdChiTietSP",
                table: "HinhAnh",
                column: "IdChiTietSP");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdKH",
                table: "HoaDon",
                column: "IdKH");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdMaGiamGia",
                table: "HoaDon",
                column: "IdMaGiamGia");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdNV",
                table: "HoaDon",
                column: "IdNV");

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
                name: "IX_NhanVien_IdGuiBcNavigationId",
                table: "NhanVien",
                column: "IdGuiBcNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_IdHang",
                table: "SanPham",
                column: "IdHang");

            migrationBuilder.CreateIndex(
                name: "IX_SanphamChitiet_IdMauSac",
                table: "SanphamChitiet",
                column: "IdMauSac");

            migrationBuilder.CreateIndex(
                name: "IX_SanphamChitiet_IdSP",
                table: "SanphamChitiet",
                column: "IdSP");

            migrationBuilder.CreateIndex(
                name: "IX_SizeSanPham_IdSanPhamChiTiet",
                table: "SizeSanPham",
                column: "IdSanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_SizeSanPham_IdSize",
                table: "SizeSanPham",
                column: "IdSize");

            migrationBuilder.CreateIndex(
                name: "IX_TheLoaiSanPham_IdChiTietSP",
                table: "TheLoaiSanPham",
                column: "IdChiTietSP");

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
                name: "HinhAnh");

            migrationBuilder.DropTable(
                name: "HoadonChitiet");

            migrationBuilder.DropTable(
                name: "SizeSanPham");

            migrationBuilder.DropTable(
                name: "TheLoaiSanPham");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "KichCo");

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
