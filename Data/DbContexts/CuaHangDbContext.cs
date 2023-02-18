﻿using Data.Extensions;
using Data.ModelsClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.DbContexts
{
    public class CuaHangDbContext : DbContext
    {
        public CuaHangDbContext()
        {

        }
        public CuaHangDbContext(DbContextOptions<CuaHangDbContext> dbContextOptions):base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer("Server=HDGNGUYENTIEN\\SQLEXPRESS;Database=Net105Database;Trusted_Connection=True;"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //seeddata
            modelBuilder.Seed();
        }
        //dbset
        public DbSet<ChucVu> ChucVus { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<GiohangChitiet> GiohangChitiets { get; set; }
        public DbSet<Hang> Hangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<HoadonChitiet> HoadonChitiets { get; set; }
        public DbSet<KichCo> KichCos { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<MaGiamGia> MaGiamGias { get; set; }
        public DbSet<MauSac> MauSacs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<SanphamChitiet> SanphamChitiets { get; set; }
        public DbSet<TheLoai> TheLoais { get; set; }
        public DbSet<TheLoaiSanPham> TheLoaiSanPhams { get; set; }
        public DbSet<HinhAnh> HinhAnhs { get; set; }


    }
}
