using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Web_QuanLySieuThiNho.Models.Authentication
{
	public class AuthorizeAdminAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var session = context.HttpContext.Session;
			var loaiTaiKhoan = session.GetString("LoaiTaiKhoan");

			// Kiểm tra nếu không phải Admin thì chuyển hướng
			if (loaiTaiKhoan != "Admin")
			{
				// Redirect đến trang không có quyền truy cập
				context.Result = new RedirectToActionResult("Index", "Home", null);
			}

			base.OnActionExecuting(context);
		}
	}
}
