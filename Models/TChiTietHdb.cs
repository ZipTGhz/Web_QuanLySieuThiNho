using System;
using System.Collections.Generic;

namespace Web_QuanLySieuThiNho.Models;

public partial class TChiTietHdb
{
    public string SoHdb { get; set; } = null!;

    public string MaSanPham { get; set; } = null!;

    public int? Slban { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual TSanPham MaSanPhamNavigation { get; set; } = null!;

    public virtual THoaDonBan SoHdbNavigation { get; set; } = null!;
}
