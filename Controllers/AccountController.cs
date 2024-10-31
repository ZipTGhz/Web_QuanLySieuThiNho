using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Web_QuanLySieuThiNho.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Web_QuanLySieuThiNho.ViewModels;

namespace Web_QuanLySieuThiNho.Controllers
{
	public class AccountController : Controller
	{
		private QlsieuThiNhoContext _db = new QlsieuThiNhoContext();
		//login
		[HttpGet]
		public IActionResult Login()
		{
			if(HttpContext.Session.GetString("username") == null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}
		[HttpPost]
		public IActionResult Login(TTaiKhoan tk)
		{
			if (HttpContext.Session.GetString("username") == null)
			{
				var u = _db.TTaiKhoans
							.Where(x => x.TenDangNhap.Equals(tk.TenDangNhap) && x.MatKhau.Equals(tk.MatKhau))
							.FirstOrDefault();
				if (u != null)
				{
					// Lưu thông tin vào session
					HttpContext.Session.SetString("TenDangNhap", tk.TenDangNhap.ToString());
					HttpContext.Session.SetString("LoaiTaiKhoan", u.LoaiTaiKhoan.ToString());

					// Chuyển hướng tùy thuộc vào loại tài khoản
					if (u.LoaiTaiKhoan == "Admin")
					{
						return RedirectToAction("DanhMucSanPham", "HomeAdmin", new { area = "Admin" });
					}
					else
					{
						// Chuyển hướng đến trang khác cho người dùng không phải Admin
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
				}
			}
			else
			{
				//Không xoá đoạn dưới.
				//var loaiTaiKhoan = HttpContext.Session.GetString("LoaiTaiKhoan");
				//if (loaiTaiKhoan == "Admin")
				//{
				//	return RedirectToAction("DanhMucSanPham", "HomeAdmin", new { area = "Admin" });
				//}
				//else
				{
					return RedirectToAction("Index", "Home");
				}
			}
			return View();
		}


		// GET: Account/SignUp
		[HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        // POST: Account/SignUp
        [HttpPost]
        public IActionResult SignUp(TTaiKhoan tk)
        {
			if (ModelState.IsValid)
			{
				if (_db.TTaiKhoans.Any(x => x.TenDangNhap == tk.TenDangNhap))
				{
					// Nếu tên đăng nhập đã tồn tại
					ViewBag.ErrorMessage = "Tên đăng nhập đã tồn tại, vui lòng chọn tên khác.";
					return View();
				}
				tk.LoaiTaiKhoan = "User";
				// Thêm tài khoản mới vào cơ sở dữ liệu
				_db.TTaiKhoans.Add(tk);
				_db.SaveChanges();

				//Tìm mã khách hàng lớn nhất hiện tại trong TKhachHang
				var maxMaKH = _db.TKhachHangs.OrderByDescending(kh=> kh.MaKh).Select(kh=>kh.MaKh).FirstOrDefault();

				//Xử lý tạo mã mới
				int soThuTu = 1;
				if(!string.IsNullOrEmpty(maxMaKH) && maxMaKH.StartsWith("KH"))
				{
					soThuTu = int.Parse(maxMaKH.Substring(2)) + 1;
				}
				var maKH = $"KH{soThuTu:D3}";
				var khachHang = new TKhachHang
				{
					TenDangNhap = tk.TenDangNhap,
					MaKh = maKH
				};

				_db.TKhachHangs.Add(khachHang);
				_db.SaveChanges();
				//Đăng ký thành công, lưu phiên đăng nhập
				HttpContext.Session.SetString("TenDangNhap", tk.TenDangNhap.ToString());

				// Chuyển hướng tới trang chủ sau khi đăng ký
				return RedirectToAction("Index", "Home");
			}
			return View(tk);
        }

		public IActionResult LogOut()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("TenDangNhap");
			return RedirectToAction("Index", "Home");
		}

        public IActionResult UserProfile()
        {
			var tenDangNhap = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenDangNhap))
			{
				return RedirectToAction("Login", "Account");
			}

			// truy vấn tài khoản TKhachHang và TTaiKhoan
			var taiKhoan = _db.TTaiKhoans.FirstOrDefault(tk=>tk.TenDangNhap == tenDangNhap);
			var khachHang = _db.TKhachHangs.FirstOrDefault(kh=>kh.TenDangNhap == tenDangNhap);
			if(taiKhoan == null || khachHang == null)
			{
				return NotFound();
			}

			//Khởi tạo viewmodel và gán giá trị
			var userProfileViewModel = new UserProfileViewModel
			{
				MaKh = khachHang.MaKh,
				TenDangNhap = taiKhoan.TenDangNhap,
				MatKhau = taiKhoan.MatKhau,
				LoaiTaiKhoan = taiKhoan.LoaiTaiKhoan,
				TenKh = khachHang.TenKh,
				GioiTinh = khachHang.GioiTinh,
				SoDienThoai = khachHang.SoDienThoai,
				DiaChi = khachHang.DiaChi
			};
			//tra ve view voi ViewModel
			return View(userProfileViewModel);
        }

		[HttpPost]
		public IActionResult Save(UserProfileViewModel model)
		{
			var tenDangNhap = HttpContext.Session.GetString("username");

			if (string.IsNullOrEmpty(tenDangNhap))
			{
				return RedirectToAction("Login", "Account");
			}

			if (ModelState.IsValid)
			{
				// Tìm bản ghi khách hàng trong TKhachHang theo TenDangNhap
				var khachHang = _db.TKhachHangs.FirstOrDefault(kh => kh.TenDangNhap == tenDangNhap);
				var taiKhoan = _db.TTaiKhoans.FirstOrDefault(tk => tk.TenDangNhap == tenDangNhap);

				if (khachHang != null && taiKhoan != null)
				{
					// Cập nhật thông tin trong TKhachHang và TTaiKhoan
					khachHang.TenKh = model.TenKh;
					khachHang.GioiTinh = model.GioiTinh;
					khachHang.SoDienThoai = model.SoDienThoai;
					khachHang.DiaChi = model.DiaChi;

					// Cập nhật TTaiKhoan nếu có thông tin thay đổi (ví dụ: mật khẩu)
					taiKhoan.MatKhau = model.MatKhau;

					_db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

					// Chuyển hướng về trang UserProfile sau khi lưu thành công
					return RedirectToAction("UserProfile");
				}
				else
				{
					ModelState.AddModelError("", "Không tìm thấy khách hàng hoặc tài khoản.");
				}
			}
			// Nếu có lỗi, trả lại view với model hiện tại để hiển thị lỗi
			return View("UserProfile", model);
		}

	}
}
