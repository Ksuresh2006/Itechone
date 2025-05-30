USE [master]
GO
/****** Object:  Database [InventoryDB]    Script Date: 26-05-2025 02:36:38 ******/
CREATE DATABASE [InventoryDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'InventoryDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\InventoryDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'InventoryDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\InventoryDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [InventoryDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [InventoryDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [InventoryDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [InventoryDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [InventoryDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [InventoryDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [InventoryDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [InventoryDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [InventoryDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [InventoryDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [InventoryDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [InventoryDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [InventoryDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [InventoryDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [InventoryDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [InventoryDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [InventoryDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [InventoryDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [InventoryDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [InventoryDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [InventoryDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [InventoryDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [InventoryDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [InventoryDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [InventoryDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [InventoryDB] SET  MULTI_USER 
GO
ALTER DATABASE [InventoryDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [InventoryDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [InventoryDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [InventoryDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [InventoryDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [InventoryDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [InventoryDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [InventoryDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [InventoryDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 26-05-2025 02:36:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[inventory]    Script Date: 26-05-2025 02:36:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[inventory](
	[ProductId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[Productdescription] [nvarchar](max) NULL,
	[ProductUOM] [nvarchar](max) NOT NULL,
	[OpeningBalance] [float] NULL,
	[Quantity] [float] NULL,
	[TaxAmt] [float] NULL,
	[TaxPer] [float] NOT NULL,
	[ProductCatagory] [nvarchar](max) NULL,
	[ProductCode] [nvarchar](max) NULL,
	[HSNNo] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NULL,
	[UnitPrice] [float] NULL,
 CONSTRAINT [PK_inventory] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetAllProduct]    Script Date: 26-05-2025 02:36:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllProduct]
	
AS
BEGIN
	SET NOCOUNT ON;    
	SELECT * FROM  inventory WHERE IsActive=1
END
GO
/****** Object:  StoredProcedure [dbo].[sp_AddInventory]    Script Date: 26-05-2025 02:36:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_AddInventory]

@HSNNo varchar(100),
@ProductCode varchar(100),
@ProductName varchar(150),
@Productdescription varchar(150),
@UnitPrice float,
@ProductUOM varchar(50),
@OpeningBalance float,
@Quantity float,
@TaxPer float,
@ProductCatagory varchar(100),
@TaxAmt float,
  @IsActive bit,
          @CreatedBy varchar(100), 
          @CreatedDate datetime
         

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[inventory]
           ([ProductName]
           ,[Productdescription]
           ,[ProductUOM]
           ,[OpeningBalance]
           ,[Quantity]
           ,[TaxAmt]
           ,[TaxPer]
           ,[ProductCatagory]
           ,[ProductCode]
           ,[HSNNo]
           ,[IsActive]
           ,[CreatedBy]          
           ,[CreatedDate])
          
     VALUES
           (@ProductName
           ,@Productdescription,
           @ProductUOM,
           @OpeningBalance, 
          @Quantity, 
          @TaxAmt, 
          @TaxPer, 
          @ProductCatagory, 
          @ProductCode, 
          @HSNNo, 
          @IsActive,
          @CreatedBy,          
          @CreatedDate)
          
END

SELECT * FROM inventory
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteInventory]    Script Date: 26-05-2025 02:36:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_DeleteInventory]
	@IsActive bit,
	@ProductId int
AS
BEGIN

	SET NOCOUNT ON;

   UPDATE inventory
   SET IsActive=@IsActive
   WHERE ProductId=@ProductId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateInventory]    Script Date: 26-05-2025 02:36:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UpdateInventory]


@HSNNo varchar(100),
@ProductCode varchar(100),
@ProductName varchar(150),
@Productdescription varchar(150),
@UnitPrice float,
@ProductUOM varchar(50),
@OpeningBalance float,
@Quantity float,
@TaxPer float,
@ProductCatagory varchar(100),
@TaxAmt float,
 
          @ModifiedBy varchar(100), 
          @ModifiedDate datetime,
		  @ProductId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
UPDATE [dbo].[inventory]
   SET [ProductName] = @ProductName
      ,[Productdescription] = @Productdescription
      ,[ProductUOM] = @ProductUOM
      ,[OpeningBalance] = @OpeningBalance
      ,[Quantity] = @Quantity
      ,[TaxAmt] = @TaxAmt
      ,[TaxPer] = @TaxPer
      ,[ProductCatagory] = @ProductCatagory
      ,[ProductCode] = @ProductCode
      ,[HSNNo] = @HSNNo
      ,[ModifiedBy] = @ModifiedBy
      ,[ModifiedDate] =@ModifiedDate
      ,[UnitPrice] = @UnitPrice
 WHERE ProductId=@ProductId
END
GO
USE [master]
GO
ALTER DATABASE [InventoryDB] SET  READ_WRITE 
GO
