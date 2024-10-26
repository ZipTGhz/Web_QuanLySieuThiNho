using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web_QuanLySieuThiNho.Models;
using X.PagedList;

namespace Web_QuanLySieuThiNho.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/Home")]
    public class HomeAdminController : Controller
    {
        private string Anh;
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
        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.LoaiHangList = new SelectList(_db.TLoaiHangs, "MaLoaiHang", "TenLoaiHang");

            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        public IActionResult ThemSanPhamMoi(TSanPham sanpham, IFormFile AnhSanPham)
        {
            Debug.WriteLine($"AnhSanPham: {sanpham.AnhSanPham}");
            string sqlQuery = @"
    SELECT TOP 1 MaSanPham 
    FROM TSanPham
    ORDER BY CAST(SUBSTRING(MaSanPham, 3, LEN(MaSanPham) - 2) AS INT) DESC";

            var maxMaSanPham = _db.TSanPhams
                .FromSqlRaw(sqlQuery)
                .Select(sp => sp.MaSanPham)
                .FirstOrDefault();

            string numberPart = maxMaSanPham.Substring(2); // Lấy phần số sau "SP"

            // Chuyển đổi thành số và tăng lên 1
            if (int.TryParse(numberPart, out int number))
            {
                number++; // Tăng giá trị lên 1

                // Tạo mã sản phẩm mới
                string newMaSanPham;

                // Định dạng mã sản phẩm mới theo yêu cầu
                if (number < 10)
                {
                    newMaSanPham = $"SP00{number}"; // 1 số => 2 số 0
                }
                else if (number < 100)
                {
                    newMaSanPham = $"SP0{number}"; // 2 số => 1 số 0
                }
                else
                {
                    newMaSanPham = $"SP{number}"; // 3 số => không cần số 0
                }
            sanpham.MaSanPham = newMaSanPham;
            }
            ViewBag.LoaiHangList = new SelectList(_db.TLoaiHangs, "MaLoaiHang", "TenLoaiHang");
            ModelState.Remove("MaLoaiHangNavigation");
            ModelState.Remove("MaSanPham");



            // Lấy loại hàng từ CSDL
            string maLoaiHang = sanpham.MaLoaiHang;

            // Ghi lại giá trị để kiểm tra

            // Tìm loại hàng tương ứng trong cơ sở dữ liệu
            var loaiHang = _db.TLoaiHangs.FirstOrDefault(lh => lh.MaLoaiHang == maLoaiHang);


            // Kiểm tra xem loại hàng có tồn tại hay không
            if (loaiHang == null)
            {
                Debug.WriteLine($"Mã loại hàng không tồn tại");
              
            }
            else
            {
                // Gán giá trị cho MaLoaiHangNavigation
                sanpham.MaLoaiHangNavigation = loaiHang;
                Debug.WriteLine($"MaLoaiHangNavigation: {sanpham.MaLoaiHangNavigation}");

            }

            

            if (ModelState.IsValid)
            {
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductImages");

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Tạo tên file mới (bạn có thể thay đổi logic đặt tên file nếu cần)
                var fileExtension = Path.GetExtension(AnhSanPham.FileName); // Ví dụ: ".png", ".jpg", v.v.

                // Tạo tên file mới bằng mã sản phẩm, giữ nguyên phần mở rộng
                var fileName = $"{sanpham.MaSanPham}{fileExtension}";  // Tạo tên duy nhất
                var filePath = Path.Combine(uploadFolder, fileName);

                // Lưu ảnh vào thư mục đã chỉ định
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                     AnhSanPham.CopyToAsync(stream); 
                }
                sanpham.AnhSanPham = fileName;
                _db.TSanPhams.Add(sanpham);
                _db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");

            }                  
            return View(sanpham);
        }

        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(string masp)
        {

            ViewBag.LoaiHangList = new SelectList(_db.TLoaiHangs, "MaLoaiHang", "TenLoaiHang");
            var sanPham = _db.TSanPhams.Find(masp);
            ViewBag.Anh = sanPham.AnhSanPham;

            return View(sanPham);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        public IActionResult SuaSanPham(TSanPham sanpham, IFormFile AnhSanPham)
        {
            var sanPham1 = _db.TSanPhams
                   .AsNoTracking()
                   .FirstOrDefault(sp => sp.MaSanPham == sanpham.MaSanPham);

            ViewBag.Anh = sanPham1.AnhSanPham;
            if (AnhSanPham != null && AnhSanPham.Length > 0)
            {
                // Xóa ảnh cũ nếu cần
                var oldImagePath = Path.Combine("wwwroot", "ProductImages", sanPham1.AnhSanPham);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                // Lưu ảnh mới
                var newFileName = sanpham.MaSanPham + Path.GetExtension(AnhSanPham.FileName);
                var newPath = Path.Combine("wwwroot", "ProductImages", newFileName);

                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    AnhSanPham.CopyTo(stream);
                }

                sanpham.AnhSanPham = newFileName; // Cập nhật tên file trong sanpham
            }
            else
            {
                sanpham.AnhSanPham = sanPham1.AnhSanPham; // Giữ lại ảnh cũ
            }
            ViewBag.LoaiHangList = new SelectList(_db.TLoaiHangs, "MaLoaiHang", "TenLoaiHang");
            ModelState.Remove("MaSanPham");
            ModelState.Remove("AnhSanPham");
            ModelState.Remove("MaLoaiHangNavigation");

            string maLoaiHang = sanpham.MaLoaiHang;
            Debug.WriteLine($"MaLoaiHang: {sanpham.MaSanPham}");

            // Ghi lại giá trị để kiểm tra

            // Tìm loại hàng tương ứng trong cơ sở dữ liệu
            var loaiHang = _db.TLoaiHangs.FirstOrDefault(lh => lh.MaLoaiHang == maLoaiHang);
            sanpham.MaLoaiHangNavigation = loaiHang;
           
            // Kiểm tra xem loại hàng có tồn tại hay không


            if (ModelState.IsValid)
            {
                
                _db.Update(sanpham);
                _db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");

            }
            return View(sanpham);
        }

        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(string masp)
        {
            var checkSanPhamGioHang=_db.TSanPhamGioHangs.Where(x=>x.MaSanPham == masp).ToList();
            var chiTietHDB = _db.TChiTietHdbs.Where(x => x.MaSanPham == masp).ToList();
            if(checkSanPhamGioHang.Count>0&& chiTietHDB.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "Sản phẩm này không được phép xóa vì đang có trong giỏ hàng hoặc hóa đơn.");
                return View();
            }
            _db.Remove(_db.TSanPhams.Find(masp)); 
            _db.SaveChanges();

            return RedirectToAction("DanhMucSanPham");
 
        }
    }
}
