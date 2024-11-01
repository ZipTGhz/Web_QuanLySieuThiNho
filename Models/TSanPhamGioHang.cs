using System;
using System.Collections.Generic;

namespace Web_QuanLySieuThiNho.Models;

public partial class TSanPhamGioHang
{
    public int MaSanPhamGioHang { get; set; }

    public int? MaGioHang { get; set; }

    public string? MaSanPham { get; set; }

    public int? SoLuong { get; set; }

    public virtual TGioHang? MaGioHangNavigation { get; set; }

    public virtual TSanPham? MaSanPhamNavigation { get; set; }
}
