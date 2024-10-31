using Web_QuanLySieuThiNho.Models;
using X.PagedList;

namespace Web_QuanLySieuThiNho.ViewModels
{
    public class ProductViewModel
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
    }
}
