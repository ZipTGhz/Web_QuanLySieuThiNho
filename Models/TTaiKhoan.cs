using System;
using System.Collections.Generic;

namespace Web_QuanLySieuThiNho.Models;

public partial class TTaiKhoan
{
    public string TenDangNhap { get; set; } = null!;

    public string? MatKhau { get; set; }

    public string? LoaiTaiKhoan { get; set; }

    public virtual ICollection<TGioHang> TGioHangs { get; set; } = new List<TGioHang>();

    public virtual ICollection<TKhachHang> TKhachHangs { get; set; } = new List<TKhachHang>();

    public virtual ICollection<TNhanVien> TNhanViens { get; set; } = new List<TNhanVien>();
}
