namespace Web_QuanLySieuThiNho.ViewModels
{
	public class UserProfileViewModel
	{
		public string? MaKh { get; set; } = null!;

		public string TenDangNhap { get; set; } = null!; 
		public string MatKhau { get; set; } = null!; 

		public string? LoaiTaiKhoan { get; set; }

		public string? TenKh { get; set; } 
		public string? GioiTinh { get; set; } 
		public string? SoDienThoai { get; set; }
		public string? DiaChi { get; set; } 
	}
}
