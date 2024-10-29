using System;
using System.Collections.Generic;

namespace Web_QuanLySieuThiNho.Models;

public partial class TNhanVien
{
    public string MaNv { get; set; } = null!;

    public string? TenDangNhap { get; set; }

    public string? TenNv { get; set; }

    public string? GioiTinh { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public string? ChucVu { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual ICollection<THoaDonBan> THoaDonBans { get; set; } = new List<THoaDonBan>();

    public virtual ICollection<THoaDonNhap> THoaDonNhaps { get; set; } = new List<THoaDonNhap>();

    public virtual TTaiKhoan? TenDangNhapNavigation { get; set; }
}
