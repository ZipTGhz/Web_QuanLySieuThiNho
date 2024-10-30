using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web_QuanLySieuThiNho.Models;
using Web_QuanLySieuThiNho.Models.Authentication;
using X.PagedList;

namespace Web_QuanLySieuThiNho.Controllers
{
    public class HomeController : Controller
    {
        private QlsieuThiNhoContext db = new QlsieuThiNhoContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        

        public IActionResult Index(int pageNumber = 1)
        {
            int pageSize = 12;
            var products = db.TSanPhams.AsNoTracking().OrderBy(x => x.TenSanPham);

              PagedList<TSanPham> pagedList = new PagedList<TSanPham>(products, pageNumber, pageSize);

            return View(pagedList);
        }
        [Authentication]
        public IActionResult Category(string categoryId, int pageNumber = 1)
        {
            int pageSize = 12;

            var filteredProducts = db.TSanPhams.AsNoTracking()
                .Where(x => x.MaLoaiHang == categoryId)
                .OrderBy(x => x.TenSanPham);

            PagedList<TSanPham> pagedList = new PagedList<TSanPham>(filteredProducts, pageNumber, pageSize);

            ViewBag.maLoaiHang = categoryId;
            return View(pagedList);
        }

        public IActionResult Detail(string productId)
        {
            var product = db.TSanPhams.SingleOrDefault(x => x.MaSanPham == productId);
            return View(product);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
