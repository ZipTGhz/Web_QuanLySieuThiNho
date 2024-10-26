using Web_QuanLySieuThiNho.Models;
using X.PagedList;

namespace Web_QuanLySieuThiNho.ViewModels
{
    public class CategoryPageViewModel
    {
        public List<TSanPham> LastestProductList { get; set; }
        public IPagedList<TSanPham> ProductList { get; set; }
        public CategoryPageViewModel(List<TSanPham> lastestProductList, IPagedList<TSanPham> productList)
        {
            LastestProductList = lastestProductList;
            ProductList = productList;
        }
    }
}
