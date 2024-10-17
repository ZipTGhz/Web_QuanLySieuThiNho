using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_QuanLySieuThiNho.Models;
using X.PagedList;

namespace Web_QuanLySieuThiNho.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/Home")]
    public class HomeAdminController : Controller
    {
        private QlsieuThiNhoContext _db = new QlsieuThiNhoContext();
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("DanhMucSanPham")]
        public IActionResult DanhMucSanPham(int pageNumber = 1)
        {
            int pageSize = 6;
            var products = _db.TSanPhams.AsNoTracking().OrderBy(x => x.DonGiaBan);
            PagedList<TSanPham> pagedList = new PagedList<TSanPham>(products, pageNumber, pageSize);
            return View(pagedList);
        }

    }
}
