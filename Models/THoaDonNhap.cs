﻿using System;
using System.Collections.Generic;

namespace Web_QuanLySieuThiNho.Models;

public partial class THoaDonNhap
{
    public string SoHdn { get; set; } = null!;

    public string? MaNcc { get; set; }

    public string? MaNv { get; set; }

    public DateTime? NgayNhap { get; set; }

    public virtual TNhaCungCap? MaNccNavigation { get; set; }

    public virtual TNhanVien? MaNvNavigation { get; set; }

    public virtual ICollection<TChiTietHdn> TChiTietHdns { get; set; } = new List<TChiTietHdn>();
}
