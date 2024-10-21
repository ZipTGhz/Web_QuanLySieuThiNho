using Web_QuanLySieuThiNho.Models;

namespace Web_QuanLySieuThiNho.Util
{
    public static class Helpers
    {
        private static QlsieuThiNhoContext _db = new QlsieuThiNhoContext();
        public static string GenerateNextID(string id, int prefixSize)
        {
            string prefix = id.Substring(0, prefixSize);
            string suffix = id.Substring(prefixSize);
            int idNumber = int.Parse(suffix);
            ++idNumber;
            return prefix + idNumber.ToString($"D{id.Length - prefixSize}");
        }
        public static string GenerateID(string prefix, int suffixSize)
        {
            return prefix + 1.ToString($"D{suffixSize}");
        }
        public static string GetCustomerID(string? username)
        {
            ArgumentNullException.ThrowIfNull(username);

            var customer = _db.TKhachHangs.FirstOrDefault(x => x.TenDangNhap ==  username);
            if (customer != null)
                return customer.MaKh;
            var customerID = _db.TKhachHangs.OrderBy(x=> x.MaKh).LastOrDefault()?.MaKh;
            if (customerID != null)
                customerID = GenerateNextID(customerID, 2);
            else
                customerID = GenerateID("KH", 4);
            customer = new TKhachHang()
            {
                MaKh = customerID,
                TenDangNhap = username,
            };
            _db.TKhachHangs.Add(customer);
            _db.SaveChanges();
            return customerID;
        }
    }
}
