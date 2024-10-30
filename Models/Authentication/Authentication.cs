﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web_QuanLySieuThiNho.Models.Authentication
{
	public class Authentication:ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if(context.HttpContext.Session.GetString("TenDangNhap") == null)
			{
				context.Result = new RedirectToRouteResult(
					new RouteValueDictionary
					{
						{"Controller","Account"},
						{"Action", "Login"}
					}); 
			}
		}
	}
}
