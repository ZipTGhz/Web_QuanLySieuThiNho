# Web_QuanLySieuThiNho
B1. clone https://github.com/ZipTGhz/Web_QuanLySieuThiNho.git về VisualStudio
B2. Mở file QLSieuThiNho.sql trong SSMS và cài đặt database
B3. Truy cập Tools > Nuget Package Manager > Manage Nuget Packages for Solution để cài 3 Entity Framework ((nếu chưa cài đặt)  là Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools
B4. Kết nối tới cơ sở dữ liệu : Mở SQL Server Object Explorer > Add sql server > Local > Tên server của máy > Database Name là tên database vừa tạo ở bước 2 (tên là QLSieuThiNho)
B5. Chọn properties của QLSieuThiNho rồi sao chép Connection String. Sau đó mở Package manager console và chạy lệnh dưới                                                    Scaffold-DbContext "tên của Connection String" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
B6. Run và truy cập bằng tài khoản, mật khẩu (user, user) để đăng nhập vào trang khách hàng, (admin,admin) để đăng nhập vào trang admin
