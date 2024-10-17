using System;
using System.Collections.Generic;

namespace Web_QuanLySieuThiNho.Models;

public partial class TChiTietHdn
{
    public string SoHdn { get; set; } = null!;

    public string MaSanPham { get; set; } = null!;

    public int? Slnhap { get; set; }

    public virtual TSanPham MaSanPhamNavigation { get; set; } = null!;

    public virtual THoaDonNhap SoHdnNavigation { get; set; } = null!;
}
