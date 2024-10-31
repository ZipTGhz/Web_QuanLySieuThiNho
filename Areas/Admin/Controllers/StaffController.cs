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

    [Route("Admin/Staff")]
    public class StaffController : Controller
    {

        private QlsieuThiNhoContext _db = new QlsieuThiNhoContext();

        public IActionResult Index()
        {
            return View();
        }

        [Route("QuanLiNhanVien")]
        public IActionResult QuanLiNhanVien(int pageNumber = 1)
        {
            int pageSize = 6;
            var staffs = _db.TNhanViens.AsNoTracking().Where(x => x.TrangThai == "DangLam").OrderBy(x => x.ChucVu);
            PagedList<TNhanVien> pagedList = new PagedList<TNhanVien>(staffs, pageNumber, pageSize);
            return View(pagedList);
        }
        [Route("ThemNhanVienMoi")]
        [HttpGet]
        public IActionResult ThemNhanVienMoi()
        {
          
            return View(new NhanVienViewModel());
        }
        [Route("ThemNhanVienMoi")]
        [HttpPost]
        public IActionResult ThemNhanVienMoi(NhanVienViewModel viewModel)
        {
            string sqlQuery = @"
    SELECT TOP 1 MaNv 
    FROM TNhanVien
    ORDER BY CAST(SUBSTRING(MaNv, 3, LEN(MaNv) - 2) AS INT) DESC";

            var maxMaNhanVien = _db.TNhanViens
                .FromSqlRaw(sqlQuery)
                .Select(sp => sp.MaNv)
                .FirstOrDefault();

            string numberPart = maxMaNhanVien.Substring(2); // Lấy phần số sau "SP"

            // Chuyển đổi thành số và tăng lên 1
            if (int.TryParse(numberPart, out int number))
            {
                number++; // Tăng giá trị lên 1

                // Tạo mã sản phẩm mới
                string newMaNV;

                // Định dạng mã sản phẩm mới theo yêu cầu
                if (number < 10)
                {
                    newMaNV = $"NV00{number}"; // 1 số => 2 số 0
                }
                else if (number < 100)
                {
                    newMaNV = $"NV0{number}"; // 2 số => 1 số 0
                }
                else
                {
                    newMaNV = $"NV{number}"; // 3 số => không cần số 0
                }
                viewModel.MaNv = newMaNV;
            }
            ModelState.Remove("MaNv");
            ModelState.Remove("TrangThai");
            viewModel.TrangThai = "DANGLAM";

            if (!ModelState.IsValid)
            {
                // Ghi log hoặc kiểm tra lỗi
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage); // Hoặc sử dụng logger
                }
                return View(viewModel);
            }
            if (ModelState.IsValid)
            {
                var nhanVien = new TNhanVien
                {
                    MaNv = viewModel.MaNv,
                    TenNv = viewModel.TenNv,
                    GioiTinh = viewModel.GioiTinh,
                    NgaySinh = viewModel.NgaySinh,
                    SoDienThoai = viewModel.SoDienThoai,
                    DiaChi = viewModel.DiaChi,
                    ChucVu = viewModel.ChucVu,
                    TrangThai = viewModel.TrangThai
                };
                _db.TNhanViens.Add(nhanVien);
                _db.SaveChanges();
                return RedirectToAction("QuanLiNhanVien");

            }
            return View(viewModel);
        }

        [Route("SuaNhanVien")]
        [HttpGet]
        public IActionResult SuaNhanVien(string manv)
        {
            var nhanVien = _db.TNhanViens.Find(manv);
            if (nhanVien == null)
            {
                return NotFound();
            }

            var viewModel = new NhanVienViewModel
            {
                MaNv = nhanVien.MaNv,
                TenNv = nhanVien.TenNv,
                GioiTinh = nhanVien.GioiTinh,
                NgaySinh = nhanVien.NgaySinh,
                SoDienThoai = nhanVien.SoDienThoai,
                DiaChi = nhanVien.DiaChi,
                ChucVu = nhanVien.ChucVu,
                TrangThai = nhanVien.TrangThai
            };

            return View(viewModel);
        }
        [Route("SuaNhanVien")]
        [HttpPost]
        public IActionResult SuaNhanVien(NhanVienViewModel viewModel)
        {

           
            ModelState.Remove("MaNv");
            ModelState.Remove("TrangThai");
           


            // Kiểm tra xem loại hàng có tồn tại hay không


            if (ModelState.IsValid)
            {
                var nhanVien = new TNhanVien
                {
                    MaNv = viewModel.MaNv,
                    TenNv = viewModel.TenNv,
                    GioiTinh = viewModel.GioiTinh,
                    NgaySinh = viewModel.NgaySinh,
                    SoDienThoai = viewModel.SoDienThoai,
                    DiaChi = viewModel.DiaChi,
                    ChucVu = viewModel.ChucVu,
                    TrangThai = "DANGLAM"
                };
                _db.Update(nhanVien);
                _db.SaveChanges();
                return RedirectToAction("QuanLiNhanVien");

            }
            return View(viewModel);
        }

        [Route("EditNhanVien")]
        [HttpGet]
        public IActionResult EditNhanVien(string manv)
        {
            var nhanVien = _db.TNhanViens.Find(manv);
            if (nhanVien != null)
            {
                nhanVien.TrangThai = "SATHAI";
                _db.SaveChanges();
            }
            return RedirectToAction("QuanLiNhanVien");

        }
    }
}
