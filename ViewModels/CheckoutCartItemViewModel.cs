namespace Web_QuanLySieuThiNho.ViewModels
{
    public class CheckoutCartItemViewModel
    {
        public string MaSanPham {  get; set; }
        public required string TenSanPham { get; set; }
        public  int SoLuong { get; set; }
        public decimal TongTien { get; set; }
        public CheckoutCartItemViewModel()
        {
        }
    }
}
