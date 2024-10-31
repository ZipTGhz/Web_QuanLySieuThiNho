using System.ComponentModel.DataAnnotations;
using Web_QuanLySieuThiNho.Models;

namespace Web_QuanLySieuThiNho.ViewModels
{
    public class SanPhamNCCViewModel
    {
        public string MaSanPham { get; set; } = null!;
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
        public string? TenSanPham { get; set; }
       
        public string MaLoaiHang { get; set; } = null!;
        [Required(ErrorMessage = "Đơn giá nhập là bắt buộc.")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá nhập phải là một số dương.")]
        public decimal? DonGiaNhap { get; set; }
        [Required(ErrorMessage = "Đơn giá bán là bắt buộc.")]
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá bán phải là một số dương.")]

        public decimal? DonGiaBan { get; set; }
        [Required(ErrorMessage = "Số lượng là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải là một số nguyên không âm.")]
        public int? SoLuong { get; set; }
        [Required(ErrorMessage = "Trọng lượng là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Trọng lượng không được vượt quá 50 ký tự.")]
        public string? TrongLuong { get; set; }

        public string? MoTa { get; set; }

        public string? AnhSanPham { get; set; }
        public string SoHdn { get; set; } = null!;

        public string MaNcc { get; set; } = null!;

        public string? TenNcc { get; set; }

        public virtual TLoaiHang MaLoaiHangNavigation { get; set; } = null!;

        public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; } = new List<TChiTietHdb>();

        public virtual ICollection<TChiTietHdn> TChiTietHdns { get; set; } = new List<TChiTietHdn>();

    }
}
