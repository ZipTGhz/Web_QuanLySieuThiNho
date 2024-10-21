using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web_QuanLySieuThiNho.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace Web_QuanLySieuThiNho.Controllers
{
    public class HomeController : Controller
    {
        private QlsieuThiNhoContext _db = new QlsieuThiNhoContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int pageNumber = 1)
        {
            HttpContext.Session.SetString("username", "user");
            int pageSize = 9;
            var products = _db.TSanPhams.AsNoTracking().OrderBy(x => x.TenSanPham);

            PagedList<TSanPham> pagedList = new PagedList<TSanPham>(products, pageNumber, pageSize);

            return View(pagedList);
        }
        public IActionResult Category(string categoryId, string sortOrder = "default", int pageNumber = 1)
        {
            int pageSize = 9;

            var filteredProducts = _db.TSanPhams.AsNoTracking()
                .Where(x => x.MaLoaiHang == categoryId);

            filteredProducts = sortOrder switch
            {
                "asc" => filteredProducts.OrderBy(x => x.DonGiaBan),
                "desc" => filteredProducts.OrderByDescending(x => x.DonGiaBan),
                _ => filteredProducts.OrderBy(x => x.MaSanPham),
            };

            var pagedList = new PagedList<TSanPham>(filteredProducts, pageNumber, pageSize);

            ViewBag.maLoaiHang = categoryId;
            ViewBag.SortOrder = sortOrder;
            return View(pagedList);
        }
        public IActionResult Detail(string productId)
        {
            var product = _db.TSanPhams.SingleOrDefault(x => x.MaSanPham == productId);
            return View(product);
        }
        public IActionResult Cart()
        {
            return RedirectToAction("Index", "Cart");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
