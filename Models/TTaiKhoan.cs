using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_QuanLySieuThiNho.Models;

public partial class TTaiKhoan
{
    [Required]

    public string TenDangNhap { get; set; } = null!;

    public string? MatKhau { get; set; }

    public string? LoaiTaiKhoan { get; set; }

    public virtual ICollection<TKhachHang> TKhachHangs { get; set; } = new List<TKhachHang>();

    public virtual ICollection<TNhanVien> TNhanViens { get; set; } = new List<TNhanVien>();
}
