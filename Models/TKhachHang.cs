using System;
using System.Collections.Generic;

namespace Web_QuanLySieuThiNho.Models;

public partial class TKhachHang
{
    public string MaKh { get; set; } = null!;

    public string? TenDangNhap { get; set; }

    public string? TenKh { get; set; }

    public string? GioiTinh { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public virtual ICollection<THoaDonBan> THoaDonBans { get; set; } = new List<THoaDonBan>();

    public virtual TTaiKhoan? TenDangNhapNavigation { get; set; }
}
