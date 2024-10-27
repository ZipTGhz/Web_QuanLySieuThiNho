using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web_QuanLySieuThiNho.Models;

namespace Web_QuanLySieuThiNho.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Report")]
    public class ReportController : Controller
    {
        private QlsieuThiNhoContext _db = new QlsieuThiNhoContext();

        public IActionResult Index()
        {
            var today = DateTime.Today;

            // Tính tổng tiền cho tất cả hóa đơn có ngày bán bằng ngày hôm nay
            var tongTien = _db.THoaDonBans
                .Where(h => h.NgayBan.HasValue && h.NgayBan.Value.Date == today)
                .Join(_db.TChiTietHdbs,
                      h => h.SoHdb,
                      ct => ct.SoHdb,
                      (h, ct) => ct.ThanhTien)
                .Sum();
            var sanPham = _db.THoaDonBans
               .Where(h => h.NgayBan.HasValue && h.NgayBan.Value.Date == today)
               .Join(_db.TChiTietHdbs,
                     h => h.SoHdb,
                     ct => ct.SoHdb,
                     (h, ct) => ct.Slban)
               .Sum();
            var tongTienTheoThang = new decimal[12];

            // Lấy dữ liệu hóa đơn và tính tổng tiền theo từng tháng
            var namHienTai = DateTime.Now.Year;
            var tongTienThang = _db.THoaDonBans
                .Where(h => h.NgayBan.HasValue && h.NgayBan.Value.Year == namHienTai) // Chỉ lấy hóa đơn có ngày bán
                .GroupBy(h => h.NgayBan.Value.Month) // Nhóm theo tháng
                .Select(g => new {
                    Thang = g.Key,
                    TongTien = g.Join(_db.TChiTietHdbs,
                                      h => h.SoHdb,
                                      ct => ct.SoHdb,
                                      (h, ct) => ct.ThanhTien).Sum() ?? 0
                })
                .ToList();

            // Gán tổng tiền cho từng tháng vào mảng (0-indexed)
            foreach (var item in tongTienThang)
            {
                tongTienTheoThang[item.Thang - 1] = Convert.ToInt32(item.TongTien);
            }

            // Kiểm tra các giá trị trong tongTienThang
            foreach (var item in tongTienThang)
            {
                Debug.WriteLine($"Tháng: {item.Thang}, Tổng tiền: {item.TongTien}");
            }

            // Gán mảng vào ViewBag để sử dụng ở phía View
            ViewBag.TongTienTheoThang = tongTienTheoThang;
            ViewBag.TongTien = tongTien;
            ViewBag.TongSanPham = sanPham;
            return View();
        }
    }
}
