using System.ComponentModel.DataAnnotations;

namespace Web_QuanLySieuThiNho.ViewModels
{
    public class CustomerInfoViewModel
    {
        [Required(ErrorMessage = "Họ không được bỏ trống.")]
        [MaxLength(255)]
        public string HoVaTen { get; set; }

        [Required(ErrorMessage ="Địa chỉ không được bỏ trống.")]
        [MaxLength(500)]
        public string DiaChi { get; set; }

        [Required(ErrorMessage ="Số điện thoại không được bỏ trống.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [MaxLength(50)]
        public string SoDienThoai { get; set; }
        //[RegularExpression(@"/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/", ErrorMessage = "Email bạn nhập không đúng định dạng.")]
        [EmailAddress(ErrorMessage = "Email bạn nhập không đúng định dạng.")]
        public string? Email { get; set; }
        [MaxLength(1000)]
        public string? GhiChu { get; set; }
    }
}
