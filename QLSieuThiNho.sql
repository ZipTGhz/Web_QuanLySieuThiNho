USE [QLSieuThiNho]
GO
/****** Object:  Table [dbo].[tChiTietHDB]    Script Date: 24/10/2024 2:52:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tChiTietHDB](
	[SoHDB] [nvarchar](10) NOT NULL,
	[MaSanPham] [nvarchar](10) NOT NULL,
	[SLBan] [int] NULL,
	[ThanhTien] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[SoHDB] ASC,
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tChiTietHDN]    Script Date: 24/10/2024 2:52:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tChiTietHDN](
	[SoHDN] [nvarchar](10) NOT NULL,
	[MaSanPham] [nvarchar](10) NOT NULL,
	[SLNhap] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SoHDN] ASC,
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tGioHang]    Script Date: 24/10/2024 2:52:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tGioHang](
	[MaGioHang] [int] IDENTITY(1,1) NOT NULL,
	[MaKH] [nvarchar](10) NULL,
	[NgayTao] [datetime] NULL,
	[TrangThai] [nvarchar](50) NULL,
	[TongTienGioHang] [money] NULL,
 CONSTRAINT [PK__tGioHang__F5001DA3E89520D5] PRIMARY KEY CLUSTERED 
(
	[MaGioHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tHoaDonBan]    Script Date: 24/10/2024 2:52:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tHoaDonBan](
	[SoHDB] [nvarchar](10) NOT NULL,
	[MaNV] [nvarchar](10) NULL,
	[MaKH] [nvarchar](10) NULL,
	[NgayBan] [datetime] NULL,
	[SoSanPham] [int] NULL,
	[GhiChu] [nvarchar](1000) NULL,
 CONSTRAINT [PK_tHoaDonBan] PRIMARY KEY CLUSTERED 
(
	[SoHDB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tHoaDonNhap]    Script Date: 24/10/2024 2:52:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tHoaDonNhap](
	[SoHDN] [nvarchar](10) NOT NULL,
	[MaNCC] [nvarchar](10) NULL,
	[MaNV] [nvarchar](10) NULL,
	[NgayNhap] [datetime] NULL,
 CONSTRAINT [PK_tHoaDonNhap] PRIMARY KEY CLUSTERED 
(
	[SoHDN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tKhachHang]    Script Date: 24/10/2024 2:52:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tKhachHang](
	[MaKH] [nvarchar](10) NOT NULL,
	[TenDangNhap] [nvarchar](50) NULL,
	[TenKH] [nvarchar](200) NULL,
	[GioiTinh] [nvarchar](10) NULL,
	[SoDienThoai] [nvarchar](20) NULL,
	[DiaChi] [nvarchar](200) NULL,
 CONSTRAINT [PK_tKhachHang] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tLoaiHang]    Script Date: 24/10/2024 2:52:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tLoaiHang](
	[MaLoaiHang] [nvarchar](10) NOT NULL,
	[TenLoaiHang] [nvarchar](100) NULL,
 CONSTRAINT [PK_tTheLoai] PRIMARY KEY CLUSTERED 
(
	[MaLoaiHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tNhaCungCap]    Script Date: 24/10/2024 2:52:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tNhaCungCap](
	[MaNCC] [nvarchar](10) NOT NULL,
	[TenNCC] [nvarchar](200) NULL,
 CONSTRAINT [PK_tNhaCungCap] PRIMARY KEY CLUSTERED 
(
	[MaNCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tNhanVien]    Script Date: 24/10/2024 2:52:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tNhanVien](
	[MaNV] [nvarchar](10) NOT NULL,
	[TenDangNhap] [nvarchar](50) NULL,
	[TenNV] [nvarchar](200) NULL,
	[GioiTinh] [nvarchar](10) NULL,
	[NgaySinh] [datetime] NULL,
	[SoDienThoai] [nvarchar](20) NULL,
	[DiaChi] [nvarchar](200) NULL,
	[ChucVu] [nvarchar](50) NULL,
 CONSTRAINT [PK_tNhanVien] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tSanPham]    Script Date: 24/10/2024 2:52:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tSanPham](
	[MaSanPham] [nvarchar](10) NOT NULL,
	[TenSanPham] [nvarchar](200) NULL,
	[MaLoaiHang] [nvarchar](10) NOT NULL,
	[DonGiaNhap] [money] NULL,
	[DonGiaBan] [money] NULL,
	[SoLuong] [int] NULL,
	[TrongLuong] [nvarchar](50) NULL,
	[MoTa] [nvarchar](3000) NULL,
	[AnhSanPham] [nvarchar](200) NULL,
 CONSTRAINT [PK_tSanPham] PRIMARY KEY CLUSTERED 
(
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tSanPhamGioHang]    Script Date: 24/10/2024 2:52:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tSanPhamGioHang](
	[MaSanPhamGioHang] [int] IDENTITY(1,1) NOT NULL,
	[MaGioHang] [int] NULL,
	[MaSanPham] [nvarchar](10) NULL,
	[SoLuong] [int] NULL,
	[DonGiaBan] [money] NULL,
	[TongTienSanPham] [money] NULL,
 CONSTRAINT [PK__tSanPham__9146F8331FC9360D] PRIMARY KEY CLUSTERED 
(
	[MaSanPhamGioHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tTaiKhoan]    Script Date: 24/10/2024 2:52:45 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tTaiKhoan](
	[TenDangNhap] [nvarchar](50) NOT NULL,
	[MatKhau] [nvarchar](100) NULL,
	[LoaiTaiKhoan] [nvarchar](20) NULL,
 CONSTRAINT [PK_tTaiKhoan] PRIMARY KEY CLUSTERED 
(
	[TenDangNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD001', N'SP001', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD001', N'SP002', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD001', N'SP003', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD002', N'SP004', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD002', N'SP005', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD002', N'SP006', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD002', N'SP007', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD003', N'SP008', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD004', N'SP009', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD004', N'SP010', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD005', N'SP011', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD005', N'SP012', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD005', N'SP013', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD006', N'SP014', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD006', N'SP015', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD007', N'SP016', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD008', N'SP017', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD008', N'SP019', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD008', N'SP020', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD009', N'SP021', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD009', N'SP022', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD010', N'SP023', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD011', N'SP024', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD012', N'SP025', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD012', N'SP026', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD013', N'SP027', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD014', N'SP028', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD015', N'SP029', 2)
GO
INSERT [dbo].[tChiTietHDN] ([SoHDN], [MaSanPham], [SLNhap]) VALUES (N'HD016', N'SP030', 2)
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD001', N'NCC001', N'NV001', CAST(N'2024-01-10T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD002', N'NCC001', N'NV001', CAST(N'2024-01-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD003', N'NCC001', N'NV001', CAST(N'2024-02-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD004', N'NCC001', N'NV001', CAST(N'2024-04-10T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD005', N'NCC002', N'NV001', CAST(N'2024-05-10T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD006', N'NCC002', N'NV001', CAST(N'2024-07-10T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD007', N'NCC002', N'NV002', CAST(N'2024-03-23T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD008', N'NCC002', N'NV002', CAST(N'2024-05-13T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD009', N'NCC003', N'NV002', CAST(N'2024-04-08T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD010', N'NCC003', N'NV002', CAST(N'2024-07-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD011', N'NCC003', N'NV002', CAST(N'2024-06-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD012', N'NCC003', N'NV002', CAST(N'2024-04-26T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD013', N'NCC004', N'NV003', CAST(N'2024-12-02T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD014', N'NCC004', N'NV003', CAST(N'2024-11-17T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD015', N'NCC004', N'NV003', CAST(N'2024-10-22T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tHoaDonNhap] ([SoHDN], [MaNCC], [MaNV], [NgayNhap]) VALUES (N'HD016', N'NCC004', N'NV003', CAST(N'2024-09-11T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[tLoaiHang] ([MaLoaiHang], [TenLoaiHang]) VALUES (N'LH01', N'Sữa')
GO
INSERT [dbo].[tLoaiHang] ([MaLoaiHang], [TenLoaiHang]) VALUES (N'LH02', N'Rau - Củ - Quả')
GO
INSERT [dbo].[tLoaiHang] ([MaLoaiHang], [TenLoaiHang]) VALUES (N'LH03', N'Thịt - Hải Sản Tươi')
GO
INSERT [dbo].[tNhaCungCap] ([MaNCC], [TenNCC]) VALUES (N'NCC001', N'CTY VINAMILK')
GO
INSERT [dbo].[tNhaCungCap] ([MaNCC], [TenNCC]) VALUES (N'NCC002', N'CTY ĐÀ LẠT GAP')
GO
INSERT [dbo].[tNhaCungCap] ([MaNCC], [TenNCC]) VALUES (N'NCC003', N'CTY CP ĐẦU MỐI GIA CẦM')
GO
INSERT [dbo].[tNhaCungCap] ([MaNCC], [TenNCC]) VALUES (N'NCC004', N'CHỢ ĐẦU MỐI MINH KHAI')
GO
INSERT [dbo].[tNhanVien] ([MaNV], [TenDangNhap], [TenNV], [GioiTinh], [NgaySinh], [SoDienThoai], [DiaChi], [ChucVu]) VALUES (N'NV001', NULL, N'Trần Văn Giáp', N'Nam', CAST(N'2004-04-25T00:00:00.000' AS DateTime), N'0123456789', N'Cầu Giấy', NULL)
GO
INSERT [dbo].[tNhanVien] ([MaNV], [TenDangNhap], [TenNV], [GioiTinh], [NgaySinh], [SoDienThoai], [DiaChi], [ChucVu]) VALUES (N'NV002', NULL, N'Nguyễn Tùng Lâm', N'Nam', CAST(N'2004-02-02T00:00:00.000' AS DateTime), N'0987654321', N'Giáp Bát', NULL)
GO
INSERT [dbo].[tNhanVien] ([MaNV], [TenDangNhap], [TenNV], [GioiTinh], [NgaySinh], [SoDienThoai], [DiaChi], [ChucVu]) VALUES (N'NV003', NULL, N'Nguyễn Đình Tuấn Anh', N'Nam', CAST(N'2004-03-03T00:00:00.000' AS DateTime), N'0678912345', N'Đình Thôn', NULL)
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP001', N'Thức uống lúa mạch vị sô cô la Ovaltine lốc 4 hộp x 180ml', N'LH01', 10000.0000, 27000.0000, 10, N'4 hộp', N'Thức uống lúa mạch Ovaltine bổ sung thêm cholin hoà cùng hương vị thơm ngon của cacao và sữa, giúp cơ thể hấp thụ canxi tốt hơn, phát triển chiều cao, cho bé cao lớn khoẻ mạnh. Lốc 4 hộp thức uống lúa mạch hương vị socola Ovaltine 180ml lốc 4 hộp tiện dùng, tiết kiệm', N'Ovaltine.png')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP002', N'Sữa bắp non LiF lốc 4 hộp x 180ml', N'LH01', 10000.0000, 26000.0000, 10, N'4 hộp', N'Siêu phẩm sữa bắp non Lif được tách hoàn toàn 100% từ trái bắp non mới hái, giàu vitamin A, B3, B1, B6 và đặc biệt là chất xơ, tốt cho làn da, cho hệ tiêu hoá. Lốc 4 hộp sữa bắp non LiF 180ml hương vị thơm ngon từ sữa hạt bắp mà ai cũng yêu thích, cùng đóng lốc tiện dùng và tiết kiệm.
', N'LiF.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP003', N'Lốc 4 hộp sữa chua lên men tự nhiên vị việt quất sữa Fristi 100ml', N'LH01', 5000.0000, 15000.0000, 10, N'4 hộp', N'Sữa chua là thức uống yêu thích của nhiều người đặc biệt là các bé nhỏ. Lốc 4 hộp sữa chua lên men tự nhiên vị việt quất sữa Fristi 100ml bổ sung nhiều dưỡng chất tốt cho sự phát triển của trẻ. Sữa chua Fristi với quá trình lên men tự nhiên, chất lượng, hỗ trợ hệ tiêu hoá của bé hoạt động tốt hơn.', N'Fristi.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP004', N'Lốc 4 hộp sữa Lof Malto lúa mach SôCôLa 180ml
', N'LH01', 10000.0000, 25000.0000, 10, N'4 hộp', N'Sữa socola, lúa mạch LOF Malto là dòng sản phẩm mới kết hợp từ sữa lúa mạch và socola thơm ngon giàu vitamin B, canxi và K2, cung cấp dồi dào năng lượng. Sữa socola LOF Malto 180ml thơm ngon tự nhiên, không chất bảo quản, không màu tổng hợp và không chất tạo ngọt.

', N'Malto.png')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP005', N'Sữa chua uống men sống Vinamilk Probi hương dứa Lốc 5 chai x 65ml
', N'LH01', 10000.0000, 23000.0000, 10, N'4 hộp', N'Sữa chua uống men sống Vinamilk Probihương dứa được xử lý bằng công nghệ tiệt trùng UHT hiện đại, hoàn toàn không sử dụng chất bảo quản nên giữ nguyên được hương vị thơm ngon tự nhiên từ sữa và dứa. Sản phẩm không chỉ là thức uống giải khát mà còn bổ sung dưỡng chất cần thiết cho cơ thể. Sữa chua rất có lợi cho sức khỏe, nó làm tăng khả năng hấp thụ Protein, bổ sung và tái tạo Canxi cho xương.

', N'Probi.png')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP006', N'Sữa tiệt trùng Cô gái Hà Lan có đường lốc 4 hộp x 180ml
', N'LH01', 15000.0000, 30000.0000, 10, N'4 hộp', N'Sữa tươi Dutch Lady được gấp đôi vitamin D và Canxi, giúp hỗ trợ phát triển chiều cao cho bé tối đa. Sữa tươi có đường mang hương vị thơm ngon, dễ uống. Sữa tiệt trùng có đường Dutch Lady Cao Khoẻ 180m tiện sử dụng, ngọt dịu dễ uống.

Sữa tiệt trùng Dutch Lady cao khoẻ có đường với vị ngọt dịu, thơm ngon, dễ uống, nay sữa có bổ sung gấp đôi Canxi, Vitamin D và chứa vitamin A, giúp bé phát triển chiều cao và hỗ trợ hệ miễn dịch của bé khoẻ mạnh hơn.

', N'CoGaiHaLan.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP007', N'Thực phẩm bổ sung Anlene gold 3X hương vani 1,2kg
', N'LH01', 200000.0000, 465000.0000, 5, N'1 hộp', N'Anlene 3 khỏe có công thức vượt trội ngoài những dưỡng chất có trong sữa tươi thông thường còn bổ sung thêm hàm lượng cao Đạm, Canxi, Collagen giúp tốt cho hệ cơ xương khớp, đặc biệt cần thiết cho người trưởng thành từ 40 tuổi, giúp hệ vận động luôn được dẻo dai, linh hoạt để có thể tự do vận động chăm sóc gia đình tốt hơn và làm được những điều mình muốn

', N'Anlene.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP008', N'Lốc 6 chai sữa chua uống Vinamilk Susu hương cam 80ml
', N'LH01', 10000.0000, 23000.0000, 10, N'6 chai', N'Sữa chua SuSu với vị ngọt béo, chua chua thơm ngon tuyệt vời. Sữa chua bổ sung vitamin A giúp bé mắt sáng tinh anh, chất xơ hòa tan (Prebiotic) hỗ trợ hệ tiêu hóa để bé luôn khỏe mạnh và năng động mỗi ngày. Lốc 6 chai sữa chua uống cam SuSu 80ml hỗ trợ tăng cường trí não cho bé.



Phù hợp: Trẻ em trên 1 tuổi.

Thành phần: Sữa chua lên men tự nhiên (44%), nước, sữa bột, whey bột, chất béo sữa, sữa tươi, men,...

', N'Susu.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP009', N'Sữa lúa mạch Milo protein canxi hộp 210ml
', N'LH01', 3000.0000, 10000.0000, 10, N'1 hộp', N'Sữa lúa mạch Milo protein canxi 210ml giúp cung cấp nguồn năng lượng dồi dào trong các bữa ăn sáng. Ngoài ra, sản phẩm còn cung cấp chất đạm, chất béo, các vitamin và khoáng chất cần thiết cho cơ thể.

Sản phẩm nên được bổ sung vào bữa ăn sáng bổ dưỡng, thơm ngon để khởi đầu ngày mới căng tràn sức sống cho trẻ mỗi ngày. Sản phẩm đã được đóng hộp kín đáo, giúp chất lượng sản phẩm được bảo quản an toàn dễ dàng và hợp vệ sinh.

Lưu ý:

- Hạn sử dụng thực tế quý khách vui lòng xem trên bao bì.

- Hình sản phẩm chỉ mang tính chất minh họa, hình thực tế bao bì của sản phẩm tùy thời điểm sẽ khác so với thực tế.

', N'MiloHop210ml.png')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP010', N'Lốc 4 hộp sữa tươi tiệt trùng Vinamilk có đường 180ml
', N'LH01', 12000.0000, 30000.0000, 10, N'4 hộp', N'Sữa tươi tiệt trùng VNM có đường hộp 180ml được sản xuất và đóng gói bởi thương hiệu Vinamilk – thương hiệu sữa nổi tiếng tại Việt Nam, được thành lập từ năm 1976 chuyên về chế biến, sản xuất các mặt hàng sản phẩm bơ sữa và sữa bò. Với cam kết mang đến cho cộng đồng nguồn sữa dinh dưỡng và chất lượng hàng đầu bằng sự trân trọng, tình yêu và trách nhiệm cao của mình với cuộc sống con người và xã hội, các sản phẩm của Vinamilk đã nhận được sự tin tưởng của người tiêu dùng trong nước và quốc tế.

', N'Vinamilk.png')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP011', N'Cam Sành loại nhỏ
', N'LH02', 5000.0000, 20000.0000, 15, N'1 kg', N'Cam sành là loại cam có lớp vỏ dày, sần sùi, màu xanh tươi. Ruột cam đậm màu, nhiều nước, hương vị thơm ngon, vị ngọt dịu nhẹ. Bạn có thể sử dụng cam ăn trực tiếp hoặc vắt lấy nước uống đều rất thơm ngon.

Cam là loại trái cây có hàm lượng vitamin C lớn. Vitamin C là loại vitamin được coi là "thần dược" từ tự nhiên, rất có lợi cho sức khỏe. Chúng ta sẽ cùng điểm qua một vài công dụng thường thấy của vitamin C. Cung cấp đầy đủ vitamin C hàng ngày cho cơ thể giúp bạn tăng lưu lượng máu đến mắt, qua đó chống lại chứng đục thủy tinh thể giúp đôi mắt của bạn khỏe mạnh hơn.

Theo một kết quả nghiên cứu trên 65 người bị đột quỵ và 65 người khỏe mạnh. Các nhà khoa học viện Hàn lâm Thần kinh học ở Philadelphia đã chỉ ra rằng những bệnh nhân bị đột quỵ có nồng độ vitamin C trong máu rất thấp trong khi những người khỏe mạnh có nồng độ bình thường. Vitamin C giúp ngăn ngừa bệnh tim mạch thông qua các chất chống oxy hóa có tác dụng ngăn chặn sự hình thành của các gốc tự do, tránh hình thành mảng bám trong thành động mạch.

Lưu ý:

- Hạn sử dụng thực tế quý khách vui lòng xem trên bao bì.

- Hình sản phẩm chỉ mang tính chất minh họa, hình thực tế bao bì của sản phẩm tùy thời điểm sẽ khác so với thực tế.

', N'CamSanh.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP012', N'Cam ruột vàng úc', N'LH02', 25000.0000, 70000.0000, 15, N'1 kg', N'Cam Úc có chứa nhiều Vitamin C, có tác dụng hồi phục sức khỏe nhanh, tốt cho da, chống lão hóa, tốt cho người ốm.

Ăn cam Úc thường xuyên sẽ giúp bảo vệ bạn khỏi mắc các bệnh truyền nhiễm do virus, giảm đáng kể nguy cơ mắc bệnh sỏi thận. Nếu bạn muốn tránh lượng calo dư thừa, hãy thêm cam Úc vào chế độ dinh dưỡng hằng ngày của mình.', N'CamRuotVangUc.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP013', N'Củ Sả 100g', N'LH02', 1000.0000, 4000.0000, 15, N'100 g', NULL, N'CuSa.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP014', N'Xà lách lolo xanh 300g', N'LH02', 5000.0000, 20000.0000, 15, N'300 g', NULL, N'XaLach.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP015', N'Xà lách mỡ 300g', N'LH02', 3500.0000, 15000.0000, 15, N'300 g', NULL, N'XaLachMo.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP016', N'Nho xanh Shine Muscat Hàn Quốc 450g', N'LH02', 100000.0000, 300000.0000, 3, N'450 g', NULL, N'NhoXanhShineMuscatHanQuoc.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP017', N'Cải thảo', N'LH02', 5000.0000, 25000.0000, 10, N'1 kg', NULL, N'CaiThao.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP018', N'Cà rốt', N'LH02', 10000.0000, 30000.0000, 5, N'1 kg', N'Cà rốt là một loại củ rất quen thuộc trong các món ăn của người Việt. Cà rốt có hàm lượng chất dinh dưỡng và vitamin A cao, được xem là nguyên liệu cần thiết cho các món ăn dặm của trẻ nhỏ, giúp trẻ sáng mắt và cung cấp nguồn chất xơ dồi dào.

', N'CaRot.png')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP019', N'Khoai tây', N'LH02', 6000.0000, 20000.0000, 5, N'1 kg', N'Khoai tây chứa hàm lượng dưỡng chất cao. Ngoài tinh bột thì khoai tây còn cung cấp rất nhiều dinh dưỡng khác gồm các loại vitamin B, C cùng các loại khoáng chất: canxi, sắt, mangan, kẽm, sắt,...Đó cũng là lý do mà khoai tây được lựa chọn làm nguyên liệu chế biến rất nhiều món ăn ngon khác nhau trong bữa cơm gia đình.

Khoai tây là loại củ chứa nhiều chất dinh dưỡng, được nhiều người yêu thích. Đây là loại rau củ ít calo, không có chất béo và cholesterol. Bên cạnh đó, loại khoai này còn có hàm lượng vitamin C cao và là nguồn cung cấp kali, vitamin B6, chất xơ. Khoai tây có đặc tính chống oxy hóa và có thể giúp đẩy mạnh quá trình tiêu hóa, tăng cường sức khỏe tim mạch, làm giảm huyết áp, thậm chí ngăn ngừa nguy cơ ung thư.

Khoai tây là nguyên liệu để chế biến rất nhiều món ăn hấp dẫn, bổ dưỡng. Một trong những món ăn được các chị em nội trợ lựa chọn nhiều nhất đó là khoai tây nấu xương, khoai tây chiên, khoai tây nghiền hay thịt bò hầm khoai tây. Không những thế, khoai tây chính là nguyên liệu để tạo ra các món ăn vặt như bim bim, snack,...

', N'KhoaiTay.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP020', N'Cà chua Đà Lạt', N'LH02', 15000.0000, 35000.0000, 5, N'1 kg', N'Cà chua Đà Lạt được nuôi trồng trên những mảnh đất màu mỡ của Đà Lạt, cùng với khâu chăm sóc, kiểm soát chuyên nghiệp, đảm bảo mang đến cho người dùng nguồn thực phẩm tươi sạch, không chứa chất bảo quản, thuốc trừ sâu. Cà chua Đà Lạt chứa một số khoáng chất quan trọng như Canxi, sắt, magie, phốt pho, natri và kẽm và kali. Ngoài ra, trong cà chua còn chứa các vitamin C, B6, thiamin, riboflavin, niacin và folate.

Cà chua Đà Lạt có công dụng giúp cải thiện thị lực, ngăn ngừa ung thư, hỗ trợ làm đẹp da, giảm lượng đường trong máu, đem lại khả năng giảm cân hiệu quả, giúp xương chắc khỏe. Cà chua Đà Lạt thích hợp cho các món salad, xào, nấu, kẹp với bánh mỳ, hoặc dùng làm sinh tố, nước ép.

Lưu ý:

- Hạn sử dụng thực tế quý khách vui lòng xem trên bao bì.

- Hình sản phẩm chỉ mang tính chất minh họa, hình thực tế bao bì của sản phẩm tùy thời điểm sẽ khác so với thực tế.', N'CaChuaDaLat.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP021', N'Nạc vai bò Mỹ Thiên Vương ACE Foods khay 500g', N'LH03', 50000.0000, 150000.0000, 3, N'500 g', N'Nạc vai bò Mỹ là phần thịt có khối lượng lớn nhất gần cổ bò, phía trên chân trước. Phần thịt này nhiều nạc nên được rất nhiều khách hàng yêu thích và lựa chọn.



Đặc điểm của nạc vai bò Mỹ là tỷ lệ nạc cao, vân mỡ xem kẽ đều trong thớ thịt, chính vì thế mà thịt rất chắc, đậm đà hương vị với mùi thơm đặc trưng.



Nạc vai bò Mỹ là thực phẩm quen thuộc giàu dinh dưỡng có thể chế biến được nhiều món trong bữa ăn hàng ngày như: lẩu bò, phở bò, bò hầm, bò kho, bò xào, bò bít tết, bò sốt vang, bò nướng,... rất ngon miệng. Nạc vai bò Mỹ tốt cho người bị thiếu máu, nhiều phần axit amin, protein cho những người có thể chất yếu. Do đó, sử dụng nạc vai bò Mỹ giúp cải thiện sức đề kháng của cơ thể.

', N'NacVaiBoMy.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP022', N'Ba chỉ bò Mỹ Thiên Vương ACEFOODS khay 500g', N'LH03', 45000.0000, 140000.0000, 3, N'500 g', NULL, N'BaChiBoMy.png')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP023', N'Lõi rùa bắp bò Tây Ban Nha ACEFOODS khay 500g', N'LH03', 80000.0000, 210000.0000, 3, N'500 g', N'Lõi bắp rùa bò Tây Ban Nha là phần thịt bên trong của bắp bò, nơi tập trung nhiều gân nhất. Những đường gân đan xen, chạy dọc trên lõi thịt này có hình dạng giống như các vân trên mai rùa nên được gọi là lõi bắp rùa bò.

Phần thịt này có số lượng ít, thịt rất giòn, không dai. Khi thưởng thức sẽ cảm nhận được độ dẻo từ những lớp gần, rất ngọt thit, phù hợp với các món như lẩu, xào, hầm, luộc hoặc nấu mì, nấu phở.

Lõi bắp rùa bò Tây Ban Nha từ nhà máy Montellos – một trong ba nhà máy trực thuộc công ty Medina (cùng với Madrid, Valencia). Nhà máy Motellos nằm tại vùng có chất lượng thịt bò ngon nhất ở Tây Ban Nha và được coi là một trong những lò giết mổ tốt nhất của Châu Âu, với sản lượng 5000 con/ tuần

Cơ sở 6000 m2 với dây chuyền giết mổ tối đa 50 con/ giờ; nhà máy cắt công suất 75 tấn/ ngày; Tủ lạnh dung tích lên đến 5000 m3 để bảo quản tươi và đông lạnh.

Giống bò Tây Ban Nha được nuôi 50% ngũ cốc kết hợp 50% cỏ non nên thịt mềm và thơm hơn so với những con bò nuôi hàn toàn bằng cỏ.
', N'LoiRuaBapBo.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP024', N'Đùi bò', N'LH03', 100000.0000, 340000.0000, 3, N'1 kg', NULL, N'DuiBo.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP025', N'Thịt Thăn bò', N'LH03', 80000.0000, 182000.0000, 3, N'500 g', N'Sản phẩm Thăn thịt bò 300g do VinMart cung cấp được sản xuất và sơ chế theo quy trình khép kín dưới sự giám sát và kiểm tra nghiêm ngặt, đảm bảo vệ sinh an toàn thực phẩm. Thăn bò rất giàu chất sắt có tác dụng bổ sung lượng máu cho cơ thể và phòng tránh cơ thể bị thiếu máu nên đặc biệt thích hợp cho những người có thể chất yếu hoặc trí não đang bị suy giảm. Ngoài ra, thành phần axit amin, protein trong thăn bò rất cần thiết cho cơ thể của con người để gia tăng sức đề kháng của cơ thể.

Thăn bò là phần thịt mềm, được xem là phần thịt ngon nhất của con bò nên thường được các bà nội trợ lựa chọn sử dụng. Thịt thường được kết hợp cùng các gia vị, thực phẩm khác chế biến thành các món ăn ngon thường ngày như bò nướng, bít tết, bò xào rau củ...

Thăn thịt bò 300g là phần ngon nhất của con bò có thể chế biến nhiều món ăn ngon cho bữa cơm sum họp như: bò xào rau muống, bò xào bông cải, bò sốt vang... đều rất thơm ngon, cho cả gia đình những món ăn hợp khẩu vị, đậm đà.​

Lưu ý:

- Hạn sử dụng thực tế quý khách vui lòng xem trên bao bì.

- Hình sản phẩm chỉ mang tính chất minh họa, hình thực tế bao bì của sản phẩm tùy thời điểm sẽ khác so với thực tế.', N'ThitThanBo.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP026', N'Cá chim trắng biển tươi', N'LH03', 120000.0000, 240000.0000, 3, N'1 kg', N'Cá chim không chỉ là một món ăn ngon mà còn là loại thực phẩm đặc biệt bổ dưỡng. Thịt cá rất chắc và thơm, là nguyên liệu cho nhiều món ăn ngon. Cá chim trắng biển VinMart được lựa chọn từ những con cá tươi sống 100% và được bảo quản ở nhiệt độ lạnh thích hợp nên vẫn giữ được độ tươi ngon. Sản phẩm được kiểm nghiệm kĩ lưỡng về vệ sinh an toàn thực', N'CaChimTrang.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP027', N'Cá diêu hồng tươi làm sạch', N'LH03', 50000.0000, 123000.0000, 3, N'1 kg', N'Cá điêu hồng loại cá phổ biến có thịt nhiều, ít xương, thịt trắng, ngọt và lành tính, cá điêu hồng chế biến thành rất nhiều món ngon trong bữa cơm gia đình như cá điêu hồng kho, cá điêu hồng nấu canh chua, cá điêu hồng chiên giòn, cá điêu hồng sốt cà chua,...



Cá diêu hồng hay cá điêu hồng hay còn gọi là cá rô phi đỏ là một loài cá nước ngọt thuộc họ cá rô phi. Là một loại cá có chất lượng thịt thơm ngon, thịt cá diêu hồng có màu trắng, trong sạch, các thớ thịt được cấu trúc chắc và đặc biệt là thịt không quá nhiều xương. Đặc biệt là cá có giá trị dinh dưỡng cao, giàu protein, vitamin A, B, D và chất khoáng như phốt pho và iốt, ít chất béo hơn thịt nên dễ tiêu hóa.

', N'CaDieuHong.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP028', N'Cá nục gai tươi', N'LH03', 25000.0000, 70000.0000, 3, N'1 kg', NULL, N'CaNuc.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP029', N'Cua đồng xay', N'LH03', 100000.0000, 215000.0000, 3, N'1 kg', N'Cua đồng xay


Cua đồng rất giàu dinh dưỡng vì thế được các bà nội trợ chọn mua cho bữa cơm gia đình, nhất là vào thời tiết nắng nóng. Cua đồng xay có thể nấu canh, bún riêu cua, lẩu riêu cua,... các món ăn với cua đồng đều rất thanh mát, khiến ai cũng thích ăn.



Giá trị dinh dưỡng của cua đồng xay

Cua đồng cung cấp một lượng lớn vitamin như vitamin B1, vitamin B2, vitamin PP, các khoáng chất và muối khoáng như sắt, photpho và đặc biệt là hàm lượng canxi rất cao.

Trong 100g thịt cua đồng có khoảng 97 Kcal.

Tác dụng của cua đồng xay đối với sức khỏe

Cua đồng có tác dụng tán kết, hoạt huyết, hàn gắn xương, ngăn chặn hoặc điều trị nồng độ canxi huyết thấp,...

', N'CuaDongXay.jpg')
GO
INSERT [dbo].[tSanPham] ([MaSanPham], [TenSanPham], [MaLoaiHang], [DonGiaNhap], [DonGiaBan], [SoLuong], [TrongLuong], [MoTa], [AnhSanPham]) VALUES (N'SP030', N'Ếch làm sạch', N'LH03', 75000.0000, 180000.0000, 3, N'1 kg', N'Là thực phẩm không chỉ thơm ngon mà còn bổ dưỡng vô cùng. Thịt ếch chứa nhiều protein, vitamin tốt cho cơ thể nên thường được dùng làm nguyên liệu nấu các món ăn chính trong gia đình như thịt ếch kho tộ, ếch xào sả ớt, cháo ếch,... Thịt ếch Bách hoá XANH tươi ngon, bổ dưỡng, hàng mới mỗi ngày.



Giá trị dinh dưỡng của ếch làm sạch

Trong thịt ếch chứa nhiều protein, chất béo, các vitamin A, các vitamin B, các vitamin E, các vitamin D, các khoáng chất như kali, photpho, kẽm,...

Trong 100g thịt ếch có khoảng 73 Kcal.

Tác dụng của ếch làm sạch đối với sức khỏe

Khắc phục tổn thương tim

Giúp chữa lành vết thương

Tăng cường sức khỏe.

', N'EchLamSach.jpg')
GO
INSERT [dbo].[tTaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'admin', N'admin', N'Admin')
GO
INSERT [dbo].[tTaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'tua', N'1234', NULL)
GO
INSERT [dbo].[tTaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user', N'user', N'User')
GO
ALTER TABLE [dbo].[tChiTietHDB]  WITH CHECK ADD  CONSTRAINT [FK_tChiTietHDB_tHoaDonBan] FOREIGN KEY([SoHDB])
REFERENCES [dbo].[tHoaDonBan] ([SoHDB])
GO
ALTER TABLE [dbo].[tChiTietHDB] CHECK CONSTRAINT [FK_tChiTietHDB_tHoaDonBan]
GO
ALTER TABLE [dbo].[tChiTietHDB]  WITH CHECK ADD  CONSTRAINT [FK_tChiTietHDB_tSanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[tSanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[tChiTietHDB] CHECK CONSTRAINT [FK_tChiTietHDB_tSanPham]
GO
ALTER TABLE [dbo].[tChiTietHDN]  WITH CHECK ADD  CONSTRAINT [FK_tChiTietHDN_tHoaDonNhap] FOREIGN KEY([SoHDN])
REFERENCES [dbo].[tHoaDonNhap] ([SoHDN])
GO
ALTER TABLE [dbo].[tChiTietHDN] CHECK CONSTRAINT [FK_tChiTietHDN_tHoaDonNhap]
GO
ALTER TABLE [dbo].[tChiTietHDN]  WITH CHECK ADD  CONSTRAINT [FK_tChiTietHDN_tSanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[tSanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[tChiTietHDN] CHECK CONSTRAINT [FK_tChiTietHDN_tSanPham]
GO
ALTER TABLE [dbo].[tGioHang]  WITH CHECK ADD  CONSTRAINT [FK_tGioHang_tKhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[tKhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[tGioHang] CHECK CONSTRAINT [FK_tGioHang_tKhachHang]
GO
ALTER TABLE [dbo].[tHoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK_tHoaDonBan_tKhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[tKhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[tHoaDonBan] CHECK CONSTRAINT [FK_tHoaDonBan_tKhachHang]
GO
ALTER TABLE [dbo].[tHoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK_tHoaDonBan_tNhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[tNhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[tHoaDonBan] CHECK CONSTRAINT [FK_tHoaDonBan_tNhanVien]
GO
ALTER TABLE [dbo].[tHoaDonNhap]  WITH CHECK ADD  CONSTRAINT [FK_tHoaDonNhap_tNhaCungCap] FOREIGN KEY([MaNCC])
REFERENCES [dbo].[tNhaCungCap] ([MaNCC])
GO
ALTER TABLE [dbo].[tHoaDonNhap] CHECK CONSTRAINT [FK_tHoaDonNhap_tNhaCungCap]
GO
ALTER TABLE [dbo].[tHoaDonNhap]  WITH CHECK ADD  CONSTRAINT [FK_tHoaDonNhap_tNhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[tNhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[tHoaDonNhap] CHECK CONSTRAINT [FK_tHoaDonNhap_tNhanVien]
GO
ALTER TABLE [dbo].[tKhachHang]  WITH CHECK ADD  CONSTRAINT [FK_tKhachHang_tTaiKhoan] FOREIGN KEY([TenDangNhap])
REFERENCES [dbo].[tTaiKhoan] ([TenDangNhap])
GO
ALTER TABLE [dbo].[tKhachHang] CHECK CONSTRAINT [FK_tKhachHang_tTaiKhoan]
GO
ALTER TABLE [dbo].[tNhanVien]  WITH CHECK ADD  CONSTRAINT [FK_tNhanVien_tTaiKhoan] FOREIGN KEY([TenDangNhap])
REFERENCES [dbo].[tTaiKhoan] ([TenDangNhap])
GO
ALTER TABLE [dbo].[tNhanVien] CHECK CONSTRAINT [FK_tNhanVien_tTaiKhoan]
GO
ALTER TABLE [dbo].[tSanPham]  WITH CHECK ADD  CONSTRAINT [FK_tSanPham_tLoaiHang] FOREIGN KEY([MaLoaiHang])
REFERENCES [dbo].[tLoaiHang] ([MaLoaiHang])
GO
ALTER TABLE [dbo].[tSanPham] CHECK CONSTRAINT [FK_tSanPham_tLoaiHang]
GO
ALTER TABLE [dbo].[tSanPhamGioHang]  WITH CHECK ADD  CONSTRAINT [FK_tSanPhamGioHang_tGioHang] FOREIGN KEY([MaGioHang])
REFERENCES [dbo].[tGioHang] ([MaGioHang])
GO
ALTER TABLE [dbo].[tSanPhamGioHang] CHECK CONSTRAINT [FK_tSanPhamGioHang_tGioHang]
GO
ALTER TABLE [dbo].[tSanPhamGioHang]  WITH CHECK ADD  CONSTRAINT [FK_tSanPhamGioHang_tSanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[tSanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[tSanPhamGioHang] CHECK CONSTRAINT [FK_tSanPhamGioHang_tSanPham]
GO
