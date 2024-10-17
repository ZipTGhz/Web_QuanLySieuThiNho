﻿using System;
using System.Collections.Generic;

namespace Web_QuanLySieuThiNho.Models;

public partial class TNhaCungCap
{
    public string MaNcc { get; set; } = null!;

    public string? TenNcc { get; set; }

    public virtual ICollection<THoaDonNhap> THoaDonNhaps { get; set; } = new List<THoaDonNhap>();
}
