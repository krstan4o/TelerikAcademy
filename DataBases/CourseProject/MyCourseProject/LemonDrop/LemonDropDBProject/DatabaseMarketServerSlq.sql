USE [master]
GO
/****** Object:  Database [SupermarketDatabase]    Script Date: 17/07/2013 09:03:36 ******/
CREATE DATABASE [SupermarketDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SupermarketDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\SupermarketDatabase.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SupermarketDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\SupermarketDatabase_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SupermarketDatabase] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SupermarketDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SupermarketDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SupermarketDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SupermarketDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SupermarketDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SupermarketDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SupermarketDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET RECOVERY FULL 
GO
ALTER DATABASE [SupermarketDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [SupermarketDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SupermarketDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SupermarketDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SupermarketDatabase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SupermarketDatabase', N'ON'
GO
USE [SupermarketDatabase]
GO
/****** Object:  Table [dbo].[Markets]    Script Date: 17/07/2013 09:03:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Markets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Markets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Measures]    Script Date: 17/07/2013 09:03:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Measures](
	[Id] [int] NOT NULL,
	[Measure Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Measures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Products]    Script Date: 17/07/2013 09:03:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[Product Name] [nvarchar](50) NULL,
	[MeasureId] [int] NOT NULL,
	[Base Prise] [float] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[sales]    Script Date: 17/07/2013 09:03:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sales](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Qantity] [int] NOT NULL,
	[UnitPrice] [float] NOT NULL,
	[Sum] [float] NOT NULL,
	[MarketId] [int] NOT NULL,
	[Data] [datetime] NOT NULL,
 CONSTRAINT [PK_sales] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Taxes]    Script Date: 17/07/2013 09:03:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Taxes](
	[Id] [int] NOT NULL,
	[Product Name] [nchar](50) NOT NULL,
	[Tax] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Taxes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Vendors]    Script Date: 17/07/2013 09:03:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vendors](
	[Id] [int] NOT NULL,
	[Vendor Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Vendors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Measures] FOREIGN KEY([MeasureId])
REFERENCES [dbo].[Measures] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Measures]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Vendors] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendors] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Vendors]
GO
ALTER TABLE [dbo].[sales]  WITH CHECK ADD  CONSTRAINT [FK_sales_Markets] FOREIGN KEY([MarketId])
REFERENCES [dbo].[Markets] ([Id])
GO
ALTER TABLE [dbo].[sales] CHECK CONSTRAINT [FK_sales_Markets]
GO
ALTER TABLE [dbo].[sales]  WITH CHECK ADD  CONSTRAINT [FK_sales_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[sales] CHECK CONSTRAINT [FK_sales_Products]
GO
USE [master]
GO
ALTER DATABASE [SupermarketDatabase] SET  READ_WRITE 
GO
