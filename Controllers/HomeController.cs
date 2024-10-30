using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web_QuanLySieuThiNho.Models;
using Web_QuanLySieuThiNho.ViewModels;
using Web_QuanLySieuThiNho.Models.Authentication;
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
        [Authentication]
        public IActionResult Index(int pageNumber = 1)
        {
            //HttpContext.Session.SetString("username", "user");
            int pageSize = 9;
            var products = _db.TSanPhams.AsNoTracking().Where(x=> x.SoLuong > 0).OrderBy(x => x.TenSanPham);

              PagedList<TSanPham> pagedList = new PagedList<TSanPham>(products, pageNumber, pageSize);

            return View(pagedList);
        }
        [Authentication]
        public IActionResult Category(string categoryId, string sortOrder = "default", int pageNumber = 1)
        {
            int pageSize = 9;
            var filteredProducts = _db.TSanPhams.AsNoTracking()
                .Where(x => x.MaLoaiHang == categoryId && x.SoLuong > 0);
            filteredProducts = sortOrder switch
            {
                "asc" => filteredProducts.OrderBy(x => x.DonGiaBan),
                "desc" => filteredProducts.OrderByDescending(x => x.DonGiaBan),
                _ => filteredProducts.OrderBy(x => x.MaSanPham),
            };
            var pagedList = new PagedList<TSanPham>(filteredProducts, pageNumber, pageSize);

            var lastestProductList = _db.TSanPhams.AsNoTracking()
                .Include(x => x.TChiTietHdns)
                .ThenInclude(x => x.SoHdnNavigation)
                .OrderByDescending(x => x.TChiTietHdns.Max(y => y.SoHdnNavigation.NgayNhap))
                .Where(x => x.MaLoaiHang == categoryId && x.SoLuong > 0)
                .Take(6).ToList();


            var categoryPage = new CategoryPageViewModel(lastestProductList, pagedList);

            var categoryName = _db.TLoaiHangs.FirstOrDefault(x => x.MaLoaiHang == categoryId).TenLoaiHang;

            ViewBag.CategoryName = categoryName;
            ViewBag.maLoaiHang = categoryId;
            ViewBag.SortOrder = sortOrder;
            return View(categoryPage);
        }
        [Authentication]
        public IActionResult Detail(string productId)
        {
            var product = _db.TSanPhams.AsNoTracking().SingleOrDefault(x => x.MaSanPham == productId);

            var categoryID = product.MaLoaiHang;
            var relatedProducts = _db.TSanPhams.AsNoTracking().Where(x => x.MaLoaiHang == categoryID).Take(4).ToList();

            var detailPage = new DetailPageViewModel
            {
                Product = product,
                RelatedProducts = relatedProducts,
            };

            var categoryName = _db.TLoaiHangs.FirstOrDefault(x => x.MaLoaiHang == categoryID).TenLoaiHang;

            ViewBag.CategoryID = categoryID;
            ViewBag.CategoryName = categoryName;

            return View(detailPage);
        }
        [Authentication]
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
