using System.ComponentModel.DataAnnotations;
using Web_QuanLySieuThiNho.Models;
using X.PagedList;

namespace Web_QuanLySieuThiNho.ViewModels
{
    public class CustomerViewModel
    {
        public string MaKh { get; set; } = null!;

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập không được vượt quá 50 ký tự.")]
        public string? TenDangNhap { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên khách hàng không được vượt quá 50 ký tự.")]
        public string? TenKh { get; set; }

        public string? GioiTinh { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Số điện thoại phải có từ 10-11 chữ số.")]
        public string? SoDienThoai { get; set; }

        public string? DiaChi { get; set; }
    }
}
