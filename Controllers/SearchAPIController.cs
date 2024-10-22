using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_QuanLySieuThiNho.Models;

namespace Web_QuanLySieuThiNho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchAPIController : ControllerBase
    {
        private readonly QlsieuThiNhoContext _db = new QlsieuThiNhoContext();

        public SearchAPIController()
        {
        }

        [HttpGet("GetSuggestions")]
        public IActionResult GetSuggestions(string query)
        {
           if (string.IsNullOrEmpty(query))
                return BadRequest("Query cannot be empty.");

            // Lấy 3 kết quả gần đúng nhất dựa trên tên sản phẩm
            var suggestions = _db.TSanPhams
                .Where(x => x.TenSanPham.Contains(query))
                .OrderBy(x => x.TenSanPham) // sắp xếp kết quả cho phù hợp
                .Take(3) // chỉ lấy 3 kết quả
                .Select(x => new { x.TenSanPham, x.MaSanPham, x.AnhSanPham, donGiaBan = x.DonGiaBan.Value.ToString("#,##0") })
                .ToList();

            return Ok(suggestions);
        }

    }
}
