using System;
using System.ComponentModel.DataAnnotations;

namespace Web_QuanLySieuThiNho.ViewModels
{
    public class NhanVienViewModel
    {
      
        public string MaNv { get; set; } = null!;

        [Required(ErrorMessage = "Tên nhân viên là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên nhân viên không được vượt quá 50 ký tự.")]
        public string TenNv { get; set; } = null!;

        [Required(ErrorMessage = "Giới tính là bắt buộc.")]
        public string GioiTinh { get; set; } = null!;

        [Required(ErrorMessage = "Ngày sinh là bắt buộc.")]
        [DataType(DataType.Date, ErrorMessage = "Ngày sinh không đúng định dạng.")]
        public DateTime? NgaySinh { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Số điện thoại phải có từ 10-11 chữ số.")]
        public string SoDienThoai { get; set; } = null!;

        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Địa chỉ không được vượt quá 100 ký tự.")]
        public string DiaChi { get; set; } = null!;

        [Required(ErrorMessage = "Chức vụ là bắt buộc.")]
        public string ChucVu { get; set; } = null!;
        public string TrangThai { get; set; } = null!;
    }
}
