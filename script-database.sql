USE [QuanLySV]
GO
/****** Object:  Table [dbo].[BaiTapLon]    Script Date: 6/25/2020 8:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaiTapLon](
	[De_Tai] [nchar](100) NOT NULL,
	[Han_Nop] [datetime] NULL,
 CONSTRAINT [PK_BaiTapLon] PRIMARY KEY CLUSTERED 
(
	[De_Tai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DangKy]    Script Date: 6/25/2020 8:53:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DangKy](
	[MSSV] [int] NOT NULL,
	[Ma_Mon_Hoc] [nchar](10) NOT NULL,
 CONSTRAINT [PK_DangKy] PRIMARY KEY CLUSTERED 
(
	[MSSV] ASC,
	[Ma_Mon_Hoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DayMonHoc]    Script Date: 6/25/2020 8:53:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DayMonHoc](
	[MSGV] [int] NOT NULL,
	[Ma_Mon_Hoc] [nchar](10) NOT NULL,
 CONSTRAINT [PK_DayMonHoc] PRIMARY KEY CLUSTERED 
(
	[MSGV] ASC,
	[Ma_Mon_Hoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DaySV]    Script Date: 6/25/2020 8:53:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DaySV](
	[MSSV] [int] NOT NULL,
	[MSGV] [int] NOT NULL,
 CONSTRAINT [PK_DaySV] PRIMARY KEY CLUSTERED 
(
	[MSSV] ASC,
	[MSGV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiaoVien]    Script Date: 6/25/2020 8:53:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiaoVien](
	[MSGV] [int] NOT NULL,
	[Ten_Day_Du] [nchar](20) NULL,
	[Ngay_Sinh] [datetime] NULL,
	[Dia_Chi] [nchar](50) NULL,
	[SDT] [int] NULL,
	[Ma_Khoa] [nchar](10) NULL,
	[Ma_Lop] [nchar](10) NULL,
 CONSTRAINT [PK_GiaoVien] PRIMARY KEY CLUSTERED 
(
	[MSGV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Khoa]    Script Date: 6/25/2020 8:53:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Khoa](
	[Ma_Khoa] [nchar](10) NOT NULL,
	[Ten_Khoa] [nchar](20) NULL,
	[MSGV] [int] NULL,
 CONSTRAINT [PK_Khoa] PRIMARY KEY CLUSTERED 
(
	[Ma_Khoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LamBTLon]    Script Date: 6/25/2020 8:53:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LamBTLon](
	[MSSV] [int] NOT NULL,
	[De_Tai] [nchar](100) NOT NULL,
 CONSTRAINT [PK_LamBTLon] PRIMARY KEY CLUSTERED 
(
	[MSSV] ASC,
	[De_Tai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lop]    Script Date: 6/25/2020 8:53:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lop](
	[Ma_Lop] [nchar](10) NOT NULL,
	[Ten_Lop] [nchar](50) NULL,
	[So_SV] [int] NULL,
	[MSGV] [int] NULL,
	[Ma_Khoa] [nchar](10) NULL,
 CONSTRAINT [PK_Lop] PRIMARY KEY CLUSTERED 
(
	[Ma_Lop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonHoc]    Script Date: 6/25/2020 8:53:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonHoc](
	[Ma_Mon_Hoc] [nchar](10) NOT NULL,
	[Ten_Mon_Hoc] [nchar](20) NULL,
	[So_Tin_Chi] [int] NULL,
	[So_SV_Dang_Ky] [int] NULL,
	[Ngay_BD] [datetime] NULL,
	[Ngay_KT] [datetime] NULL,
 CONSTRAINT [PK_MonHoc] PRIMARY KEY CLUSTERED 
(
	[Ma_Mon_Hoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phong]    Script Date: 6/25/2020 8:53:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phong](
	[Ten_Phong] [nchar](10) NOT NULL,
	[Suc_Chua] [int] NULL,
 CONSTRAINT [PK_Phong] PRIMARY KEY CLUSTERED 
(
	[Ten_Phong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SDPhongHoc]    Script Date: 6/25/2020 8:53:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SDPhongHoc](
	[Ma_Mon_Hoc] [nchar](10) NOT NULL,
	[Ten_Phong] [nchar](10) NOT NULL,
 CONSTRAINT [PK_SDPhongHoc] PRIMARY KEY CLUSTERED 
(
	[Ma_Mon_Hoc] ASC,
	[Ten_Phong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SinhVien]    Script Date: 6/25/2020 8:53:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SinhVien](
	[MSSV] [nchar](20) NOT NULL,
	[Ten_Day_Du] [nchar](30) NULL,
	[Ngay_Sinh] [datetime] NULL,
	[Email] [nchar](20) NULL,
	[SDT] [int] NULL,
	[Khoa_Hoc] [nchar](10) NULL,
	[Diem_Ren_Luyen] [float] NULL,
	[Ma_Lop] [nchar](10) NULL,
 CONSTRAINT [PK_SinhVien] PRIMARY KEY CLUSTERED 
(
	[MSSV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[BaiTapLon] ([De_Tai], [Han_Nop]) VALUES (N'Dự toán chi tiêu trong một tháng của một của hàng tạp hóa                                           ', CAST(N'2020-06-29T00:00:00.000' AS DateTime))
INSERT [dbo].[BaiTapLon] ([De_Tai], [Han_Nop]) VALUES (N'Quản lý Bất động sản                                                                                ', CAST(N'2020-07-10T00:00:00.000' AS DateTime))
INSERT [dbo].[BaiTapLon] ([De_Tai], [Han_Nop]) VALUES (N'Thiết kế mô hình cầu vượt                                                                           ', CAST(N'2019-12-23T00:00:00.000' AS DateTime))
INSERT [dbo].[BaiTapLon] ([De_Tai], [Han_Nop]) VALUES (N'Vẽ sơ đồ ký túc xá                                                                                  ', CAST(N'2019-01-02T00:00:00.000' AS DateTime))
INSERT [dbo].[BaiTapLon] ([De_Tai], [Han_Nop]) VALUES (N'Xây dựng CSDL quản lý nhà sách                                                                      ', CAST(N'2020-07-08T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[DangKy] ([MSSV], [Ma_Mon_Hoc]) VALUES (10710403, N'CPM215.3  ')
INSERT [dbo].[DangKy] ([MSSV], [Ma_Mon_Hoc]) VALUES (10710403, N'GIT01.3   ')
INSERT [dbo].[DangKy] ([MSSV], [Ma_Mon_Hoc]) VALUES (10710403, N'QLY17.2   ')
INSERT [dbo].[DangKy] ([MSSV], [Ma_Mon_Hoc]) VALUES (14021025, N'KVT201.4  ')
INSERT [dbo].[DangKy] ([MSSV], [Ma_Mon_Hoc]) VALUES (14021025, N'QLY01.2   ')
INSERT [dbo].[DangKy] ([MSSV], [Ma_Mon_Hoc]) VALUES (14021025, N'QLY17.2   ')
INSERT [dbo].[DangKy] ([MSSV], [Ma_Mon_Hoc]) VALUES (51050005, N'KVT201.4  ')
INSERT [dbo].[DangKy] ([MSSV], [Ma_Mon_Hoc]) VALUES (51050005, N'QLY17.2   ')
INSERT [dbo].[DangKy] ([MSSV], [Ma_Mon_Hoc]) VALUES (51071045, N'KVT201.4  ')
INSERT [dbo].[DangKy] ([MSSV], [Ma_Mon_Hoc]) VALUES (51071045, N'QLY01.2   ')
INSERT [dbo].[DangKy] ([MSSV], [Ma_Mon_Hoc]) VALUES (51071108, N'MHT36.3   ')
INSERT [dbo].[DangKy] ([MSSV], [Ma_Mon_Hoc]) VALUES (51235468, N'KVT201.4  ')
INSERT [dbo].[DangKy] ([MSSV], [Ma_Mon_Hoc]) VALUES (51235468, N'QLY17.2   ')
INSERT [dbo].[DangKy] ([MSSV], [Ma_Mon_Hoc]) VALUES (61245865, N'QLY01.2   ')
INSERT [dbo].[DangKy] ([MSSV], [Ma_Mon_Hoc]) VALUES (61245865, N'QLY17.2   ')
GO
INSERT [dbo].[DayMonHoc] ([MSGV], [Ma_Mon_Hoc]) VALUES (12354, N'KVT201.4  ')
INSERT [dbo].[DayMonHoc] ([MSGV], [Ma_Mon_Hoc]) VALUES (23645, N'KVT201.4  ')
INSERT [dbo].[DayMonHoc] ([MSGV], [Ma_Mon_Hoc]) VALUES (58654, N'CPM215.3  ')
INSERT [dbo].[DayMonHoc] ([MSGV], [Ma_Mon_Hoc]) VALUES (58654, N'MHT36.3   ')
INSERT [dbo].[DayMonHoc] ([MSGV], [Ma_Mon_Hoc]) VALUES (78512, N'QLY01.2   ')
INSERT [dbo].[DayMonHoc] ([MSGV], [Ma_Mon_Hoc]) VALUES (78512, N'QLY17.2   ')
GO
INSERT [dbo].[DaySV] ([MSSV], [MSGV]) VALUES (12354, 51232548)
INSERT [dbo].[DaySV] ([MSSV], [MSGV]) VALUES (12354, 61245865)
INSERT [dbo].[DaySV] ([MSSV], [MSGV]) VALUES (23645, 14021025)
INSERT [dbo].[DaySV] ([MSSV], [MSGV]) VALUES (23645, 54021001)
INSERT [dbo].[DaySV] ([MSSV], [MSGV]) VALUES (58654, 10710403)
INSERT [dbo].[DaySV] ([MSSV], [MSGV]) VALUES (58654, 51071108)
INSERT [dbo].[DaySV] ([MSSV], [MSGV]) VALUES (78512, 51050005)
INSERT [dbo].[DaySV] ([MSSV], [MSGV]) VALUES (78512, 51071045)
INSERT [dbo].[DaySV] ([MSSV], [MSGV]) VALUES (78512, 51235468)
INSERT [dbo].[DaySV] ([MSSV], [MSGV]) VALUES (85456, 10400082)
GO
INSERT [dbo].[GiaoVien] ([MSGV], [Ten_Day_Du], [Ngay_Sinh], [Dia_Chi], [SDT], [Ma_Khoa], [Ma_Lop]) VALUES (12354, N'Lê Mỹ Linh          ', CAST(N'1985-12-12T00:00:00.000' AS DateTime), N'Quận 9                                            ', 5236589, N'KHCB      ', N'QTKD      ')
INSERT [dbo].[GiaoVien] ([MSGV], [Ten_Day_Du], [Ngay_Sinh], [Dia_Chi], [SDT], [Ma_Khoa], [Ma_Lop]) VALUES (23645, N'Nguyễn Khánh An     ', CAST(N'1982-08-07T00:00:00.000' AS DateTime), N'Quận 4                                            ', 8789546, N'DDT       ', N'KTBCVT    ')
INSERT [dbo].[GiaoVien] ([MSGV], [Ten_Day_Du], [Ngay_Sinh], [Dia_Chi], [SDT], [Ma_Khoa], [Ma_Lop]) VALUES (54689, N'Trịnh Vũ San        ', CAST(N'1978-07-17T00:00:00.000' AS DateTime), N'Quận 5                                            ', 7854526, N'CT        ', N'QLXD      ')
INSERT [dbo].[GiaoVien] ([MSGV], [Ten_Day_Du], [Ngay_Sinh], [Dia_Chi], [SDT], [Ma_Khoa], [Ma_Lop]) VALUES (58654, N'Trần Thanh Tâm      ', CAST(N'1979-10-03T00:00:00.000' AS DateTime), N'Thủ Đức                                           ', 7854251, N'CNTT      ', N'CNTT      ')
INSERT [dbo].[GiaoVien] ([MSGV], [Ten_Day_Du], [Ngay_Sinh], [Dia_Chi], [SDT], [Ma_Khoa], [Ma_Lop]) VALUES (78512, N'Nguyễn Diễm Huỳnh   ', CAST(N'1990-04-27T00:00:00.000' AS DateTime), N'Quận 2                                            ', 7452652, N'VTKT      ', N'KTVTDL    ')
INSERT [dbo].[GiaoVien] ([MSGV], [Ten_Day_Du], [Ngay_Sinh], [Dia_Chi], [SDT], [Ma_Khoa], [Ma_Lop]) VALUES (85456, N'Bùi Văn Mạnh        ', CAST(N'1989-02-25T00:00:00.000' AS DateTime), N'Thủ Đức                                           ', 8545651, N'CK        ', N'KTCK      ')
GO
INSERT [dbo].[Khoa] ([Ma_Khoa], [Ten_Khoa], [MSGV]) VALUES (N'CK        ', N'Cơ khí              ', 85456)
INSERT [dbo].[Khoa] ([Ma_Khoa], [Ten_Khoa], [MSGV]) VALUES (N'CNTT      ', N'Công nghệ thông tin ', 58654)
INSERT [dbo].[Khoa] ([Ma_Khoa], [Ten_Khoa], [MSGV]) VALUES (N'CT        ', N'Công trình          ', 54689)
INSERT [dbo].[Khoa] ([Ma_Khoa], [Ten_Khoa], [MSGV]) VALUES (N'DDT       ', N'Điện - Điện tử      ', 23645)
INSERT [dbo].[Khoa] ([Ma_Khoa], [Ten_Khoa], [MSGV]) VALUES (N'KHCB      ', N'Khoa học cơ bản     ', 12354)
INSERT [dbo].[Khoa] ([Ma_Khoa], [Ten_Khoa], [MSGV]) VALUES (N'VTKT      ', N'Vận tải kinh tế     ', 78512)
GO
INSERT [dbo].[LamBTLon] ([MSSV], [De_Tai]) VALUES (10400082, N'Thiết kế mô hình cầu vượt                                                                           ')
INSERT [dbo].[LamBTLon] ([MSSV], [De_Tai]) VALUES (10710403, N'Quản lý Bất động sản                                                                                ')
INSERT [dbo].[LamBTLon] ([MSSV], [De_Tai]) VALUES (14021025, N'Dự toán chi tiêu trong một tháng của một của hàng tạp hóa                                           ')
INSERT [dbo].[LamBTLon] ([MSSV], [De_Tai]) VALUES (51050005, N'Dự toán chi tiêu trong một tháng của một của hàng tạp hóa                                           ')
INSERT [dbo].[LamBTLon] ([MSSV], [De_Tai]) VALUES (51071108, N'Xây dựng CSDL quản lý nhà sách                                                                      ')
GO
INSERT [dbo].[Lop] ([Ma_Lop], [Ten_Lop], [So_SV], [MSGV], [Ma_Khoa]) VALUES (N'CNTT      ', N'Công nghệ thông tin                               ', 109, NULL, NULL)
INSERT [dbo].[Lop] ([Ma_Lop], [Ten_Lop], [So_SV], [MSGV], [Ma_Khoa]) VALUES (N'KTBCVT    ', N'Kinh tế bưu chính viễn thông                      ', 36, NULL, NULL)
INSERT [dbo].[Lop] ([Ma_Lop], [Ten_Lop], [So_SV], [MSGV], [Ma_Khoa]) VALUES (N'KTCK      ', N'Kỹ thuật cơ khí                                   ', 97, NULL, NULL)
INSERT [dbo].[Lop] ([Ma_Lop], [Ten_Lop], [So_SV], [MSGV], [Ma_Khoa]) VALUES (N'KTVT      ', N'Khai thác vận tải                                 ', 67, NULL, NULL)
INSERT [dbo].[Lop] ([Ma_Lop], [Ten_Lop], [So_SV], [MSGV], [Ma_Khoa]) VALUES (N'KTVTDL    ', N'Kinh tế vận tải du lịch                           ', 63, NULL, NULL)
INSERT [dbo].[Lop] ([Ma_Lop], [Ten_Lop], [So_SV], [MSGV], [Ma_Khoa]) VALUES (N'KTXD      ', N'Kỹ thuật xây dựng                                 ', 97, NULL, NULL)
INSERT [dbo].[Lop] ([Ma_Lop], [Ten_Lop], [So_SV], [MSGV], [Ma_Khoa]) VALUES (N'QLXD      ', N'Quản lý xây dựng                                  ', 48, NULL, NULL)
INSERT [dbo].[Lop] ([Ma_Lop], [Ten_Lop], [So_SV], [MSGV], [Ma_Khoa]) VALUES (N'QTKD      ', N'Quản trị kinh doanh                               ', 64, NULL, NULL)
INSERT [dbo].[Lop] ([Ma_Lop], [Ten_Lop], [So_SV], [MSGV], [Ma_Khoa]) VALUES (N'TDH       ', N'Tự động hóa                                       ', 76, NULL, NULL)
GO
INSERT [dbo].[MonHoc] ([Ma_Mon_Hoc], [Ten_Mon_Hoc], [So_Tin_Chi], [So_SV_Dang_Ky], [Ngay_BD], [Ngay_KT]) VALUES (N'CPM215.3  ', N'Lập trình nâng cao  ', 3, 89, CAST(N'2019-09-10T00:00:00.000' AS DateTime), CAST(N'2020-01-10T00:00:00.000' AS DateTime))
INSERT [dbo].[MonHoc] ([Ma_Mon_Hoc], [Ten_Mon_Hoc], [So_Tin_Chi], [So_SV_Dang_Ky], [Ngay_BD], [Ngay_KT]) VALUES (N'GIT01.3   ', N'Giải tích           ', 3, 70, CAST(N'2019-02-15T00:00:00.000' AS DateTime), CAST(N'2019-06-29T00:00:00.000' AS DateTime))
INSERT [dbo].[MonHoc] ([Ma_Mon_Hoc], [Ten_Mon_Hoc], [So_Tin_Chi], [So_SV_Dang_Ky], [Ngay_BD], [Ngay_KT]) VALUES (N'KVT201.4  ', N'Kinh tế học         ', 4, 75, CAST(N'2019-09-05T00:00:00.000' AS DateTime), CAST(N'2020-01-15T00:00:00.000' AS DateTime))
INSERT [dbo].[MonHoc] ([Ma_Mon_Hoc], [Ten_Mon_Hoc], [So_Tin_Chi], [So_SV_Dang_Ky], [Ngay_BD], [Ngay_KT]) VALUES (N'MHT36.3   ', N'Thiết kế Web        ', 3, 89, CAST(N'2019-09-10T00:00:00.000' AS DateTime), CAST(N'2020-01-12T00:00:00.000' AS DateTime))
INSERT [dbo].[MonHoc] ([Ma_Mon_Hoc], [Ten_Mon_Hoc], [So_Tin_Chi], [So_SV_Dang_Ky], [Ngay_BD], [Ngay_KT]) VALUES (N'QLY01.2   ', N'Pháp luật đại cương ', 2, 68, CAST(N'2019-09-05T00:00:00.000' AS DateTime), CAST(N'2020-01-30T00:00:00.000' AS DateTime))
INSERT [dbo].[MonHoc] ([Ma_Mon_Hoc], [Ten_Mon_Hoc], [So_Tin_Chi], [So_SV_Dang_Ky], [Ngay_BD], [Ngay_KT]) VALUES (N'QLY17.2   ', N'Kỹ năng mềm         ', 2, 78, CAST(N'2019-09-12T00:00:00.000' AS DateTime), CAST(N'2020-01-20T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Phong] ([Ten_Phong], [Suc_Chua]) VALUES (N'101C2     ', 100)
INSERT [dbo].[Phong] ([Ten_Phong], [Suc_Chua]) VALUES (N'102C2     ', 80)
INSERT [dbo].[Phong] ([Ten_Phong], [Suc_Chua]) VALUES (N'1E5       ', 60)
INSERT [dbo].[Phong] ([Ten_Phong], [Suc_Chua]) VALUES (N'201C2     ', 100)
INSERT [dbo].[Phong] ([Ten_Phong], [Suc_Chua]) VALUES (N'2E5       ', 75)
INSERT [dbo].[Phong] ([Ten_Phong], [Suc_Chua]) VALUES (N'4E6       ', 65)
GO
INSERT [dbo].[SDPhongHoc] ([Ma_Mon_Hoc], [Ten_Phong]) VALUES (N'CPM215.3  ', N'101C2     ')
INSERT [dbo].[SDPhongHoc] ([Ma_Mon_Hoc], [Ten_Phong]) VALUES (N'GIT01.3   ', N'1E5       ')
INSERT [dbo].[SDPhongHoc] ([Ma_Mon_Hoc], [Ten_Phong]) VALUES (N'KVT201.4  ', N'201C2     ')
INSERT [dbo].[SDPhongHoc] ([Ma_Mon_Hoc], [Ten_Phong]) VALUES (N'MHT36.3   ', N'4E6       ')
INSERT [dbo].[SDPhongHoc] ([Ma_Mon_Hoc], [Ten_Phong]) VALUES (N'QLY01.2   ', N'201C2     ')
INSERT [dbo].[SDPhongHoc] ([Ma_Mon_Hoc], [Ten_Phong]) VALUES (N'QLY17.2   ', N'2E5       ')
GO
INSERT [dbo].[SinhVien] ([MSSV], [Ten_Day_Du], [Ngay_Sinh], [Email], [SDT], [Khoa_Hoc], [Diem_Ren_Luyen], [Ma_Lop]) VALUES (N'10400082            ', N'Nguyễn Đình Chính             ', CAST(N'2000-07-15T00:00:00.000' AS DateTime), N'dchinnh@naver.com   ', 875412569, N'K59       ', 7, N'KTCK      ')
INSERT [dbo].[SinhVien] ([MSSV], [Ten_Day_Du], [Ngay_Sinh], [Email], [SDT], [Khoa_Hoc], [Diem_Ren_Luyen], [Ma_Lop]) VALUES (N'10710403            ', N'Nguyễn Thị Ngọc Hiền          ', CAST(N'2001-09-04T00:00:00.000' AS DateTime), N'hiennru@outlook.com ', 785469856, N'K60       ', 8, N'CNTT      ')
INSERT [dbo].[SinhVien] ([MSSV], [Ten_Day_Du], [Ngay_Sinh], [Email], [SDT], [Khoa_Hoc], [Diem_Ren_Luyen], [Ma_Lop]) VALUES (N'14021025            ', N'Nguyễn Trần Hoài Nghi         ', CAST(N'2000-08-28T00:00:00.000' AS DateTime), N'nnghi@outlook.com   ', 587465845, N'K59       ', 8, N'KTBCVT    ')
INSERT [dbo].[SinhVien] ([MSSV], [Ten_Day_Du], [Ngay_Sinh], [Email], [SDT], [Khoa_Hoc], [Diem_Ren_Luyen], [Ma_Lop]) VALUES (N'51050005            ', N'Võ Ngọc Ánh                   ', CAST(N'2001-09-29T00:00:00.000' AS DateTime), N'anhnek@naver.com    ', 845657895, N'K60       ', 7.5, N'KTVT      ')
INSERT [dbo].[SinhVien] ([MSSV], [Ten_Day_Du], [Ngay_Sinh], [Email], [SDT], [Khoa_Hoc], [Diem_Ren_Luyen], [Ma_Lop]) VALUES (N'51071045            ', N'Đoàn Lê Mỹ Linh               ', CAST(N'2000-06-20T00:00:00.000' AS DateTime), N'linkgm@gmail.com    ', 512458967, N'K59       ', 8, N'KTVT      ')
INSERT [dbo].[SinhVien] ([MSSV], [Ten_Day_Du], [Ngay_Sinh], [Email], [SDT], [Khoa_Hoc], [Diem_Ren_Luyen], [Ma_Lop]) VALUES (N'51071108            ', N'Trần Lê Thanh Tính            ', CAST(N'2000-05-26T00:00:00.000' AS DateTime), N'ttutc2@gmail.com    ', 325164789, N'K59       ', 7, N'CNTT      ')
INSERT [dbo].[SinhVien] ([MSSV], [Ten_Day_Du], [Ngay_Sinh], [Email], [SDT], [Khoa_Hoc], [Diem_Ren_Luyen], [Ma_Lop]) VALUES (N'51232548            ', N'Bùi Hồng Quốc                 ', CAST(N'2000-08-10T00:00:00.000' AS DateTime), N'hquocb@gmail.com    ', 215478956, N'K59       ', 7, N'QTKD      ')
INSERT [dbo].[SinhVien] ([MSSV], [Ten_Day_Du], [Ngay_Sinh], [Email], [SDT], [Khoa_Hoc], [Diem_Ren_Luyen], [Ma_Lop]) VALUES (N'51235468            ', N'Đặng Ngọc Thiên Kim           ', CAST(N'2000-02-14T00:00:00.000' AS DateTime), N'thinkim@gmail.com   ', 231547899, N'K59       ', 9, N'KTVTDL    ')
INSERT [dbo].[SinhVien] ([MSSV], [Ten_Day_Du], [Ngay_Sinh], [Email], [SDT], [Khoa_Hoc], [Diem_Ren_Luyen], [Ma_Lop]) VALUES (N'54021001            ', N'Nguyễn Thị Mỹ Ái              ', CAST(N'2001-04-17T00:00:00.000' AS DateTime), N'meiai@gmail.com     ', 548785565, N'K60       ', 6.5, N'KTBCVT    ')
INSERT [dbo].[SinhVien] ([MSSV], [Ten_Day_Du], [Ngay_Sinh], [Email], [SDT], [Khoa_Hoc], [Diem_Ren_Luyen], [Ma_Lop]) VALUES (N'61245865            ', N'Nguyễn Thu Thảo               ', CAST(N'2000-12-28T00:00:00.000' AS DateTime), N'hanlucha@gmail.com  ', 562135478, N'K59       ', 8.5, N'QTKD      ')
GO
ALTER TABLE [dbo].[GiaoVien]  WITH CHECK ADD FOREIGN KEY([Ma_Khoa])
REFERENCES [dbo].[Khoa] ([Ma_Khoa])
GO
ALTER TABLE [dbo].[GiaoVien]  WITH CHECK ADD  CONSTRAINT [FK__GiaoVien__Ma_Lop__48CFD27E] FOREIGN KEY([Ma_Lop])
REFERENCES [dbo].[Lop] ([Ma_Lop])
GO
ALTER TABLE [dbo].[GiaoVien] CHECK CONSTRAINT [FK__GiaoVien__Ma_Lop__48CFD27E]
GO
ALTER TABLE [dbo].[Khoa]  WITH CHECK ADD FOREIGN KEY([MSGV])
REFERENCES [dbo].[GiaoVien] ([MSGV])
GO
ALTER TABLE [dbo].[Lop]  WITH CHECK ADD  CONSTRAINT [FK__Lop__Ma_Khoa__4AB81AF0] FOREIGN KEY([Ma_Khoa])
REFERENCES [dbo].[Khoa] ([Ma_Khoa])
GO
ALTER TABLE [dbo].[Lop] CHECK CONSTRAINT [FK__Lop__Ma_Khoa__4AB81AF0]
GO
ALTER TABLE [dbo].[Lop]  WITH CHECK ADD  CONSTRAINT [FK__Lop__MSGV__49C3F6B7] FOREIGN KEY([MSGV])
REFERENCES [dbo].[GiaoVien] ([MSGV])
GO
ALTER TABLE [dbo].[Lop] CHECK CONSTRAINT [FK__Lop__MSGV__49C3F6B7]
GO
ALTER TABLE [dbo].[SinhVien]  WITH CHECK ADD  CONSTRAINT [FK__SinhVien__Ma_Lop__46E78A0C] FOREIGN KEY([Ma_Lop])
REFERENCES [dbo].[Lop] ([Ma_Lop])
GO
ALTER TABLE [dbo].[SinhVien] CHECK CONSTRAINT [FK__SinhVien__Ma_Lop__46E78A0C]
GO
