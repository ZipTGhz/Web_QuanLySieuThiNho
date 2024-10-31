using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web_QuanLySieuThiNho.Models;
using Web_QuanLySieuThiNho.ViewModels;
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
            ViewBag.NCCList = new SelectList(_db.TNhaCungCaps, "MaNcc", "TenNcc");

            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        public IActionResult ThemSanPhamMoi(SanPhamNCCViewModel viewModel, IFormFile AnhSanPham)
        {

            string sqlQuery = @"
                SELECT TOP 1 MaSanPham 
                FROM TSanPham
                ORDER BY CAST(SUBSTRING(MaSanPham, 3, LEN(MaSanPham) - 2) AS INT) DESC";

            string sqlQueryMcc = @"
                SELECT TOP 1 SoHDN 
                FROM THoaDonNhap
                ORDER BY CAST(SUBSTRING(SoHDN, 3, LEN(SoHDN) - 2) AS INT) DESC";

            var maxMaSanPham = _db.TSanPhams
                .FromSqlRaw(sqlQuery)
                .Select(sp => sp.MaSanPham)
                .FirstOrDefault();
            var maxMaNcc = _db.THoaDonNhaps
               .FromSqlRaw(sqlQueryMcc)
               .Select(sp => sp.SoHdn)
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
                viewModel.MaSanPham = newMaSanPham;
            }

            string numberPartNCC = maxMaNcc.Substring(2); // Lấy phần số sau "SP"

            // Chuyển đổi thành số và tăng lên 1
            if (int.TryParse(numberPartNCC, out int numberHdn))
            {
                numberHdn++; // Tăng giá trị lên 1

                // Tạo mã sản phẩm mới
                string newHDN;

                // Định dạng mã sản phẩm mới theo yêu cầu
                if (number < 10)
                {
                    newHDN = $"HD00{numberHdn}"; // 1 số => 2 số 0
                }
                else if (number < 100)
                {
                    newHDN = $"HD0{numberHdn}"; // 2 số => 1 số 0
                }
                else
                {
                    newHDN = $"HD{numberHdn}"; // 3 số => không cần số 0
                }
                viewModel.SoHdn = newHDN;
            }
            Debug.WriteLine($"MaLoaiHangNavigation: {viewModel.MaNcc}");

            ViewBag.NCCList = new SelectList(_db.TNhaCungCaps, "MaNcc", "TenNcc");
            ViewBag.LoaiHangList = new SelectList(_db.TLoaiHangs, "MaLoaiHang", "TenLoaiHang");
            ModelState.Remove("MaLoaiHangNavigation");
            ModelState.Remove("MaSanPham");
            ModelState.Remove("SoHdn");




            // Lấy loại hàng từ CSDL
            string maLoaiHang = viewModel.MaLoaiHang;

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
                viewModel.MaLoaiHangNavigation = loaiHang;
                Debug.WriteLine($"MaLoaiHangNavigation: {viewModel.MaLoaiHangNavigation}");

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
                var fileName = $"{viewModel.MaSanPham}{fileExtension}";  // Tạo tên duy nhất
                var filePath = Path.Combine(uploadFolder, fileName);

                // Lưu ảnh vào thư mục đã chỉ định
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                     AnhSanPham.CopyToAsync(stream); 
                }
                viewModel.AnhSanPham = fileName;
                var sanPham = new TSanPham
                {
                    MaSanPham = viewModel.MaSanPham,
                    TenSanPham = viewModel.TenSanPham,
                    MaLoaiHang = viewModel.MaLoaiHang,
                    DonGiaNhap = viewModel.DonGiaNhap,
                    DonGiaBan = viewModel.DonGiaBan,
                    SoLuong = viewModel.SoLuong,
                    TrongLuong = viewModel.TrongLuong,
                    MoTa = viewModel.MoTa,
                    AnhSanPham = viewModel.AnhSanPham
                };
                var hoaDonNhap = new THoaDonNhap
                {
                  SoHdn= viewModel.SoHdn,
                  MaNcc= viewModel.MaNcc,
                  MaNv="NV001"
                };
                var chiTietHoaDonNhap = new TChiTietHdn
                {
                    SoHdn = viewModel.SoHdn,
                    MaSanPham = viewModel.MaSanPham,
                    Slnhap = viewModel.SoLuong
                };
                _db.TChiTietHdns.Add(chiTietHoaDonNhap);
               
                _db.THoaDonNhaps.Add(hoaDonNhap);
             

               

                _db.TSanPhams.Add(sanPham);
                _db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");

            }
           
            return View();
        }

        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(string masp)
        {

            ViewBag.LoaiHangList = new SelectList(_db.TLoaiHangs, "MaLoaiHang", "TenLoaiHang");
            var viewModel = _db.TSanPhams.Find(masp);
            ViewBag.Anh = viewModel.AnhSanPham;
            var sanPham = new SanPhamNCCViewModel
            {
                MaSanPham = viewModel.MaSanPham,
                TenSanPham = viewModel.TenSanPham,
                MaLoaiHang = viewModel.MaLoaiHang,
                DonGiaNhap = viewModel.DonGiaNhap,
                DonGiaBan = viewModel.DonGiaBan,
                SoLuong = viewModel.SoLuong,
                TrongLuong = viewModel.TrongLuong,
                MoTa = viewModel.MoTa,
                AnhSanPham = viewModel.AnhSanPham
            };
            return View(sanPham);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        public IActionResult SuaSanPham(SanPhamNCCViewModel viewModel, IFormFile AnhSanPham)
        {
            var sanPham1 = _db.TSanPhams
                   .AsNoTracking()
                   .FirstOrDefault(sp => sp.MaSanPham == viewModel.MaSanPham);

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
                var newFileName = viewModel.MaSanPham + Path.GetExtension(AnhSanPham.FileName);
                var newPath = Path.Combine("wwwroot", "ProductImages", newFileName);

                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    AnhSanPham.CopyTo(stream);
                }

                viewModel.AnhSanPham = newFileName; // Cập nhật tên file trong sanpham
            }
            else
            {
                viewModel.AnhSanPham = sanPham1.AnhSanPham; // Giữ lại ảnh cũ
            }
            ViewBag.LoaiHangList = new SelectList(_db.TLoaiHangs, "MaLoaiHang", "TenLoaiHang");
            ModelState.Remove("MaSanPham");
            ModelState.Remove("AnhSanPham");
            ModelState.Remove("MaLoaiHangNavigation");
            ModelState.Remove("SoHdn"); 
                ModelState.Remove("MaNcc");

            string maLoaiHang = viewModel.MaLoaiHang;
            Debug.WriteLine($"MaLoaiHang: {viewModel.MaSanPham}");

            // Ghi lại giá trị để kiểm tra

            // Tìm loại hàng tương ứng trong cơ sở dữ liệu
            var loaiHang = _db.TLoaiHangs.FirstOrDefault(lh => lh.MaLoaiHang == maLoaiHang);
            viewModel.MaLoaiHangNavigation = loaiHang;
           
            // Kiểm tra xem loại hàng có tồn tại hay không


            if (ModelState.IsValid)
            {
                var sanPham = new TSanPham
                {
                    MaSanPham = viewModel.MaSanPham,
                    TenSanPham = viewModel.TenSanPham,
                    MaLoaiHang = viewModel.MaLoaiHang,
                    DonGiaNhap = viewModel.DonGiaNhap,
                    DonGiaBan = viewModel.DonGiaBan,
                    SoLuong = viewModel.SoLuong,
                    TrongLuong = viewModel.TrongLuong,
                    MoTa = viewModel.MoTa,
                    AnhSanPham = viewModel.AnhSanPham
                };
                _db.Update(sanPham);
                _db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");

            }
            return View(viewModel);
        }

        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(string masp)
        {
            var checkSanPhamGioHang=_db.TSanPhamGioHangs.Where(x=>x.MaSanPham == masp).ToList();
            var chiTietHDB = _db.TChiTietHdbs.Where(x => x.MaSanPham == masp).ToList();


            if (checkSanPhamGioHang.Count>0|| chiTietHDB.Count > 0)
            {
                TempData["ErrorMessage"] = "Sản phẩm này không được phép xóa vì đang có trong giỏ hàng hoặc hóa đơn.";
                return RedirectToAction("DanhMucSanPham");
            }
            var chiTietHdn = _db.TChiTietHdns.FirstOrDefault(x => x.MaSanPham == masp);
      
            if (chiTietHdn!=null)  {
                var sanpham = _db.TSanPhams.Find(masp);
                var soHDN = chiTietHdn.SoHdn;
                Debug.WriteLine("T1: " + soHDN);
                Debug.WriteLine("T2: " + masp);
                _db.Remove(_db.TChiTietHdns.Find(soHDN, masp));
                _db.SaveChanges();
                _db.Remove(sanpham);
                _db.SaveChanges();
                var oldImagePath = Path.Combine("wwwroot", "ProductImages", sanpham.AnhSanPham);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                var countHDB = _db.TChiTietHdns.Where(x => x.SoHdn == soHDN).ToList();
                Debug.WriteLine("T3: " + countHDB.Count);

                if (countHDB.Count == 0)
                {
                    _db.Remove(_db.THoaDonNhaps.Find(soHDN));
                }
                _db.SaveChanges();

                // Get the SoHDN value
            }
            else
            {
                var sanpham = _db.TSanPhams.Find(masp);
                _db.Remove(sanpham);
                _db.SaveChanges();
                var oldImagePath = Path.Combine("wwwroot", "ProductImages", sanpham.AnhSanPham);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
          
            // You can use soHDN for any further processing here





            return RedirectToAction("DanhMucSanPham");

        }
    }
}
