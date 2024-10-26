using Microsoft.AspNetCore.Mvc;

namespace Web_QuanLySieuThiNho.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Report")]
    public class ReportController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
