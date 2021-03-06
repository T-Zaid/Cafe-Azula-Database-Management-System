USE [master]
GO
/****** Object:  Database [AzulaDB]    Script Date: 21/12/2021 12:12:44 ******/
CREATE DATABASE [AzulaDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AzulaDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SERWORK\MSSQL\DATA\AzulaDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AzulaDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SERWORK\MSSQL\DATA\AzulaDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AzulaDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AzulaDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AzulaDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AzulaDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AzulaDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AzulaDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AzulaDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [AzulaDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AzulaDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AzulaDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AzulaDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AzulaDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AzulaDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AzulaDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AzulaDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AzulaDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AzulaDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [AzulaDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AzulaDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AzulaDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AzulaDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AzulaDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AzulaDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AzulaDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AzulaDB] SET RECOVERY FULL 
GO
ALTER DATABASE [AzulaDB] SET  MULTI_USER 
GO
ALTER DATABASE [AzulaDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AzulaDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AzulaDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AzulaDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AzulaDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AzulaDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'AzulaDB', N'ON'
GO
ALTER DATABASE [AzulaDB] SET QUERY_STORE = OFF
GO
USE [AzulaDB]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 21/12/2021 12:12:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountNo] [int] NOT NULL,
	[Username] [varchar](25) NOT NULL,
	[AccPassword] [varchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 21/12/2021 12:12:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] NOT NULL,
	[CustName] [varchar](50) NOT NULL,
	[PhoneNo] [varchar](11) NULL,
	[AccountNo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Games]    Script Date: 21/12/2021 12:12:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Games](
	[GameID] [int] NOT NULL,
	[GameName] [varchar](50) NOT NULL,
	[Genre] [varchar](25) NULL,
	[GameDescription] [varchar](100) NULL,
	[Popularity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[GameID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Leaderboard]    Script Date: 21/12/2021 12:12:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leaderboard](
	[GameID] [int] NOT NULL,
	[CustomerID] [int] NOT NULL,
	[GameRank] [int] NULL,
 CONSTRAINT [Leaderboard_PK] PRIMARY KEY CLUSTERED 
(
	[GameID] ASC,
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Leaderboard_Details]    Script Date: 21/12/2021 12:12:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Leaderboard_Details] AS 
SELECT L.GameRank AS "Rank", A.Username AS "Gamer Tag", C.CustName AS "Name", G.GameName AS "Game"
FROM Leaderboard AS L	JOIN Customers AS C ON L.CustomerID = C.CustomerID
						JOIN Accounts AS A ON C.AccountNo = A.AccountNo
						JOIN Games AS G on L.GameID = G.GameID;
GO
/****** Object:  View [dbo].[Customer_Profile]    Script Date: 21/12/2021 12:12:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Customer_Profile] AS
SELECT CustomerID, CustName, PhoneNo, Username, AccPassword FROM Accounts AS A JOIN Customers AS C ON A.AccountNo = C.AccountNo;
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 21/12/2021 12:12:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[SeatNo] [int] NOT NULL,
	[Date_of_Booking] [date] NOT NULL,
	[Start_Time] [datetime] NOT NULL,
	[End_Time] [datetime] NOT NULL,
	[Amount_Paid] [int] NULL,
	[CustomerID] [int] NOT NULL,
 CONSTRAINT [Bookings_PK] PRIMARY KEY CLUSTERED 
(
	[SeatNo] ASC,
	[Start_Time] ASC,
	[End_Time] ASC,
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Computers]    Script Date: 21/12/2021 12:12:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Computers](
	[ComputerID] [int] NOT NULL,
	[CPU] [varchar](25) NULL,
	[GPU] [varchar](25) NULL,
	[RAM] [int] NULL,
	[NetSpeed] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ComputerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 21/12/2021 12:12:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[EventID] [int] NOT NULL,
	[EventName] [varchar](50) NOT NULL,
	[Start_Time] [datetime] NOT NULL,
	[End_Time] [datetime] NOT NULL,
	[GameID] [int] NULL,
	[Max_Participants] [int] NULL,
	[Poster_link] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seats]    Script Date: 21/12/2021 12:12:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seats](
	[SeatNo] [int] NOT NULL,
	[CurrentStatus] [varchar](15) NOT NULL,
	[Premium_YES_NO] [bit] NOT NULL,
	[ComputerID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SeatNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 21/12/2021 12:12:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[StaffID] [int] NOT NULL,
	[StaffName] [varchar](50) NOT NULL,
	[PhoneNo] [varchar](11) NULL,
	[Salary] [int] NOT NULL,
	[AccountNo] [int] NOT NULL,
	[Position] [varchar](25) NOT NULL,
	[Supervisor_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StaffID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Accounts] ([AccountNo], [Username], [AccPassword]) VALUES (1, N'Deiji_Okzen', N'Usman')
INSERT [dbo].[Accounts] ([AccountNo], [Username], [AccPassword]) VALUES (2, N'TigerMaster', N'Anushka')
INSERT [dbo].[Accounts] ([AccountNo], [Username], [AccPassword]) VALUES (3, N'Teakay23', N'MissMakran')
INSERT [dbo].[Accounts] ([AccountNo], [Username], [AccPassword]) VALUES (4, N'DeadLock', N'Areeba')
INSERT [dbo].[Accounts] ([AccountNo], [Username], [AccPassword]) VALUES (5, N'Mudguard', N'U22')
INSERT [dbo].[Accounts] ([AccountNo], [Username], [AccPassword]) VALUES (9, N'Tenzin', N'Tenzin')
INSERT [dbo].[Accounts] ([AccountNo], [Username], [AccPassword]) VALUES (10, N'ImTheAttack', N'ammarkhan')
GO
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (1, CAST(N'2021-12-03' AS Date), CAST(N'2021-12-03T04:51:00.000' AS DateTime), CAST(N'2021-12-03T05:51:00.000' AS DateTime), 100, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (1, CAST(N'2021-12-03' AS Date), CAST(N'2021-12-04T11:00:00.000' AS DateTime), CAST(N'2021-12-04T13:00:00.000' AS DateTime), 200, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (1, CAST(N'2021-12-05' AS Date), CAST(N'2021-12-05T04:00:00.000' AS DateTime), CAST(N'2021-12-05T06:00:00.000' AS DateTime), 300, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (1, CAST(N'2021-12-04' AS Date), CAST(N'2021-12-05T05:00:00.000' AS DateTime), CAST(N'2021-12-05T07:00:00.000' AS DateTime), 200, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (1, CAST(N'2021-12-06' AS Date), CAST(N'2021-12-06T01:33:29.000' AS DateTime), CAST(N'2021-12-06T02:33:29.000' AS DateTime), 100, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (1, CAST(N'2021-12-05' AS Date), CAST(N'2021-12-06T20:44:11.000' AS DateTime), CAST(N'2021-12-07T01:44:11.000' AS DateTime), 500, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (1, CAST(N'2021-12-07' AS Date), CAST(N'2021-12-07T21:33:10.000' AS DateTime), CAST(N'2021-12-07T22:33:10.000' AS DateTime), 100, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (1, CAST(N'2021-12-06' AS Date), CAST(N'2021-12-08T01:43:54.000' AS DateTime), CAST(N'2021-12-08T02:43:54.000' AS DateTime), 100, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (1, CAST(N'2021-12-07' AS Date), CAST(N'2021-12-10T17:41:20.000' AS DateTime), CAST(N'2021-12-10T21:41:20.000' AS DateTime), 400, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (1, CAST(N'2021-12-10' AS Date), CAST(N'2021-12-11T14:22:44.000' AS DateTime), CAST(N'2021-12-11T15:22:44.000' AS DateTime), 100, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (1, CAST(N'2021-12-10' AS Date), CAST(N'2021-12-12T23:07:14.000' AS DateTime), CAST(N'2021-12-13T02:07:14.000' AS DateTime), 300, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (1, CAST(N'2021-12-12' AS Date), CAST(N'2021-12-15T20:00:00.000' AS DateTime), CAST(N'2021-12-16T07:00:00.000' AS DateTime), 1100, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (1, CAST(N'2021-12-21' AS Date), CAST(N'2021-12-21T11:35:32.000' AS DateTime), CAST(N'2021-12-21T12:35:32.000' AS DateTime), 100, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (1, CAST(N'2021-12-12' AS Date), CAST(N'2021-12-26T12:30:00.000' AS DateTime), CAST(N'2021-12-26T13:30:00.000' AS DateTime), 100, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (1, CAST(N'2021-12-19' AS Date), CAST(N'2021-12-31T12:00:00.000' AS DateTime), CAST(N'2021-12-31T15:00:00.000' AS DateTime), 300, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (2, CAST(N'2021-12-04' AS Date), CAST(N'2021-12-05T05:00:00.000' AS DateTime), CAST(N'2021-12-05T07:00:00.000' AS DateTime), 400, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (2, CAST(N'2021-12-06' AS Date), CAST(N'2021-12-06T01:33:29.000' AS DateTime), CAST(N'2021-12-06T02:33:29.000' AS DateTime), 300, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (2, CAST(N'2021-12-07' AS Date), CAST(N'2021-12-07T21:33:10.000' AS DateTime), CAST(N'2021-12-07T22:33:10.000' AS DateTime), 300, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (2, CAST(N'2021-12-07' AS Date), CAST(N'2021-12-10T17:41:20.000' AS DateTime), CAST(N'2021-12-10T21:41:20.000' AS DateTime), 600, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (2, CAST(N'2021-12-10' AS Date), CAST(N'2021-12-11T14:22:44.000' AS DateTime), CAST(N'2021-12-11T15:22:44.000' AS DateTime), 300, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (2, CAST(N'2021-12-10' AS Date), CAST(N'2021-12-12T23:07:14.000' AS DateTime), CAST(N'2021-12-13T02:07:14.000' AS DateTime), 500, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (2, CAST(N'2021-12-12' AS Date), CAST(N'2021-12-15T20:00:00.000' AS DateTime), CAST(N'2021-12-16T07:00:00.000' AS DateTime), 1300, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (2, CAST(N'2021-12-10' AS Date), CAST(N'2021-12-20T17:00:00.000' AS DateTime), CAST(N'2021-12-20T19:00:00.000' AS DateTime), 400, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (2, CAST(N'2021-12-21' AS Date), CAST(N'2021-12-21T11:35:32.000' AS DateTime), CAST(N'2021-12-21T12:35:32.000' AS DateTime), 300, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (2, CAST(N'2021-12-12' AS Date), CAST(N'2021-12-26T12:30:00.000' AS DateTime), CAST(N'2021-12-26T13:30:00.000' AS DateTime), 300, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (2, CAST(N'2021-12-19' AS Date), CAST(N'2021-12-26T19:25:21.000' AS DateTime), CAST(N'2021-12-26T23:25:21.000' AS DateTime), 600, 3)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (2, CAST(N'2021-12-19' AS Date), CAST(N'2021-12-31T12:00:00.000' AS DateTime), CAST(N'2021-12-31T15:00:00.000' AS DateTime), 500, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (3, CAST(N'2021-12-06' AS Date), CAST(N'2021-12-06T01:33:29.000' AS DateTime), CAST(N'2021-12-06T02:33:29.000' AS DateTime), 100, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (3, CAST(N'2021-12-05' AS Date), CAST(N'2021-12-06T20:44:11.000' AS DateTime), CAST(N'2021-12-07T01:44:11.000' AS DateTime), 500, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (3, CAST(N'2021-12-07' AS Date), CAST(N'2021-12-07T21:33:10.000' AS DateTime), CAST(N'2021-12-07T22:33:10.000' AS DateTime), 100, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (3, CAST(N'2021-12-07' AS Date), CAST(N'2021-12-10T17:41:20.000' AS DateTime), CAST(N'2021-12-10T21:41:20.000' AS DateTime), 400, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (3, CAST(N'2021-12-12' AS Date), CAST(N'2021-12-15T20:00:00.000' AS DateTime), CAST(N'2021-12-16T07:00:00.000' AS DateTime), 1100, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (3, CAST(N'2021-12-10' AS Date), CAST(N'2021-12-20T17:00:00.000' AS DateTime), CAST(N'2021-12-20T19:00:00.000' AS DateTime), 200, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (3, CAST(N'2021-12-19' AS Date), CAST(N'2021-12-31T12:00:00.000' AS DateTime), CAST(N'2021-12-31T15:00:00.000' AS DateTime), 300, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (4, CAST(N'2021-12-06' AS Date), CAST(N'2021-12-06T01:33:29.000' AS DateTime), CAST(N'2021-12-06T02:33:29.000' AS DateTime), 300, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (4, CAST(N'2021-12-07' AS Date), CAST(N'2021-12-07T21:40:24.000' AS DateTime), CAST(N'2021-12-07T22:40:24.000' AS DateTime), 300, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (4, CAST(N'2021-12-07' AS Date), CAST(N'2021-12-10T17:41:20.000' AS DateTime), CAST(N'2021-12-10T21:41:20.000' AS DateTime), 600, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (4, CAST(N'2021-12-12' AS Date), CAST(N'2021-12-15T20:00:00.000' AS DateTime), CAST(N'2021-12-16T07:00:00.000' AS DateTime), 1300, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (4, CAST(N'2021-12-12' AS Date), CAST(N'2021-12-20T17:00:00.000' AS DateTime), CAST(N'2021-12-20T19:00:00.000' AS DateTime), 400, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (4, CAST(N'2021-12-19' AS Date), CAST(N'2021-12-26T19:25:21.000' AS DateTime), CAST(N'2021-12-26T23:25:21.000' AS DateTime), 600, 3)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (4, CAST(N'2021-12-19' AS Date), CAST(N'2021-12-31T12:00:00.000' AS DateTime), CAST(N'2021-12-31T15:00:00.000' AS DateTime), 500, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (5, CAST(N'2021-12-07' AS Date), CAST(N'2021-12-07T21:33:10.000' AS DateTime), CAST(N'2021-12-07T22:33:10.000' AS DateTime), 100, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (5, CAST(N'2021-12-12' AS Date), CAST(N'2021-12-15T20:00:00.000' AS DateTime), CAST(N'2021-12-16T07:00:00.000' AS DateTime), 1100, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (5, CAST(N'2021-12-12' AS Date), CAST(N'2021-12-20T17:00:00.000' AS DateTime), CAST(N'2021-12-20T19:00:00.000' AS DateTime), 200, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (5, CAST(N'2021-12-19' AS Date), CAST(N'2021-12-26T19:25:21.000' AS DateTime), CAST(N'2021-12-26T23:25:21.000' AS DateTime), 400, 3)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (5, CAST(N'2021-12-19' AS Date), CAST(N'2021-12-30T14:00:00.000' AS DateTime), CAST(N'2021-12-30T19:00:00.000' AS DateTime), 500, 3)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (5, CAST(N'2021-12-19' AS Date), CAST(N'2021-12-31T12:00:00.000' AS DateTime), CAST(N'2021-12-31T15:00:00.000' AS DateTime), 300, 1)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (6, CAST(N'2021-12-07' AS Date), CAST(N'2021-12-07T21:33:10.000' AS DateTime), CAST(N'2021-12-07T22:33:10.000' AS DateTime), 300, 2)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (6, CAST(N'2021-12-19' AS Date), CAST(N'2021-12-30T14:00:00.000' AS DateTime), CAST(N'2021-12-30T19:00:00.000' AS DateTime), 700, 3)
INSERT [dbo].[Bookings] ([SeatNo], [Date_of_Booking], [Start_Time], [End_Time], [Amount_Paid], [CustomerID]) VALUES (7, CAST(N'2021-12-19' AS Date), CAST(N'2021-12-30T14:00:00.000' AS DateTime), CAST(N'2021-12-30T19:00:00.000' AS DateTime), 500, 3)
GO
INSERT [dbo].[Computers] ([ComputerID], [CPU], [GPU], [RAM], [NetSpeed]) VALUES (1, N'i5-2500', N'GTX 750ti', 8, 2)
INSERT [dbo].[Computers] ([ComputerID], [CPU], [GPU], [RAM], [NetSpeed]) VALUES (2, N'i7-3770', N'RX580', 16, 8)
INSERT [dbo].[Computers] ([ComputerID], [CPU], [GPU], [RAM], [NetSpeed]) VALUES (3, N'i3-6300', N'RX560', 4, 10)
INSERT [dbo].[Computers] ([ComputerID], [CPU], [GPU], [RAM], [NetSpeed]) VALUES (4, N'Ryzen 5', N'RTX 3060', 16, 20)
INSERT [dbo].[Computers] ([ComputerID], [CPU], [GPU], [RAM], [NetSpeed]) VALUES (5, N'i5-4440', N'RX5500xt', 6, 5)
GO
INSERT [dbo].[Customers] ([CustomerID], [CustName], [PhoneNo], [AccountNo]) VALUES (1, N'Ali Tanveer', N'03420420421', 4)
INSERT [dbo].[Customers] ([CustomerID], [CustName], [PhoneNo], [AccountNo]) VALUES (2, N'Fawad J. Fateh', N'03215342786', 5)
INSERT [dbo].[Customers] ([CustomerID], [CustName], [PhoneNo], [AccountNo]) VALUES (3, N'Ammar Sabir', N'03123094199', 10)
GO
INSERT [dbo].[Events] ([EventID], [EventName], [Start_Time], [End_Time], [GameID], [Max_Participants], [Poster_link]) VALUES (2, N'Vanguard Elite', CAST(N'2021-12-15T20:00:00.000' AS DateTime), CAST(N'2021-12-16T07:00:00.000' AS DateTime), 2, 5, N'C:\Users\umera\Downloads\ismael-fofana-valorant-game-poster-design.jpg')
INSERT [dbo].[Events] ([EventID], [EventName], [Start_Time], [End_Time], [GameID], [Max_Participants], [Poster_link]) VALUES (3, N'Valorant Elite', CAST(N'2021-12-31T12:00:00.000' AS DateTime), CAST(N'2021-12-31T15:00:00.000' AS DateTime), 2, 5, N'C:\Users\umera\Downloads\ismael-fofana-valorant-game-poster-design.jpg')
INSERT [dbo].[Events] ([EventID], [EventName], [Start_Time], [End_Time], [GameID], [Max_Participants], [Poster_link]) VALUES (4, N'Fight Club', CAST(N'2021-12-30T14:00:00.000' AS DateTime), CAST(N'2021-12-30T19:00:00.000' AS DateTime), 1, 3, N'C:\Users\umera\Downloads\Tekken_6_Box_Art.jpg')
GO
INSERT [dbo].[Games] ([GameID], [GameName], [Genre], [GameDescription], [Popularity]) VALUES (1, N'Tekken 2', N'Fighting', N'Tekken 2 is the anticipated sequal to the beloved Tekken series.', 6)
INSERT [dbo].[Games] ([GameID], [GameName], [Genre], [GameDescription], [Popularity]) VALUES (2, N'Valorant', N'FPS', N'First person hero shooter. Be a hero, CSGO is Zero.', 10)
GO
INSERT [dbo].[Leaderboard] ([GameID], [CustomerID], [GameRank]) VALUES (1, 2, 1)
INSERT [dbo].[Leaderboard] ([GameID], [CustomerID], [GameRank]) VALUES (2, 1, 1)
GO
INSERT [dbo].[Seats] ([SeatNo], [CurrentStatus], [Premium_YES_NO], [ComputerID]) VALUES (1, N'Free', 0, 3)
INSERT [dbo].[Seats] ([SeatNo], [CurrentStatus], [Premium_YES_NO], [ComputerID]) VALUES (2, N'Free', 1, 1)
INSERT [dbo].[Seats] ([SeatNo], [CurrentStatus], [Premium_YES_NO], [ComputerID]) VALUES (3, N'Free', 0, 2)
INSERT [dbo].[Seats] ([SeatNo], [CurrentStatus], [Premium_YES_NO], [ComputerID]) VALUES (4, N'Free', 1, NULL)
INSERT [dbo].[Seats] ([SeatNo], [CurrentStatus], [Premium_YES_NO], [ComputerID]) VALUES (5, N'Free', 0, 1)
INSERT [dbo].[Seats] ([SeatNo], [CurrentStatus], [Premium_YES_NO], [ComputerID]) VALUES (6, N'Free', 1, 2)
INSERT [dbo].[Seats] ([SeatNo], [CurrentStatus], [Premium_YES_NO], [ComputerID]) VALUES (7, N'Free', 0, 3)
GO
INSERT [dbo].[Staff] ([StaffID], [StaffName], [PhoneNo], [Salary], [AccountNo], [Position], [Supervisor_ID]) VALUES (1, N'Saud', N'03696969690', 25000, 1, N'Chairman', NULL)
INSERT [dbo].[Staff] ([StaffID], [StaffName], [PhoneNo], [Salary], [AccountNo], [Position], [Supervisor_ID]) VALUES (2, N'Zaid', N'03232678212', 20000, 2, N'CEO', NULL)
INSERT [dbo].[Staff] ([StaffID], [StaffName], [PhoneNo], [Salary], [AccountNo], [Position], [Supervisor_ID]) VALUES (3, N'Umer Ahmed', N'03173352019', 20000, 3, N'CEO', NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Accounts__536C85E4BCD83D9F]    Script Date: 21/12/2021 12:12:45 ******/
ALTER TABLE [dbo].[Accounts] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Unique_Customer_Account]    Script Date: 21/12/2021 12:12:45 ******/
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [Unique_Customer_Account] UNIQUE NONCLUSTERED 
(
	[AccountNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UniqueGameName]    Script Date: 21/12/2021 12:12:45 ******/
ALTER TABLE [dbo].[Games] ADD  CONSTRAINT [UniqueGameName] UNIQUE NONCLUSTERED 
(
	[GameName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Unique_Staff_Account]    Script Date: 21/12/2021 12:12:45 ******/
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [Unique_Staff_Account] UNIQUE NONCLUSTERED 
(
	[AccountNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [Customers_FK] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [Customers_FK]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [Seats_FK] FOREIGN KEY([SeatNo])
REFERENCES [dbo].[Seats] ([SeatNo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [Seats_FK]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [Accounts_FK] FOREIGN KEY([AccountNo])
REFERENCES [dbo].[Accounts] ([AccountNo])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [Accounts_FK]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [Games_FK] FOREIGN KEY([GameID])
REFERENCES [dbo].[Games] ([GameID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [Games_FK]
GO
ALTER TABLE [dbo].[Leaderboard]  WITH CHECK ADD  CONSTRAINT [Customers_FK_Leaderboard] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Leaderboard] CHECK CONSTRAINT [Customers_FK_Leaderboard]
GO
ALTER TABLE [dbo].[Leaderboard]  WITH CHECK ADD  CONSTRAINT [Games_FK_Leaderboard] FOREIGN KEY([GameID])
REFERENCES [dbo].[Games] ([GameID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Leaderboard] CHECK CONSTRAINT [Games_FK_Leaderboard]
GO
ALTER TABLE [dbo].[Seats]  WITH CHECK ADD  CONSTRAINT [Computers_FK] FOREIGN KEY([ComputerID])
REFERENCES [dbo].[Computers] ([ComputerID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Seats] CHECK CONSTRAINT [Computers_FK]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [Accounts_FK_Staff] FOREIGN KEY([AccountNo])
REFERENCES [dbo].[Accounts] ([AccountNo])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [Accounts_FK_Staff]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [Supervisor_FK] FOREIGN KEY([Supervisor_ID])
REFERENCES [dbo].[Staff] ([StaffID])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [Supervisor_FK]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [PhoneNo_Format_Customer] CHECK  (([PhoneNo] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [PhoneNo_Format_Customer]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [PhoneNo_Format_Staff] CHECK  (([PhoneNo] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [PhoneNo_Format_Staff]
GO
/****** Object:  Trigger [dbo].[Account_delete_cascade_trigger]    Script Date: 21/12/2021 12:12:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   TRIGGER [dbo].[Account_delete_cascade_trigger] ON [dbo].[Accounts]
INSTEAD OF DELETE
AS
BEGIN
	DELETE FROM Customers
	WHERE AccountNo IN (SELECT AccountNo FROM Deleted);
	PRINT('Customer with accountNo deleted');

	DELETE FROM Staff
	WHERE AccountNo IN (SELECT AccountNo FROM Deleted);
	PRINT('Staff with accountNo deleted');

	DELETE FROM Accounts
	WHERE AccountNo IN (SELECT AccountNo FROM Deleted);
	PRINT('Deleted the row');
END;
GO
ALTER TABLE [dbo].[Accounts] ENABLE TRIGGER [Account_delete_cascade_trigger]
GO
/****** Object:  Trigger [dbo].[Supervisor_delete_trigger]    Script Date: 21/12/2021 12:12:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   TRIGGER [dbo].[Supervisor_delete_trigger] ON [dbo].[Staff]
INSTEAD OF DELETE
AS
DECLARE
	@deletedStaffID INT;
BEGIN
	UPDATE Staff 
	Set Supervisor_ID = NULL 
	WHERE Supervisor_ID IN (SELECT StaffID FROM Deleted);
	PRINT('Referenced SupervisorIDs set NULL');

	DELETE FROM Staff
	WHERE StaffID IN (SELECT StaffID FROM Deleted);
	PRINT('Deleted the row');
END;
GO
ALTER TABLE [dbo].[Staff] ENABLE TRIGGER [Supervisor_delete_trigger]
GO
USE [master]
GO
ALTER DATABASE [AzulaDB] SET  READ_WRITE 
GO
