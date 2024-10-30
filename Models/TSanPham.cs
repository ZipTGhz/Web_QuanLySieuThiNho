using System;
using System.Collections.Generic;

namespace Web_QuanLySieuThiNho.Models;

public partial class TSanPham
{
    public string MaSanPham { get; set; } = null!;

    public string? TenSanPham { get; set; }

    public string MaLoaiHang { get; set; } = null!;

    public decimal? DonGiaNhap { get; set; }

    public decimal? DonGiaBan { get; set; }

    public int? SoLuong { get; set; }

    public string? TrongLuong { get; set; }

    public string? MoTa { get; set; }

    public string? AnhSanPham { get; set; }

    public virtual TLoaiHang MaLoaiHangNavigation { get; set; } = null!;

    public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; } = new List<TChiTietHdb>();

    public virtual ICollection<TChiTietHdn> TChiTietHdns { get; set; } = new List<TChiTietHdn>();

    public virtual ICollection<TSanPhamGioHang> TSanPhamGioHangs { get; set; } = new List<TSanPhamGioHang>();
}
