using System;
using System.Collections.Generic;

namespace Web_QuanLySieuThiNho.Models;

public partial class TLoaiHang
{
    public string MaLoaiHang { get; set; } = null!;

    public string? TenLoaiHang { get; set; }

    public virtual ICollection<TSanPham> TSanPhams { get; set; } = new List<TSanPham>();
}
