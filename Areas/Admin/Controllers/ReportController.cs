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

            var namHienTai = DateTime.Now.Year;
            var tongTienThang = _db.THoaDonBans
                .Where(h => h.NgayBan.HasValue && h.NgayBan.Value.Year == namHienTai)
                .GroupBy(h => h.NgayBan.Value.Month) 
                .Select(g => new {
                    Thang = g.Key,
                    TongTien = g.Join(_db.TChiTietHdbs,
                                      h => h.SoHdb,
                                      ct => ct.SoHdb,
                                      (h, ct) => ct.ThanhTien).Sum() ?? 0
                })
                .ToList();

            foreach (var item in tongTienThang)
            {
                tongTienTheoThang[item.Thang - 1] = Convert.ToInt32(item.TongTien);
            }

            foreach (var item in tongTienThang)
            {
                Debug.WriteLine($"Tháng: {item.Thang}, Tổng tiền: {item.TongTien}");
            }

            ViewBag.TongTienTheoThang = tongTienTheoThang;
            ViewBag.TongTien = tongTien;
            ViewBag.TongSanPham = sanPham;
            return View();
        }
    }
}
