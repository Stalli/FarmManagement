USE [master]
GO
/****** Object:  Database [FarmManagement]    Script Date: 11/5/2016 7:57:20 AM ******/
CREATE DATABASE [FarmManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FarmManagement', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\FarmManagement.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FarmManagement_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\FarmManagement_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FarmManagement] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FarmManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FarmManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FarmManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FarmManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FarmManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FarmManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [FarmManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FarmManagement] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [FarmManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FarmManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FarmManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FarmManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FarmManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FarmManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FarmManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FarmManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FarmManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FarmManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FarmManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FarmManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FarmManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FarmManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FarmManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FarmManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FarmManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FarmManagement] SET  MULTI_USER 
GO
ALTER DATABASE [FarmManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FarmManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FarmManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FarmManagement] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [FarmManagement]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 11/5/2016 7:57:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Animal]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Animal](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FarmId] [int] NOT NULL,
	[AnimalName] [varchar](200) NOT NULL,
	[Category] [varchar](50) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[PurchaseDate] [datetime] NOT NULL,
	[Age] [int] NOT NULL,
	[Color] [varchar](200) NOT NULL,
	[Description] [varchar](500) NULL,
	[Photo] [varchar](500) NOT NULL,
	[VendorId] [int] NOT NULL,
	[FamilyName] [varchar](200) NOT NULL,
	[Sex] [varchar](50) NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Animal] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AttendanceManagement]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendanceManagement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
 CONSTRAINT [PK_AttendanceMgmt] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AttendanceManagementDetail]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendanceManagementDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AttendanceManagementId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[IsPresentMorning] [bit] NULL,
	[IsAbsentMorning] [bit] NULL,
	[IsLeaveMorning] [bit] NULL,
	[IsPresentNight] [bit] NULL,
	[IsAbsentNight] [bit] NULL,
	[IsLeaveNight] [bit] NULL,
	[UserId] [int] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
 CONSTRAINT [PK_AttendanceMgmtDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CropManagement]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CropManagement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FarmId] [int] NOT NULL,
	[AreaId] [int] NOT NULL,
	[CropName] [varchar](200) NOT NULL,
	[CropDuration] [varchar](200) NOT NULL,
	[CropStartDate] [datetime] NOT NULL,
	[CropEndDate] [datetime] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_CropManagement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Farm]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Farm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FarmName] [varchar](200) NOT NULL,
	[Description] [varchar](500) NULL,
	[Address] [varchar](500) NOT NULL,
	[TotalArea] [varchar](200) NOT NULL,
	[ActualPrice] [decimal](18, 2) NOT NULL,
	[TaxAmount] [decimal](18, 2) NOT NULL,
	[TotalValue] [decimal](18, 2) NOT NULL,
	[PaperValue] [decimal](18, 2) NOT NULL,
	[Rent] [decimal](18, 2) NOT NULL,
	[Owner] [varchar](200) NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[PurchasedDate] [datetime] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Farm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FarmArea]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FarmArea](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FarmId] [int] NOT NULL,
	[AreaName] [varchar](200) NOT NULL,
	[AreaInAcars] [varchar](200) NOT NULL,
	[StartingPoint] [varchar](200) NOT NULL,
	[EndingPoint] [varchar](200) NOT NULL,
	[Date] [datetime] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_FarmArea] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Fertilizer]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fertilizer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FarmId] [int] NOT NULL,
	[Brand] [varchar](200) NOT NULL,
	[FertilizerName] [varchar](200) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Quality] [varchar](50) NOT NULL,
	[Quantity] [varchar](50) NOT NULL,
	[QuantityInNumber] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[PricePerItem] [decimal](18, 2) NOT NULL,
	[DatePurchase] [datetime] NOT NULL,
	[PaymentBill] [varchar](500) NOT NULL,
	[OtherDescription] [varchar](500) NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Fertilizer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FertilizerExpense]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FertilizerExpense](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FarmId] [int] NOT NULL,
	[AreaId] [int] NOT NULL,
	[CropId] [int] NOT NULL,
	[FertilizerId] [int] NOT NULL,
	[QuantityInNumber] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Description] [varchar](500) NULL,
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_FertilizerExpense] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Form]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Form](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Type] [varchar](200) NOT NULL,
	[ControllerName] [varchar](200) NOT NULL,
	[ActionName] [varchar](200) NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Form] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FormSetting]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormSetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FormId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[IsAdd] [bit] NOT NULL,
	[IsUpdate] [bit] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_FormSetting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Fuel]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fuel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FarmId] [int] NOT NULL,
	[FuelName] [varchar](50) NOT NULL,
	[DatePurchase] [datetime] NOT NULL,
	[QuantityInLiter] [decimal](18, 2) NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[PricePerLiter] [decimal](18, 2) NOT NULL,
	[PaymentBill] [varchar](500) NOT NULL,
	[VendorId] [int] NOT NULL,
	[OtherDescription] [varchar](500) NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Fuel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FuelExpense]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FuelExpense](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FarmId] [int] NOT NULL,
	[AreaId] [int] NOT NULL,
	[CropId] [int] NOT NULL,
	[FuelId] [int] NOT NULL,
	[PersonName] [varchar](200) NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Description] [varchar](500) NULL,
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_FuelExpense] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoanManagement]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoanManagement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[LoanAmount] [decimal](18, 2) NOT NULL,
	[NoOfInstallment] [int] NOT NULL,
	[PerMonthOrYear] [bit] NOT NULL,
	[LoanStartDate] [datetime] NOT NULL,
	[LoanEndDate] [datetime] NOT NULL,
	[Description] [varchar](500) NULL,
	[IntersetRate] [decimal](18, 2) NOT NULL,
	[Status] [varchar](200) NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_LoanManagement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoanReceivableFromBank]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoanReceivableFromBank](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoanId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[NoOfInstallment] [decimal](18, 2) NOT NULL,
	[InterestRate] [decimal](18, 2) NOT NULL,
	[TotalAmountTobePaid] [decimal](18, 2) NOT NULL,
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[LoanReceiveDate] [datetime] NOT NULL,
	[LoanEndDate] [datetime] NOT NULL,
	[Description] [varchar](500) NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_BankLoan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoanReceivableFromEmployee]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanReceivableFromEmployee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoanId] [int] NOT NULL,
	[AmountReceive] [decimal](18, 2) NOT NULL,
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_EmployeeLoan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Machinery]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Machinery](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FarmId] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Color] [varchar](200) NOT NULL,
	[Type] [varchar](200) NOT NULL,
	[RegistrationNo] [varchar](200) NOT NULL,
	[ModelNo] [varchar](200) NOT NULL,
	[CompanyName] [varchar](200) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[PurchaseDate] [datetime] NOT NULL,
	[OtherDescription] [varchar](500) NULL,
	[Condition] [varchar](50) NOT NULL,
	[VendorId] [int] NOT NULL,
	[Picture] [varchar](500) NOT NULL,
	[LicenseFile] [varchar](500) NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Machinery] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OtherExpense]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OtherExpense](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FarmId] [int] NOT NULL,
	[OtherItemId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Description] [varchar](500) NULL,
	[Date] [datetime] NOT NULL,
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_OtherExpense] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OtherItem]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OtherItem](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[FarmId] [int] NOT NULL,
	[TitleExpense] [varchar](200) NOT NULL,
	[Description] [varchar](200) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[DatePurchase] [datetime] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_OtherItem] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PaidLoan]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaidLoan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoanId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_PaidLoan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PermanentEmployeeSalary]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermanentEmployeeSalary](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[IsAllowFund] [bit] NOT NULL,
	[BasicSalary] [decimal](18, 2) NOT NULL,
	[Bonus] [decimal](18, 2) NOT NULL,
	[MedicalAllowance] [decimal](18, 2) NOT NULL,
	[TA_DA] [decimal](18, 2) NOT NULL,
	[HouseRent] [decimal](18, 2) NOT NULL,
	[UtilityAllowance] [decimal](18, 2) NOT NULL,
	[ProvidentFund] [decimal](18, 2) NOT NULL,
	[Penality] [decimal](18, 2) NULL,
	[Date] [datetime] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_PermanentEmployeeSalary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PersonalAccount]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonalAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[OpeningBalance] [decimal](18, 2) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ClosingDate] [datetime] NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_PersonalAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReturnLoan]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReturnLoan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoanId] [int] NOT NULL,
	[InstallmentAmount] [decimal](18, 2) NOT NULL,
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[PaidDate] [datetime] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_ReturnLoan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](200) NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Seed]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Seed](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FarmId] [int] NOT NULL,
	[Quality] [varchar](50) NOT NULL,
	[Quantity] [varchar](50) NOT NULL,
	[QuantityInNumber] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[TotalPricePer] [decimal](18, 2) NOT NULL,
	[PricePerItem] [decimal](18, 2) NOT NULL,
	[DatePurchase] [datetime] NOT NULL,
	[CropId] [int] NOT NULL,
	[PaymentBill] [varchar](500) NOT NULL,
	[OtherDescription] [varchar](500) NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Seed] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SeedExpense]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SeedExpense](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FarmId] [int] NOT NULL,
	[AreaId] [int] NOT NULL,
	[SeedId] [int] NOT NULL,
	[QuantityInNumber] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Description] [varchar](500) NULL,
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_SeedExpense] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TaskAssignment]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaskAssignment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskName] [varchar](max) NOT NULL,
	[StaffNameId] [int] NOT NULL,
	[Description] [varchar](max) NULL,
	[DeadlineStartDate] [datetime] NOT NULL,
	[DeadlineEndDate] [datetime] NOT NULL,
	[Status] [varchar](50) NULL,
	[Remarks] [varchar](50) NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TaskAssignment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TemporaryEmployeeSalary]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TemporaryEmployeeSalary](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[MorningWages] [decimal](18, 2) NULL,
	[NightWages] [decimal](18, 2) NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TemporaryEmployeeSalary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TransectionAccount]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransectionAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PayByUserId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[Date] [datetime] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
 CONSTRAINT [PK_TransectionAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TransectionPersonalAccount]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransectionPersonalAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[Credit] [decimal](18, 2) NULL,
	[Debit] [decimal](18, 2) NULL,
	[Date] [datetime] NOT NULL,
	[IsAddedByAdmin] [bit] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_TransectionPersonalAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](200) NULL,
	[Password] [varchar](max) NULL,
	[RoleId] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[FatherName] [varchar](200) NOT NULL,
	[CNIC] [varchar](200) NOT NULL,
	[Gender] [varchar](200) NOT NULL,
	[MaritalStatus] [varchar](50) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[PlaceOfBirth] [varchar](200) NULL,
	[ContactNo] [varchar](200) NOT NULL,
	[Landline] [varchar](200) NULL,
	[Nationality] [varchar](200) NULL,
	[Email] [varchar](200) NULL,
	[PermanentAddress] [varchar](max) NOT NULL,
	[CurrentAddress] [varchar](max) NULL,
	[DateOfJoining] [datetime] NOT NULL,
	[ResignationDate] [datetime] NULL,
	[Status] [varchar](50) NULL,
	[Picture] [varchar](max) NOT NULL,
	[Position] [varchar](200) NULL,
	[IsTemporaryEmployee] [bit] NOT NULL,
	[ScannedDocument] [varchar](max) NULL,
	[CNICFrontSide] [varchar](max) NULL,
	[CNICBackSide] [varchar](max) NULL,
	[AgreementDocument] [varchar](max) NULL,
	[IsUser] [bit] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[LastLogin] [datetime] NULL,
	[FarmId] [int] NOT NULL,
	[Department] [varchar](200) NOT NULL,
	[Salary] [varchar](200) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Vehicle]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vehicle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FarmId] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Color] [varchar](200) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Power] [varchar](200) NOT NULL,
	[RegistrationNo] [varchar](200) NOT NULL,
	[Picture] [varchar](500) NOT NULL,
	[LicenseFile] [varchar](500) NOT NULL,
	[CompanyName] [varchar](200) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[ModelNo] [varchar](200) NOT NULL,
	[PurchaseDate] [datetime] NOT NULL,
	[VendorId] [int] NOT NULL,
	[OtherDescription] [varchar](500) NULL,
	[Condition] [varchar](50) NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VendorManagement]    Script Date: 11/5/2016 7:57:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VendorManagement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[CompanyName] [varchar](200) NOT NULL,
	[PhoneNo] [varchar](200) NOT NULL,
	[PersonalPhoneNo] [varchar](200) NOT NULL,
	[AddressInfo] [varchar](200) NOT NULL,
	[OpeningBalance] [decimal](18, 2) NOT NULL,
	[OtherDescription] [varchar](500) NULL,
	[Type] [varchar](200) NOT NULL,
	[Date] [datetime] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_VendorManagement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([Id], [Name], [InsertDate], [UpdateDate]) VALUES (1, N' Test Account', CAST(N'2016-09-08 10:40:55.097' AS DateTime), CAST(N'2016-09-08 10:41:49.427' AS DateTime))
