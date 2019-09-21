USE [QL_TRUNGTAM]
GO
/****** Object:  Table [dbo].[BANG_GIA_HOC_PHI]    Script Date: 9/8/2019 11:46:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BANG_GIA_HOC_PHI](
	[NGAY_AP_DUNG] [datetime] NOT NULL,
	[MA_KHOI] [uniqueidentifier] NOT NULL,
	[MA_MON] [uniqueidentifier] NOT NULL,
	[MA_LOAI] [uniqueidentifier] NOT NULL,
	[DON_GIA] [money] NOT NULL,
	[SO_BUOI] [float] NOT NULL,
 CONSTRAINT [PK_BANG_GIA_HOC_PHI] PRIMARY KEY CLUSTERED 
(
	[NGAY_AP_DUNG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BANG_LUONG]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BANG_LUONG](
	[MA_LOAI_LUONG] [uniqueidentifier] NOT NULL,
	[TEN_LOAI] [nvarchar](50) NULL,
	[SO_LUONG_MIN] [float] NULL,
	[SO_LUONG_MAX] [float] NULL,
	[DON_GIA] [float] NULL,
 CONSTRAINT [PK_BANG_LUONG] PRIMARY KEY CLUSTERED 
(
	[MA_LOAI_LUONG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BUOI_HOC]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BUOI_HOC](
	[MA_BUOI] [uniqueidentifier] NOT NULL,
	[STT_BUOI] [int] NULL,
	[THOI_GIAN] [datetime] NOT NULL,
	[TINH_TRANG] [bit] NULL,
	[MA_GV] [nchar](10) NULL,
	[MA_LUONG] [uniqueidentifier] NULL,
	[MA_LOP] [uniqueidentifier] NULL,
 CONSTRAINT [PK_BUOI_HOC] PRIMARY KEY CLUSTERED 
(
	[MA_BUOI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CHI_TIEU_NGOAI]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHI_TIEU_NGOAI](
	[MA_CT] [uniqueidentifier] NOT NULL,
	[TEN_CT] [nvarchar](50) NULL,
	[NGAY] [datetime] NULL,
	[THANH_TIEN] [decimal](18, 0) NULL,
 CONSTRAINT [PK_CHI_TIEU_NGOAI] PRIMARY KEY CLUSTERED 
(
	[MA_CT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CONG_NO]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONG_NO](
	[MA_CONG_NO] [uniqueidentifier] NOT NULL,
	[TEN_CONG_NO] [nvarchar](50) NULL,
	[TONG_TIEN] [money] NULL,
	[NGAY_THANH_TOAN] [datetime] NULL,
	[NGAY_LAP_CONG_NO] [datetime] NOT NULL,
	[MA_HS] [nchar](10) NOT NULL,
	[MA_KM] [uniqueidentifier] NULL,
	[TRANG_THAI] [bit] NOT NULL,
 CONSTRAINT [PK_CONG_NO] PRIMARY KEY CLUSTERED 
(
	[MA_CONG_NO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CT_BUOIHOC]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CT_BUOIHOC](
	[MA_BUOI] [uniqueidentifier] NOT NULL,
	[MA_HS] [nchar](10) NOT NULL,
	[DIEM_DANH_HS] [bit] NULL,
	[NHAN_XET_GV] [nvarchar](200) NULL,
	[BAI_TAP_VN] [nvarchar](50) NULL,
	[LI_DO_VANG] [nvarchar](100) NULL,
 CONSTRAINT [PK_CT_BUOIHOC] PRIMARY KEY CLUSTERED 
(
	[MA_BUOI] ASC,
	[MA_HS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CT_CONG_NO]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CT_CONG_NO](
	[MA_CONG_NO] [uniqueidentifier] NOT NULL,
	[MA_LOP] [uniqueidentifier] NOT NULL,
	[THANH_TIEN] [money] NULL,
 CONSTRAINT [PK_CT_CONG_NO] PRIMARY KEY CLUSTERED 
(
	[MA_CONG_NO] ASC,
	[MA_LOP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CT_HOADON]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CT_HOADON](
	[MA_HD] [uniqueidentifier] NOT NULL,
	[MA_BUOI] [uniqueidentifier] NOT NULL,
	[LUONG] [float] NULL,
 CONSTRAINT [PK_CT_HOADON] PRIMARY KEY CLUSTERED 
(
	[MA_HD] ASC,
	[MA_BUOI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CT_HOADON_NGOAIGIO]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CT_HOADON_NGOAIGIO](
	[MA_HD] [uniqueidentifier] NOT NULL,
	[MA_NGOAI_GIO] [uniqueidentifier] NOT NULL,
	[LUONG] [float] NULL,
 CONSTRAINT [PK_CT_HOADON_NGOAIGIO] PRIMARY KEY CLUSTERED 
(
	[MA_HD] ASC,
	[MA_NGOAI_GIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CT_LOP_HOC]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CT_LOP_HOC](
	[MA_LOP] [uniqueidentifier] NOT NULL,
	[MA_HS] [nchar](10) NOT NULL,
	[NGAY_VAO_HOC] [date] NULL,
 CONSTRAINT [PK_CT_LOP] PRIMARY KEY CLUSTERED 
(
	[MA_LOP] ASC,
	[MA_HS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GHI_DANH]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHI_DANH](
	[MA_TB] [uniqueidentifier] NOT NULL,
	[HO_TEN] [nvarchar](50) NULL,
	[NG_SINH] [datetime] NULL,
	[SDT] [varchar](15) NULL,
	[DIA_CHI] [nvarchar](100) NULL,
	[TRUONG] [nvarchar](50) NULL,
	[NOI_DUNG] [nvarchar](200) NULL,
	[TINH_TRANG] [bit] NULL,
 CONSTRAINT [PK_GHI_DANH] PRIMARY KEY CLUSTERED 
(
	[MA_TB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GIAO_VIEN]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GIAO_VIEN](
	[MA_GV] [nchar](10) NOT NULL,
	[HO_TEN] [nvarchar](50) NOT NULL,
	[SDT] [nchar](15) NOT NULL,
	[GIOI_TINH] [nvarchar](10) NULL,
	[EMAIL] [varchar](max) NULL,
	[TRANG_THAI] [bit] NULL,
	[NG_SINH] [date] NULL,
 CONSTRAINT [PK_giao_vien] PRIMARY KEY CLUSTERED 
(
	[MA_GV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HOA_DON]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOA_DON](
	[MA_HD] [uniqueidentifier] NOT NULL,
	[TEN_HD] [nvarchar](30) NOT NULL,
	[MA_GV] [nchar](10) NOT NULL,
	[NGAY_THANH_TOAN] [datetime] NOT NULL,
	[TONG_TIEN] [float] NULL,
 CONSTRAINT [PK_HOA_DON] PRIMARY KEY CLUSTERED 
(
	[MA_HD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HOC_SINH]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOC_SINH](
	[MA_HS] [nchar](10) NOT NULL,
	[HO_TEN] [nvarchar](50) NOT NULL,
	[NG_SINH] [datetime] NOT NULL,
	[GIOI_TINH] [nvarchar](10) NULL,
	[KHOI] [int] NULL,
	[TRUONG] [nvarchar](50) NULL,
	[SDT] [nchar](15) NULL,
	[DIA_CHI] [nvarchar](100) NULL,
	[SDT_PH] [nchar](15) NULL,
	[NG_VAO_HOC] [datetime] NULL,
	[PHU_HUYNH] [nvarchar](50) NULL,
 CONSTRAINT [PK_HOC_SINH] PRIMARY KEY CLUSTERED 
(
	[MA_HS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KHOI_LOP]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHOI_LOP](
	[MA_KHOI] [uniqueidentifier] NOT NULL,
	[TEN_KHOI] [nvarchar](50) NULL,
 CONSTRAINT [PK_KHOI_LOP] PRIMARY KEY CLUSTERED 
(
	[MA_KHOI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KHUYEN_MAI]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHUYEN_MAI](
	[MA_KM] [uniqueidentifier] NOT NULL,
	[TEN_KM] [nvarchar](50) NOT NULL,
	[SO_MON_DK] [int] NULL,
	[TIEN_GIAM] [int] NOT NULL,
 CONSTRAINT [PK_KHUYEN_MAI] PRIMARY KEY CLUSTERED 
(
	[MA_KM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LOAI_LOP]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOAI_LOP](
	[MA_LOAI] [uniqueidentifier] NOT NULL,
	[SI_SO] [int] NULL,
	[TEN_LOAI] [nvarchar](50) NULL,
 CONSTRAINT [PK_LOAI_LOP] PRIMARY KEY CLUSTERED 
(
	[MA_LOAI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LOP_HOC]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOP_HOC](
	[MA_LOP] [uniqueidentifier] NOT NULL,
	[TEN_LOP] [nvarchar](50) NOT NULL,
	[SI_SO] [int] NULL,
	[NGAY_AP_DUNG] [datetime] NULL,
	[MA_GV] [nchar](10) NOT NULL,
	[NGAY_KET_THUC] [date] NULL,
	[NGAY_BAT_DAU] [date] NULL,
	[TRANG_THAI] [smallint] NULL,
 CONSTRAINT [PK_LOP_HOC] PRIMARY KEY CLUSTERED 
(
	[MA_LOP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MON_HOC]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MON_HOC](
	[MA_MON] [uniqueidentifier] NOT NULL,
	[TEN_MON] [nvarchar](50) NULL,
 CONSTRAINT [PK_MON_HOC] PRIMARY KEY CLUSTERED 
(
	[MA_MON] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NGOAI_GIO]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NGOAI_GIO](
	[MA_NGOAI_GIO] [uniqueidentifier] NOT NULL,
	[MA_LUONG] [uniqueidentifier] NULL,
	[MA_GV] [nchar](10) NULL,
	[NGAY_LAM] [datetime] NOT NULL,
	[SO_LUONG] [int] NOT NULL,
	[TINH_TRANG] [bit] NULL,
 CONSTRAINT [PK_NGOAI_GIO_1] PRIMARY KEY CLUSTERED 
(
	[MA_NGOAI_GIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TAI_KHOAN]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAI_KHOAN](
	[ID] [nchar](10) NOT NULL,
	[TEN] [nvarchar](50) NOT NULL,
	[MAT_KHAU] [nchar](50) NOT NULL,
 CONSTRAINT [PK_TAI_KHOAN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[THOI_KHOA_BIEU]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[THOI_KHOA_BIEU](
	[MA_LOP] [uniqueidentifier] NOT NULL,
	[THU] [int] NOT NULL,
	[THOI_GIAN_BD] [time](7) NOT NULL,
	[THOI_GIAN_KT] [time](7) NOT NULL,
 CONSTRAINT [PK_THOI_KHOA_BIEU] PRIMARY KEY CLUSTERED 
(
	[MA_LOP] ASC,
	[THU] ASC,
	[THOI_GIAN_BD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TRANG_CHU]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRANG_CHU](
	[NGAY_AP_DUNG] [datetime] NOT NULL,
	[MUC_TIEU] [nvarchar](1000) NULL,
	[GIOI_THIEU] [nvarchar](1000) NULL,
	[DIA_CHI] [nvarchar](1000) NULL,
	[EMAIL] [nchar](50) NULL,
	[SDT1] [nchar](15) NULL,
	[SDT2] [nchar](15) NULL,
	[TIEUDE_MONHOC] [nvarchar](1000) NULL,
 CONSTRAINT [PK_TRANG_CHU] PRIMARY KEY CLUSTERED 
(
	[NGAY_AP_DUNG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[BANG_GIA_HOC_PHI] ([NGAY_AP_DUNG], [MA_KHOI], [MA_MON], [MA_LOAI], [DON_GIA], [SO_BUOI]) VALUES (CAST(N'2019-08-12 00:00:00.000' AS DateTime), N'80fb5e54-d91a-460c-bd57-7641a129d49e', N'9de30d72-a306-4b24-89ca-be3ea40f05af', N'd67c852b-e804-4663-a772-0df2b097ccb4', 500.0000, 3)
INSERT [dbo].[BANG_GIA_HOC_PHI] ([NGAY_AP_DUNG], [MA_KHOI], [MA_MON], [MA_LOAI], [DON_GIA], [SO_BUOI]) VALUES (CAST(N'2019-08-12 12:00:00.000' AS DateTime), N'524baf23-a597-4a3a-9c2b-9c0297bee630', N'9de30d72-a306-4b24-89ca-be3ea40f05af', N'd67c852b-e804-4663-a772-0df2b097ccb4', 150.0000, 3)
INSERT [dbo].[BANG_LUONG] ([MA_LOAI_LUONG], [TEN_LOAI], [SO_LUONG_MIN], [SO_LUONG_MAX], [DON_GIA]) VALUES (N'304991d9-3bc0-4df1-8ffb-08eddb715ab9', N'Trực văn phòng', 3.5, 0, 100000)
INSERT [dbo].[BANG_LUONG] ([MA_LOAI_LUONG], [TEN_LOAI], [SO_LUONG_MIN], [SO_LUONG_MAX], [DON_GIA]) VALUES (N'86349bb4-b964-4416-bb6c-259371ce3cab', N'Nhóm 1-2 HS', 1, 2, 150000)
INSERT [dbo].[BANG_LUONG] ([MA_LOAI_LUONG], [TEN_LOAI], [SO_LUONG_MIN], [SO_LUONG_MAX], [DON_GIA]) VALUES (N'a40b6ad3-5eaf-4917-a228-2f01ae2a2b85', N'daaa', 0, 0, 1)
INSERT [dbo].[BANG_LUONG] ([MA_LOAI_LUONG], [TEN_LOAI], [SO_LUONG_MIN], [SO_LUONG_MAX], [DON_GIA]) VALUES (N'4700cc57-7f13-45b1-839a-505f257ade4d', N'Nhóm 11-12 HS', 11, 12, 300000)
INSERT [dbo].[BANG_LUONG] ([MA_LOAI_LUONG], [TEN_LOAI], [SO_LUONG_MIN], [SO_LUONG_MAX], [DON_GIA]) VALUES (N'57acbd8d-8086-48bd-a719-65148d72af98', N'Tổ chức hoạt động trải nghiệm', 1, 0, 300000)
INSERT [dbo].[BANG_LUONG] ([MA_LOAI_LUONG], [TEN_LOAI], [SO_LUONG_MIN], [SO_LUONG_MAX], [DON_GIA]) VALUES (N'a7d58d4e-b095-4a47-a804-8027dcc4acae', N'Nhóm 7-8 HS', 7, 8, 240000)
INSERT [dbo].[BANG_LUONG] ([MA_LOAI_LUONG], [TEN_LOAI], [SO_LUONG_MIN], [SO_LUONG_MAX], [DON_GIA]) VALUES (N'a8798f1b-7fc3-4632-b881-b9d9489c8c8e', N'Nhóm 3-4 HS', 3, 4, 180000)
INSERT [dbo].[BANG_LUONG] ([MA_LOAI_LUONG], [TEN_LOAI], [SO_LUONG_MIN], [SO_LUONG_MAX], [DON_GIA]) VALUES (N'ea27e1f6-d934-4705-a9bc-dc0c1b78ab48', N'Nhóm 4-5 HS', 4, 5, 210000)
INSERT [dbo].[BANG_LUONG] ([MA_LOAI_LUONG], [TEN_LOAI], [SO_LUONG_MIN], [SO_LUONG_MAX], [DON_GIA]) VALUES (N'5899540b-35a6-4354-b263-f2d7481e95e6', N'Tổ trưởng chuyên môn (1 tháng)', NULL, NULL, 1000000)
INSERT [dbo].[BANG_LUONG] ([MA_LOAI_LUONG], [TEN_LOAI], [SO_LUONG_MIN], [SO_LUONG_MAX], [DON_GIA]) VALUES (N'c781a9f2-f233-419a-992d-f6f2dd8ef528', N'Nhóm 9-10 HS', 9, 10, 270000)
INSERT [dbo].[BUOI_HOC] ([MA_BUOI], [STT_BUOI], [THOI_GIAN], [TINH_TRANG], [MA_GV], [MA_LUONG], [MA_LOP]) VALUES (N'da14a5af-de51-47d9-bd63-5fb3814cfd35', 3, CAST(N'2019-09-06 00:00:00.000' AS DateTime), 1, N'1000000002', N'a7d58d4e-b095-4a47-a804-8027dcc4acae', N'5977c4a1-479b-4dd6-ba56-e134f3d48ac4')
INSERT [dbo].[BUOI_HOC] ([MA_BUOI], [STT_BUOI], [THOI_GIAN], [TINH_TRANG], [MA_GV], [MA_LUONG], [MA_LOP]) VALUES (N'e44add58-383a-467f-9fab-6d9717803a89', 4, CAST(N'2019-09-06 00:00:00.000' AS DateTime), 1, N'1000000001', N'a7d58d4e-b095-4a47-a804-8027dcc4acae', N'5977c4a1-479b-4dd6-ba56-e134f3d48ac4')
INSERT [dbo].[BUOI_HOC] ([MA_BUOI], [STT_BUOI], [THOI_GIAN], [TINH_TRANG], [MA_GV], [MA_LUONG], [MA_LOP]) VALUES (N'1b0b7fe7-ff10-405f-a0ed-94aa4147cf9a', 6, CAST(N'2019-09-06 00:00:00.000' AS DateTime), 1, N'1000000001', N'a7d58d4e-b095-4a47-a804-8027dcc4acae', N'5977c4a1-479b-4dd6-ba56-e134f3d48ac4')
INSERT [dbo].[BUOI_HOC] ([MA_BUOI], [STT_BUOI], [THOI_GIAN], [TINH_TRANG], [MA_GV], [MA_LUONG], [MA_LOP]) VALUES (N'1f791798-6174-47cd-9d8d-a02367a4c089', 5, CAST(N'2019-09-06 00:00:00.000' AS DateTime), 1, N'1000000001', N'a7d58d4e-b095-4a47-a804-8027dcc4acae', N'5977c4a1-479b-4dd6-ba56-e134f3d48ac4')
INSERT [dbo].[BUOI_HOC] ([MA_BUOI], [STT_BUOI], [THOI_GIAN], [TINH_TRANG], [MA_GV], [MA_LUONG], [MA_LOP]) VALUES (N'e4511af1-c6ce-483b-a864-afd0b2ef1e70', 2, CAST(N'2019-09-06 00:00:00.000' AS DateTime), 1, N'1000000001', N'a7d58d4e-b095-4a47-a804-8027dcc4acae', N'5977c4a1-479b-4dd6-ba56-e134f3d48ac4')
INSERT [dbo].[BUOI_HOC] ([MA_BUOI], [STT_BUOI], [THOI_GIAN], [TINH_TRANG], [MA_GV], [MA_LUONG], [MA_LOP]) VALUES (N'b0d693cd-cc46-493e-9084-cad5a3858685', 7, CAST(N'2019-09-09 00:00:00.000' AS DateTime), 1, N'1000000001', N'a7d58d4e-b095-4a47-a804-8027dcc4acae', N'5977c4a1-479b-4dd6-ba56-e134f3d48ac4')
INSERT [dbo].[BUOI_HOC] ([MA_BUOI], [STT_BUOI], [THOI_GIAN], [TINH_TRANG], [MA_GV], [MA_LUONG], [MA_LOP]) VALUES (N'73436cf4-e9b1-4ef6-9ed7-f48590b8cc78', 1, CAST(N'2019-08-31 00:00:00.000' AS DateTime), 1, N'1000000001', N'a7d58d4e-b095-4a47-a804-8027dcc4acae', N'5977c4a1-479b-4dd6-ba56-e134f3d48ac4')
INSERT [dbo].[CONG_NO] ([MA_CONG_NO], [TEN_CONG_NO], [TONG_TIEN], [NGAY_THANH_TOAN], [NGAY_LAP_CONG_NO], [MA_HS], [MA_KM], [TRANG_THAI]) VALUES (N'304991d9-3bc0-4df1-8ffb-08eddb715ab3', N'sd', 3000000.0000, NULL, CAST(N'2019-07-21 00:00:00.000' AS DateTime), N'2000000001', NULL, 1)
INSERT [dbo].[CONG_NO] ([MA_CONG_NO], [TEN_CONG_NO], [TONG_TIEN], [NGAY_THANH_TOAN], [NGAY_LAP_CONG_NO], [MA_HS], [MA_KM], [TRANG_THAI]) VALUES (N'304991d9-3bc0-4df1-8ffb-08eddb715ab7', N'dsdads', 2000000.0000, NULL, CAST(N'2019-08-20 00:00:00.000' AS DateTime), N'2000000001', NULL, 1)
INSERT [dbo].[CONG_NO] ([MA_CONG_NO], [TEN_CONG_NO], [TONG_TIEN], [NGAY_THANH_TOAN], [NGAY_LAP_CONG_NO], [MA_HS], [MA_KM], [TRANG_THAI]) VALUES (N'304991d9-3bc0-4df1-8ffb-08eddb715ab9', N'a', 5000000.0000, NULL, CAST(N'2019-06-21 00:00:00.000' AS DateTime), N'2000000001', NULL, 1)
INSERT [dbo].[CT_BUOIHOC] ([MA_BUOI], [MA_HS], [DIEM_DANH_HS], [NHAN_XET_GV], [BAI_TAP_VN], [LI_DO_VANG]) VALUES (N'da14a5af-de51-47d9-bd63-5fb3814cfd35', N'2000000005', 0, NULL, NULL, N'đau bụng')
INSERT [dbo].[CT_BUOIHOC] ([MA_BUOI], [MA_HS], [DIEM_DANH_HS], [NHAN_XET_GV], [BAI_TAP_VN], [LI_DO_VANG]) VALUES (N'da14a5af-de51-47d9-bd63-5fb3814cfd35', N'2000000007', 1, NULL, NULL, NULL)
INSERT [dbo].[CT_BUOIHOC] ([MA_BUOI], [MA_HS], [DIEM_DANH_HS], [NHAN_XET_GV], [BAI_TAP_VN], [LI_DO_VANG]) VALUES (N'e44add58-383a-467f-9fab-6d9717803a89', N'2000000005', 1, NULL, NULL, NULL)
INSERT [dbo].[CT_BUOIHOC] ([MA_BUOI], [MA_HS], [DIEM_DANH_HS], [NHAN_XET_GV], [BAI_TAP_VN], [LI_DO_VANG]) VALUES (N'e44add58-383a-467f-9fab-6d9717803a89', N'2000000007', 1, NULL, NULL, NULL)
INSERT [dbo].[CT_BUOIHOC] ([MA_BUOI], [MA_HS], [DIEM_DANH_HS], [NHAN_XET_GV], [BAI_TAP_VN], [LI_DO_VANG]) VALUES (N'1b0b7fe7-ff10-405f-a0ed-94aa4147cf9a', N'2000000005', 1, NULL, NULL, NULL)
INSERT [dbo].[CT_BUOIHOC] ([MA_BUOI], [MA_HS], [DIEM_DANH_HS], [NHAN_XET_GV], [BAI_TAP_VN], [LI_DO_VANG]) VALUES (N'1b0b7fe7-ff10-405f-a0ed-94aa4147cf9a', N'2000000007', 1, NULL, NULL, NULL)
INSERT [dbo].[CT_BUOIHOC] ([MA_BUOI], [MA_HS], [DIEM_DANH_HS], [NHAN_XET_GV], [BAI_TAP_VN], [LI_DO_VANG]) VALUES (N'1f791798-6174-47cd-9d8d-a02367a4c089', N'2000000005', 1, NULL, NULL, NULL)
INSERT [dbo].[CT_BUOIHOC] ([MA_BUOI], [MA_HS], [DIEM_DANH_HS], [NHAN_XET_GV], [BAI_TAP_VN], [LI_DO_VANG]) VALUES (N'1f791798-6174-47cd-9d8d-a02367a4c089', N'2000000007', 1, NULL, NULL, NULL)
INSERT [dbo].[CT_BUOIHOC] ([MA_BUOI], [MA_HS], [DIEM_DANH_HS], [NHAN_XET_GV], [BAI_TAP_VN], [LI_DO_VANG]) VALUES (N'e4511af1-c6ce-483b-a864-afd0b2ef1e70', N'2000000005', 0, NULL, NULL, N'csa')
INSERT [dbo].[CT_BUOIHOC] ([MA_BUOI], [MA_HS], [DIEM_DANH_HS], [NHAN_XET_GV], [BAI_TAP_VN], [LI_DO_VANG]) VALUES (N'e4511af1-c6ce-483b-a864-afd0b2ef1e70', N'2000000007', 1, NULL, NULL, NULL)
INSERT [dbo].[CT_BUOIHOC] ([MA_BUOI], [MA_HS], [DIEM_DANH_HS], [NHAN_XET_GV], [BAI_TAP_VN], [LI_DO_VANG]) VALUES (N'b0d693cd-cc46-493e-9084-cad5a3858685', N'2000000005', 1, NULL, NULL, NULL)
INSERT [dbo].[CT_BUOIHOC] ([MA_BUOI], [MA_HS], [DIEM_DANH_HS], [NHAN_XET_GV], [BAI_TAP_VN], [LI_DO_VANG]) VALUES (N'b0d693cd-cc46-493e-9084-cad5a3858685', N'2000000007', 1, NULL, NULL, NULL)
INSERT [dbo].[CT_HOADON] ([MA_HD], [MA_BUOI], [LUONG]) VALUES (N'5b3dcc04-e9dd-4912-bd51-116b24ddb1d9', N'1b0b7fe7-ff10-405f-a0ed-94aa4147cf9a', 240000)
INSERT [dbo].[CT_HOADON] ([MA_HD], [MA_BUOI], [LUONG]) VALUES (N'5b3dcc04-e9dd-4912-bd51-116b24ddb1d9', N'1f791798-6174-47cd-9d8d-a02367a4c089', 240000)
INSERT [dbo].[CT_HOADON] ([MA_HD], [MA_BUOI], [LUONG]) VALUES (N'f5276964-8a20-4c64-8c96-4874f163a879', N'da14a5af-de51-47d9-bd63-5fb3814cfd35', 240000)
INSERT [dbo].[CT_HOADON] ([MA_HD], [MA_BUOI], [LUONG]) VALUES (N'f8efdd6f-f167-4148-b192-5512cd39bb6e', N'e44add58-383a-467f-9fab-6d9717803a89', 240000)
INSERT [dbo].[CT_HOADON] ([MA_HD], [MA_BUOI], [LUONG]) VALUES (N'f8efdd6f-f167-4148-b192-5512cd39bb6e', N'e4511af1-c6ce-483b-a864-afd0b2ef1e70', 240000)
INSERT [dbo].[CT_HOADON] ([MA_HD], [MA_BUOI], [LUONG]) VALUES (N'51f1efad-960b-429a-a268-8f91afb428c0', N'b0d693cd-cc46-493e-9084-cad5a3858685', 240000)
INSERT [dbo].[CT_HOADON] ([MA_HD], [MA_BUOI], [LUONG]) VALUES (N'3d366c2d-07d5-4a74-921d-d5d7c5a5291b', N'73436cf4-e9b1-4ef6-9ed7-f48590b8cc78', 240000)
INSERT [dbo].[CT_HOADON_NGOAIGIO] ([MA_HD], [MA_NGOAI_GIO], [LUONG]) VALUES (N'f8efdd6f-f167-4148-b192-5512cd39bb6e', N'7c2c8df5-3036-4f3d-95d9-436115f8504e', 100000)
INSERT [dbo].[CT_LOP_HOC] ([MA_LOP], [MA_HS], [NGAY_VAO_HOC]) VALUES (N'5977c4a1-479b-4dd6-ba56-e134f3d48ac4', N'2000000005', NULL)
INSERT [dbo].[CT_LOP_HOC] ([MA_LOP], [MA_HS], [NGAY_VAO_HOC]) VALUES (N'5977c4a1-479b-4dd6-ba56-e134f3d48ac4', N'2000000007', NULL)
INSERT [dbo].[GIAO_VIEN] ([MA_GV], [HO_TEN], [SDT], [GIOI_TINH], [EMAIL], [TRANG_THAI], [NG_SINH]) VALUES (N'1000000001', N'Phạm Hữu Ngọc', N'0372417647     ', N'nam', N'phamhuungoc0507@gmail.com', NULL, CAST(N'1998-08-07' AS Date))
INSERT [dbo].[GIAO_VIEN] ([MA_GV], [HO_TEN], [SDT], [GIOI_TINH], [EMAIL], [TRANG_THAI], [NG_SINH]) VALUES (N'1000000002', N'Lê Văn Tèo', N'0372417647     ', N'nữ', N'phamhuungoc0507@gmail.com', NULL, CAST(N'1998-12-12' AS Date))
INSERT [dbo].[GIAO_VIEN] ([MA_GV], [HO_TEN], [SDT], [GIOI_TINH], [EMAIL], [TRANG_THAI], [NG_SINH]) VALUES (N'1000000003', N'Nguyễn Văn A', N'0372417647     ', N'bê đê', N'imvietha@yahoo.com', NULL, CAST(N'1998-11-12' AS Date))
INSERT [dbo].[GIAO_VIEN] ([MA_GV], [HO_TEN], [SDT], [GIOI_TINH], [EMAIL], [TRANG_THAI], [NG_SINH]) VALUES (N'1000000004', N'Demo', N'0372417647     ', N'nam', N'phamhuungoc0507@gmail.com', NULL, CAST(N'2019-09-04' AS Date))
INSERT [dbo].[HOA_DON] ([MA_HD], [TEN_HD], [MA_GV], [NGAY_THANH_TOAN], [TONG_TIEN]) VALUES (N'304991d9-3bc0-4df1-8ffb-01eddb715ab0', N'thang8', N'1000000002', CAST(N'2019-06-23 00:00:00.000' AS DateTime), 5000000)
INSERT [dbo].[HOA_DON] ([MA_HD], [TEN_HD], [MA_GV], [NGAY_THANH_TOAN], [TONG_TIEN]) VALUES (N'304991d9-3bc0-4df1-8ffb-08cddb715ab0', N'thang6', N'1000000002', CAST(N'2019-07-21 00:00:00.000' AS DateTime), 2000000)
INSERT [dbo].[HOA_DON] ([MA_HD], [TEN_HD], [MA_GV], [NGAY_THANH_TOAN], [TONG_TIEN]) VALUES (N'304991d9-3bc0-4df1-8ffb-08eddb715ab0', N'THANG7', N'1000000001', CAST(N'2019-08-28 00:00:00.000' AS DateTime), 2000000)
INSERT [dbo].[HOA_DON] ([MA_HD], [TEN_HD], [MA_GV], [NGAY_THANH_TOAN], [TONG_TIEN]) VALUES (N'304991d9-3bc0-4df1-8ffb-08eddb725ab0', N'thang6', N'1000000001', CAST(N'2019-07-20 00:00:00.000' AS DateTime), 2000000)
INSERT [dbo].[HOA_DON] ([MA_HD], [TEN_HD], [MA_GV], [NGAY_THANH_TOAN], [TONG_TIEN]) VALUES (N'304991d9-3bc0-4df1-8ffb-08eddc715ab0', N'thang7', N'1000000002', CAST(N'2019-08-28 00:00:00.000' AS DateTime), 1000000)
INSERT [dbo].[HOA_DON] ([MA_HD], [TEN_HD], [MA_GV], [NGAY_THANH_TOAN], [TONG_TIEN]) VALUES (N'5b3dcc04-e9dd-4912-bd51-116b24ddb1d9', N'Lương tháng 9/2019', N'1000000001', CAST(N'2019-09-08 16:24:42.987' AS DateTime), 480000)
INSERT [dbo].[HOA_DON] ([MA_HD], [TEN_HD], [MA_GV], [NGAY_THANH_TOAN], [TONG_TIEN]) VALUES (N'f5276964-8a20-4c64-8c96-4874f163a879', N'Lương tháng 9/2019', N'1000000002', CAST(N'2019-09-07 11:44:52.103' AS DateTime), 240000)
INSERT [dbo].[HOA_DON] ([MA_HD], [TEN_HD], [MA_GV], [NGAY_THANH_TOAN], [TONG_TIEN]) VALUES (N'f8efdd6f-f167-4148-b192-5512cd39bb6e', N'Lương tháng 9/2019', N'1000000001', CAST(N'2019-09-07 11:44:52.940' AS DateTime), 580000)
INSERT [dbo].[HOA_DON] ([MA_HD], [TEN_HD], [MA_GV], [NGAY_THANH_TOAN], [TONG_TIEN]) VALUES (N'51f1efad-960b-429a-a268-8f91afb428c0', N'Lương tháng 9/2019', N'1000000001', CAST(N'2019-09-08 16:26:06.837' AS DateTime), 240000)
INSERT [dbo].[HOA_DON] ([MA_HD], [TEN_HD], [MA_GV], [NGAY_THANH_TOAN], [TONG_TIEN]) VALUES (N'3d366c2d-07d5-4a74-921d-d5d7c5a5291b', N'Lương tháng 8/2019', N'1000000001', CAST(N'2019-09-08 16:24:50.457' AS DateTime), 240000)
INSERT [dbo].[HOC_SINH] ([MA_HS], [HO_TEN], [NG_SINH], [GIOI_TINH], [KHOI], [TRUONG], [SDT], [DIA_CHI], [SDT_PH], [NG_VAO_HOC], [PHU_HUYNH]) VALUES (N'2000000001', N'Trần Khánh Nhật', CAST(N'1998-07-11 00:00:00.000' AS DateTime), N'Nam', 8, N'THPT Lấp Vò 3', N'0388174111     ', N'Số 140 Lê Trọng Tấn, Tây Thạnh, Tân Phú, HCM', NULL, NULL, N'Nguyễn Văn B')
INSERT [dbo].[HOC_SINH] ([MA_HS], [HO_TEN], [NG_SINH], [GIOI_TINH], [KHOI], [TRUONG], [SDT], [DIA_CHI], [SDT_PH], [NG_VAO_HOC], [PHU_HUYNH]) VALUES (N'2000000002', N'Phạm Hữu Ngọc', CAST(N'1998-07-05 00:00:00.000' AS DateTime), N'Nam', 10, N'THPT Lấp Vò 3', N'08123809091    ', N'Số', NULL, NULL, N'Nguyễn Văn B')
INSERT [dbo].[HOC_SINH] ([MA_HS], [HO_TEN], [NG_SINH], [GIOI_TINH], [KHOI], [TRUONG], [SDT], [DIA_CHI], [SDT_PH], [NG_VAO_HOC], [PHU_HUYNH]) VALUES (N'2000000003', N'Nguyễn Văn Lành', CAST(N'1997-04-09 00:00:00.000' AS DateTime), N'Nam', 10, N'THPT Lấp Vò 3', N'8954303190     ', N'Số 140 Lê Trọng Tấn, Tây Thạnh, Tân Phú, HCM', NULL, NULL, N'Nguyễn Văn B')
INSERT [dbo].[HOC_SINH] ([MA_HS], [HO_TEN], [NG_SINH], [GIOI_TINH], [KHOI], [TRUONG], [SDT], [DIA_CHI], [SDT_PH], [NG_VAO_HOC], [PHU_HUYNH]) VALUES (N'2000000004', N'Nguyễn Văn Nam', CAST(N'2001-08-09 00:00:00.000' AS DateTime), N'Nam', 12, N'THPT Lấp Vò 3', N'0388174111     ', N'Số 140 Lê Trọng Tấn, Tây Thạnh, Tân Phú, HCM', NULL, NULL, N'Nguyễn Văn B')
INSERT [dbo].[HOC_SINH] ([MA_HS], [HO_TEN], [NG_SINH], [GIOI_TINH], [KHOI], [TRUONG], [SDT], [DIA_CHI], [SDT_PH], [NG_VAO_HOC], [PHU_HUYNH]) VALUES (N'2000000005', N'Trần Nhật Duật', CAST(N'2004-02-29 00:00:00.000' AS DateTime), N'Nam', 10, N'THPT Lấp Vò 3', N'8954303190     ', N'Số 140 Lê Trọng Tấn, Tây Thạnh, Tân Phú, HCM', NULL, NULL, N'Nguyễn Văn B')
INSERT [dbo].[HOC_SINH] ([MA_HS], [HO_TEN], [NG_SINH], [GIOI_TINH], [KHOI], [TRUONG], [SDT], [DIA_CHI], [SDT_PH], [NG_VAO_HOC], [PHU_HUYNH]) VALUES (N'2000000006', N'Mạc Đỉnh Chi', CAST(N'2005-01-09 00:00:00.000' AS DateTime), N'Nam', 10, N'THPT Lấp Vò 3', N'0388174111     ', N'Số 140 Lê Trọng Tấn, Tây Thạnh, Tân Phú, HCM', NULL, NULL, N'Nguyễn Văn B')
INSERT [dbo].[HOC_SINH] ([MA_HS], [HO_TEN], [NG_SINH], [GIOI_TINH], [KHOI], [TRUONG], [SDT], [DIA_CHI], [SDT_PH], [NG_VAO_HOC], [PHU_HUYNH]) VALUES (N'2000000007', N'Trần Hưng Đạo', CAST(N'1998-07-11 00:00:00.000' AS DateTime), N'Nam', 10, N'THPT Trần Hưng Đạo', N'0584949974     ', N'150 Lê Trọng Tấn', NULL, NULL, N'Nguyễn Văn A')
INSERT [dbo].[KHOI_LOP] ([MA_KHOI], [TEN_KHOI]) VALUES (N'80fb5e54-d91a-460c-bd57-7641a129d49e', N'THPT')
INSERT [dbo].[KHOI_LOP] ([MA_KHOI], [TEN_KHOI]) VALUES (N'524baf23-a597-4a3a-9c2b-9c0297bee630', N'THCS')
INSERT [dbo].[KHUYEN_MAI] ([MA_KM], [TEN_KM], [SO_MON_DK], [TIEN_GIAM]) VALUES (N'c29848aa-31e4-4615-8e96-59938ab3076e', N'Đăng ký 3 môn', 3, 30)
INSERT [dbo].[KHUYEN_MAI] ([MA_KM], [TEN_KM], [SO_MON_DK], [TIEN_GIAM]) VALUES (N'e45de7dd-6b9f-4a3f-9698-7876c0a28814', N'Đăng ký 4 môn', 4, 40)
INSERT [dbo].[KHUYEN_MAI] ([MA_KM], [TEN_KM], [SO_MON_DK], [TIEN_GIAM]) VALUES (N'aff0d742-cea7-4723-b4b3-958ac6ef8303', N'Đăng ký 2 môn', 2, 20)
INSERT [dbo].[LOAI_LOP] ([MA_LOAI], [SI_SO], [TEN_LOAI]) VALUES (N'd67c852b-e804-4663-a772-0df2b097ccb4', NULL, N'loại 4-5')
INSERT [dbo].[LOAI_LOP] ([MA_LOAI], [SI_SO], [TEN_LOAI]) VALUES (N'21d678c1-3bbb-4f93-aeda-6d148331b87a', NULL, N'loại 1 - 2')
INSERT [dbo].[LOP_HOC] ([MA_LOP], [TEN_LOP], [SI_SO], [NGAY_AP_DUNG], [MA_GV], [NGAY_KET_THUC], [NGAY_BAT_DAU], [TRANG_THAI]) VALUES (N'5793bd93-3c17-4b3b-b01e-0c457b9e5c67', N'bac', 6, CAST(N'2019-08-12 00:00:00.000' AS DateTime), N'1000000001', NULL, CAST(N'2019-08-20' AS Date), -1)
INSERT [dbo].[LOP_HOC] ([MA_LOP], [TEN_LOP], [SI_SO], [NGAY_AP_DUNG], [MA_GV], [NGAY_KET_THUC], [NGAY_BAT_DAU], [TRANG_THAI]) VALUES (N'c688556e-26e3-4ca5-9e74-64903f6b58c3', N'Toán Lớp 7 A', NULL, NULL, N'1000000002', NULL, NULL, NULL)
INSERT [dbo].[LOP_HOC] ([MA_LOP], [TEN_LOP], [SI_SO], [NGAY_AP_DUNG], [MA_GV], [NGAY_KET_THUC], [NGAY_BAT_DAU], [TRANG_THAI]) VALUES (N'063b29b6-8b64-4d3e-bba9-aab2567fdbd9', N'abc', 6, CAST(N'2019-08-12 00:00:00.000' AS DateTime), N'1000000001', NULL, CAST(N'2019-08-20' AS Date), -1)
INSERT [dbo].[LOP_HOC] ([MA_LOP], [TEN_LOP], [SI_SO], [NGAY_AP_DUNG], [MA_GV], [NGAY_KET_THUC], [NGAY_BAT_DAU], [TRANG_THAI]) VALUES (N'96e609c7-3ea8-487e-a763-b9470dca3a0b', N'Toán Lớp 6 A', NULL, NULL, N'1000000002', NULL, NULL, NULL)
INSERT [dbo].[LOP_HOC] ([MA_LOP], [TEN_LOP], [SI_SO], [NGAY_AP_DUNG], [MA_GV], [NGAY_KET_THUC], [NGAY_BAT_DAU], [TRANG_THAI]) VALUES (N'ece594df-d91c-4dcb-8cd4-c32cdb626d16', N'Thầy A', 3, CAST(N'2019-08-12 00:00:00.000' AS DateTime), N'1000000001', NULL, CAST(N'2019-08-20' AS Date), 1)
INSERT [dbo].[LOP_HOC] ([MA_LOP], [TEN_LOP], [SI_SO], [NGAY_AP_DUNG], [MA_GV], [NGAY_KET_THUC], [NGAY_BAT_DAU], [TRANG_THAI]) VALUES (N'5977c4a1-479b-4dd6-ba56-e134f3d48ac4', N'Lớp X', 7, CAST(N'2019-08-12 00:00:00.000' AS DateTime), N'1000000001', NULL, CAST(N'2019-08-20' AS Date), 1)
INSERT [dbo].[MON_HOC] ([MA_MON], [TEN_MON]) VALUES (N'19d7da79-0782-4b51-acc0-19406850607f', N'Sinh')
INSERT [dbo].[MON_HOC] ([MA_MON], [TEN_MON]) VALUES (N'9de30d72-a306-4b24-89ca-be3ea40f05af', N'Lý')
INSERT [dbo].[MON_HOC] ([MA_MON], [TEN_MON]) VALUES (N'2b3d6f21-f114-4901-8903-db6bb091c737', N'Hóa')
INSERT [dbo].[NGOAI_GIO] ([MA_NGOAI_GIO], [MA_LUONG], [MA_GV], [NGAY_LAM], [SO_LUONG], [TINH_TRANG]) VALUES (N'7c2c8df5-3036-4f3d-95d9-436115f8504e', N'304991d9-3bc0-4df1-8ffb-08eddb715ab9', N'1000000001', CAST(N'2019-09-04 00:00:00.000' AS DateTime), 1, 1)
INSERT [dbo].[TAI_KHOAN] ([ID], [TEN], [MAT_KHAU]) VALUES (N'1000000002', N'gv', N'123                                               ')
INSERT [dbo].[TAI_KHOAN] ([ID], [TEN], [MAT_KHAU]) VALUES (N'1000000004', N'abc', N'123                                               ')
INSERT [dbo].[TAI_KHOAN] ([ID], [TEN], [MAT_KHAU]) VALUES (N'2000000001', N'hs', N'123456                                            ')
INSERT [dbo].[TAI_KHOAN] ([ID], [TEN], [MAT_KHAU]) VALUES (N'8000000001', N'user', N'123                                               ')
INSERT [dbo].[TAI_KHOAN] ([ID], [TEN], [MAT_KHAU]) VALUES (N'8000000002', N'nhat', N'123                                               ')
INSERT [dbo].[TAI_KHOAN] ([ID], [TEN], [MAT_KHAU]) VALUES (N'9000000001', N'admin', N'123                                               ')
INSERT [dbo].[TAI_KHOAN] ([ID], [TEN], [MAT_KHAU]) VALUES (N'9000000002', N'ngoc', N'123                                               ')
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'5793bd93-3c17-4b3b-b01e-0c457b9e5c67', 1, CAST(N'12:03:00' AS Time), CAST(N'13:03:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'5793bd93-3c17-4b3b-b01e-0c457b9e5c67', 3, CAST(N'12:03:00' AS Time), CAST(N'13:03:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'5793bd93-3c17-4b3b-b01e-0c457b9e5c67', 5, CAST(N'12:03:00' AS Time), CAST(N'13:03:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'c688556e-26e3-4ca5-9e74-64903f6b58c3', 1, CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'c688556e-26e3-4ca5-9e74-64903f6b58c3', 3, CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'c688556e-26e3-4ca5-9e74-64903f6b58c3', 5, CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'063b29b6-8b64-4d3e-bba9-aab2567fdbd9', 1, CAST(N'03:02:00' AS Time), CAST(N'02:03:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'063b29b6-8b64-4d3e-bba9-aab2567fdbd9', 3, CAST(N'03:02:00' AS Time), CAST(N'02:03:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'063b29b6-8b64-4d3e-bba9-aab2567fdbd9', 5, CAST(N'03:02:00' AS Time), CAST(N'02:03:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'96e609c7-3ea8-487e-a763-b9470dca3a0b', 1, CAST(N'07:00:00' AS Time), CAST(N'09:00:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'96e609c7-3ea8-487e-a763-b9470dca3a0b', 3, CAST(N'07:00:00' AS Time), CAST(N'09:00:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'96e609c7-3ea8-487e-a763-b9470dca3a0b', 5, CAST(N'07:00:00' AS Time), CAST(N'09:00:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'ece594df-d91c-4dcb-8cd4-c32cdb626d16', 1, CAST(N'15:00:00' AS Time), CAST(N'17:00:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'ece594df-d91c-4dcb-8cd4-c32cdb626d16', 3, CAST(N'15:00:00' AS Time), CAST(N'17:00:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'ece594df-d91c-4dcb-8cd4-c32cdb626d16', 5, CAST(N'15:00:00' AS Time), CAST(N'17:00:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'5977c4a1-479b-4dd6-ba56-e134f3d48ac4', 1, CAST(N'12:03:00' AS Time), CAST(N'13:03:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'5977c4a1-479b-4dd6-ba56-e134f3d48ac4', 3, CAST(N'12:03:00' AS Time), CAST(N'13:03:00' AS Time))
INSERT [dbo].[THOI_KHOA_BIEU] ([MA_LOP], [THU], [THOI_GIAN_BD], [THOI_GIAN_KT]) VALUES (N'5977c4a1-479b-4dd6-ba56-e134f3d48ac4', 5, CAST(N'12:03:00' AS Time), CAST(N'13:03:00' AS Time))
INSERT [dbo].[TRANG_CHU] ([NGAY_AP_DUNG], [MUC_TIEU], [GIOI_THIEU], [DIA_CHI], [EMAIL], [SDT1], [SDT2], [TIEUDE_MONHOC]) VALUES (CAST(N'2019-08-28 00:00:00.000' AS DateTime), N'Chúng tôi không cho bạn kiến thức,
Chúng tôi cho bạn khả năng gặt lấy chúng.
Vì bạn là chính bạn!
', N'Về chúng tôi
Gồm tập thể các giáo viên lâu năm kinh nghiệm đang công tác tại các trường trung học trọng địa bàn TPHCM.
Chúng tôi có chung một niềm tin rằng việc cải thiện thái độ học tập là con đường ngắn nhất để học sinh có thể tự chinh phục một nền kiến thức rộng lớn.
Chúng tôi có chung quan điểm rằng một học sinh giỏi, trước tiên phải đạt nhân cách tốt. Giáo dục đạo đức là một trong những mục tiêu mà Trung tâm KHTN đặt lên hàng đầu và thực hiện thường xuyên trong mỗi buổi học.
Chúng tôi có chung một mục tiêu là đào tạo mầm xanh của tương lai, giỏi và tốt hơn chúng tôi ở hiện tại.
', N'12 Trịnh Đình Thảo, phường Hòa Thạnh, quận Tân Phú, TPHCM (Lầu 1, phòng 107, trường Cao Đẳng Công Nghệ Thông Tin)', N'info@khoahoctrainghiem.com                        ', N'033.496.0752   ', N'033.496.0752   ', N'Hỗ trợ bồi dưỡng các môn TOÁN – LÝ – HOÁ – ANH cho học sinh từ lớp 6 đến lớp 12 và luyện thi THPTQG.')
INSERT [dbo].[TRANG_CHU] ([NGAY_AP_DUNG], [MUC_TIEU], [GIOI_THIEU], [DIA_CHI], [EMAIL], [SDT1], [SDT2], [TIEUDE_MONHOC]) VALUES (CAST(N'2019-08-28 22:48:10.833' AS DateTime), N'abc', N'Về chúng tôiGồm tập thể các giáo viên lâu năm kinh nghiệm đang công tác tại các trường trung học trọng địa bàn TPHCM.Chúng tôi có chung một niềm tin rằng việc cải thiện thái độ học tập là con đường ngắn nhất để học sinh có thể tự chinh phục một nền kiến thức rộng lớn.Chúng tôi có chung quan điểm rằng một học sinh giỏi, trước tiên phải đạt nhân cách tốt. Giáo dục đạo đức là một trong những mục tiêu mà Trung tâm KHTN đặt lên hàng đầu và thực hiện thường xuyên trong mỗi buổi học.Chúng tôi có chung một mục tiêu là đào tạo mầm xanh của tương lai, giỏi và tốt hơn chúng tôi ở hiện tại.', N'12 Trịnh Đình Thảo, phường Hòa Thạnh, quận Tân Phú, TPHCM (Lầu 1, phòng 107, trường Cao Đẳng Công Nghệ Thông Tin)', N'info@khoahoctrainghiem.com                        ', N'033.496.0752   ', N'033.496.0752   ', NULL)
INSERT [dbo].[TRANG_CHU] ([NGAY_AP_DUNG], [MUC_TIEU], [GIOI_THIEU], [DIA_CHI], [EMAIL], [SDT1], [SDT2], [TIEUDE_MONHOC]) VALUES (CAST(N'2019-08-28 22:48:17.997' AS DateTime), N'Chúng tôi không cho bạn kiến thức,Chúng tôi cho bạn khả năng gặt lấy chúng.Vì bạn là chính bạn!', N'Về chúng tôiGồm tập thể các giáo viên lâu năm kinh nghiệm đang công tác tại các trường trung học trọng địa bàn TPHCM.Chúng tôi có chung một niềm tin rằng việc cải thiện thái độ học tập là con đường ngắn nhất để học sinh có thể tự chinh phục một nền kiến thức rộng lớn.Chúng tôi có chung quan điểm rằng một học sinh giỏi, trước tiên phải đạt nhân cách tốt. Giáo dục đạo đức là một trong những mục tiêu mà Trung tâm KHTN đặt lên hàng đầu và thực hiện thường xuyên trong mỗi buổi học.Chúng tôi có chung một mục tiêu là đào tạo mầm xanh của tương lai, giỏi và tốt hơn chúng tôi ở hiện tại.', N'12 Trịnh Đình Thảo, phường Hòa Thạnh, quận Tân Phú, TPHCM (Lầu 1, phòng 107, trường Cao Đẳng Công Nghệ Thông Tin)', N'info@khoahoctrainghiem.com                        ', N'033.496.0752   ', N'033.496.0752   ', NULL)
INSERT [dbo].[TRANG_CHU] ([NGAY_AP_DUNG], [MUC_TIEU], [GIOI_THIEU], [DIA_CHI], [EMAIL], [SDT1], [SDT2], [TIEUDE_MONHOC]) VALUES (CAST(N'2019-08-28 22:55:26.323' AS DateTime), N'Chúng tôi không cho bạn kiến thức,Chúng tôi cho bạn khả năng gặt lấy chúng.Vì bạn là chính bạn!', N'Về chúng tôi
Gồm tập thể các giáo viên lâu năm kinh nghiệm đang công tác tại các trường trung học trọng địa bàn TPHCM.Chúng tôi có chung một niềm tin rằng việc cải thiện thái độ học tập là con đường ngắn nhất để học sinh có thể tự chinh phục một nền kiến thức rộng lớn.Chúng tôi có chung quan điểm rằng một học sinh giỏi, trước tiên phải đạt nhân cách tốt. Giáo dục đạo đức là một trong những mục tiêu mà Trung tâm KHTN đặt lên hàng đầu và thực hiện thường xuyên trong mỗi buổi học.Chúng tôi có chung một mục tiêu là đào tạo mầm xanh của tương lai, giỏi và tốt hơn chúng tôi ở hiện tại.', N'12 Trịnh Đình Thảo, phường Hòa Thạnh, quận Tân Phú, TPHCM (Lầu 1, phòng 107, trường Cao Đẳng Công Nghệ Thông Tin)', N'info@khoahoctrainghiem.com                        ', N'033.496.0752   ', N'033.496.0752   ', NULL)
ALTER TABLE [dbo].[BANG_GIA_HOC_PHI]  WITH CHECK ADD  CONSTRAINT [FK_BANG_GIA_HOC_PHI_KHOI_LOP] FOREIGN KEY([MA_KHOI])
REFERENCES [dbo].[KHOI_LOP] ([MA_KHOI])
GO
ALTER TABLE [dbo].[BANG_GIA_HOC_PHI] CHECK CONSTRAINT [FK_BANG_GIA_HOC_PHI_KHOI_LOP]
GO
ALTER TABLE [dbo].[BANG_GIA_HOC_PHI]  WITH CHECK ADD  CONSTRAINT [FK_BANG_GIA_HOC_PHI_LOAI_LOP] FOREIGN KEY([MA_LOAI])
REFERENCES [dbo].[LOAI_LOP] ([MA_LOAI])
GO
ALTER TABLE [dbo].[BANG_GIA_HOC_PHI] CHECK CONSTRAINT [FK_BANG_GIA_HOC_PHI_LOAI_LOP]
GO
ALTER TABLE [dbo].[BANG_GIA_HOC_PHI]  WITH CHECK ADD  CONSTRAINT [FK_BANG_GIA_HOC_PHI_MON_HOC] FOREIGN KEY([MA_MON])
REFERENCES [dbo].[MON_HOC] ([MA_MON])
GO
ALTER TABLE [dbo].[BANG_GIA_HOC_PHI] CHECK CONSTRAINT [FK_BANG_GIA_HOC_PHI_MON_HOC]
GO
ALTER TABLE [dbo].[BUOI_HOC]  WITH CHECK ADD  CONSTRAINT [FK_BUOI_HOC_BANG_LUONG] FOREIGN KEY([MA_LUONG])
REFERENCES [dbo].[BANG_LUONG] ([MA_LOAI_LUONG])
GO
ALTER TABLE [dbo].[BUOI_HOC] CHECK CONSTRAINT [FK_BUOI_HOC_BANG_LUONG]
GO
ALTER TABLE [dbo].[BUOI_HOC]  WITH CHECK ADD  CONSTRAINT [FK_BUOI_HOC_GIAO_VIEN] FOREIGN KEY([MA_GV])
REFERENCES [dbo].[GIAO_VIEN] ([MA_GV])
GO
ALTER TABLE [dbo].[BUOI_HOC] CHECK CONSTRAINT [FK_BUOI_HOC_GIAO_VIEN]
GO
ALTER TABLE [dbo].[BUOI_HOC]  WITH CHECK ADD  CONSTRAINT [FK_BUOI_HOC_LOP_HOC] FOREIGN KEY([MA_LOP])
REFERENCES [dbo].[LOP_HOC] ([MA_LOP])
GO
ALTER TABLE [dbo].[BUOI_HOC] CHECK CONSTRAINT [FK_BUOI_HOC_LOP_HOC]
GO
ALTER TABLE [dbo].[CONG_NO]  WITH CHECK ADD  CONSTRAINT [FK_CONG_NO_HOC_SINH] FOREIGN KEY([MA_HS])
REFERENCES [dbo].[HOC_SINH] ([MA_HS])
GO
ALTER TABLE [dbo].[CONG_NO] CHECK CONSTRAINT [FK_CONG_NO_HOC_SINH]
GO
ALTER TABLE [dbo].[CONG_NO]  WITH CHECK ADD  CONSTRAINT [FK_CONG_NO_KHUYEN_MAI] FOREIGN KEY([MA_KM])
REFERENCES [dbo].[KHUYEN_MAI] ([MA_KM])
GO
ALTER TABLE [dbo].[CONG_NO] CHECK CONSTRAINT [FK_CONG_NO_KHUYEN_MAI]
GO
ALTER TABLE [dbo].[CT_BUOIHOC]  WITH CHECK ADD  CONSTRAINT [FK_CT_BUOIHOC_BUOI_HOC] FOREIGN KEY([MA_BUOI])
REFERENCES [dbo].[BUOI_HOC] ([MA_BUOI])
GO
ALTER TABLE [dbo].[CT_BUOIHOC] CHECK CONSTRAINT [FK_CT_BUOIHOC_BUOI_HOC]
GO
ALTER TABLE [dbo].[CT_BUOIHOC]  WITH CHECK ADD  CONSTRAINT [FK_CT_BUOIHOC_HOC_SINH] FOREIGN KEY([MA_HS])
REFERENCES [dbo].[HOC_SINH] ([MA_HS])
GO
ALTER TABLE [dbo].[CT_BUOIHOC] CHECK CONSTRAINT [FK_CT_BUOIHOC_HOC_SINH]
GO
ALTER TABLE [dbo].[CT_CONG_NO]  WITH CHECK ADD  CONSTRAINT [FK_CT_CONG_NO_CONG_NO] FOREIGN KEY([MA_CONG_NO])
REFERENCES [dbo].[CONG_NO] ([MA_CONG_NO])
GO
ALTER TABLE [dbo].[CT_CONG_NO] CHECK CONSTRAINT [FK_CT_CONG_NO_CONG_NO]
GO
ALTER TABLE [dbo].[CT_CONG_NO]  WITH CHECK ADD  CONSTRAINT [FK_CT_CONG_NO_LOP_HOC] FOREIGN KEY([MA_LOP])
REFERENCES [dbo].[LOP_HOC] ([MA_LOP])
GO
ALTER TABLE [dbo].[CT_CONG_NO] CHECK CONSTRAINT [FK_CT_CONG_NO_LOP_HOC]
GO
ALTER TABLE [dbo].[CT_HOADON]  WITH CHECK ADD  CONSTRAINT [FK_CT_HOADON_BUOI_HOC1] FOREIGN KEY([MA_BUOI])
REFERENCES [dbo].[BUOI_HOC] ([MA_BUOI])
GO
ALTER TABLE [dbo].[CT_HOADON] CHECK CONSTRAINT [FK_CT_HOADON_BUOI_HOC1]
GO
ALTER TABLE [dbo].[CT_HOADON]  WITH CHECK ADD  CONSTRAINT [FK_CT_HOADON_HOA_DON] FOREIGN KEY([MA_HD])
REFERENCES [dbo].[HOA_DON] ([MA_HD])
GO
ALTER TABLE [dbo].[CT_HOADON] CHECK CONSTRAINT [FK_CT_HOADON_HOA_DON]
GO
ALTER TABLE [dbo].[CT_HOADON_NGOAIGIO]  WITH CHECK ADD  CONSTRAINT [FK_CT_HOADON_NGOAIGIO_HOA_DON] FOREIGN KEY([MA_HD])
REFERENCES [dbo].[HOA_DON] ([MA_HD])
GO
ALTER TABLE [dbo].[CT_HOADON_NGOAIGIO] CHECK CONSTRAINT [FK_CT_HOADON_NGOAIGIO_HOA_DON]
GO
ALTER TABLE [dbo].[CT_HOADON_NGOAIGIO]  WITH CHECK ADD  CONSTRAINT [FK_CT_HOADON_NGOAIGIO_NGOAI_GIO] FOREIGN KEY([MA_NGOAI_GIO])
REFERENCES [dbo].[NGOAI_GIO] ([MA_NGOAI_GIO])
GO
ALTER TABLE [dbo].[CT_HOADON_NGOAIGIO] CHECK CONSTRAINT [FK_CT_HOADON_NGOAIGIO_NGOAI_GIO]
GO
ALTER TABLE [dbo].[CT_LOP_HOC]  WITH CHECK ADD  CONSTRAINT [FK_CT_LOP_HOC_HOC_SINH] FOREIGN KEY([MA_HS])
REFERENCES [dbo].[HOC_SINH] ([MA_HS])
GO
ALTER TABLE [dbo].[CT_LOP_HOC] CHECK CONSTRAINT [FK_CT_LOP_HOC_HOC_SINH]
GO
ALTER TABLE [dbo].[CT_LOP_HOC]  WITH CHECK ADD  CONSTRAINT [FK_CT_LOP_HOC_LOP_HOC] FOREIGN KEY([MA_LOP])
REFERENCES [dbo].[LOP_HOC] ([MA_LOP])
GO
ALTER TABLE [dbo].[CT_LOP_HOC] CHECK CONSTRAINT [FK_CT_LOP_HOC_LOP_HOC]
GO
ALTER TABLE [dbo].[HOA_DON]  WITH CHECK ADD  CONSTRAINT [FK_HOA_DON_GIAO_VIEN] FOREIGN KEY([MA_GV])
REFERENCES [dbo].[GIAO_VIEN] ([MA_GV])
GO
ALTER TABLE [dbo].[HOA_DON] CHECK CONSTRAINT [FK_HOA_DON_GIAO_VIEN]
GO
ALTER TABLE [dbo].[LOP_HOC]  WITH CHECK ADD  CONSTRAINT [FK_LOP_HOC_BANG_GIA_HOC_PHI] FOREIGN KEY([NGAY_AP_DUNG])
REFERENCES [dbo].[BANG_GIA_HOC_PHI] ([NGAY_AP_DUNG])
GO
ALTER TABLE [dbo].[LOP_HOC] CHECK CONSTRAINT [FK_LOP_HOC_BANG_GIA_HOC_PHI]
GO
ALTER TABLE [dbo].[LOP_HOC]  WITH CHECK ADD  CONSTRAINT [FK_LOP_HOC_GIAO_VIEN] FOREIGN KEY([MA_GV])
REFERENCES [dbo].[GIAO_VIEN] ([MA_GV])
GO
ALTER TABLE [dbo].[LOP_HOC] CHECK CONSTRAINT [FK_LOP_HOC_GIAO_VIEN]
GO
ALTER TABLE [dbo].[NGOAI_GIO]  WITH CHECK ADD  CONSTRAINT [FK_NGOAI_GIO_BANG_LUONG1] FOREIGN KEY([MA_LUONG])
REFERENCES [dbo].[BANG_LUONG] ([MA_LOAI_LUONG])
GO
ALTER TABLE [dbo].[NGOAI_GIO] CHECK CONSTRAINT [FK_NGOAI_GIO_BANG_LUONG1]
GO
ALTER TABLE [dbo].[NGOAI_GIO]  WITH CHECK ADD  CONSTRAINT [FK_NGOAI_GIO_GIAO_VIEN] FOREIGN KEY([MA_GV])
REFERENCES [dbo].[GIAO_VIEN] ([MA_GV])
GO
ALTER TABLE [dbo].[NGOAI_GIO] CHECK CONSTRAINT [FK_NGOAI_GIO_GIAO_VIEN]
GO
ALTER TABLE [dbo].[THOI_KHOA_BIEU]  WITH CHECK ADD  CONSTRAINT [FK_THOI_KHOA_BIEU_LOP_HOC] FOREIGN KEY([MA_LOP])
REFERENCES [dbo].[LOP_HOC] ([MA_LOP])
GO
ALTER TABLE [dbo].[THOI_KHOA_BIEU] CHECK CONSTRAINT [FK_THOI_KHOA_BIEU_LOP_HOC]
GO
/****** Object:  Trigger [dbo].[sobuoi]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[sobuoi]
ON [dbo].[BUOI_HOC]
AFTER INSERT
AS
	BEGIN
		update BUOI_HOC
		SET STT_BUOI = (SELECT COUNT(*) FROM BUOI_HOC p,inserted i WHERE p.MA_LOP = i.MA_LOP)
		FROM BUOI_HOC
		WHERE MA_BUOI = (SELECT MA_BUOI
						FROM inserted)
	END


GO
/****** Object:  Trigger [dbo].[UpdateSumStudent]    Script Date: 9/8/2019 11:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[UpdateSumStudent]
on [dbo].[CT_LOP_HOC]
for insert, update, delete
as
begin
	update LOP_HOC set SI_SO = (select COUNT(*) from CT_LOP_HOC ctl, inserted i where ctl.MA_LOP = i.MA_LOP) 
	from LOP_HOC 
	join inserted on LOP_HOC.MA_LOP = inserted.MA_LOP
end

GO
