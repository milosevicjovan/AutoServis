USE [master]
GO
/****** Object:  Database [AutoServis]    Script Date: 01/12/2018 21:58:05 ******/
CREATE DATABASE [AutoServis]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AutoServis', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\AutoServis.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AutoServis_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\AutoServis_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AutoServis].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AutoServis] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AutoServis] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AutoServis] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AutoServis] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AutoServis] SET ARITHABORT OFF 
GO
ALTER DATABASE [AutoServis] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AutoServis] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AutoServis] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AutoServis] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AutoServis] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AutoServis] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AutoServis] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AutoServis] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AutoServis] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AutoServis] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AutoServis] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AutoServis] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AutoServis] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AutoServis] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AutoServis] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AutoServis] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AutoServis] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AutoServis] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AutoServis] SET  MULTI_USER 
GO
ALTER DATABASE [AutoServis] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AutoServis] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AutoServis] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AutoServis] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
USE [AutoServis]
GO
/****** Object:  Table [dbo].[tblDelovi]    Script Date: 01/12/2018 21:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDelovi](
	[RedniBroj] [int] NOT NULL,
	[Sifra] [nvarchar](20) NULL,
	[Naziv] [nvarchar](100) NOT NULL,
	[JedinicaMere] [nvarchar](20) NULL,
	[Kolicina] [numeric](10, 3) NOT NULL,
	[Cena] [numeric](10, 2) NOT NULL,
	[RadniNalogID] [int] NOT NULL,
 CONSTRAINT [PK_tblDelovi] PRIMARY KEY CLUSTERED 
(
	[RedniBroj] ASC,
	[RadniNalogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblFaktura]    Script Date: 01/12/2018 21:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFaktura](
	[FakturaID] [int] IDENTITY(1,1) NOT NULL,
	[Datum] [date] NOT NULL,
	[Valuta] [date] NOT NULL,
	[BrojFiskalnogRacuna] [int] NOT NULL,
	[RadniNalogID] [int] NOT NULL,
 CONSTRAINT [PK_tblFaktura] PRIMARY KEY CLUSTERED 
(
	[FakturaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblGarancija]    Script Date: 01/12/2018 21:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblGarancija](
	[GarancijaID] [int] IDENTITY(1,1) NOT NULL,
	[FakturaID] [int] NOT NULL,
	[Opis] [nvarchar](100) NULL,
	[RokVazenja] [int] NOT NULL,
 CONSTRAINT [PK_tblGarancija] PRIMARY KEY CLUSTERED 
(
	[GarancijaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblGorivo]    Script Date: 01/12/2018 21:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblGorivo](
	[VrstaGorivaID] [int] IDENTITY(1,1) NOT NULL,
	[VrstaGoriva] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblGorivo] PRIMARY KEY CLUSTERED 
(
	[VrstaGorivaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblIzvrseniRadovi]    Script Date: 01/12/2018 21:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblIzvrseniRadovi](
	[RedniBroj] [int] NOT NULL,
	[Naziv] [nvarchar](100) NOT NULL,
	[Kolicina] [numeric](10, 3) NOT NULL,
	[Cena] [numeric](10, 3) NOT NULL,
	[JedinicaMere] [nvarchar](20) NULL,
	[RadniNalogID] [int] NOT NULL,
 CONSTRAINT [PK_tblIzvrseniRadovi] PRIMARY KEY CLUSTERED 
(
	[RedniBroj] ASC,
	[RadniNalogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblMarka]    Script Date: 01/12/2018 21:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMarka](
	[MarkaID] [int] IDENTITY(1,1) NOT NULL,
	[NazivMarke] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblMarka] PRIMARY KEY CLUSTERED 
(
	[MarkaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblModel]    Script Date: 01/12/2018 21:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblModel](
	[ModelID] [int] IDENTITY(1,1) NOT NULL,
	[NazivModela] [nvarchar](50) NULL,
	[MarkaID] [int] NULL,
 CONSTRAINT [PK_tblModel] PRIMARY KEY CLUSTERED 
(
	[ModelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNaruceniRadovi]    Script Date: 01/12/2018 21:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNaruceniRadovi](
	[RedniBroj] [int] NOT NULL,
	[Opis] [nvarchar](100) NULL,
	[RadniNalogID] [int] NOT NULL,
 CONSTRAINT [PK_tblNaruceniRadovi] PRIMARY KEY CLUSTERED 
(
	[RedniBroj] ASC,
	[RadniNalogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblRadniNalog]    Script Date: 01/12/2018 21:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblRadniNalog](
	[RadniNalogID] [int] IDENTITY(1,1) NOT NULL,
	[DatumOtvaranja] [date] NOT NULL,
	[DatumZatvaranja] [date] NULL,
	[VoziloID] [int] NOT NULL,
	[ZaposleniID] [int] NOT NULL,
 CONSTRAINT [PK_tblRadniNalog] PRIMARY KEY CLUSTERED 
(
	[RadniNalogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTipVozila]    Script Date: 01/12/2018 21:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTipVozila](
	[TipVozilaID] [int] IDENTITY(1,1) NOT NULL,
	[TipVozila] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblTipVozila] PRIMARY KEY CLUSTERED 
(
	[TipVozilaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblVlasnik]    Script Date: 01/12/2018 21:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVlasnik](
	[VlasnikID] [int] IDENTITY(1,1) NOT NULL,
	[ImeVlasnika] [nvarchar](50) NOT NULL,
	[PrezimeVlasnika] [nvarchar](50) NULL,
	[KontaktVlasnika] [nvarchar](50) NOT NULL,
	[JMBGVlasnika] [nvarchar](13) NULL,
	[GradVlasnika] [nvarchar](50) NOT NULL,
	[BrojLKVlasnika] [nvarchar](10) NULL,
	[AdresaVlasnika] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblVlasnik] PRIMARY KEY CLUSTERED 
(
	[VlasnikID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblVozilo]    Script Date: 01/12/2018 21:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVozilo](
	[VoziloID] [int] IDENTITY(1,1) NOT NULL,
	[VrstaGorivaID] [int] NOT NULL,
	[SnagaMotora] [int] NULL,
	[BrojMotora] [nvarchar](50) NULL,
	[GodinaProizvodnje] [int] NULL,
	[ZapreminaMotora] [int] NULL,
	[RegOznaka] [nvarchar](10) NULL,
	[BrojSasije] [nvarchar](17) NULL,
	[VlasnikID] [int] NOT NULL,
	[TipVozilaID] [int] NOT NULL,
	[ModelID] [int] NOT NULL,
 CONSTRAINT [PK_tblVozilo] PRIMARY KEY CLUSTERED 
(
	[VoziloID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblZaposleni]    Script Date: 01/12/2018 21:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblZaposleni](
	[ZaposleniID] [int] IDENTITY(1,1) NOT NULL,
	[ImeZaposlenog] [nvarchar](50) NOT NULL,
	[PrezimeZaposlenog] [nvarchar](50) NOT NULL,
	[JMBGZaposlenog] [nvarchar](13) NOT NULL,
	[GradZaposlenog] [nvarchar](50) NOT NULL,
	[KorisnickoIme] [nvarchar](50) NOT NULL,
	[Lozinka] [nvarchar](50) NOT NULL,
	[BrojLKZaposlenog] [nvarchar](10) NOT NULL,
	[AdresaZaposlenog] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblZaposleni] PRIMARY KEY CLUSTERED 
(
	[ZaposleniID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblGorivo] ON 

INSERT [dbo].[tblGorivo] ([VrstaGorivaID], [VrstaGoriva]) VALUES (1, N'Benzin')
INSERT [dbo].[tblGorivo] ([VrstaGorivaID], [VrstaGoriva]) VALUES (2, N'TNG')
INSERT [dbo].[tblGorivo] ([VrstaGorivaID], [VrstaGoriva]) VALUES (3, N'Dizel')
SET IDENTITY_INSERT [dbo].[tblGorivo] OFF
SET IDENTITY_INSERT [dbo].[tblMarka] ON 

INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (1, N'Alfa Romeo')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (2, N'Audi')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (3, N'BMW')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (4, N'Bugatti')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (5, N'Chevrolet')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (6, N'Chrysler')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (7, N'Citroen')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (8, N'Daewoo')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (9, N'Daihatsu')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (10, N'Ferrari')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (11, N'FIAT')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (12, N'Ford')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (13, N'GMC')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (14, N'Honda')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (15, N'Hummer')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (16, N'Hyundai')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (17, N'Infiniti')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (18, N'Isuzu')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (19, N'Jaguar')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (20, N'Jeep')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (21, N'Kia')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (22, N'Lamborghini')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (23, N'Land Rover')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (24, N'Lexus')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (25, N'Lotus')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (26, N'Maserati')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (27, N'Maybach')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (28, N'Mazda')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (29, N'McLaren')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (30, N'Mercedes-Benz')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (31, N'MINI')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (32, N'Mitsubishi')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (33, N'Nissan')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (34, N'Peugeot')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (35, N'Porsche')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (36, N'Ram')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (37, N'Renault')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (38, N'Rolls-Royce')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (39, N'Saab')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (40, N'Smart')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (41, N'Subaru')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (42, N'Suzuki')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (43, N'Tesla')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (44, N'Toyota')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (45, N'Volkswagen')
INSERT [dbo].[tblMarka] ([MarkaID], [NazivMarke]) VALUES (46, N'Volvo')
SET IDENTITY_INSERT [dbo].[tblMarka] OFF
SET IDENTITY_INSERT [dbo].[tblModel] ON 

INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1, N'147', 1)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (2, N'156', 1)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (3, N'164', 1)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (4, N'Spider', 1)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (5, N'1007', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (6, N'106', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (7, N'107', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (8, N'108', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (9, N'128i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (10, N'135i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (11, N'1500', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (12, N'1500 Club Coupe', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (13, N'19', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (14, N'190E', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (15, N'1M', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (16, N'1-ton', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (17, N'2008', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (18, N'200SX', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (19, N'205', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (20, N'206', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (21, N'206 CC', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (22, N'206 SW', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (23, N'207', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (24, N'207 CC', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (25, N'208', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (26, N'228', 26)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (27, N'228i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (28, N'230i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (29, N'240', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (30, N'240SX', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (31, N'2500', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (32, N'2500 Club Coupe', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (33, N'3 Series', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (34, N'300', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (35, N'3000GT', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (36, N'3008', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (37, N'300C', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (38, N'300CE', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (39, N'300D', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (40, N'300E', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (41, N'300M', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (42, N'300SD', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (43, N'300SE', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (44, N'300SL', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (45, N'300TE', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (46, N'300ZX', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (47, N'306', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (48, N'307', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (49, N'308', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (50, N'318i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (51, N'318iC', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (52, N'318iS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (53, N'318ti', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (54, N'320i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (55, N'323', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (56, N'323Ci', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (57, N'323i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (58, N'323iC', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (59, N'323is', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (60, N'325', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (61, N'325/325e', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (62, N'325Ci', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (63, N'325Cic', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (64, N'325e', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (65, N'325i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (66, N'325i/325is', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (67, N'325iC', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (68, N'325iS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (69, N'325iT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (70, N'325ix', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (71, N'325xi', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (72, N'325xiT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (73, N'328Ci', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (74, N'328d', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (75, N'328i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (76, N'328iC', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (77, N'328iS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (78, N'328xi', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (79, N'330', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (80, N'330Ci', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (81, N'330Cic', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (82, N'330e', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (83, N'330i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (84, N'330xi', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (85, N'335', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (86, N'335d', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (87, N'335i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (88, N'335is', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (89, N'335xi', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (90, N'340i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (91, N'3500', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (92, N'3500 Club Coupe', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (93, N'350Z', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (94, N'350Z Roadster', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (95, N'370Z', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (96, N'4007', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (97, N'400E', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (98, N'400SE', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (99, N'400SEL', 30)
GO
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (100, N'406', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (101, N'407', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (102, N'428i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (103, N'430', 26)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (104, N'430 Scuderia', 10)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (105, N'430i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (106, N'435i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (107, N'438i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (108, N'440i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (109, N'458 Italia', 10)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (110, N'4Runner', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (111, N'4-Runner', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (112, N'5', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (113, N'5 Series', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (114, N'500', 11)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (115, N'5008', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (116, N'500E', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (117, N'500SEC', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (118, N'500SEL', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (119, N'500SL', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (120, N'508', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (121, N'524td', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (122, N'525', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (123, N'525i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (124, N'525iA', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (125, N'525iAT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (126, N'525iT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (127, N'525xi', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (128, N'528e', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (129, N'528i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (130, N'528xi', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (131, N'530', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (132, N'530e', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (133, N'530i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (134, N'530iA', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (135, N'530iT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (136, N'530xi', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (137, N'530xiT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (138, N'533i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (139, N'535d', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (140, N'535i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (141, N'535i/535is', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (142, N'535xi', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (143, N'540d', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (144, N'540i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (145, N'540iA', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (146, N'540iAT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (147, N'545', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (148, N'545i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (149, N'550', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (150, N'550i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (151, N'57', 27)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (152, N'57S', 27)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (153, N'599 GTB Fiorano', 10)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (154, N'6 Series', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (155, N'600SEC', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (156, N'600SEL', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (157, N'600SL', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (158, N'612 Scaglietti', 10)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (159, N'62', 27)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (160, N'626', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (161, N'633 csi', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (162, N'635CSi', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (163, N'640i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (164, N'640xi', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (165, N'645', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (166, N'645Ci', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (167, N'645Cic', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (168, N'645i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (169, N'650', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (170, N'650i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (171, N'650i / ALPINA B6', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (172, N'650i / B6', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (173, N'650xi', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (174, N'7 Series', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (175, N'730i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (176, N'733i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (177, N'735i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (178, N'735iL', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (179, N'740', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (180, N'740e', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (181, N'740i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (182, N'740iL', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (183, N'740Li', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (184, N'745', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (185, N'745e', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (186, N'745i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (187, N'745Li', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (188, N'750', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (189, N'750i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (190, N'750i / ALPINA B7', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (191, N'750i / B7', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (192, N'750iL', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (193, N'750Li', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (194, N'750Li / ALPINA B7', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (195, N'750Lxi', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (196, N'750Lxi / ALPINA B7', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (197, N'750xi', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (198, N'750xi / ALPINA B7', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (199, N'760', 3)
GO
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (200, N'760i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (201, N'760Li', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (202, N'8 Series', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (203, N'80', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (204, N'840Ci', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (205, N'840i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (206, N'850', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (207, N'850Ci', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (208, N'850CSi', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (209, N'850i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (210, N'86', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (211, N'9', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (212, N'90', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (213, N'900', 39)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (214, N'9000', 39)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (215, N'911', 35)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (216, N'928', 35)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (217, N'929', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (218, N'9-2X', 39)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (219, N'9-3', 39)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (220, N'940', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (221, N'944', 35)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (222, N'9-4X', 39)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (223, N'9-5', 39)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (224, N'960', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (225, N'968', 35)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (226, N'9-7X', 39)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (227, N'A1', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (228, N'A2', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (229, N'A3', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (230, N'A4', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (231, N'A4 allroad', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (232, N'A5', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (233, N'A6', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (234, N'A7', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (235, N'A8', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (236, N'Acadia', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (237, N'Accent', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (238, N'Accord', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (239, N'Accord Crosstour', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (240, N'Active E', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (241, N'ActiveHybrid 3', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (242, N'ActiveHybrid 5', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (243, N'ActiveHybrid 7', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (244, N'Aerio', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (245, N'Aerostar', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (246, N'Allroad', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (247, N'Alpina', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (248, N'Alpina B7', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (249, N'Altima', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (250, N'Altra', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (251, N'Altra-EV', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (252, N'Amanti', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (253, N'Amigo', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (254, N'APV', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (255, N'Armada', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (256, N'Arteon', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (257, N'Ascender', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (258, N'Aspen', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (259, N'Aspire', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (260, N'Astro', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (261, N'Atlas', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (262, N'Avalanche', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (263, N'Avalanche 1500', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (264, N'Avalanche 2500', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (265, N'Avalon', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (266, N'Avalon/Avalon Hybrid', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (267, N'Aventador', 22)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (268, N'Aveo', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (269, N'Axiom', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (270, N'Axxess', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (271, N'Azera', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (272, N'B2500', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (273, N'B6', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (274, N'B7', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (275, N'Beetle', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (276, N'Beretta', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (277, N'Blazer', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (278, N'Borrego', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (279, N'Boxster', 35)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (280, N'Bronco', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (281, N'Bronco II', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (282, N'B-Series', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (283, N'B-Series Plus', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (284, N'C 400 GT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (285, N'C 400 X', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (286, N'C 650 GT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (287, N'C 650 Sport', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (288, N'C Evolution', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (289, N'C/V', 36)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (290, N'C1', 7)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (291, N'C2', 7)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (292, N'C3', 7)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (293, N'C30', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (294, N'C4', 7)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (295, N'C5', 7)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (296, N'C6', 7)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (297, N'C600', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (298, N'C600 Sport', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (299, N'C650', 3)
GO
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (300, N'C650GT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (301, N'C70', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (302, N'C8', 7)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (303, N'Cabrio', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (304, N'Cabriolet', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (305, N'California', 10)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (306, N'Camaro', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (307, N'Camry', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (308, N'Camry Hybrid', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (309, N'Camry Solara', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (310, N'Canyon', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (311, N'Caprice', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (312, N'Caprice Classic', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (313, N'Captur', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (314, N'Carens', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (315, N'Cargo Van', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (316, N'Carrera GT', 35)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (317, N'Cavalier', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (318, N'Cayenne', 35)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (319, N'Cayman', 35)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (320, N'CC', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (321, N'C-Class', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (322, N'Celica', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (323, N'Charade', 9)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (324, N'Chariot', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (325, N'Cherokee', 20)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (326, N'C-HR', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (327, N'Cirrus', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (328, N'Civic', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (329, N'Civic GX', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (330, N'Civic Si', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (331, N'CL65 AMG', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (332, N'Classic', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (333, N'CL-Class', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (334, N'Clio', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (335, N'CLK-Class', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (336, N'CLS-Class', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (337, N'Club Wagon', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (338, N'Clubman', 31)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (339, N'C-MAX Hybrid', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (340, N'Cobalt', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (341, N'Cobalt SS', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (342, N'Colorado', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (343, N'Comanche', 20)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (344, N'Commander', 20)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (345, N'Compact Truck', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (346, N'Compass', 20)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (347, N'Concorde', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (348, N'Contour', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (349, N'Cooper', 31)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (350, N'Cooper Clubman', 31)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (351, N'Cooper Countryman', 31)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (352, N'Corolla', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (353, N'COROLLA iM', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (354, N'Corolla Matrix', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (355, N'Corona', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (356, N'Corrado', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (357, N'Corsica', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (358, N'Corvette', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (359, N'Countach', 22)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (360, N'Countryman', 31)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (361, N'Coupe', 26)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (362, N'Cressida', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (363, N'Crossfire', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (364, N'Crossfire Roadster', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (365, N'Crosstour', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (366, N'Crown Victoria', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (367, N'Cruze', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (368, N'CR-V', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (369, N'CR-Z', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (370, N'CT', 24)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (371, N'Cube', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (372, N'CX-5', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (373, N'CX-7', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (374, N'CX-9', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (375, N'Daewoo Lacetti', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (376, N'Daewoo Magnus', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (377, N'Dasher', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (378, N'Datsun/Nissan Z-car', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (379, N'Defender', 23)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (380, N'Defender 110', 23)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (381, N'Defender 90', 23)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (382, N'Defender Ice Edition', 23)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (383, N'del Sol', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (384, N'Diablo', 22)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (385, N'Diamante', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (386, N'Discovery', 23)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (387, N'Discovery Series II', 23)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (388, N'DS3', 7)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (389, N'DS5', 7)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (390, N'E150', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (391, N'E250', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (392, N'E350', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (393, N'E-350 Super Duty', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (394, N'E-350 Super Duty Van', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (395, N'Echo', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (396, N'E-Class', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (397, N'Eclipse', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (398, N'Econoline E150', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (399, N'Econoline E250', 12)
GO
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (400, N'Econoline E350', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (401, N'Edge', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (402, N'e-Golf', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (403, N'Elan', 25)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (404, N'Elantra', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (405, N'Element', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (406, N'Elise', 25)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (407, N'Endeavor', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (408, N'Entourage', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (409, N'Envoy', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (410, N'Envoy XL', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (411, N'Envoy XUV', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (412, N'Eos', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (413, N'Equator', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (414, N'Equinox', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (415, N'Equus', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (416, N'ES', 24)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (417, N'Escape', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (418, N'Escort', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (419, N'Escort ZX2', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (420, N'E-Series', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (421, N'Espace', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (422, N'Esprit', 25)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (423, N'Esteem', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (424, N'Euro Van', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (425, N'Eurovan', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (426, N'EV1', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (427, N'Evora', 25)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (428, N'EX', 17)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (429, N'Excel', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (430, N'Excursion', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (431, N'Exige', 25)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (432, N'Expedition', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (433, N'Expedition EL', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (434, N'Explorer', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (435, N'Explorer Sport', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (436, N'Explorer Sport Trac', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (437, N'Expo', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (438, N'Expo LRV', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (439, N'Express', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (440, N'Express 1500', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (441, N'Express 2500', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (442, N'Express 3500', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (443, N'F 750 GS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (444, N'F 800 GS Adventure', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (445, N'F 800 GT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (446, N'F 800 R', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (447, N'F 850 GS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (448, N'F 850 GS Adventure', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (449, N'F150', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (450, N'F250', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (451, N'F-250 Super Duty', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (452, N'F350', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (453, N'F-350 Super Duty', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (454, N'F430', 10)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (455, N'F430 Spider', 10)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (456, N'F450', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (457, N'F650', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (458, N'F650CS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (459, N'F650GS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (460, N'F650S', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (461, N'F700GS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (462, N'F800GS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (463, N'F800GS Adventure', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (464, N'F800GT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (465, N'F800R', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (466, N'F800S', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (467, N'F800ST', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (468, N'Familia', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (469, N'FCHV-adv', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (470, N'FCX Clarity', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (471, N'Festiva', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (472, N'FF', 10)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (473, N'Fiesta', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (474, N'Fifth Ave', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (475, N'Fit', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (476, N'Five Hundred', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (477, N'FJ Cruiser', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (478, N'Flex', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (479, N'Fluence', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (480, N'Focus', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (481, N'Focus ST', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (482, N'Forenza', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (483, N'Forester', 41)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (484, N'Forte', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (485, N'Fortwo', 40)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (486, N'Fox', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (487, N'Freelander', 23)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (488, N'Freestar', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (489, N'Freestyle', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (490, N'Frontier', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (491, N'F-Series', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (492, N'F-Series Super Duty', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (493, N'Fusion', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (494, N'FX', 17)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (495, N'G', 17)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (496, N'G 310 GS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (497, N'G 310 R', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (498, N'G25', 17)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (499, N'G35', 17)
GO
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (500, N'G37', 17)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (501, N'G450X', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (502, N'G55 AMG', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (503, N'G650 Xchallenge', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (504, N'G650 Xcountry', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (505, N'G650 Xmoto', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (506, N'G650GS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (507, N'G650GS Sertao', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (508, N'Galant', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (509, N'Gallardo', 22)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (510, N'G-Class', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (511, N'Genesis', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (512, N'Genesis Coupe', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (513, N'Ghost', 38)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (514, N'GL-Class', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (515, N'GLI', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (516, N'GLK-Class', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (517, N'Golf Alltrack', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (518, N'Golf GTI', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (519, N'Golf I', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (520, N'Golf II', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (521, N'Golf III', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (522, N'Golf IV', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (523, N'Golf R', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (524, N'Golf SportWagen', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (525, N'Golf V', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (526, N'Golf VI', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (527, N'Golf VII', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (528, N'Gran Sport', 26)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (529, N'Grand Cherokee', 20)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (530, N'Grand Espace', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (531, N'Grand Modus', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (532, N'Grand Scenic', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (533, N'Grand Vitara', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (534, N'Grand Voyager', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (535, N'GranSport', 26)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (536, N'GranTurismo', 26)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (537, N'GS', 24)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (538, N'G-Series 1500', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (539, N'G-Series 2500', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (540, N'G-Series 3500', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (541, N'G-Series G10', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (542, N'G-Series G20', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (543, N'G-Series G30', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (544, N'GT', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (545, N'GT500', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (546, N'GTI', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (547, N'GTO', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (548, N'GT-R', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (549, N'GX', 24)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (550, N'H1', 15)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (551, N'H2', 15)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (552, N'H2 SUT', 15)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (553, N'H2 SUV', 15)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (554, N'H3', 15)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (555, N'H3T', 15)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (556, N'HED-5', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (557, N'HHR', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (558, N'HHR Panel', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (559, N'Highlander', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (560, N'Highlander Hybrid', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (561, N'Hombre', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (562, N'Hombre Space', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (563, N'HP2', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (564, N'HP2 Megamoto', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (565, N'HP2 Sport', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (566, N'HP4', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (567, N'HS', 24)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (568, N'I', 17)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (569, N'i-280', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (570, N'i-290', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (571, N'i3', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (572, N'i-350', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (573, N'i-370', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (574, N'i8', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (575, N'i-MiEV', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (576, N'Impala', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (577, N'Impala SS', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (578, N'Imperial', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (579, N'Impreza', 41)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (580, N'Impreza WRX', 41)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (581, N'Impulse', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (582, N'Insight', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (583, N'IPL G', 17)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (584, N'Ipsum', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (585, N'IS', 24)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (586, N'IS F', 24)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (587, N'i-Series', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (588, N'IS-F', 24)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (589, N'J', 17)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (590, N'Jetta', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (591, N'Jetta III', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (592, N'Jetta SportWagen', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (593, N'Jetta Wagon', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (594, N'Jimmy', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (595, N'JUKE', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (596, N'Jumper', 7)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (597, N'Justy', 41)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (598, N'JX', 17)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (599, N'K 1600 B', 3)
GO
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (600, N'K 1600 GT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (601, N'K 1600 GTL', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (602, N'K02', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (603, N'K03', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (604, N'K08', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (605, N'K09', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (606, N'K1', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (607, N'K100', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (608, N'K100 LT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (609, N'K100 RS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (610, N'K100 RT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (611, N'K1100LT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (612, N'K1100RS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (613, N'K1200GT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (614, N'K1200LT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (615, N'K1200R', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (616, N'K1200RS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (617, N'K1200S', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (618, N'K1300GT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (619, N'K1300R', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (620, N'K1300S', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (621, N'K1600GT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (622, N'K1600GTL', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (623, N'K1600GTL Exc', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (624, N'K17', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (625, N'K18', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (626, N'K19', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (627, N'K21', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (628, N'K22', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (629, N'K23', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (630, N'K32', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (631, N'K33', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (632, N'K46', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (633, N'K47', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (634, N'K48', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (635, N'K49', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (636, N'K5 Blazer', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (637, N'K50', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (638, N'K51', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (639, N'K52', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (640, N'K53', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (641, N'K54', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (642, N'K61', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (643, N'K70', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (644, N'K71', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (645, N'K72', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (646, N'K73', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (647, N'K75', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (648, N'K75RT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (649, N'K75S', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (650, N'K80', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (651, N'K81', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (652, N'K82', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (653, N'Kadjar', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (654, N'Kangoo', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (655, N'Karif', 26)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (656, N'Kicks', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (657, N'Kizashi', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (658, N'Koleos', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (659, N'KOMBI', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (660, N'L300', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (661, N'L7', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (662, N'Laguna', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (663, N'Lancer', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (664, N'Lancer Evolution', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (665, N'Land Cruiser', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (666, N'Landaulet', 27)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (667, N'Lanos', 8)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (668, N'Leaf', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (669, N'LeBaron', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (670, N'Legacy', 41)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (671, N'Leganza', 8)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (672, N'LFA', 24)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (673, N'LHS', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (674, N'Liberty', 20)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (675, N'Lightning', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (676, N'Loyale', 41)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (677, N'LR2', 23)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (678, N'LR3', 23)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (679, N'LR4', 23)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (680, N'LS', 24)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (681, N'LS Hybrid', 24)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (682, N'LTD Crown Victoria', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (683, N'Lumina', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (684, N'Lumina APV', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (685, N'LX', 24)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (686, N'M', 17)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (687, N'M Roadster', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (688, N'M2', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (689, N'M235i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (690, N'M240i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (691, N'M3', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (692, N'M340i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (693, N'M3Ci', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (694, N'M3Cic', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (695, N'M4', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (696, N'M5', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (697, N'M550i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (698, N'M6', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (699, N'M760i', 3)
GO
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (700, N'M8', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (701, N'M850i', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (702, N'Malibu', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (703, N'Malibu Maxx', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (704, N'Matrix', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (705, N'Maxima', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (706, N'Mazda2', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (707, N'Mazda3', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (708, N'Mazda5', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (709, N'Mazda6', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (710, N'Mazda6 5-Door', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (711, N'Mazda6 Sport', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (712, N'Mazdaspeed 3', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (713, N'Mazdaspeed6', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (714, N'M-Class', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (715, N'Megane', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (716, N'Mentor', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (717, N'Metro', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (718, N'Miata MX-5', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (719, N'Micra', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (720, N'Mighty Max', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (721, N'Mighty Max Macro', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (722, N'Millenia', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (723, N'MINI', 31)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (724, N'Mirage', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (725, N'Mirai', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (726, N'Model S', 43)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (727, N'Modus', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (728, N'Mohave/Borrego', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (729, N'Monte Carlo', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (730, N'Montero', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (731, N'Montero Sport', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (732, N'MP4-12C', 29)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (733, N'MPV', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (734, N'MR2', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (735, N'MULTI-VAN', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (736, N'Murano', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (737, N'Murciélago', 22)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (738, N'Murciélago LP640', 22)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (739, N'Mustang', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (740, N'MX-3', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (741, N'MX-5', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (742, N'MX-6', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (743, N'Navajo', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (744, N'New Yorker', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (745, N'Nubira', 8)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (746, N'Nuova 500', 11)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (747, N'NV', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (748, N'NV1500', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (749, N'NV200', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (750, N'NV2500', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (751, N'NV3500', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (752, N'NX', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (753, N'NX/Sentra', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (754, N'Oasis', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (755, N'Odyssey', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (756, N'Optima', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (757, N'Outback', 41)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (758, N'Outback Sport', 41)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (759, N'Outlander', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (760, N'Outlander Sport', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (761, N'Pacifica', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (762, N'Pajero', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (763, N'Panamera', 35)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (764, N'Partner', 34)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (765, N'Paseo', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (766, N'Passat', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (767, N'Passport', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (768, N'Pathfinder', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (769, N'Pathfinder Armada', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (770, N'Patriot', 20)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (771, N'Phaeton', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (772, N'Phantom', 38)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (773, N'Picasso', 7)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (774, N'Pickup', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (775, N'Pick-Up', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (776, N'Pilot', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (777, N'Precis', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (778, N'Prelude', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (779, N'Previa', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (780, N'Prius', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (781, N'Prius c', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (782, N'Prius Plug-in', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (783, N'Prius Plug-in Hybrid', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (784, N'Prius Prime', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (785, N'Prius v', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (786, N'Prizm', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (787, N'Probe', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (788, N'Protege', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (789, N'Protege5', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (790, N'Prowler', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (791, N'PT Cruiser', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (792, N'PULSAR', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (793, N'Q', 17)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (794, N'Q3', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (795, N'Q5', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (796, N'Q7', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (797, N'Q8', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (798, N'Quantum', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (799, N'Quattro', 2)
GO
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (800, N'Quattroporte', 26)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (801, N'Quest', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (802, N'QX', 17)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (803, N'QX56', 17)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (804, N'R32', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (805, N'R65', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (806, N'R65 LS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (807, N'R8', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (808, N'R80', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (809, N'R80 GS', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (810, N'R80 RT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (811, N'R80 ST', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (812, N'R850R', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (813, N'R900RT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (814, N'Rabbit', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (815, N'Raider', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (816, N'Rally Wagon 1500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (817, N'Rally Wagon 2500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (818, N'Rally Wagon 3500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (819, N'Rally Wagon G2500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (820, N'Rally Wagon G3500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (821, N'Range Rover', 23)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (822, N'Range Rover Classic', 23)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (823, N'Range Rover Evoque', 23)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (824, N'Range Rover Sport', 23)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (825, N'Ranger', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (826, N'RAV4', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (827, N'R-Class', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (828, N'Reno', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (829, N'Reventón', 22)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (830, N'Ridgeline', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (831, N'Rio', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (832, N'Rio5', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (833, N'RnineT', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (834, N'Roadster', 43)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (835, N'Rocky', 9)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (836, N'Rodeo', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (837, N'Rodeo Sport', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (838, N'Rogue', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (839, N'Rogue Select', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (840, N'Rogue Sports', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (841, N'Rondo', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (842, N'Routan', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (843, N'RS3', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (844, N'RS4', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (845, N'RS5', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (846, N'RS6', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (847, N'RS7', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (848, N'RVR', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (849, N'RX', 24)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (850, N'RX Hybrid', 24)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (851, N'RX-7', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (852, N'RX-8', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (853, N'S 1000 R', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (854, N'S 1000 RR', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (855, N'S 1000 XR', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (856, N'S10', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (857, N'S10 Blazer', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (858, N'S1000R', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (859, N'S1000RR', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (860, N'S1000XR', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (861, N'S2000', 14)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (862, N'S3', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (863, N'S4', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (864, N'S40', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (865, N'S5', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (866, N'S6', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (867, N'S60', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (868, N'S7', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (869, N'S70', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (870, N'S8', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (871, N'S80', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (872, N'S90', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (873, N'Safari', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (874, N'Samurai', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (875, N'Santa Fe', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (876, N'Savana', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (877, N'Savana 1500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (878, N'Savana 2500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (879, N'Savana 3500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (880, N'Savana Cargo Van', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (881, N'Saxo', 7)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (882, N'SC', 24)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (883, N'Scenic', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (884, N'Scion FR-S', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (885, N'Scion iA', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (886, N'Scion iM', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (887, N'Scion iQ', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (888, N'SCION tC', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (889, N'SCION xA', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (890, N'SCION xB', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (891, N'SCION xD', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (892, N'Scirocco', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (893, N'S-Class', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (894, N'Scoupe', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (895, N'Sebring', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (896, N'Sedona', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (897, N'Sentra', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (898, N'Sentra Classic', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (899, N'Sephia', 21)
GO
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (900, N'Sequoia', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (901, N'Sidekick', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (902, N'Sienna', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (903, N'Sierra', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (904, N'Sierra 1500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (905, N'Sierra 2500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (906, N'Sierra 2500HD', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (907, N'Sierra 3500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (908, N'Sierra 3500HD', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (909, N'Sierra Denali', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (910, N'Sierra Hybrid', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (911, N'Sigma', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (912, N'Silverado', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (913, N'Silverado 1500', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (914, N'Silverado 2500', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (915, N'Silverado 3500', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (916, N'Silverado 3500HD', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (917, N'Silverado Hybrid', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (918, N'SJ', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (919, N'SL65 AMG', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (920, N'SL-Class', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (921, N'SLK55 AMG', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (922, N'SLK-Class', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (923, N'SLR McLaren', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (924, N'SLS AMG', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (925, N'SLS-Class', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (926, N'Solara', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (927, N'Sonata', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (928, N'Sonic', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (929, N'Sonoma', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (930, N'Sonoma Club', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (931, N'Sonoma Club Coupe', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (932, N'Sorento', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (933, N'Soul', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (934, N'Space', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (935, N'Spectra', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (936, N'Spectra5', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (937, N'Sportage', 21)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (938, N'Sportvan G10', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (939, N'Sportvan G20', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (940, N'Sportvan G30', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (941, N'Sprinter', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (942, N'Sprinter 2500', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (943, N'Sprinter 3500', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (944, N'Spyder', 26)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (945, N'SQ5', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (946, N'SSR', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (947, N'Stanza', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (948, N'Stanza Wagon', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (949, N'Starlet', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (950, N'Sterling', 23)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (951, N'Stylus', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (952, N'S-Type', 19)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (953, N'Suburban', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (954, N'Suburban 1500', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (955, N'Suburban 2500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (956, N'Supra', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (957, N'SVX', 41)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (958, N'Swift', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (959, N'SX4', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (960, N'T100', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (961, N'T100 Xtra', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (962, N'Tacoma', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (963, N'Tacoma Xtra', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (964, N'Tahoe', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (965, N'Talisman', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (966, N'Taurus', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (967, N'Taurus X', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (968, N'Tempo', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (969, N'Tercel', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (970, N'Terrain', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (971, N'TH!NK', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (972, N'Thunderbird', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (973, N'Tiburon', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (974, N'Tiguan', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (975, N'Tiguan Limited', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (976, N'Titan', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (977, N'Touareg', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (978, N'Touareg 2', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (979, N'Town & Country', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (980, N'Toyota - Supra', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (981, N'Tracker', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (982, N'TrailBlazer', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (983, N'Transit Connect', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (984, N'Traverse', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (985, N'Tribeca', 41)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (986, N'Tribute', 28)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (987, N'Trooper', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (988, N'Truck', 32)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (989, N'TT', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (990, N'TT RS', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (991, N'TTS', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (992, N'Tucson', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (993, N'Tundra', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (994, N'TundraMax', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (995, N'Twingo', 37)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (996, N'Type 2', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (997, N'Uplander', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (998, N'V40', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (999, N'V50', 46)
GO
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1000, N'V70', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1001, N'V8', 2)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1002, N'V90', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1003, N'Van', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1004, N'Vanagon', 45)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1005, N'Vandura 1500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1006, N'Vandura 2500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1007, N'Vandura 3500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1008, N'Vandura G1500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1009, N'Vandura G2500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1010, N'Vandura G3500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1011, N'VehiCROSS', 18)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1012, N'Veloster', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1013, N'Venture', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1014, N'Venza', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1015, N'Veracruz', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1016, N'Verona', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1017, N'Versa', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1018, N'Versa Note', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1019, N'Veyron', 4)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1020, N'Vitara', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1021, N'Volt', 5)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1022, N'Voyager', 6)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1023, N'W201', 30)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1024, N'Windstar', 12)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1025, N'Wrangler', 20)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1026, N'X1', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1027, N'X2', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1028, N'X3', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1029, N'X4', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1030, N'X5', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1031, N'X5 M', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1032, N'X6', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1033, N'X6 M', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1034, N'X7', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1035, N'X-90', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1036, N'Xantia', 7)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1037, N'XC60', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1038, N'XC70', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1039, N'XC90', 46)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1040, N'XF', 19)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1041, N'XG300', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1042, N'XG350', 16)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1043, N'XJ', 19)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1044, N'XJ Series', 19)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1045, N'XK', 19)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1046, N'XK Series', 19)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1047, N'XL7', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1048, N'XL-7', 42)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1049, N'Xsara', 7)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1050, N'Xsara Picasso', 7)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1051, N'XT', 41)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1052, N'Xterra', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1053, N'Xtra', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1054, N'X-Trail', 33)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1055, N'X-Type', 19)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1056, N'Yaris', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1057, N'Yaris iA', 44)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1058, N'Yukon', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1059, N'Yukon Denali', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1060, N'Yukon XL', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1061, N'Yukon XL 1500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1062, N'Yukon XL 2500', 13)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1063, N'Z3', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1064, N'Z4', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1065, N'Z4 M', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1066, N'Z4 M Roadster', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1067, N'Z8', 3)
INSERT [dbo].[tblModel] ([ModelID], [NazivModela], [MarkaID]) VALUES (1068, N'ZX2', 12)
SET IDENTITY_INSERT [dbo].[tblModel] OFF
SET IDENTITY_INSERT [dbo].[tblRadniNalog] ON 

INSERT [dbo].[tblRadniNalog] ([RadniNalogID], [DatumOtvaranja], [DatumZatvaranja], [VoziloID], [ZaposleniID]) VALUES (31, CAST(N'2018-12-01' AS Date), CAST(N'2018-12-01' AS Date), 1, 1)
SET IDENTITY_INSERT [dbo].[tblRadniNalog] OFF
SET IDENTITY_INSERT [dbo].[tblTipVozila] ON 

INSERT [dbo].[tblTipVozila] ([TipVozilaID], [TipVozila]) VALUES (1, N'Putnicko vozilo')
INSERT [dbo].[tblTipVozila] ([TipVozilaID], [TipVozila]) VALUES (2, N'Teretno vozilo')
INSERT [dbo].[tblTipVozila] ([TipVozilaID], [TipVozila]) VALUES (3, N'Kombi')
SET IDENTITY_INSERT [dbo].[tblTipVozila] OFF
SET IDENTITY_INSERT [dbo].[tblVlasnik] ON 

INSERT [dbo].[tblVlasnik] ([VlasnikID], [ImeVlasnika], [PrezimeVlasnika], [KontaktVlasnika], [JMBGVlasnika], [GradVlasnika], [BrojLKVlasnika], [AdresaVlasnika]) VALUES (1, N'Jovan', N'Milosevic', N'021123456', N'2403999730035', N'Novi Sad', N'123456', N'Adresa 1')
SET IDENTITY_INSERT [dbo].[tblVlasnik] OFF
SET IDENTITY_INSERT [dbo].[tblVozilo] ON 

INSERT [dbo].[tblVozilo] ([VoziloID], [VrstaGorivaID], [SnagaMotora], [BrojMotora], [GodinaProizvodnje], [ZapreminaMotora], [RegOznaka], [BrojSasije], [VlasnikID], [TipVozilaID], [ModelID]) VALUES (1, 1, 180, N'DWAG3432', 2018, 2000, N'BG000JK', N'DWAFEDFGTR4567891', 1, 1, 235)
SET IDENTITY_INSERT [dbo].[tblVozilo] OFF
SET IDENTITY_INSERT [dbo].[tblZaposleni] ON 

INSERT [dbo].[tblZaposleni] ([ZaposleniID], [ImeZaposlenog], [PrezimeZaposlenog], [JMBGZaposlenog], [GradZaposlenog], [KorisnickoIme], [Lozinka], [BrojLKZaposlenog], [AdresaZaposlenog]) VALUES (1, N'Jovan', N'Milosevic', N'2403999730035', N'Novi Sad', N'username_example', N'password_example', N'123456', N'Adresa 1')
SET IDENTITY_INSERT [dbo].[tblZaposleni] OFF
ALTER TABLE [dbo].[tblDelovi]  WITH CHECK ADD  CONSTRAINT [FK_tblDelovi_tblRadniNalog] FOREIGN KEY([RadniNalogID])
REFERENCES [dbo].[tblRadniNalog] ([RadniNalogID])
GO
ALTER TABLE [dbo].[tblDelovi] CHECK CONSTRAINT [FK_tblDelovi_tblRadniNalog]
GO
ALTER TABLE [dbo].[tblFaktura]  WITH CHECK ADD  CONSTRAINT [FK_tblFaktura_tblRadniNalog] FOREIGN KEY([RadniNalogID])
REFERENCES [dbo].[tblRadniNalog] ([RadniNalogID])
GO
ALTER TABLE [dbo].[tblFaktura] CHECK CONSTRAINT [FK_tblFaktura_tblRadniNalog]
GO
ALTER TABLE [dbo].[tblGarancija]  WITH CHECK ADD  CONSTRAINT [FK_tblGarancija_tblFaktura] FOREIGN KEY([FakturaID])
REFERENCES [dbo].[tblFaktura] ([FakturaID])
GO
ALTER TABLE [dbo].[tblGarancija] CHECK CONSTRAINT [FK_tblGarancija_tblFaktura]
GO
ALTER TABLE [dbo].[tblIzvrseniRadovi]  WITH CHECK ADD  CONSTRAINT [FK_tblIzvrseniRadovi_tblIzvrseniRadovi] FOREIGN KEY([RadniNalogID])
REFERENCES [dbo].[tblRadniNalog] ([RadniNalogID])
GO
ALTER TABLE [dbo].[tblIzvrseniRadovi] CHECK CONSTRAINT [FK_tblIzvrseniRadovi_tblIzvrseniRadovi]
GO
ALTER TABLE [dbo].[tblModel]  WITH CHECK ADD  CONSTRAINT [FK_tblModel_tblMarka] FOREIGN KEY([MarkaID])
REFERENCES [dbo].[tblMarka] ([MarkaID])
GO
ALTER TABLE [dbo].[tblModel] CHECK CONSTRAINT [FK_tblModel_tblMarka]
GO
ALTER TABLE [dbo].[tblNaruceniRadovi]  WITH CHECK ADD  CONSTRAINT [FK_tblNaruceniRadovi_tblRadniNalog] FOREIGN KEY([RadniNalogID])
REFERENCES [dbo].[tblRadniNalog] ([RadniNalogID])
GO
ALTER TABLE [dbo].[tblNaruceniRadovi] CHECK CONSTRAINT [FK_tblNaruceniRadovi_tblRadniNalog]
GO
ALTER TABLE [dbo].[tblRadniNalog]  WITH CHECK ADD  CONSTRAINT [FK_tblRadniNalog_tblVozilo] FOREIGN KEY([VoziloID])
REFERENCES [dbo].[tblVozilo] ([VoziloID])
GO
ALTER TABLE [dbo].[tblRadniNalog] CHECK CONSTRAINT [FK_tblRadniNalog_tblVozilo]
GO
ALTER TABLE [dbo].[tblRadniNalog]  WITH CHECK ADD  CONSTRAINT [FK_tblRadniNalog_tblZaposleni] FOREIGN KEY([ZaposleniID])
REFERENCES [dbo].[tblZaposleni] ([ZaposleniID])
GO
ALTER TABLE [dbo].[tblRadniNalog] CHECK CONSTRAINT [FK_tblRadniNalog_tblZaposleni]
GO
ALTER TABLE [dbo].[tblVozilo]  WITH CHECK ADD  CONSTRAINT [FK_tblVozilo_tblGorivo] FOREIGN KEY([VrstaGorivaID])
REFERENCES [dbo].[tblGorivo] ([VrstaGorivaID])
GO
ALTER TABLE [dbo].[tblVozilo] CHECK CONSTRAINT [FK_tblVozilo_tblGorivo]
GO
ALTER TABLE [dbo].[tblVozilo]  WITH CHECK ADD  CONSTRAINT [FK_tblVozilo_tblModel] FOREIGN KEY([ModelID])
REFERENCES [dbo].[tblModel] ([ModelID])
GO
ALTER TABLE [dbo].[tblVozilo] CHECK CONSTRAINT [FK_tblVozilo_tblModel]
GO
ALTER TABLE [dbo].[tblVozilo]  WITH CHECK ADD  CONSTRAINT [FK_tblVozilo_tblTipVozila] FOREIGN KEY([TipVozilaID])
REFERENCES [dbo].[tblTipVozila] ([TipVozilaID])
GO
ALTER TABLE [dbo].[tblVozilo] CHECK CONSTRAINT [FK_tblVozilo_tblTipVozila]
GO
ALTER TABLE [dbo].[tblVozilo]  WITH CHECK ADD  CONSTRAINT [FK_tblVozilo_tblVlasnik] FOREIGN KEY([VlasnikID])
REFERENCES [dbo].[tblVlasnik] ([VlasnikID])
GO
ALTER TABLE [dbo].[tblVozilo] CHECK CONSTRAINT [FK_tblVozilo_tblVlasnik]
GO
USE [master]
GO
ALTER DATABASE [AutoServis] SET  READ_WRITE 
GO
