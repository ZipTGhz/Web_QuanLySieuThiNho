using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web_QuanLySieuThiNho.Models;
using Web_QuanLySieuThiNho.Models.Authentication;
using Web_QuanLySieuThiNho.ViewModels;
using X.PagedList;

namespace Web_QuanLySieuThiNho.Areas.Admin.Controllers
{
    [Area("Admin")]
	[AuthorizeAdmin]
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
            TempData["ErrorMessage"] = null;
            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        public async Task<IActionResult> ThemSanPhamMoi(SanPhamNCCViewModel viewModel, IFormFile AnhSanPham)
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

            string numberPart = maxMaSanPham.Substring(2); 

            if (int.TryParse(numberPart, out int number))
            {
                number++; 

                string newMaSanPham;
                if (number < 10)
                {
                    newMaSanPham = $"SP00{number}"; 
                }
                else if (number < 100)
                {
                    newMaSanPham = $"SP0{number}"; 
                }
                else
                {
                    newMaSanPham = $"SP{number}"; 
                }
                viewModel.MaSanPham = newMaSanPham;
            }

            string numberPartNCC = maxMaNcc.Substring(2); 

            if (int.TryParse(numberPartNCC, out int numberHdn))
            {
                numberHdn++; 

                string newHDN;
                if (number < 10)
                {
                    newHDN = $"HD00{numberHdn}"; 
                }
                else if (number < 100)
                {
                    newHDN = $"HD0{numberHdn}"; 
                }
                else
                {
                    newHDN = $"HD{numberHdn}"; 
                }
                viewModel.SoHdn = newHDN;
            }
            Debug.WriteLine($"MaLoaiHangNavigation: {viewModel.MaNcc}");

            ViewBag.NCCList = new SelectList(_db.TNhaCungCaps, "MaNcc", "TenNcc");
            ViewBag.LoaiHangList = new SelectList(_db.TLoaiHangs, "MaLoaiHang", "TenLoaiHang");
            ModelState.Remove("MaLoaiHangNavigation");
            ModelState.Remove("MaSanPham");
            ModelState.Remove("SoHdn");
            string maLoaiHang = viewModel.MaLoaiHang;

            var loaiHang = _db.TLoaiHangs.FirstOrDefault(lh => lh.MaLoaiHang == maLoaiHang);

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

                if (AnhSanPham != null && AnhSanPham.Length > 0)
                {
                    try
                    {
                        var fileExtension = Path.GetExtension(AnhSanPham.FileName);
                        var fileName = $"{viewModel.MaSanPham}_{DateTime.Now.Ticks}{fileExtension}";
                        var filePath = Path.Combine(uploadFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await AnhSanPham.CopyToAsync(stream); // Ensure this completes
                        }

                        // Double-check that the file was created and is not empty
                        var fileInfo = new FileInfo(filePath);
                        if (fileInfo.Exists && fileInfo.Length > 0)
                        {
                            viewModel.AnhSanPham = fileName; // Save file name in the model
                        }
                        else
                        {
                            ModelState.AddModelError("", "The image file could not be saved.");
                            Debug.WriteLine("File is empty after upload.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"An error occurred while saving the image: {ex.Message}");
                        ModelState.AddModelError("", "Unable to save the image file. Please try again.");
                    }
                }

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
            TempData["ErrorMessage"] = "";
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
            TempData["ErrorMessage"] = "";
            var sanPham1 = _db.TSanPhams
                   .AsNoTracking()
                   .FirstOrDefault(sp => sp.MaSanPham == viewModel.MaSanPham);

            ViewBag.Anh = sanPham1.AnhSanPham;
            if (AnhSanPham != null && AnhSanPham.Length > 0)
            {
                var oldImagePath = Path.Combine("wwwroot", "ProductImages", sanPham1.AnhSanPham);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                var newFileName = viewModel.MaSanPham + Path.GetExtension(AnhSanPham.FileName);
                var newPath = Path.Combine("wwwroot", "ProductImages", newFileName);

                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    AnhSanPham.CopyTo(stream);
                }

                viewModel.AnhSanPham = newFileName; 
            }
            else
            {
                viewModel.AnhSanPham = sanPham1.AnhSanPham; 
            }
            ViewBag.LoaiHangList = new SelectList(_db.TLoaiHangs, "MaLoaiHang", "TenLoaiHang");
            ModelState.Remove("MaSanPham");
            ModelState.Remove("AnhSanPham");
            ModelState.Remove("MaLoaiHangNavigation");
            ModelState.Remove("SoHdn"); 
            ModelState.Remove("MaNcc");

            string maLoaiHang = viewModel.MaLoaiHang;
            Debug.WriteLine($"MaLoaiHang: {viewModel.MaSanPham}");
            var loaiHang = _db.TLoaiHangs.FirstOrDefault(lh => lh.MaLoaiHang == maLoaiHang);
            viewModel.MaLoaiHangNavigation = loaiHang;
          
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
            TempData["ErrorMessage"] = "";
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
                TempData["ErrorMessage"] = "Sản phẩm đã được xóa";
                _db.SaveChanges();
            }
            else
            {
                var sanpham = _db.TSanPhams.Find(masp);
                _db.Remove(sanpham);
                TempData["ErrorMessage"] = "Sản phẩm đã được xóa";
                _db.SaveChanges();
                var oldImagePath = Path.Combine("wwwroot", "ProductImages", sanpham.AnhSanPham);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
          
            return RedirectToAction("DanhMucSanPham");

        }
    }
}
