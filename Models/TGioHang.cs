using System;
using System.Collections.Generic;

namespace Web_QuanLySieuThiNho.Models;

public partial class TGioHang
{
    public int MaGioHang { get; set; }

    public string? TenDangNhap { get; set; }

    public DateTime? NgayTao { get; set; }

    public string? TrangThai { get; set; }

    public decimal? TongTienGioHang { get; set; }

    public virtual ICollection<TSanPhamGioHang> TSanPhamGioHangs { get; set; } = new List<TSanPhamGioHang>();

    public virtual TTaiKhoan? TenDangNhapNavigation { get; set; }
}
