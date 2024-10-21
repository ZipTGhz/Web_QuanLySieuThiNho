namespace Web_QuanLySieuThiNho.ViewModels
{
    public class CheckoutPageViewModel
    {
        public CustomerInfoViewModel CustomerInfo { get; set; } = new CustomerInfoViewModel();
        public List<CheckoutCartItemViewModel> CartItems { get; set; } = new List<CheckoutCartItemViewModel>();
        public CheckoutPageViewModel()
        {
        }
    }
}
