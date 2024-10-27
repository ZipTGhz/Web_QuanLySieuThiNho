using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web_QuanLySieuThiNho.Models;

public partial class QlsieuThiNhoContext : DbContext
{
    public QlsieuThiNhoContext()
    {
    }

    public QlsieuThiNhoContext(DbContextOptions<QlsieuThiNhoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TChiTietHdb> TChiTietHdbs { get; set; }

    public virtual DbSet<TChiTietHdn> TChiTietHdns { get; set; }

    public virtual DbSet<TGioHang> TGioHangs { get; set; }

    public virtual DbSet<THoaDonBan> THoaDonBans { get; set; }

    public virtual DbSet<THoaDonNhap> THoaDonNhaps { get; set; }

    public virtual DbSet<TKhachHang> TKhachHangs { get; set; }

    public virtual DbSet<TLoaiHang> TLoaiHangs { get; set; }

    public virtual DbSet<TNhaCungCap> TNhaCungCaps { get; set; }

    public virtual DbSet<TNhanVien> TNhanViens { get; set; }

    public virtual DbSet<TSanPham> TSanPhams { get; set; }

    public virtual DbSet<TSanPhamGioHang> TSanPhamGioHangs { get; set; }

    public virtual DbSet<TTaiKhoan> TTaiKhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-J66C5J9\\SQLEXPRESS;Initial Catalog=QLSieuThiNho;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TChiTietHdb>(entity =>
        {
            entity.HasKey(e => new { e.SoHdb, e.MaSanPham }).HasName("PK__tChiTiet__E86F9AC2ACC69060");

            entity.ToTable("tChiTietHDB");

            entity.Property(e => e.SoHdb)
                .HasMaxLength(10)
                .HasColumnName("SoHDB");
            entity.Property(e => e.MaSanPham).HasMaxLength(10);
            entity.Property(e => e.Slban).HasColumnName("SLBan");
            entity.Property(e => e.ThanhTien).HasColumnType("money");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.TChiTietHdbs)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tChiTietHDB_tSanPham");

            entity.HasOne(d => d.SoHdbNavigation).WithMany(p => p.TChiTietHdbs)
                .HasForeignKey(d => d.SoHdb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tChiTietHDB_tHoaDonBan");
        });

        modelBuilder.Entity<TChiTietHdn>(entity =>
        {
            entity.HasKey(e => new { e.SoHdn, e.MaSanPham }).HasName("PK__tChiTiet__E86F9AF691969485");

            entity.ToTable("tChiTietHDN");

            entity.Property(e => e.SoHdn)
                .HasMaxLength(10)
                .HasColumnName("SoHDN");
            entity.Property(e => e.MaSanPham).HasMaxLength(10);
            entity.Property(e => e.Slnhap).HasColumnName("SLNhap");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.TChiTietHdns)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tChiTietHDN_tSanPham");

            entity.HasOne(d => d.SoHdnNavigation).WithMany(p => p.TChiTietHdns)
                .HasForeignKey(d => d.SoHdn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tChiTietHDN_tHoaDonNhap");
        });

        modelBuilder.Entity<TGioHang>(entity =>
        {
            entity.HasKey(e => e.MaGioHang).HasName("PK__tGioHang__F5001DA3E89520D5");

            entity.ToTable("tGioHang");

            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .HasColumnName("MaKH");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.TongTienGioHang).HasColumnType("money");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.TGioHangs)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_tGioHang_tKhachHang");
        });

        modelBuilder.Entity<THoaDonBan>(entity =>
        {
            entity.HasKey(e => e.SoHdb);

            entity.ToTable("tHoaDonBan");

            entity.Property(e => e.SoHdb)
                .HasMaxLength(10)
                .HasColumnName("SoHDB");
            entity.Property(e => e.GhiChu).HasMaxLength(1000);
            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .HasColumnName("MaKH");
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayBan).HasColumnType("datetime");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.THoaDonBans)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_tHoaDonBan_tKhachHang");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.THoaDonBans)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_tHoaDonBan_tNhanVien");
        });

        modelBuilder.Entity<THoaDonNhap>(entity =>
        {
            entity.HasKey(e => e.SoHdn);

            entity.ToTable("tHoaDonNhap");

            entity.Property(e => e.SoHdn)
                .HasMaxLength(10)
                .HasColumnName("SoHDN");
            entity.Property(e => e.MaNcc)
                .HasMaxLength(10)
                .HasColumnName("MaNCC");
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayNhap).HasColumnType("datetime");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.THoaDonNhaps)
                .HasForeignKey(d => d.MaNcc)
                .HasConstraintName("FK_tHoaDonNhap_tNhaCungCap");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.THoaDonNhaps)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_tHoaDonNhap_tNhanVien");
        });

        modelBuilder.Entity<TKhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh);

            entity.ToTable("tKhachHang");

            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .HasColumnName("MaKH");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
            entity.Property(e => e.TenKh)
                .HasMaxLength(200)
                .HasColumnName("TenKH");

            entity.HasOne(d => d.TenDangNhapNavigation).WithMany(p => p.TKhachHangs)
                .HasForeignKey(d => d.TenDangNhap)
                .HasConstraintName("FK_tKhachHang_tTaiKhoan");
        });

        modelBuilder.Entity<TLoaiHang>(entity =>
        {
            entity.HasKey(e => e.MaLoaiHang).HasName("PK_tTheLoai");

            entity.ToTable("tLoaiHang");

            entity.Property(e => e.MaLoaiHang).HasMaxLength(10);
            entity.Property(e => e.TenLoaiHang).HasMaxLength(100);
        });

        modelBuilder.Entity<TNhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc);

            entity.ToTable("tNhaCungCap");

            entity.Property(e => e.MaNcc)
                .HasMaxLength(10)
                .HasColumnName("MaNCC");
            entity.Property(e => e.TenNcc)
                .HasMaxLength(200)
                .HasColumnName("TenNCC");
        });

        modelBuilder.Entity<TNhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv);

            entity.ToTable("tNhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");
            entity.Property(e => e.ChucVu).HasMaxLength(50);
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
            entity.Property(e => e.TenNv)
                .HasMaxLength(200)
                .HasColumnName("TenNV");

            entity.HasOne(d => d.TenDangNhapNavigation).WithMany(p => p.TNhanViens)
                .HasForeignKey(d => d.TenDangNhap)
                .HasConstraintName("FK_tNhanVien_tTaiKhoan");
        });

        modelBuilder.Entity<TSanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanPham);

            entity.ToTable("tSanPham");

            entity.Property(e => e.MaSanPham).HasMaxLength(10);
            entity.Property(e => e.AnhSanPham).HasMaxLength(200);
            entity.Property(e => e.DonGiaBan).HasColumnType("money");
            entity.Property(e => e.DonGiaNhap).HasColumnType("money");
            entity.Property(e => e.MaLoaiHang).HasMaxLength(10);
            entity.Property(e => e.MoTa).HasMaxLength(3000);
            entity.Property(e => e.TenSanPham).HasMaxLength(200);
            entity.Property(e => e.TrongLuong).HasMaxLength(50);

            entity.HasOne(d => d.MaLoaiHangNavigation).WithMany(p => p.TSanPhams)
                .HasForeignKey(d => d.MaLoaiHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tSanPham_tLoaiHang");
        });

        modelBuilder.Entity<TSanPhamGioHang>(entity =>
        {
            entity.HasKey(e => e.MaSanPhamGioHang).HasName("PK__tSanPham__9146F8331FC9360D");

            entity.ToTable("tSanPhamGioHang");

            entity.Property(e => e.DonGiaBan).HasColumnType("money");
            entity.Property(e => e.MaSanPham).HasMaxLength(10);
            entity.Property(e => e.TongTienSanPham).HasColumnType("money");

            entity.HasOne(d => d.MaGioHangNavigation).WithMany(p => p.TSanPhamGioHangs)
                .HasForeignKey(d => d.MaGioHang)
                .HasConstraintName("FK_tSanPhamGioHang_tGioHang");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.TSanPhamGioHangs)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK_tSanPhamGioHang_tSanPham");
        });

        modelBuilder.Entity<TTaiKhoan>(entity =>
        {
            entity.HasKey(e => e.TenDangNhap);

            entity.ToTable("tTaiKhoan");

            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
            entity.Property(e => e.LoaiTaiKhoan).HasMaxLength(20);
            entity.Property(e => e.MatKhau).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
