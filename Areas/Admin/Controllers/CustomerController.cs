using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_QuanLySieuThiNho.Models;
using X.PagedList;

namespace Web_QuanLySieuThiNho.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Route("Admin/User")]
    public class CustomerController : Controller
    {
        private QlsieuThiNhoContext _db = new QlsieuThiNhoContext();

        public IActionResult Index()
        {
            return View();
        }

        [Route("QuanLiKhachHang")]
        public IActionResult QuanLiKhachHang(int pageNumber = 1)
        {
            int pageSize = 6;
            var users = _db.TKhachHangs.AsNoTracking().OrderBy(x => x.MaKh);
            PagedList<TKhachHang> pagedList = new PagedList<TKhachHang>(users, pageNumber, pageSize);
            return View(pagedList);
        }
       

        [Route("SuaKhachHang")]
        [HttpGet]
        public IActionResult SuaKhachHang(string maKh)
        {
            var user = _db.TKhachHangs.Find(maKh);

            return View(user);
        }
        [Route("SuaKhachHang")]
        [HttpPost]
        public IActionResult SuaKhachHang(TKhachHang customer)
        {


            ModelState.Remove("MaKh");


            // Kiểm tra xem loại hàng có tồn tại hay không


            if (ModelState.IsValid)
            {

                _db.Update(customer);
                _db.SaveChanges();
                return RedirectToAction("QuanLiKhachHang");

            }
            return View(customer);
        }

    }
}
