using Web_QuanLySieuThiNho.Models;

namespace Web_QuanLySieuThiNho.ViewModels
{
    public class DetailPageViewModel
    {
        public TSanPham? Product { get; set; }
        public List<TSanPham>? RelatedProducts { get; set; } = new List<TSanPham>();
        public DetailPageViewModel() { }
    }
}
