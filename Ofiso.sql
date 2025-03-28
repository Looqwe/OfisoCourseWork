USE [master]
GO
/****** Object:  Database [Ofico]    Script Date: 23.03.2025 21:59:57 ******/
CREATE DATABASE [Ofico]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Ofico', FILENAME = N'E:\SQL\MSSQL16.MSSQLSERVER\MSSQL\DATA\Ofico.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Ofico_log', FILENAME = N'E:\SQL\MSSQL16.MSSQLSERVER\MSSQL\DATA\Ofico_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Ofico] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Ofico].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Ofico] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Ofico] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Ofico] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Ofico] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Ofico] SET ARITHABORT OFF 
GO
ALTER DATABASE [Ofico] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Ofico] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Ofico] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Ofico] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Ofico] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Ofico] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Ofico] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Ofico] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Ofico] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Ofico] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Ofico] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Ofico] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Ofico] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Ofico] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Ofico] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Ofico] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Ofico] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Ofico] SET RECOVERY FULL 
GO
ALTER DATABASE [Ofico] SET  MULTI_USER 
GO
ALTER DATABASE [Ofico] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Ofico] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Ofico] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Ofico] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Ofico] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Ofico] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Ofico', N'ON'
GO
ALTER DATABASE [Ofico] SET QUERY_STORE = ON
GO
ALTER DATABASE [Ofico] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Ofico]
GO
/****** Object:  Table [dbo].[Offices]    Script Date: 23.03.2025 21:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OwnerID] [int] NULL,
	[Title] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Floor] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[PricePerMont] [decimal](18, 0) NULL,
	[CreatedDate] [date] NULL,
	[Photo] [nvarchar](max) NULL,
	[IDOfficesType] [int] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_Offices] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OfficesType]    Script Date: 23.03.2025 21:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OfficesType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_OfficesType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rentals]    Script Date: 23.03.2025 21:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rentals](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OfficeID] [int] NULL,
	[TenantID] [int] NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[TotalPrice] [decimal](18, 0) NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Rentals] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 23.03.2025 21:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Fname] [nvarchar](50) NULL,
	[NumberPhone] [nvarchar](11) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[UserType] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 23.03.2025 21:59:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[ID] [int] NOT NULL,
	[Type] [nvarchar](10) NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Offices]  WITH CHECK ADD  CONSTRAINT [FK_Offices_OfficesType] FOREIGN KEY([IDOfficesType])
REFERENCES [dbo].[OfficesType] ([ID])
GO
ALTER TABLE [dbo].[Offices] CHECK CONSTRAINT [FK_Offices_OfficesType]
GO
ALTER TABLE [dbo].[Offices]  WITH CHECK ADD  CONSTRAINT [FK_Offices_Users2] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Offices] CHECK CONSTRAINT [FK_Offices_Users2]
GO
ALTER TABLE [dbo].[Rentals]  WITH CHECK ADD  CONSTRAINT [FK_Rentals_Offices] FOREIGN KEY([OfficeID])
REFERENCES [dbo].[Offices] ([ID])
GO
ALTER TABLE [dbo].[Rentals] CHECK CONSTRAINT [FK_Rentals_Offices]
GO
ALTER TABLE [dbo].[Rentals]  WITH CHECK ADD  CONSTRAINT [FK_Rentals_Users] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Rentals] CHECK CONSTRAINT [FK_Rentals_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserType] FOREIGN KEY([UserType])
REFERENCES [dbo].[UserType] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserType]
GO
USE [master]
GO
ALTER DATABASE [Ofico] SET  READ_WRITE 
GO