SET IDENTITY_INSERT [dbo].[Account] OFF
SET IDENTITY_INSERT [dbo].[AttendanceManagement] ON 

INSERT [dbo].[AttendanceManagement] ([Id], [Year], [Month], [InsertDate]) VALUES (1, 2016, 1, CAST(N'2016-09-02 10:32:11.943' AS DateTime))
INSERT [dbo].[AttendanceManagement] ([Id], [Year], [Month], [InsertDate]) VALUES (2, 2016, 9, CAST(N'2016-09-02 10:38:05.307' AS DateTime))
SET IDENTITY_INSERT [dbo].[AttendanceManagement] OFF
SET IDENTITY_INSERT [dbo].[AttendanceManagementDetail] ON 

INSERT [dbo].[AttendanceManagementDetail] ([Id], [AttendanceManagementId], [Date], [IsPresentMorning], [IsAbsentMorning], [IsLeaveMorning], [IsPresentNight], [IsAbsentNight], [IsLeaveNight], [UserId], [InsertDate]) VALUES (1, 1, CAST(N'2016-01-01 00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, NULL, NULL, 3, CAST(N'2016-09-02 10:32:11.947' AS DateTime))
INSERT [dbo].[AttendanceManagementDetail] ([Id], [AttendanceManagementId], [Date], [IsPresentMorning], [IsAbsentMorning], [IsLeaveMorning], [IsPresentNight], [IsAbsentNight], [IsLeaveNight], [UserId], [InsertDate]) VALUES (2, 1, CAST(N'2016-01-02 00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, NULL, NULL, 3, CAST(N'2016-09-02 10:32:14.967' AS DateTime))
INSERT [dbo].[AttendanceManagementDetail] ([Id], [AttendanceManagementId], [Date], [IsPresentMorning], [IsAbsentMorning], [IsLeaveMorning], [IsPresentNight], [IsAbsentNight], [IsLeaveNight], [UserId], [InsertDate]) VALUES (3, 2, CAST(N'2016-09-01 00:00:00.000' AS DateTime), 0, 0, NULL, 1, NULL, NULL, 3, CAST(N'2016-09-02 10:38:05.307' AS DateTime))
INSERT [dbo].[AttendanceManagementDetail] ([Id], [AttendanceManagementId], [Date], [IsPresentMorning], [IsAbsentMorning], [IsLeaveMorning], [IsPresentNight], [IsAbsentNight], [IsLeaveNight], [UserId], [InsertDate]) VALUES (4, 2, CAST(N'2016-09-02 00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, NULL, NULL, 3, CAST(N'2016-09-02 10:38:06.827' AS DateTime))
INSERT [dbo].[AttendanceManagementDetail] ([Id], [AttendanceManagementId], [Date], [IsPresentMorning], [IsAbsentMorning], [IsLeaveMorning], [IsPresentNight], [IsAbsentNight], [IsLeaveNight], [UserId], [InsertDate]) VALUES (5, 2, CAST(N'2016-09-03 00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, NULL, NULL, 3, CAST(N'2016-09-14 10:08:04.900' AS DateTime))
INSERT [dbo].[AttendanceManagementDetail] ([Id], [AttendanceManagementId], [Date], [IsPresentMorning], [IsAbsentMorning], [IsLeaveMorning], [IsPresentNight], [IsAbsentNight], [IsLeaveNight], [UserId], [InsertDate]) VALUES (6, 2, CAST(N'2016-09-04 00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, NULL, NULL, 3, CAST(N'2016-09-14 10:08:06.600' AS DateTime))
INSERT [dbo].[AttendanceManagementDetail] ([Id], [AttendanceManagementId], [Date], [IsPresentMorning], [IsAbsentMorning], [IsLeaveMorning], [IsPresentNight], [IsAbsentNight], [IsLeaveNight], [UserId], [InsertDate]) VALUES (7, 2, CAST(N'2016-09-05 00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, NULL, NULL, 3, CAST(N'2016-09-14 10:08:07.900' AS DateTime))
INSERT [dbo].[AttendanceManagementDetail] ([Id], [AttendanceManagementId], [Date], [IsPresentMorning], [IsAbsentMorning], [IsLeaveMorning], [IsPresentNight], [IsAbsentNight], [IsLeaveNight], [UserId], [InsertDate]) VALUES (8, 2, CAST(N'2016-09-06 00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, NULL, NULL, 3, CAST(N'2016-09-14 10:08:08.847' AS DateTime))
INSERT [dbo].[AttendanceManagementDetail] ([Id], [AttendanceManagementId], [Date], [IsPresentMorning], [IsAbsentMorning], [IsLeaveMorning], [IsPresentNight], [IsAbsentNight], [IsLeaveNight], [UserId], [InsertDate]) VALUES (9, 2, CAST(N'2016-09-07 00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, NULL, NULL, 3, CAST(N'2016-09-14 10:08:09.867' AS DateTime))
INSERT [dbo].[AttendanceManagementDetail] ([Id], [AttendanceManagementId], [Date], [IsPresentMorning], [IsAbsentMorning], [IsLeaveMorning], [IsPresentNight], [IsAbsentNight], [IsLeaveNight], [UserId], [InsertDate]) VALUES (10, 2, CAST(N'2016-09-08 00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, NULL, NULL, 3, CAST(N'2016-09-14 10:08:11.157' AS DateTime))
INSERT [dbo].[AttendanceManagementDetail] ([Id], [AttendanceManagementId], [Date], [IsPresentMorning], [IsAbsentMorning], [IsLeaveMorning], [IsPresentNight], [IsAbsentNight], [IsLeaveNight], [UserId], [InsertDate]) VALUES (11, 2, CAST(N'2016-09-09 00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, NULL, NULL, 3, CAST(N'2016-09-14 10:08:12.043' AS DateTime))
INSERT [dbo].[AttendanceManagementDetail] ([Id], [AttendanceManagementId], [Date], [IsPresentMorning], [IsAbsentMorning], [IsLeaveMorning], [IsPresentNight], [IsAbsentNight], [IsLeaveNight], [UserId], [InsertDate]) VALUES (12, 2, CAST(N'2016-09-10 00:00:00.000' AS DateTime), 1, NULL, NULL, NULL, NULL, NULL, 3, CAST(N'2016-09-14 10:08:12.813' AS DateTime))
SET IDENTITY_INSERT [dbo].[AttendanceManagementDetail] OFF
SET IDENTITY_INSERT [dbo].[CropManagement] ON 

INSERT [dbo].[CropManagement] ([Id], [FarmId], [AreaId], [CropName], [CropDuration], [CropStartDate], [CropEndDate], [InsertDate], [UpdateDate]) VALUES (1, 1, 1, N'Test Crop', N'12345', CAST(N'2016-09-08 00:00:00.000' AS DateTime), CAST(N'2016-09-08 00:00:00.000' AS DateTime), CAST(N'2016-09-08 10:44:14.237' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[CropManagement] OFF
SET IDENTITY_INSERT [dbo].[Farm] ON 

INSERT [dbo].[Farm] ([Id], [FarmName], [Description], [Address], [TotalArea], [ActualPrice], [TaxAmount], [TotalValue], [PaperValue], [Rent], [Owner], [Status], [PurchasedDate], [StartDate], [EndDate], [InsertDate], [UpdateDate]) VALUES (1, N'Test Farm Name', N'Test', N'Test', N'100', CAST(100.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), N'Test', N'InUse', CAST(N'2016-09-08 00:00:00.000' AS DateTime), CAST(N'2016-09-08 00:00:00.000' AS DateTime), CAST(N'2016-09-08 00:00:00.000' AS DateTime), CAST(N'2016-09-08 10:42:55.980' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Farm] OFF
SET IDENTITY_INSERT [dbo].[FarmArea] ON 

INSERT [dbo].[FarmArea] ([Id], [FarmId], [AreaName], [AreaInAcars], [StartingPoint], [EndingPoint], [Date], [InsertDate], [UpdateDate]) VALUES (1, 1, N'Farm Area', N'1000', N'test', N'test', CAST(N'2016-09-08 00:00:00.000' AS DateTime), CAST(N'2016-09-08 10:43:54.637' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[FarmArea] OFF
SET IDENTITY_INSERT [dbo].[Fertilizer] ON 

INSERT [dbo].[Fertilizer] ([Id], [FarmId], [Brand], [FertilizerName], [Type], [Quality], [Quantity], [QuantityInNumber], [VendorId], [Price], [PricePerItem], [DatePurchase], [PaymentBill], [OtherDescription], [InsertDate], [UpdateDate], [AccountId], [UserId]) VALUES (1, 1, N'new', N'Fertilizer', N'Liquid', N'High', N'PerPacket', 10, 1, CAST(10000.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), CAST(N'2016-09-13 00:00:00.000' AS DateTime), N'BestMe_20160731135045_20160913155727707.jpg', N'test', CAST(N'2016-09-13 15:57:29.037' AS DateTime), NULL, 1, 5)
SET IDENTITY_INSERT [dbo].[Fertilizer] OFF
SET IDENTITY_INSERT [dbo].[FertilizerExpense] ON 

INSERT [dbo].[FertilizerExpense] ([Id], [FarmId], [AreaId], [CropId], [FertilizerId], [QuantityInNumber], [Date], [Description], [AccountId], [UserId]) VALUES (1, 1, 1, 1, 1, 9, CAST(N'2016-09-12 00:00:00.000' AS DateTime), N'Used', 1, 5)
SET IDENTITY_INSERT [dbo].[FertilizerExpense] OFF
SET IDENTITY_INSERT [dbo].[Form] ON 

INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (17, N'Employee', N'Employee', N'Employee', N'Index', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (18, N'Permanent Salary', N'Employee', N'Salary', N'PermanentEmployeeSalary', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (20, N'Temporary Wages', N'Employee', N'Salary', N'TemporaryEmployeeWages', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (22, N'Attendance', N'Employee', N'Attendance', N'Index', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (23, N'Crop', N'Crop', N'Crop', N'Index', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (24, N'Farm', N'Farm', N'Farm', N'Index', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (25, N'Vendor', N'Vendor', N'Vendor', N'Index', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (26, N'Task Assignment', N'TaskAssignment', N'TaskAssignment', N'Index', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (27, N'Loan Management', N'LoanManagement', N'LoanManagement', N'Index', CAST(N'2016-06-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (28, N'Personal Account', N'LoanManagement', N'LoanManagement', N'PersonalAccount', CAST(N'2016-06-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (29, N'Paid Loan', N'LoanManagement', N'LoanManagement', N'PaidLoan', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (32, N'Transection Personal Account', N'LoanManagement', N'LoanManagement', N'TransectionPersonalAccount', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (33, N'LoanReturn', N'LoanManagement', N'LoanManagement', N'LoanReturn', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (34, N'TransectionAccount', N'LoanManagement', N'LoanManagement', N'TransectionAccount', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (35, N'Loan From Employee', N'Income', N'Income', N'LoanFromEmployee', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (36, N'Loan From Bank', N'Income', N'Income', N'LoanFromBank', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (37, N'Seed', N'Stock', N'Stock', N'Seed', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (38, N'Vehicle', N'Stock', N'Stock', N'Vehicle', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (39, N'Animal', N'Stock', N'Stock', N'Animal', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (40, N'Fertilizer', N'Stock', N'Stock', N'Fertilizer', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (41, N'Fuel', N'Stock', N'Stock', N'Fuel', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (42, N'Machinery', N'Stock', N'Stock', N'Machinery', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (43, N'OtherItem', N'Stock', N'Stock', N'OtherItem', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (44, N'Seed Expense', N'Expense', N'Expense', N'SeedExpense', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (45, N'Fertilizer Expense', N'Expense', N'Expense', N'FertilizerExpense', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (46, N'Fuel Expense', N'Expense', N'Expense', N'FuelExpense', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (47, N'OtherItemExpense', N'Expense', N'Expense', N'OtherItemExpense', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (48, N'Farm Area', N'Farm', N'Farm', N'FarmArea', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (49, N'Form Setting', N'Setting', N'Setting', N'FormSetting', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (50, N'Task Assignment', N'Report', N'Report', N'TaskAssignmentReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (52, N'Seed', N'Report', N'Report', N'SeedReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (53, N'Fertilizer', N'Report', N'Report', N'FertilizerReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (54, N'Fuel', N'Report', N'Report', N'FuelReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (55, N'Animal', N'Report', N'Report', N'AnimalReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (56, N'Machinery', N'Report', N'Report', N'MachineryReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (57, N'Vehicle', N'Report', N'Report', N'VehicleReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (58, N'Other Item', N'Report', N'Report', N'OtherItemReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (59, N'Expense', N'Report', N'Report', N'ExpenseReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (62, N'Loan Management', N'Report', N'Report', N'LoanManagementReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (63, N'Paid Loan', N'Report', N'Report', N'PaidLoanReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (64, N'Transection Personal', N'Report', N'Report', N'TransectionPersonalReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (66, N'Permanent Salary', N'Report', N'Report', N'PermanentSalaryReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (67, N'Temporary Salary', N'Report', N'Report', N'TemporarySalaryReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (68, N'Attendance', N'Report', N'Report', N'AttendanceReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (69, N'Employee', N'Report', N'Report', N'EmployeeReport', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (70, N'Employee Summary', N'Report', N'Report', N'EmployeeSummary', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Form] ([Id], [Name], [Type], [ControllerName], [ActionName], [InsertDate], [UpdateDate]) VALUES (71, N'Account', N'Farm', N'Farm', N'FarmAccount', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Form] OFF
SET IDENTITY_INSERT [dbo].[FormSetting] ON 

INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (17, 17, 1, 1, 1, CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (18, 18, 1, 1, 1, CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (19, 20, 1, 1, 1, CAST(N'2016-08-30 09:52:57.187' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (20, 22, 1, 1, 1, CAST(N'2016-08-30 09:52:58.257' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (21, 23, 1, 1, 1, CAST(N'2016-08-30 09:52:59.233' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (22, 24, 1, 1, 1, CAST(N'2016-08-30 09:53:00.347' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (23, 26, 1, 1, 1, CAST(N'2016-08-30 09:53:02.147' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (24, 25, 1, 1, 1, CAST(N'2016-08-30 09:53:03.047' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (25, 27, 1, 1, 1, CAST(N'2016-08-30 09:53:04.243' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (26, 28, 1, 1, 1, CAST(N'2016-08-30 09:53:06.180' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (27, 29, 1, 1, 1, CAST(N'2016-08-30 09:53:21.250' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (28, 32, 1, 1, 1, CAST(N'2016-08-30 09:53:22.290' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (29, 33, 1, 1, 1, CAST(N'2016-08-30 09:53:23.497' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (30, 34, 1, 1, 1, CAST(N'2016-08-30 09:53:24.797' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (31, 35, 1, 1, 1, CAST(N'2016-08-30 09:53:25.943' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (32, 36, 1, 1, 1, CAST(N'2016-08-30 09:53:27.190' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (33, 37, 1, 1, 1, CAST(N'2016-08-30 09:53:28.593' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (34, 38, 1, 1, 1, CAST(N'2016-08-30 09:53:29.817' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (35, 39, 1, 1, 1, CAST(N'2016-08-30 09:53:31.013' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (36, 40, 1, 1, 1, CAST(N'2016-08-30 09:53:32.213' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (37, 41, 1, 1, 1, CAST(N'2016-08-30 09:53:47.580' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (38, 42, 1, 1, 1, CAST(N'2016-08-30 09:53:48.613' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (39, 43, 1, 1, 1, CAST(N'2016-08-30 09:53:49.887' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (40, 44, 1, 1, 1, CAST(N'2016-08-30 09:53:50.943' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (41, 45, 1, 1, 1, CAST(N'2016-08-30 09:53:52.097' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (42, 46, 1, 1, 1, CAST(N'2016-08-30 09:53:53.317' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (43, 47, 1, 1, 1, CAST(N'2016-08-30 09:53:54.470' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (44, 48, 1, 1, 1, CAST(N'2016-08-30 09:53:55.617' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (45, 49, 1, 1, 1, CAST(N'2016-08-30 09:53:56.833' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (46, 50, 1, 1, 1, CAST(N'2016-08-30 10:07:05.520' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (47, 52, 1, 1, 1, CAST(N'2016-08-30 10:07:09.677' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (48, 53, 1, 1, 1, CAST(N'2016-08-30 10:07:10.723' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (49, 54, 1, 1, 1, CAST(N'2016-08-30 10:07:11.893' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (50, 55, 1, 1, 1, CAST(N'2016-08-30 10:07:12.900' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (51, 56, 1, 1, 1, CAST(N'2016-08-30 10:07:13.967' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (52, 57, 1, 1, 1, CAST(N'2016-08-30 10:07:15.077' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (53, 58, 1, 1, 1, CAST(N'2016-08-30 10:07:16.070' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (54, 59, 1, 1, 1, CAST(N'2016-08-30 10:07:17.277' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (57, 62, 1, 1, 1, CAST(N'2016-08-30 10:07:36.760' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (58, 63, 1, 1, 1, CAST(N'2016-08-30 10:07:40.120' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (59, 64, 1, 1, 1, CAST(N'2016-08-30 10:07:41.233' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (60, 66, 1, 1, 1, CAST(N'2016-08-30 10:07:42.417' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (61, 67, 1, 1, 1, CAST(N'2016-08-30 10:07:43.633' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (62, 68, 1, 1, 1, CAST(N'2016-08-30 10:07:44.810' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (63, 69, 1, 1, 1, CAST(N'2016-08-30 10:07:45.987' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (64, 70, 1, 1, 1, CAST(N'2016-08-30 10:07:47.163' AS DateTime), NULL)
INSERT [dbo].[FormSetting] ([Id], [FormId], [RoleId], [IsAdd], [IsUpdate], [InsertDate], [UpdateDate]) VALUES (65, 71, 1, 1, 1, CAST(N'2016-08-30 00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[FormSetting] OFF
SET IDENTITY_INSERT [dbo].[PersonalAccount] ON 

INSERT [dbo].[PersonalAccount] ([Id], [UserId], [OpeningBalance], [CreatedDate], [ClosingDate], [InsertDate], [UpdateDate]) VALUES (1, 5, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2016-09-11 00:00:00.000' AS DateTime), NULL, CAST(N'2016-09-11 12:33:17.983' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[PersonalAccount] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [RoleName], [InsertDate], [UpdateDate]) VALUES (1, N'IT', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Role] ([Id], [RoleName], [InsertDate], [UpdateDate]) VALUES (2, N'Employee', CAST(N'2016-03-06 09:36:49.037' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[Seed] ON 

INSERT [dbo].[Seed] ([Id], [FarmId], [Quality], [Quantity], [QuantityInNumber], [VendorId], [TotalPricePer], [PricePerItem], [DatePurchase], [CropId], [PaymentBill], [OtherDescription], [InsertDate], [UpdateDate], [AccountId], [UserId]) VALUES (1, 1, N'High', N'PerBag', 10, 1, CAST(1000.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(N'2016-09-08 00:00:00.000' AS DateTime), 1, N'?+91 95 30 952265? 20140328_181446_20160908105240235.jpg', NULL, CAST(N'2016-09-08 10:52:50.773' AS DateTime), CAST(N'2016-09-08 10:53:10.327' AS DateTime), 1, 5)
SET IDENTITY_INSERT [dbo].[Seed] OFF
SET IDENTITY_INSERT [dbo].[SeedExpense] ON 

INSERT [dbo].[SeedExpense] ([Id], [FarmId], [AreaId], [SeedId], [QuantityInNumber], [Date], [Description], [AccountId], [UserId]) VALUES (1, 1, 1, 1, 10, CAST(N'2016-09-13 00:00:00.000' AS DateTime), N'test', 1, 5)
SET IDENTITY_INSERT [dbo].[SeedExpense] OFF
SET IDENTITY_INSERT [dbo].[TemporaryEmployeeSalary] ON 

INSERT [dbo].[TemporaryEmployeeSalary] ([Id], [Year], [Month], [MorningWages], [NightWages], [InsertDate], [UpdateDate]) VALUES (1, 2016, 9, CAST(400.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), CAST(N'2016-09-01 10:18:18.647' AS DateTime), CAST(N'2016-09-01 10:18:33.173' AS DateTime))
SET IDENTITY_INSERT [dbo].[TemporaryEmployeeSalary] OFF
SET IDENTITY_INSERT [dbo].[TransectionPersonalAccount] ON 

INSERT [dbo].[TransectionPersonalAccount] ([Id], [UserId], [Balance], [Credit], [Debit], [Date], [IsAddedByAdmin], [InsertDate], [UpdateDate]) VALUES (1, 5, CAST(1000000.00 AS Decimal(18, 2)), CAST(1000000.00 AS Decimal(18, 2)), NULL, CAST(N'2016-09-08 00:00:00.000' AS DateTime), 1, CAST(N'2016-09-08 10:46:54.123' AS DateTime), NULL)
INSERT [dbo].[TransectionPersonalAccount] ([Id], [UserId], [Balance], [Credit], [Debit], [Date], [IsAddedByAdmin], [InsertDate], [UpdateDate]) VALUES (2, 3, CAST(1000000.00 AS Decimal(18, 2)), CAST(1000000.00 AS Decimal(18, 2)), NULL, CAST(N'2016-09-08 00:00:00.000' AS DateTime), 1, CAST(N'2016-09-08 10:51:34.927' AS DateTime), NULL)
INSERT [dbo].[TransectionPersonalAccount] ([Id], [UserId], [Balance], [Credit], [Debit], [Date], [IsAddedByAdmin], [InsertDate], [UpdateDate]) VALUES (3, 3, CAST(999000.00 AS Decimal(18, 2)), NULL, CAST(1000.00 AS Decimal(18, 2)), CAST(N'2016-09-08 10:52:51.460' AS DateTime), 0, CAST(N'2016-09-08 10:52:51.460' AS DateTime), NULL)
INSERT [dbo].[TransectionPersonalAccount] ([Id], [UserId], [Balance], [Credit], [Debit], [Date], [IsAddedByAdmin], [InsertDate], [UpdateDate]) VALUES (4, 3, CAST(989000.00 AS Decimal(18, 2)), NULL, CAST(10000.00 AS Decimal(18, 2)), CAST(N'2016-09-13 15:57:29.113' AS DateTime), 0, CAST(N'2016-09-13 15:57:29.113' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[TransectionPersonalAccount] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [UserName], [Password], [RoleId], [Name], [FatherName], [CNIC], [Gender], [MaritalStatus], [DateOfBirth], [PlaceOfBirth], [ContactNo], [Landline], [Nationality], [Email], [PermanentAddress], [CurrentAddress], [DateOfJoining], [ResignationDate], [Status], [Picture], [Position], [IsTemporaryEmployee], [ScannedDocument], [CNICFrontSide], [CNICBackSide], [AgreementDocument], [IsUser], [IsAdmin], [InsertDate], [UpdateDate], [LastLogin], [FarmId], [Department], [Salary]) VALUES (3, N'Test', N'1234', 1, N'Test', N'Test', N'Test', N'Male', N'Married', CAST(N'1995-05-07 00:00:00.000' AS DateTime), N'Test', N'789654123', N'89654123', N'Test', N'test@test.com', N'Test', N'Test', CAST(N'2016-08-28 00:00:00.000' AS DateTime), NULL, N'Active', N'Test.png', N'Professor', 1, NULL, NULL, NULL, NULL, 1, 1, CAST(N'2016-08-28 00:00:00.000' AS DateTime), CAST(N'2016-10-23 19:53:16.310' AS DateTime), CAST(N'2016-10-24 19:17:36.057' AS DateTime), 1, N'Test Department', N'10000')
INSERT [dbo].[User] ([Id], [UserName], [Password], [RoleId], [Name], [FatherName], [CNIC], [Gender], [MaritalStatus], [DateOfBirth], [PlaceOfBirth], [ContactNo], [Landline], [Nationality], [Email], [PermanentAddress], [CurrentAddress], [DateOfJoining], [ResignationDate], [Status], [Picture], [Position], [IsTemporaryEmployee], [ScannedDocument], [CNICFrontSide], [CNICBackSide], [AgreementDocument], [IsUser], [IsAdmin], [InsertDate], [UpdateDate], [LastLogin], [FarmId], [Department], [Salary]) VALUES (5, N'', N'', 1, N'Test1', N'Test', N'Test', N'Male', N'Married', CAST(N'2016-09-03 00:00:00.000' AS DateTime), NULL, N'test', NULL, N'TEst', N'test@test.co', N'Test', NULL, CAST(N'2016-09-03 00:00:00.000' AS DateTime), NULL, N'Active', N'bhagat singh_20160903103002811.jpg', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, CAST(N'2016-09-03 10:30:06.100' AS DateTime), NULL, NULL, 1, N'Test Department', NULL)
INSERT [dbo].[User] ([Id], [UserName], [Password], [RoleId], [Name], [FatherName], [CNIC], [Gender], [MaritalStatus], [DateOfBirth], [PlaceOfBirth], [ContactNo], [Landline], [Nationality], [Email], [PermanentAddress], [CurrentAddress], [DateOfJoining], [ResignationDate], [Status], [Picture], [Position], [IsTemporaryEmployee], [ScannedDocument], [CNICFrontSide], [CNICBackSide], [AgreementDocument], [IsUser], [IsAdmin], [InsertDate], [UpdateDate], [LastLogin], [FarmId], [Department], [Salary]) VALUES (6, N'', N'', 2, N' Test Employee', N'Test Name', N'Test', N'Male', N'UnMarried', CAST(N'2016-09-08 00:00:00.000' AS DateTime), NULL, N'44646', NULL, NULL, N'test@test.co', N'Test', NULL, CAST(N'2016-09-08 00:00:00.000' AS DateTime), NULL, N'Active', N'102698_20160908145417544.gif', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, CAST(N'2016-09-08 14:54:53.993' AS DateTime), CAST(N'2016-10-24 19:19:25.653' AS DateTime), NULL, 1, N'Test', NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[VendorManagement] ON 

INSERT [dbo].[VendorManagement] ([Id], [Name], [CompanyName], [PhoneNo], [PersonalPhoneNo], [AddressInfo], [OpeningBalance], [OtherDescription], [Type], [Date], [InsertDate], [UpdateDate]) VALUES (1, N'Test Vendor', N'Test Vendor', N'789979879', N'5654654654', N'Tst', CAST(10000.00 AS Decimal(18, 2)), NULL, N'ServiceProvider', CAST(N'2016-09-08 00:00:00.000' AS DateTime), CAST(N'2016-09-08 10:45:24.143' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[VendorManagement] OFF
ALTER TABLE [dbo].[Animal]  WITH CHECK ADD  CONSTRAINT [FK_Account_Animal] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Animal] CHECK CONSTRAINT [FK_Account_Animal]
GO
ALTER TABLE [dbo].[Animal]  WITH CHECK ADD  CONSTRAINT [FK_Animal_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Animal] CHECK CONSTRAINT [FK_Animal_UserId]
GO
ALTER TABLE [dbo].[Animal]  WITH CHECK ADD  CONSTRAINT [FK_Farm_Animal] FOREIGN KEY([FarmId])
REFERENCES [dbo].[Farm] ([Id])
GO
ALTER TABLE [dbo].[Animal] CHECK CONSTRAINT [FK_Farm_Animal]
GO
ALTER TABLE [dbo].[Animal]  WITH CHECK ADD  CONSTRAINT [FK_Vendor_Animal] FOREIGN KEY([VendorId])
REFERENCES [dbo].[VendorManagement] ([Id])
GO
ALTER TABLE [dbo].[Animal] CHECK CONSTRAINT [FK_Vendor_Animal]
GO
ALTER TABLE [dbo].[AttendanceManagementDetail]  WITH CHECK ADD  CONSTRAINT [FK_AttendanceMgmt_AttendanceMgmtDetail] FOREIGN KEY([AttendanceManagementId])
REFERENCES [dbo].[AttendanceManagement] ([Id])
GO
ALTER TABLE [dbo].[AttendanceManagementDetail] CHECK CONSTRAINT [FK_AttendanceMgmt_AttendanceMgmtDetail]
GO
ALTER TABLE [dbo].[AttendanceManagementDetail]  WITH CHECK ADD  CONSTRAINT [FK_AttendanceMgmtDetail_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[AttendanceManagementDetail] CHECK CONSTRAINT [FK_AttendanceMgmtDetail_UserId]
GO
ALTER TABLE [dbo].[CropManagement]  WITH CHECK ADD  CONSTRAINT [FK_Farm_CropManagement] FOREIGN KEY([FarmId])
REFERENCES [dbo].[Farm] ([Id])
GO
ALTER TABLE [dbo].[CropManagement] CHECK CONSTRAINT [FK_Farm_CropManagement]
GO
ALTER TABLE [dbo].[CropManagement]  WITH CHECK ADD  CONSTRAINT [FK_FarmArea_CropManagement] FOREIGN KEY([AreaId])
REFERENCES [dbo].[FarmArea] ([Id])
GO
ALTER TABLE [dbo].[CropManagement] CHECK CONSTRAINT [FK_FarmArea_CropManagement]
GO
ALTER TABLE [dbo].[FarmArea]  WITH CHECK ADD  CONSTRAINT [FK_Farm_FarmArea] FOREIGN KEY([FarmId])
REFERENCES [dbo].[Farm] ([Id])
GO
ALTER TABLE [dbo].[FarmArea] CHECK CONSTRAINT [FK_Farm_FarmArea]
GO
ALTER TABLE [dbo].[Fertilizer]  WITH CHECK ADD  CONSTRAINT [FK_Account_Fertilizer] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Fertilizer] CHECK CONSTRAINT [FK_Account_Fertilizer]
GO
ALTER TABLE [dbo].[Fertilizer]  WITH CHECK ADD  CONSTRAINT [FK_Farm_Fertilizer] FOREIGN KEY([FarmId])
REFERENCES [dbo].[Farm] ([Id])
GO
ALTER TABLE [dbo].[Fertilizer] CHECK CONSTRAINT [FK_Farm_Fertilizer]
GO
ALTER TABLE [dbo].[Fertilizer]  WITH CHECK ADD  CONSTRAINT [FK_Fertilizer_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Fertilizer] CHECK CONSTRAINT [FK_Fertilizer_UserId]
GO
ALTER TABLE [dbo].[Fertilizer]  WITH CHECK ADD  CONSTRAINT [FK_Vendor_Fertilizer] FOREIGN KEY([VendorId])
REFERENCES [dbo].[VendorManagement] ([Id])
GO
ALTER TABLE [dbo].[Fertilizer] CHECK CONSTRAINT [FK_Vendor_Fertilizer]
GO
ALTER TABLE [dbo].[FertilizerExpense]  WITH CHECK ADD  CONSTRAINT [FK_Account_FertilizerExpense] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[FertilizerExpense] CHECK CONSTRAINT [FK_Account_FertilizerExpense]
GO
ALTER TABLE [dbo].[FertilizerExpense]  WITH CHECK ADD  CONSTRAINT [FK_CropManagement_FertilizerExpense] FOREIGN KEY([CropId])
REFERENCES [dbo].[CropManagement] ([Id])
GO
ALTER TABLE [dbo].[FertilizerExpense] CHECK CONSTRAINT [FK_CropManagement_FertilizerExpense]
GO
ALTER TABLE [dbo].[FertilizerExpense]  WITH CHECK ADD  CONSTRAINT [FK_Farm_FertilizerExpense] FOREIGN KEY([FarmId])
REFERENCES [dbo].[Farm] ([Id])
GO
ALTER TABLE [dbo].[FertilizerExpense] CHECK CONSTRAINT [FK_Farm_FertilizerExpense]
GO
ALTER TABLE [dbo].[FertilizerExpense]  WITH CHECK ADD  CONSTRAINT [FK_FarmArea_FertilizerExpense] FOREIGN KEY([AreaId])
REFERENCES [dbo].[FarmArea] ([Id])
GO
ALTER TABLE [dbo].[FertilizerExpense] CHECK CONSTRAINT [FK_FarmArea_FertilizerExpense]
GO
ALTER TABLE [dbo].[FertilizerExpense]  WITH CHECK ADD  CONSTRAINT [FK_Fertilizer_FertilizerExpense] FOREIGN KEY([FertilizerId])
REFERENCES [dbo].[Fertilizer] ([Id])
GO
ALTER TABLE [dbo].[FertilizerExpense] CHECK CONSTRAINT [FK_Fertilizer_FertilizerExpense]
GO
ALTER TABLE [dbo].[FertilizerExpense]  WITH CHECK ADD  CONSTRAINT [FK_FertilizerExpense_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[FertilizerExpense] CHECK CONSTRAINT [FK_FertilizerExpense_UserId]
GO
ALTER TABLE [dbo].[FormSetting]  WITH CHECK ADD  CONSTRAINT [FK_Form_FromSetting] FOREIGN KEY([FormId])
REFERENCES [dbo].[Form] ([Id])
GO
ALTER TABLE [dbo].[FormSetting] CHECK CONSTRAINT [FK_Form_FromSetting]
GO
ALTER TABLE [dbo].[FormSetting]  WITH CHECK ADD  CONSTRAINT [FK_From_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[FormSetting] CHECK CONSTRAINT [FK_From_Role]
GO
ALTER TABLE [dbo].[Fuel]  WITH CHECK ADD  CONSTRAINT [FK_Account_Fuel] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Fuel] CHECK CONSTRAINT [FK_Account_Fuel]
GO
ALTER TABLE [dbo].[Fuel]  WITH CHECK ADD  CONSTRAINT [FK_Farm_Fuel] FOREIGN KEY([FarmId])
REFERENCES [dbo].[Farm] ([Id])
GO
ALTER TABLE [dbo].[Fuel] CHECK CONSTRAINT [FK_Farm_Fuel]
GO
ALTER TABLE [dbo].[Fuel]  WITH CHECK ADD  CONSTRAINT [FK_Fuel_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Fuel] CHECK CONSTRAINT [FK_Fuel_UserId]
GO
ALTER TABLE [dbo].[Fuel]  WITH CHECK ADD  CONSTRAINT [FK_Vendor_Fuel] FOREIGN KEY([VendorId])
REFERENCES [dbo].[VendorManagement] ([Id])
GO
ALTER TABLE [dbo].[Fuel] CHECK CONSTRAINT [FK_Vendor_Fuel]
GO
ALTER TABLE [dbo].[FuelExpense]  WITH CHECK ADD  CONSTRAINT [FK_Account_FuelExpense] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[FuelExpense] CHECK CONSTRAINT [FK_Account_FuelExpense]
GO
ALTER TABLE [dbo].[FuelExpense]  WITH CHECK ADD  CONSTRAINT [FK_CropManagement_FuelExpense] FOREIGN KEY([CropId])
REFERENCES [dbo].[CropManagement] ([Id])
GO
ALTER TABLE [dbo].[FuelExpense] CHECK CONSTRAINT [FK_CropManagement_FuelExpense]
GO
ALTER TABLE [dbo].[FuelExpense]  WITH CHECK ADD  CONSTRAINT [FK_Farm_FuelExpense] FOREIGN KEY([FarmId])
REFERENCES [dbo].[Farm] ([Id])
GO
ALTER TABLE [dbo].[FuelExpense] CHECK CONSTRAINT [FK_Farm_FuelExpense]
GO
ALTER TABLE [dbo].[FuelExpense]  WITH CHECK ADD  CONSTRAINT [FK_FarmArea_FuelExpense] FOREIGN KEY([AreaId])
REFERENCES [dbo].[FarmArea] ([Id])
GO
ALTER TABLE [dbo].[FuelExpense] CHECK CONSTRAINT [FK_FarmArea_FuelExpense]
GO
ALTER TABLE [dbo].[FuelExpense]  WITH CHECK ADD  CONSTRAINT [FK_Fuel_FuelExpense] FOREIGN KEY([FuelId])
REFERENCES [dbo].[Fuel] ([Id])
GO
ALTER TABLE [dbo].[FuelExpense] CHECK CONSTRAINT [FK_Fuel_FuelExpense]
GO
ALTER TABLE [dbo].[FuelExpense]  WITH CHECK ADD  CONSTRAINT [FK_FuelExpense_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[FuelExpense] CHECK CONSTRAINT [FK_FuelExpense_UserId]
GO
ALTER TABLE [dbo].[LoanManagement]  WITH CHECK ADD  CONSTRAINT [FK_LoanManagement_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[LoanManagement] CHECK CONSTRAINT [FK_LoanManagement_User]
GO
ALTER TABLE [dbo].[LoanReceivableFromBank]  WITH CHECK ADD  CONSTRAINT [FK_Account_BankLoan] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[LoanReceivableFromBank] CHECK CONSTRAINT [FK_Account_BankLoan]
GO
ALTER TABLE [dbo].[LoanReceivableFromBank]  WITH CHECK ADD  CONSTRAINT [FK_LoanManagement_BankLoan] FOREIGN KEY([LoanId])
REFERENCES [dbo].[LoanManagement] ([Id])
GO
ALTER TABLE [dbo].[LoanReceivableFromBank] CHECK CONSTRAINT [FK_LoanManagement_BankLoan]
GO
ALTER TABLE [dbo].[LoanReceivableFromBank]  WITH CHECK ADD  CONSTRAINT [FK_User_BankLoan] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[LoanReceivableFromBank] CHECK CONSTRAINT [FK_User_BankLoan]
GO
ALTER TABLE [dbo].[LoanReceivableFromEmployee]  WITH CHECK ADD  CONSTRAINT [FK_Account_EmployeeLoan] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[LoanReceivableFromEmployee] CHECK CONSTRAINT [FK_Account_EmployeeLoan]
GO
ALTER TABLE [dbo].[LoanReceivableFromEmployee]  WITH CHECK ADD  CONSTRAINT [FK_LoanManagement_EmployeeLoan] FOREIGN KEY([LoanId])
REFERENCES [dbo].[LoanManagement] ([Id])
GO
ALTER TABLE [dbo].[LoanReceivableFromEmployee] CHECK CONSTRAINT [FK_LoanManagement_EmployeeLoan]
GO
ALTER TABLE [dbo].[LoanReceivableFromEmployee]  WITH CHECK ADD  CONSTRAINT [FK_User_EmployeeLoan] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[LoanReceivableFromEmployee] CHECK CONSTRAINT [FK_User_EmployeeLoan]
GO
ALTER TABLE [dbo].[Machinery]  WITH CHECK ADD  CONSTRAINT [FK_Account_Machinery] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Machinery] CHECK CONSTRAINT [FK_Account_Machinery]
GO
ALTER TABLE [dbo].[Machinery]  WITH CHECK ADD  CONSTRAINT [FK_Farm_Machinery] FOREIGN KEY([FarmId])
REFERENCES [dbo].[Farm] ([Id])
GO
ALTER TABLE [dbo].[Machinery] CHECK CONSTRAINT [FK_Farm_Machinery]
GO
ALTER TABLE [dbo].[Machinery]  WITH CHECK ADD  CONSTRAINT [FK_Machinery_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Machinery] CHECK CONSTRAINT [FK_Machinery_UserId]
GO
ALTER TABLE [dbo].[Machinery]  WITH CHECK ADD  CONSTRAINT [FK_Vendor_Machinery] FOREIGN KEY([VendorId])
REFERENCES [dbo].[VendorManagement] ([Id])
GO
ALTER TABLE [dbo].[Machinery] CHECK CONSTRAINT [FK_Vendor_Machinery]
GO
ALTER TABLE [dbo].[OtherExpense]  WITH CHECK ADD  CONSTRAINT [FK_Account_OtherExpense] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[OtherExpense] CHECK CONSTRAINT [FK_Account_OtherExpense]
GO
ALTER TABLE [dbo].[OtherExpense]  WITH CHECK ADD  CONSTRAINT [FK_Farm_OtherExpense] FOREIGN KEY([FarmId])
REFERENCES [dbo].[Farm] ([Id])
GO
ALTER TABLE [dbo].[OtherExpense] CHECK CONSTRAINT [FK_Farm_OtherExpense]
GO
ALTER TABLE [dbo].[OtherExpense]  WITH CHECK ADD  CONSTRAINT [FK_OtherExpense_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[OtherExpense] CHECK CONSTRAINT [FK_OtherExpense_UserId]
GO
ALTER TABLE [dbo].[OtherExpense]  WITH CHECK ADD  CONSTRAINT [FK_OtherItem_OtherItemExpense] FOREIGN KEY([OtherItemId])
REFERENCES [dbo].[OtherItem] ([ItemId])
GO
ALTER TABLE [dbo].[OtherExpense] CHECK CONSTRAINT [FK_OtherItem_OtherItemExpense]
GO
ALTER TABLE [dbo].[OtherItem]  WITH CHECK ADD  CONSTRAINT [FK_Account_OtherItem] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[OtherItem] CHECK CONSTRAINT [FK_Account_OtherItem]
GO
ALTER TABLE [dbo].[OtherItem]  WITH CHECK ADD  CONSTRAINT [FK_Farm_OtherItem] FOREIGN KEY([FarmId])
REFERENCES [dbo].[Farm] ([Id])
GO
ALTER TABLE [dbo].[OtherItem] CHECK CONSTRAINT [FK_Farm_OtherItem]
GO
ALTER TABLE [dbo].[OtherItem]  WITH CHECK ADD  CONSTRAINT [FK_OtherItem_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[OtherItem] CHECK CONSTRAINT [FK_OtherItem_UserId]
GO
ALTER TABLE [dbo].[PaidLoan]  WITH CHECK ADD  CONSTRAINT [FK_Account_PaidLoan] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[PaidLoan] CHECK CONSTRAINT [FK_Account_PaidLoan]
GO
ALTER TABLE [dbo].[PaidLoan]  WITH CHECK ADD  CONSTRAINT [FK_LoanManagement_PaidLoan] FOREIGN KEY([LoanId])
REFERENCES [dbo].[LoanManagement] ([Id])
GO
ALTER TABLE [dbo].[PaidLoan] CHECK CONSTRAINT [FK_LoanManagement_PaidLoan]
GO
ALTER TABLE [dbo].[PaidLoan]  WITH CHECK ADD  CONSTRAINT [FK_PaidLoan_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[PaidLoan] CHECK CONSTRAINT [FK_PaidLoan_User]
GO
ALTER TABLE [dbo].[PermanentEmployeeSalary]  WITH CHECK ADD  CONSTRAINT [FK_PermanentEmployeeSalary_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[PermanentEmployeeSalary] CHECK CONSTRAINT [FK_PermanentEmployeeSalary_User]
GO
ALTER TABLE [dbo].[PersonalAccount]  WITH CHECK ADD  CONSTRAINT [FK_PersonalAccount_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[PersonalAccount] CHECK CONSTRAINT [FK_PersonalAccount_User]
GO
ALTER TABLE [dbo].[ReturnLoan]  WITH CHECK ADD  CONSTRAINT [FK_Account_ReturnLoan] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[ReturnLoan] CHECK CONSTRAINT [FK_Account_ReturnLoan]
GO
ALTER TABLE [dbo].[ReturnLoan]  WITH CHECK ADD  CONSTRAINT [FK_LoanManagement_ReturnLoan] FOREIGN KEY([LoanId])
REFERENCES [dbo].[LoanManagement] ([Id])
GO
ALTER TABLE [dbo].[ReturnLoan] CHECK CONSTRAINT [FK_LoanManagement_ReturnLoan]
GO
ALTER TABLE [dbo].[ReturnLoan]  WITH CHECK ADD  CONSTRAINT [FK_User_ReturnLoan] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ReturnLoan] CHECK CONSTRAINT [FK_User_ReturnLoan]
GO
ALTER TABLE [dbo].[Seed]  WITH CHECK ADD  CONSTRAINT [FK_Account_Seed] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Seed] CHECK CONSTRAINT [FK_Account_Seed]
GO
ALTER TABLE [dbo].[Seed]  WITH CHECK ADD  CONSTRAINT [FK_CropManagement_SeedExpense] FOREIGN KEY([CropId])
REFERENCES [dbo].[CropManagement] ([Id])
GO
ALTER TABLE [dbo].[Seed] CHECK CONSTRAINT [FK_CropManagement_SeedExpense]
GO
ALTER TABLE [dbo].[Seed]  WITH CHECK ADD  CONSTRAINT [FK_Farm_Seed] FOREIGN KEY([FarmId])
REFERENCES [dbo].[Farm] ([Id])
GO
ALTER TABLE [dbo].[Seed] CHECK CONSTRAINT [FK_Farm_Seed]
GO
ALTER TABLE [dbo].[Seed]  WITH CHECK ADD  CONSTRAINT [FK_Seed_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Seed] CHECK CONSTRAINT [FK_Seed_UserId]
GO
ALTER TABLE [dbo].[Seed]  WITH CHECK ADD  CONSTRAINT [FK_Vendor_Seed] FOREIGN KEY([VendorId])
REFERENCES [dbo].[VendorManagement] ([Id])
GO
ALTER TABLE [dbo].[Seed] CHECK CONSTRAINT [FK_Vendor_Seed]
GO
ALTER TABLE [dbo].[SeedExpense]  WITH CHECK ADD  CONSTRAINT [FK_Account_SeedExpense] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[SeedExpense] CHECK CONSTRAINT [FK_Account_SeedExpense]
GO
ALTER TABLE [dbo].[SeedExpense]  WITH CHECK ADD  CONSTRAINT [FK_Farm_SeedExpense] FOREIGN KEY([FarmId])
REFERENCES [dbo].[Farm] ([Id])
GO
ALTER TABLE [dbo].[SeedExpense] CHECK CONSTRAINT [FK_Farm_SeedExpense]
GO
ALTER TABLE [dbo].[SeedExpense]  WITH CHECK ADD  CONSTRAINT [FK_FarmArea_SeedExpense] FOREIGN KEY([AreaId])
REFERENCES [dbo].[FarmArea] ([Id])
GO
ALTER TABLE [dbo].[SeedExpense] CHECK CONSTRAINT [FK_FarmArea_SeedExpense]
GO
ALTER TABLE [dbo].[SeedExpense]  WITH CHECK ADD  CONSTRAINT [FK_Seed_SeedExpense] FOREIGN KEY([SeedId])
REFERENCES [dbo].[Seed] ([Id])
GO
ALTER TABLE [dbo].[SeedExpense] CHECK CONSTRAINT [FK_Seed_SeedExpense]
GO
ALTER TABLE [dbo].[SeedExpense]  WITH CHECK ADD  CONSTRAINT [FK_SeedExpense_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[SeedExpense] CHECK CONSTRAINT [FK_SeedExpense_UserId]
GO
ALTER TABLE [dbo].[TaskAssignment]  WITH CHECK ADD  CONSTRAINT [FK_User_TaskAssignment] FOREIGN KEY([StaffNameId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[TaskAssignment] CHECK CONSTRAINT [FK_User_TaskAssignment]
GO
ALTER TABLE [dbo].[TransectionAccount]  WITH CHECK ADD  CONSTRAINT [FK_TransectionAccount_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[TransectionAccount] CHECK CONSTRAINT [FK_TransectionAccount_User]
GO
ALTER TABLE [dbo].[TransectionAccount]  WITH CHECK ADD  CONSTRAINT [FK_TransectionAccount_User_PayBy] FOREIGN KEY([PayByUserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[TransectionAccount] CHECK CONSTRAINT [FK_TransectionAccount_User_PayBy]
GO
ALTER TABLE [dbo].[TransectionPersonalAccount]  WITH CHECK ADD  CONSTRAINT [FK_TransectionPersonalAccount_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[TransectionPersonalAccount] CHECK CONSTRAINT [FK_TransectionPersonalAccount_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_Farm_User] FOREIGN KEY([FarmId])
REFERENCES [dbo].[Farm] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_Farm_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_Role_User] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_Role_User]
GO
ALTER TABLE [dbo].[Vehicle]  WITH CHECK ADD  CONSTRAINT [FK_Account_Vehicle] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Vehicle] CHECK CONSTRAINT [FK_Account_Vehicle]
GO
ALTER TABLE [dbo].[Vehicle]  WITH CHECK ADD  CONSTRAINT [FK_Farm_Vehicle] FOREIGN KEY([FarmId])
REFERENCES [dbo].[Farm] ([Id])
GO
ALTER TABLE [dbo].[Vehicle] CHECK CONSTRAINT [FK_Farm_Vehicle]
GO
ALTER TABLE [dbo].[Vehicle]  WITH CHECK ADD  CONSTRAINT [FK_Vehicle_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Vehicle] CHECK CONSTRAINT [FK_Vehicle_UserId]
GO
ALTER TABLE [dbo].[Vehicle]  WITH CHECK ADD  CONSTRAINT [FK_Vendor_Vehicle] FOREIGN KEY([VendorId])
REFERENCES [dbo].[VendorManagement] ([Id])
GO
ALTER TABLE [dbo].[Vehicle] CHECK CONSTRAINT [FK_Vendor_Vehicle]
GO
USE [master]
GO
ALTER DATABASE [FarmManagement] SET  READ_WRITE 
GO
