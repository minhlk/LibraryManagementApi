USE [master]
GO
/****** Object:  Database [LibraryManagement]    Script Date: 5/11/2018 8:19:21 PM ******/
CREATE DATABASE [LibraryManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibraryManagerment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MKSERVER\MSSQL\DATA\LibraryManagerment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibraryManagerment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MKSERVER\MSSQL\DATA\LibraryManagerment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [LibraryManagement] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LibraryManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LibraryManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LibraryManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LibraryManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LibraryManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LibraryManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [LibraryManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LibraryManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LibraryManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LibraryManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LibraryManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LibraryManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LibraryManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LibraryManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LibraryManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LibraryManagement] SET  ENABLE_BROKER 
GO
ALTER DATABASE [LibraryManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LibraryManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LibraryManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LibraryManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LibraryManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LibraryManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LibraryManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LibraryManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [LibraryManagement] SET  MULTI_USER 
GO
ALTER DATABASE [LibraryManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LibraryManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LibraryManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LibraryManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LibraryManagement] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'LibraryManagement', N'ON'
GO
ALTER DATABASE [LibraryManagement] SET QUERY_STORE = OFF
GO
USE [LibraryManagement]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 5/11/2018 8:19:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[YearOfBirth] [char](4) NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 5/11/2018 8:19:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](35) NOT NULL,
	[IdAuthor] [bigint] NOT NULL,
	[Amount] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookGenre]    Script Date: 5/11/2018 8:19:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookGenre](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdGenre] [bigint] NOT NULL,
	[IdBook] [bigint] NOT NULL,
 CONSTRAINT [PK_BookGenre] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 5/11/2018 8:19:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 5/11/2018 8:19:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/11/2018 8:19:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](35) NOT NULL,
	[IdRole] [tinyint] NULL,
	[YearOfBirth] [char](4) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserBook]    Script Date: 5/11/2018 8:19:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserBook](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdUser] [bigint] NOT NULL,
	[IdBook] [bigint] NOT NULL,
	[StartDate] [nvarchar](50) NOT NULL,
	[EndDate] [nvarchar](50) NOT NULL,
	[NumberOfDays] [tinyint] NOT NULL,
 CONSTRAINT [PK_UserBook_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Author] ON 

INSERT [dbo].[Author] ([Id], [Name], [YearOfBirth]) VALUES (1, N'Tô Hoài', N'1992')
INSERT [dbo].[Author] ([Id], [Name], [YearOfBirth]) VALUES (2, N'Tố Hữu', N'1993')
INSERT [dbo].[Author] ([Id], [Name], [YearOfBirth]) VALUES (3, N'Nam Cao', N'1922')
INSERT [dbo].[Author] ([Id], [Name], [YearOfBirth]) VALUES (4, N'Bà Huyện Thanh Quan', N'1888')
INSERT [dbo].[Author] ([Id], [Name], [YearOfBirth]) VALUES (5, N'Ngô Quyền', N'1912')
INSERT [dbo].[Author] ([Id], [Name], [YearOfBirth]) VALUES (6, N'Lý Ngô', N'1777')
SET IDENTITY_INSERT [dbo].[Author] OFF
SET IDENTITY_INSERT [dbo].[Book] ON 

INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (1, N'Lượm 12', 3, 1001, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (2, N'Nhặt', 2, 5, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (5, N'lkj', 2, 12, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (9, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (11, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (13, N'sach gi do', 2, 101, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (14, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (15, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (16, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (17, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (18, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (19, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (20, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (21, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (22, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (23, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (24, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (25, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (26, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (27, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (28, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (29, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (30, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (31, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (32, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (33, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (34, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (35, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (36, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (37, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (38, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (39, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (40, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (41, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (42, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (43, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (44, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (45, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (46, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (47, N'gg', 2, 10, CAST(N'2018-11-01T12:04:03.767' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (48, N'kjahsd', 2, 14, CAST(N'2018-11-01T12:04:26.157' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (49, N'sdjh', 2, 23, CAST(N'2018-11-01T12:08:01.870' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (51, N'12354sdf', 6, 123, CAST(N'2018-11-05T19:54:37.470' AS DateTime), N'http://fakeimg.pl/365x362/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (52, N'WordPressShopping', 6, 122, CAST(N'2018-11-05T20:06:14.253' AS DateTime), N'http://fakeimg.pl/365x365/')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (53, N'Lượm ', 4, 12, CAST(N'2018-11-05T20:09:54.950' AS DateTime), N'33123')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (54, N'Lượm ', 4, 12, CAST(N'2018-11-05T20:11:04.520' AS DateTime), N'33123')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (55, N'asd', 4, 12, CAST(N'2018-11-05T20:11:41.273' AS DateTime), N'asdasd')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (56, N'asd', 4, 12, CAST(N'2018-11-05T20:12:15.047' AS DateTime), N'asdasd')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (57, N'asd', 4, 12, CAST(N'2018-11-05T20:13:19.280' AS DateTime), N'asdasd')
INSERT [dbo].[Book] ([Id], [Name], [IdAuthor], [Amount], [CreateTime], [Image]) VALUES (58, N'English', 4, 12, CAST(N'2018-11-05T20:14:55.283' AS DateTime), N'asdasd')
SET IDENTITY_INSERT [dbo].[Book] OFF
SET IDENTITY_INSERT [dbo].[BookGenre] ON 

INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (3, 2, 2)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (52, 5, 13)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (53, 4, 13)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (72, 5, 51)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (73, 4, 51)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (74, 2, 51)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (75, 6, 51)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (76, 2, 52)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (77, 4, 52)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (78, 1, 52)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (79, 2, 53)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (80, 2, 54)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (81, 5, 55)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (82, 5, 56)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (83, 5, 57)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (84, 4, 58)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (85, 5, 58)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (86, 2, 1)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (87, 7, 1)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (88, 4, 1)
INSERT [dbo].[BookGenre] ([Id], [IdGenre], [IdBook]) VALUES (89, 1, 1)
SET IDENTITY_INSERT [dbo].[BookGenre] OFF
SET IDENTITY_INSERT [dbo].[Genre] ON 

INSERT [dbo].[Genre] ([Id], [Name]) VALUES (1, N'Kinh Di')
INSERT [dbo].[Genre] ([Id], [Name]) VALUES (2, N'Tình Cảm')
INSERT [dbo].[Genre] ([Id], [Name]) VALUES (4, N'Trinh Thám')
INSERT [dbo].[Genre] ([Id], [Name]) VALUES (5, N'Hoạt Hình')
INSERT [dbo].[Genre] ([Id], [Name]) VALUES (6, N'Anime')
INSERT [dbo].[Genre] ([Id], [Name]) VALUES (7, N'Winx')
SET IDENTITY_INSERT [dbo].[Genre] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (2, N'Librarian')
INSERT [dbo].[Role] ([Id], [RoleName]) VALUES (3, N'Customer')
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [UserName], [Name], [IdRole], [YearOfBirth], [Phone], [Password]) VALUES (12, N'minh                ', N'minh minh', 1, N'1995', N'01234123       ', N'cWw/1Xmouphk2YpjKCvVejJpLmsQZ3hhkrGfJ/qgkvI=                                                        ')
INSERT [dbo].[User] ([Id], [UserName], [Name], [IdRole], [YearOfBirth], [Phone], [Password]) VALUES (13, N'minha               ', N'minh', 1, N'1992', N'0123123        ', N'acVzazHN7hAJ4Zf08E9LstckblrUUG3Qd4mamobrlQM=                                                        ')
INSERT [dbo].[User] ([Id], [UserName], [Name], [IdRole], [YearOfBirth], [Phone], [Password]) VALUES (23, N'Minhlk', N'Minh', 1, N'1996', N'0123123123', N'jH2/eY4Ee3FMDQa4s5iXsBCZCGDcbwxuvOQdz25QJ6g=')
INSERT [dbo].[User] ([Id], [UserName], [Name], [IdRole], [YearOfBirth], [Phone], [Password]) VALUES (24, N'Minhlkz', N'Minh', 3, N'1996', N'0123123123', N'jH2/eY4Ee3FMDQa4s5iXsBCZCGDcbwxuvOQdz25QJ6g=')
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_Amount]  DEFAULT ((0)) FOR [Amount]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_Image]  DEFAULT ('http://fakeimg.pl/365x365/') FOR [Image]
GO
ALTER TABLE [dbo].[UserBook] ADD  CONSTRAINT [DF_UserBook_NumberOfDays]  DEFAULT ((0)) FOR [NumberOfDays]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Author] FOREIGN KEY([IdAuthor])
REFERENCES [dbo].[Author] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Author]
GO
ALTER TABLE [dbo].[BookGenre]  WITH CHECK ADD  CONSTRAINT [FK_BookGenre_Book] FOREIGN KEY([IdBook])
REFERENCES [dbo].[Book] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookGenre] CHECK CONSTRAINT [FK_BookGenre_Book]
GO
ALTER TABLE [dbo].[BookGenre]  WITH CHECK ADD  CONSTRAINT [FK_BookGenre_Genre] FOREIGN KEY([IdGenre])
REFERENCES [dbo].[Genre] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookGenre] CHECK CONSTRAINT [FK_BookGenre_Genre]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [dbo].[UserBook]  WITH CHECK ADD  CONSTRAINT [FK_UserBook_Book] FOREIGN KEY([IdBook])
REFERENCES [dbo].[Book] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserBook] CHECK CONSTRAINT [FK_UserBook_Book]
GO
ALTER TABLE [dbo].[UserBook]  WITH CHECK ADD  CONSTRAINT [FK_UserBook_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserBook] CHECK CONSTRAINT [FK_UserBook_User]
GO
USE [master]
GO
ALTER DATABASE [LibraryManagement] SET  READ_WRITE 
GO
