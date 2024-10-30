using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web_QuanLySieuThiNho.Extensions;
using Web_QuanLySieuThiNho.Models;
using Web_QuanLySieuThiNho.Models.Authentication;
using Web_QuanLySieuThiNho.Util;
using Web_QuanLySieuThiNho.ViewModels;

namespace Web_QuanLySieuThiNho.Controllers
{
    public class CartController : Controller
    {
        private QlsieuThiNhoContext _db = new QlsieuThiNhoContext();
        [Authentication]
        public IActionResult Index()
        {
            var cart = GetCartIncludedExtended();
            HttpContext.Session.SetInt32("CartCount", cart.TSanPhamGioHangs.Sum(item => item.SoLuong) ?? 0);
            return View(cart);
        }
        private TGioHang GetCart()
        {
            string username = HttpContext.Session.GetString("username");
            string customerID = Helpers.GetCustomerID(username);
            var cart = _db.TGioHangs.FirstOrDefault(x => x.MaKh == customerID && x.TrangThai == "ChuaThanhToan");
            if (cart == null)
            {
                cart = new TGioHang()
                {
                    MaKh = customerID,
                    NgayTao = DateTime.Now,
                    TrangThai = "ChuaThanhToan",
                    TongTienGioHang = 0,
                };
                _db.TGioHangs.Add(cart);
                _db.SaveChanges();
            }
            return cart;
        }
        private TGioHang GetCartIncluded()
        {
            string username = HttpContext.Session.GetString("username");
            string customerID = Helpers.GetCustomerID(username);
            var cart = _db.TGioHangs.FirstOrDefault(x => x.MaKh == customerID && x.TrangThai == "ChuaThanhToan");
            if (cart == null)
            {
                cart = new TGioHang()
                {
                    MaKh = customerID,
                    NgayTao = DateTime.Now,
                    TrangThai = "ChuaThanhToan",
                    TongTienGioHang = 0,
                };
                _db.TGioHangs.Add(cart);
                _db.SaveChanges();
                return cart;
            }
            var cartIncluded = _db.TGioHangs
                .Include(x => x.TSanPhamGioHangs)
                .FirstOrDefault(x => x.MaKh == customerID && x.TrangThai == "ChuaThanhToan");
            return cartIncluded;
        }
        private TGioHang GetCartIncludedExtended()
        {
            string username = HttpContext.Session.GetString("username");
            string customerID = Helpers.GetCustomerID(username);
            var cart = _db.TGioHangs.FirstOrDefault(x => x.MaKh == customerID && x.TrangThai == "ChuaThanhToan");
            if (cart == null)
            {
                cart = new TGioHang()
                {
                    MaKh = customerID,
                    NgayTao = DateTime.Now,
                    TrangThai = "ChuaThanhToan",
                    TongTienGioHang = 0,
                };
                _db.TGioHangs.Add(cart);
                _db.SaveChanges();
                return cart;
            }
            var cartIncluded = _db.TGioHangs
                .Include(x => x.TSanPhamGioHangs)
                .ThenInclude(x => x.MaSanPhamNavigation)
                .FirstOrDefault(x => x.MaKh == customerID && x.TrangThai == "ChuaThanhToan");
            return cartIncluded;
        }
        //[Authentication]
        public IActionResult LoadCartOnce()
        {
            var username = HttpContext.Session.GetString("username");
            var cartLoaded = HttpContext.Session.GetString("CartLoaded");
            if (username == null || cartLoaded != null)
                return Json(new { isCartLoaded = true });
            var cart = GetCartIncluded();
            HttpContext.Session.SetInt32("CartCount", cart.TSanPhamGioHangs.Sum(item => item.SoLuong) ?? 0);
            HttpContext.Session.SetString("TotalAmountCart", (cart.TongTienGioHang ?? 0).ToString("#,##0"));
            return Json(new
            {
                isCartLoaded = false,
                cartCount = cart.TSanPhamGioHangs.Sum(x => x.SoLuong),
                totalAmountCart = cart.TongTienGioHang.Value.ToString("#,##0"),
            });
        }
        [HttpPost]
        [Authentication]
        public IActionResult AddToCart(string productId, int quantity)
        {
            var cart = GetCartIncluded();
            var cartItem = cart.TSanPhamGioHangs.FirstOrDefault(x => x.MaGioHang == cart.MaGioHang && x.MaSanPham == productId);
            var product = _db.TSanPhams.FirstOrDefault(x => x.MaSanPham == productId);
            if (cartItem == null)
            {
                cartItem = new TSanPhamGioHang()
                {
                    MaGioHang = cart.MaGioHang,
                    MaSanPham = productId,
                    SoLuong = quantity,
                    DonGiaBan = product?.DonGiaBan ?? 0,
                    TongTienSanPham = (product?.DonGiaBan ?? 0) * quantity,
                };
                _db.TSanPhamGioHangs.Add(cartItem);
            }
            else
            {
                if (cartItem.SoLuong + quantity > product.SoLuong)
                {
                    return Json(new
                    {
                        success = false,
                    });
                };
                cartItem.SoLuong += quantity;
                cartItem.TongTienSanPham = cartItem.DonGiaBan * cartItem.SoLuong;
            }
            _db.SaveChanges();
            cart.TongTienGioHang = cart.TSanPhamGioHangs.Sum(x => x.TongTienSanPham);
            _db.SaveChanges();
            HttpContext.Session.SetInt32("CartCount", cart.TSanPhamGioHangs.Sum(item => item.SoLuong) ?? 0);
            HttpContext.Session.SetString("TotalAmountCart", (cart.TongTienGioHang ?? 0).ToString("#,##0"));
            return Json(new
            {
                success = true,
                cartCount = cart.TSanPhamGioHangs.Sum(x => x.SoLuong),
                totalAmountCart = cart.TongTienGioHang.Value.ToString("#,##0"),
            });
        }
        [HttpPost]
        [Authentication]
        public IActionResult UpdateCartItemQuantity(string productId, int quantity)
        {
            var cart = GetCartIncluded();
            var cartItem = _db.TSanPhamGioHangs.FirstOrDefault(x => x.MaGioHang == cart.MaGioHang && x.MaSanPham == productId);
            if (cartItem != null)
            {
                cartItem.SoLuong = quantity;
                cartItem.TongTienSanPham = cartItem.DonGiaBan * quantity;
            }
            _db.SaveChanges();
            cart.TongTienGioHang = _db.TSanPhamGioHangs.Where(x => x.MaGioHang == cart.MaGioHang).Sum(x => x.TongTienSanPham);
            _db.SaveChanges();
            HttpContext.Session.SetInt32("CartCount", cart.TSanPhamGioHangs.Sum(item => item.SoLuong) ?? 0);
            HttpContext.Session.SetString("TotalAmountCart", (cart.TongTienGioHang ?? 0).ToString("#,##0"));
            return Json(new
            {
                cartCount = cart.TSanPhamGioHangs.Sum(x => x.SoLuong),
                totalAmountCartItem = cartItem.TongTienSanPham.Value.ToString("#,##0"),
                totalAmountCart = cart.TongTienGioHang.Value.ToString("#,##0")
            });
        }
        [HttpPost]
        [Authentication]
        public IActionResult RemoveCartItem(string productId)
        {
            var cart = GetCartIncluded();
            var cartItem = _db.TSanPhamGioHangs.FirstOrDefault(x => x.MaGioHang == cart.MaGioHang && x.MaSanPham == productId);
            if (cartItem != null)
            {
                _db.TSanPhamGioHangs.Remove(cartItem);
                _db.SaveChanges();
                cart.TongTienGioHang = _db.TSanPhamGioHangs.Where(x => x.MaGioHang == cart.MaGioHang).Sum(x => x.TongTienSanPham);
                _db.SaveChanges();
            }
            HttpContext.Session.SetString("TotalAmountCart", (cart.TongTienGioHang ?? 0).ToString("#,##0"));
            HttpContext.Session.SetInt32("CartCount", cart.TSanPhamGioHangs.Sum(item => item.SoLuong) ?? 0);
            return Json(new
            {
                cartCount = cart.TSanPhamGioHangs.Sum(x => x.SoLuong),
                totalAmountCart = cart.TongTienGioHang.Value.ToString("#,##0"),
            });
        }
        [Authentication]
        public IActionResult CheckCartBeforeCheckout()
        {
            var cart = GetCart();
            var cartItem = _db.TSanPhamGioHangs.FirstOrDefault(x => x.MaGioHang == cart.MaGioHang);
            bool cartHasItems = cartItem != null;
            return Json(new { cartHasItems });
        }
        [Authentication]
        public IActionResult Checkout()
        {
            CheckoutPageViewModel model = GetCartInfo();
            return View(model);
        }
        private CheckoutPageViewModel GetCartInfo()
        {
            List<CheckoutCartItemViewModel> checkoutCartItems = new List<CheckoutCartItemViewModel>();
            var cart = GetCartIncludedExtended();
            var cartItems = cart.TSanPhamGioHangs;
            foreach (var item in cartItems)
            {
                var checkoutCartItem = new CheckoutCartItemViewModel()
                {
                    MaSanPham = item.MaSanPham ?? "",
                    TenSanPham = item.MaSanPhamNavigation?.TenSanPham ?? "",
                    SoLuong = item.SoLuong ?? 0,
                    TongTien = item.TongTienSanPham ?? 0,
                };
                checkoutCartItems.Add(checkoutCartItem);
            }
            CheckoutPageViewModel model = new CheckoutPageViewModel()
            {
                CartItems = checkoutCartItems
            };
            return model;
        }
        [HttpPost]
        [Authentication]
        public IActionResult Checkout(CheckoutPageViewModel model)
        {
            if (ModelState.IsValid)
            {
                InsertSaleInvoice(model);
                ClearCart();
                TempData["CheckoutSuccessMessage"] = "Thanh toán thành công! Cảm ơn bạn đã mua hàng.";
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        private void InsertSaleInvoice(CheckoutPageViewModel model)
        {
            var soHDB = _db.THoaDonBans.OrderBy(x => x.SoHdb).LastOrDefault()?.SoHdb;
            if (soHDB == null)
                soHDB = Helpers.GenerateID("HDB", 5);
            else
                soHDB = Helpers.GenerateNextID(soHDB, 3);

            string? employeeID = null;
            int employeeCount = _db.TNhanViens.Count() - 1;
            if (employeeCount != 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(employeeCount);
                employeeID = _db.TNhanViens.Skip(randomIndex).FirstOrDefault()?.MaNv;
            }

            string? username = HttpContext.Session.GetString("username");
            string customerID = Helpers.GetCustomerID(username);
            var customer = _db.TKhachHangs.FirstOrDefault(x => x.MaKh == customerID);

            var saleInvoice = new THoaDonBan()
            {
                SoHdb = soHDB,
                MaNv = employeeID,
                MaKh = customerID,
                NgayBan = DateTime.Now,
                SoSanPham = HttpContext.Session.GetInt32("CartCount"),
            };
            _db.THoaDonBans.Add(saleInvoice);
            _db.SaveChanges();
            InsertSaleInvoiceDetail(soHDB, model.CartItems);
        }
        private void InsertSaleInvoiceDetail(string soHDB, List<CheckoutCartItemViewModel> checkoutCartItems)
        {
            foreach (var cartItem in checkoutCartItems)
            {
                _db.TSanPhams.First(x => x.MaSanPham == cartItem.MaSanPham).SoLuong -= cartItem.SoLuong;
                var saleInvoiceDetail = new TChiTietHdb()
                {
                    SoHdb = soHDB,
                    MaSanPham = cartItem.MaSanPham,
                    Slban = cartItem.SoLuong,
                    ThanhTien = cartItem.TongTien,
                };
                _db.TChiTietHdbs.Add(saleInvoiceDetail);
            }
            _db.SaveChanges();
        }
        private void ClearCart()
        {
            HttpContext.Session.Remove("CartCount");
            HttpContext.Session.Remove("TotalAmountCart");
            var cart = GetCart();
            cart.TrangThai = "DaThanhToan";
            _db.SaveChanges();
        }

    }
}
