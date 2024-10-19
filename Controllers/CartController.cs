using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web_QuanLySieuThiNho.Extensions;
using Web_QuanLySieuThiNho.Models;

namespace Web_QuanLySieuThiNho.Controllers
{
    public class CartController : Controller
    {
        private QlsieuThiNhoContext _db = new QlsieuThiNhoContext();
        public IActionResult Index()
        {
            TGioHang cart;
            if (HttpContext.Session.GetString("username") != null)
            {
                string username = HttpContext.Session.GetString("UserName");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                cart = _db.TGioHangs
                    .Include(c => c.TSanPhamGioHangs)
                    .ThenInclude(sp => sp.MaSanPhamNavigation)
                    .FirstOrDefault(u => u.TenDangNhap == username && u.TrangThai == "ChuaThanhToan");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                if (cart == null)
                {
                    cart = new TGioHang()
                    {
                        TenDangNhap = username,
                        NgayTao = DateTime.Now,
                        TrangThai = "ChuaThanhToan",
                        TongTienGioHang = 0,
                    };
                    _db.TGioHangs.Add(cart);
                    _db.SaveChanges();
                }
            }
            else
            {
                cart = HttpContext.Session.GetObjectFromJson<TGioHang>("Cart");
                if (cart == null)
                {
                    cart = new TGioHang()
                    {
                        NgayTao = DateTime.Now,
                        TrangThai = "ChuaThanhToan",
                        TongTienGioHang = 0,
                    };
                }
            }
            HttpContext.Session.SetInt32("CartCount", cart.TSanPhamGioHangs.Sum(item => item.SoLuong) ?? 0);
            return View(cart);

        }
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(string productId, int quantity = 1)
        {
            if (HttpContext.Session.GetString("username") != null)
                return AddToCartLoggedInUser(productId, quantity);
            return AddToCartSession(productId, quantity);
        }

        private IActionResult AddToCartLoggedInUser(string productId, int quantity = 1)
        {
            var username = HttpContext.Session.GetString("username");
            var cart = _db.TGioHangs.FirstOrDefault(x => x.TenDangNhap == username && x.TrangThai == "ChuaThanhToan");
            if (cart == null)
            {
                cart = new TGioHang()
                {
                    TenDangNhap = username,
                    NgayTao = DateTime.Now,
                    TrangThai = "ChuaThanhToan",
                    TongTienGioHang = 0
                };
                _db.TGioHangs.Add(cart);
                _db.SaveChanges();
            }

            var cartItem = _db.TSanPhamGioHangs.FirstOrDefault(x => x.MaGioHang == cart.MaGioHang && x.MaSanPham == productId);
            var product = _db.TSanPhams.FirstOrDefault(p => p.MaSanPham == productId);
            if (cartItem == null)
            {
                cartItem = new TSanPhamGioHang()
                {
                    MaGioHang = cart.MaGioHang,
                    MaSanPham = productId,
                    SoLuong = quantity,
                    DonGiaBan = product?.DonGiaBan,
                    TongTienSanPham = (product?.DonGiaBan ?? 0) * quantity
                };
            }
            else
            {
                cartItem.SoLuong += quantity;
                cartItem.TongTienSanPham = cartItem.DonGiaBan * cartItem.SoLuong;
            }

            cart.TongTienGioHang = _db.TSanPhamGioHangs.Where(x => x.MaGioHang == cart.MaGioHang).Sum(s => s.TongTienSanPham);
            _db.SaveChanges();
            HttpContext.Session.SetInt32("CartCount", cart.TSanPhamGioHangs.Sum(item => item.SoLuong) ?? 0);
            return RedirectToAction("Index");
        }
        private IActionResult AddToCartSession(string productId, int quantity = 1)
        {
            var cart = HttpContext.Session.GetObjectFromJson<TGioHang>("Cart");
            if (cart == null)
            {
                cart = new TGioHang()
                {
                    TenDangNhap = null,
                    NgayTao = DateTime.Now,
                    TrangThai = "ChuaThanhToan",
                    TongTienGioHang = 0
                };
            }

            var cartItems = cart.TSanPhamGioHangs;
            var cartItem = cartItems.FirstOrDefault(x => x.MaSanPham == productId);
            var product = _db.TSanPhams.FirstOrDefault(p => p.MaSanPham == productId);
            if (cartItem == null)
            {
                cartItem = new TSanPhamGioHang()
                {
                    MaSanPham = productId,
                    SoLuong = quantity,
                    DonGiaBan = product?.DonGiaBan,
                    TongTienSanPham = (product?.DonGiaBan ?? 0) * quantity,
                    MaSanPhamNavigation = product
                };
                cartItems.Add(cartItem);
            }
            else
            {
                cartItem.SoLuong += quantity;
                cartItem.TongTienSanPham = cartItem.DonGiaBan * cartItem.SoLuong;
            }
            cart.TongTienGioHang = cart.TSanPhamGioHangs.Sum(x => x.TongTienSanPham);

            HttpContext.Session.SetObjectAsJson("Cart", cart);
            HttpContext.Session.SetInt32("CartCount", cart.TSanPhamGioHangs.Sum(item => item.SoLuong) ?? 0);
            return RedirectToAction("Index");
        }
        public void MigrateSessionCartToDatabase(string username)
        {
            var sessionCart = HttpContext.Session.GetObjectFromJson<List<TSanPhamGioHang>>("Cart");

            if (sessionCart != null)
            {
                var cart = _db.TGioHangs.FirstOrDefault(x => x.TenDangNhap == username && x.TrangThai == "ChuaThanhToan");
                if (cart == null)
                {
                    cart = new TGioHang()
                    {
                        TenDangNhap = username,
                        NgayTao = DateTime.Now,
                        TrangThai = "ChuaThanhToan",
                        TongTienGioHang = 0
                    };
                    _db.TGioHangs.Add(cart);
                    _db.SaveChanges();
                }

                foreach (var item in sessionCart)
                {
                    var cartItem = new TSanPhamGioHang()
                    {
                        MaGioHang = cart.MaGioHang,
                        MaSanPham = item.MaSanPham,
                        SoLuong = item.SoLuong,
                        DonGiaBan = item.DonGiaBan,
                        TongTienSanPham = item.TongTienSanPham
                    };
                    _db.TSanPhamGioHangs.Add(cartItem);
                }

                cart.TongTienGioHang = sessionCart.Sum(x => x.TongTienSanPham);
                _db.SaveChanges();

                HttpContext.Session.Remove("Cart");
            }
        }
        [HttpPost]
        public JsonResult UpdateCart(string productId, int quantity)
        {
            return UpdateCartSession(productId, quantity);
        }
        private JsonResult UpdateCartSession(string productId, int quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<TGioHang>("Cart");
            var cartItem = cart.TSanPhamGioHangs.FirstOrDefault(x => x.MaSanPham == productId);
            if (cartItem != null)
            {
                cartItem.SoLuong = quantity;
                cartItem.TongTienSanPham = cartItem.DonGiaBan * quantity;
            }
            cart.TongTienGioHang = cart.TSanPhamGioHangs.Sum(x => x.TongTienSanPham);
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return Json(new
            {
                totalAmountCartItem = cartItem.TongTienSanPham.Value.ToString("#,##0"),
                totalAmountCart = cart.TongTienGioHang.Value.ToString("#,##0")
            });
        }
    }
}
