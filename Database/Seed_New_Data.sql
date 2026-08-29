-- ============================================================================
-- سكربت تفريغ وإعادة بناء البيانات العربية الشاملة (POS_DB)
-- يحتوي على أسماء أشخاص حقيقيين، شركات وموردين، أقسام، منتجات، مبيعات، مشتريات، وورديات
-- ============================================================================

USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'POS_DB')
BEGIN
    CREATE DATABASE POS_DB;
END
GO

USE POS_DB;
GO

-- 1. التأكد من وجود كافة الجداول
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Users] (
        [UserId]       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Username]     NVARCHAR(50)      NOT NULL UNIQUE,
        [PasswordHash] NVARCHAR(256)     NOT NULL,
        [FullName]     NVARCHAR(100)     NOT NULL,
        [Role]         NVARCHAR(50)      NOT NULL DEFAULT N'كاشير',
        [IsActive]     BIT               NOT NULL DEFAULT 1,
        [CreatedAt]    DATETIME          NOT NULL DEFAULT GETDATE(),
        [LastLogin]    DATETIME          NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Categories] (
        [CategoryId]   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [CategoryName] NVARCHAR(100)     NOT NULL UNIQUE
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Products] (
        [ProductId]     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Barcode]       NVARCHAR(50)      NOT NULL UNIQUE,
        [ProductName]   NVARCHAR(150)     NOT NULL,
        [CategoryId]    INT               NULL,
        [BuyPrice]      DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        [SellPrice]     DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        [StockQuantity] INT               NOT NULL DEFAULT 0,
        [MinStockAlert] INT               NOT NULL DEFAULT 5,
        [CreatedAt]     DATETIME          NOT NULL DEFAULT GETDATE(),
        CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CategoryId]) 
            REFERENCES [dbo].[Categories] ([CategoryId]) ON DELETE SET NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Suppliers]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Suppliers] (
        [SupplierId]   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [SupplierName] NVARCHAR(150)     NOT NULL,
        [Phone]        NVARCHAR(20)      NULL,
        [Address]      NVARCHAR(250)     NULL,
        [CreatedAt]    DATETIME          NOT NULL DEFAULT GETDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Purchases]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Purchases] (
        [PurchaseId]   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [SupplierId]   INT               NULL,
        [PurchaseDate] DATETIME          NOT NULL DEFAULT GETDATE(),
        [TotalAmount]  DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        [Notes]        NVARCHAR(MAX)     NULL,
        CONSTRAINT [FK_Purchases_Suppliers] FOREIGN KEY ([SupplierId]) 
            REFERENCES [dbo].[Suppliers] ([SupplierId]) ON DELETE SET NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PurchaseDetails]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[PurchaseDetails] (
        [DetailId]    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [PurchaseId]  INT               NOT NULL,
        [ProductId]   INT               NOT NULL,
        [Quantity]    INT               NOT NULL,
        [UnitPrice]   DECIMAL(18,2)     NOT NULL,
        [LineTotal]   DECIMAL(18,2)     NOT NULL,
        CONSTRAINT [FK_PurchaseDetails_Purchases] FOREIGN KEY ([PurchaseId]) 
            REFERENCES [dbo].[Purchases] ([PurchaseId]) ON DELETE CASCADE,
        CONSTRAINT [FK_PurchaseDetails_Products] FOREIGN KEY ([ProductId]) 
            REFERENCES [dbo].[Products] ([ProductId])
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sales]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Sales] (
        [SaleId]        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [UserId]        INT               NULL,
        [SaleDate]      DATETIME          NOT NULL DEFAULT GETDATE(),
        [TotalAmount]   DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        [Discount]      DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        [TaxAmount]     DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        [FinalAmount]   DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        [PaidAmount]    DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        [ChangeAmount]  DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        [PaymentMethod] NVARCHAR(50)      NOT NULL DEFAULT N'نقدي',
        [ReturnStatus]  NVARCHAR(50)      NOT NULL DEFAULT N'مكتملة',
        [TotalRefunded] DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        CONSTRAINT [FK_Sales_Users] FOREIGN KEY ([UserId]) 
            REFERENCES [dbo].[Users] ([UserId]) ON DELETE SET NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaleDetails]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SaleDetails] (
        [DetailId]         INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [SaleId]           INT               NOT NULL,
        [ProductId]        INT               NOT NULL,
        [Quantity]         INT               NOT NULL,
        [ReturnedQuantity] INT               NOT NULL DEFAULT 0,
        [UnitPrice]        DECIMAL(18,2)     NOT NULL,
        [LineTotal]        DECIMAL(18,2)     NOT NULL,
        CONSTRAINT [FK_SaleDetails_Sales] FOREIGN KEY ([SaleId]) 
            REFERENCES [dbo].[Sales] ([SaleId]) ON DELETE CASCADE,
        CONSTRAINT [FK_SaleDetails_Products] FOREIGN KEY ([ProductId]) 
            REFERENCES [dbo].[Products] ([ProductId])
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SalesReturns]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SalesReturns] (
        [ReturnId]          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [SaleId]            INT               NOT NULL,
        [UserId]            INT               NULL,
        [ReturnDate]        DATETIME          NOT NULL DEFAULT GETDATE(),
        [TotalRefundAmount] DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        [Reason]            NVARCHAR(250)     NULL,
        CONSTRAINT [FK_SalesReturns_Sales] FOREIGN KEY ([SaleId]) REFERENCES [dbo].[Sales] ([SaleId]) ON DELETE CASCADE,
        CONSTRAINT [FK_SalesReturns_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE SET NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SalesReturnDetails]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SalesReturnDetails] (
        [ReturnDetailId]   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [ReturnId]         INT               NOT NULL,
        [DetailId]         INT               NULL,
        [ProductId]        INT               NOT NULL,
        [ReturnedQuantity] INT               NOT NULL,
        [UnitPrice]        DECIMAL(18,2)     NOT NULL,
        [RefundAmount]     DECIMAL(18,2)     NOT NULL,
        CONSTRAINT [FK_SalesReturnDetails_SalesReturns] FOREIGN KEY ([ReturnId]) REFERENCES [dbo].[SalesReturns] ([ReturnId]) ON DELETE CASCADE,
        CONSTRAINT [FK_SalesReturnDetails_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId])
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemSettings]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SystemSettings] (
        [SettingKey]   NVARCHAR(50)  NOT NULL PRIMARY KEY,
        [SettingValue] NVARCHAR(MAX) NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Shifts]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Shifts] (
        [ShiftId]      INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [UserId]       INT               NOT NULL,
        [ClockInTime]  DATETIME          NOT NULL DEFAULT GETDATE(),
        [ClockOutTime] DATETIME          NULL,
        [Notes]        NVARCHAR(500)     NULL,
        CONSTRAINT [FK_Shifts_Users] FOREIGN KEY ([UserId]) 
            REFERENCES [dbo].[Users] ([UserId]) ON DELETE CASCADE
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__SchemaVersion]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[__SchemaVersion] (
        [VersionNumber] INT NOT NULL PRIMARY KEY,
        [AppliedAt]     DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- ============================================================================
-- 2. مسح وتنظيف كافة البيانات السابقة بالكامل وإعادة ضبط العدادات
-- ============================================================================
DELETE FROM [dbo].[SalesReturnDetails];
DELETE FROM [dbo].[SalesReturns];
DELETE FROM [dbo].[SaleDetails];
DELETE FROM [dbo].[Sales];
DELETE FROM [dbo].[PurchaseDetails];
DELETE FROM [dbo].[Purchases];
DELETE FROM [dbo].[Shifts];
DELETE FROM [dbo].[Products];
DELETE FROM [dbo].[Categories];
DELETE FROM [dbo].[Suppliers];
DELETE FROM [dbo].[Users];
DELETE FROM [dbo].[SystemSettings];
DELETE FROM [dbo].[__SchemaVersion];

DBCC CHECKIDENT ('[dbo].[Users]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Categories]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Products]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Suppliers]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Purchases]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[PurchaseDetails]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Sales]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[SaleDetails]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[SalesReturns]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[SalesReturnDetails]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Shifts]', RESEED, 0);

-- ============================================================================
-- 3. إدخال إعدادات النظام (SystemSettings)
-- ============================================================================
INSERT INTO [dbo].[SystemSettings] ([SettingKey], [SettingValue]) VALUES
(N'StoreName', N'هايبر ماركت البركة والصداقة'),
(N'StoreSubtitle', N'لتجارة المواد الغذائية والمستلزمات الاستهلاكية'),
(N'StorePhone', N'01023456789'),
(N'StoreAddress', N'شارع النصر، المعادي، القاهرة'),
(N'TaxNumber', N'452-981-630'),
(N'ReceiptHeader', N'أهلاً وسهلاً بكم في هايبر ماركت البركة'),
(N'ReceiptFooter', N'نشكركم لزيارتكم • البضاعة المباعة تستبدل وترد خلال 14 يوماً بالفاتورة • خدمة العملاء: 01023456789'),
(N'CurrencySymbol', N'ج.م'),
(N'VatRate', N'0.00'),
(N'DefaultMinStock', N'5'),
(N'EnablePrintPreview', N'True'),
(N'AutoPrintOnSale', N'False'),
(N'AllowNegativeStock', N'False');

-- ============================================================================
-- 4. إدخال المستخدمين (أشخاص حقيقيون وموظفون واقعيون)
-- الباسورد لـ admin / ahmed.shennawy / sara.aziz: admin123
-- الباسورد للكاشيرين: cashier123
-- ============================================================================
SET IDENTITY_INSERT [dbo].[Users] ON;

INSERT INTO [dbo].[Users] ([UserId], [Username], [PasswordHash], [FullName], [Role], [IsActive], [CreatedAt], [LastLogin]) VALUES
(1, N'admin',           N'240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', N'محمود مجدي إبراهيم',       N'Admin',   1, DATEADD(DAY, -30, GETDATE()), GETDATE()),
(2, N'ahmed.shennawy',  N'240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', N'أحمد عبد الرحمن الشناوي', N'مدير',    1, DATEADD(DAY, -28, GETDATE()), DATEADD(HOUR, -2, GETDATE())),
(3, N'sara.aziz',       N'240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', N'سارة محمد عبد العزيز',     N'Admin',   1, DATEADD(DAY, -25, GETDATE()), DATEADD(HOUR, -4, GETDATE())),
(4, N'cashier',         N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'كريم حسن الجوهري',        N'Cashier', 1, DATEADD(DAY, -20, GETDATE()), DATEADD(MINUTE, -30, GETDATE())),
(5, N'mostafa.hawary',  N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'مصطفى خالد الهواري',      N'كاشير',   1, DATEADD(DAY, -18, GETDATE()), DATEADD(HOUR, -1, GETDATE())),
(6, N'nour.saadany',    N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'نور الهدى محمود السعدني', N'كاشير',   1, DATEADD(DAY, -15, GETDATE()), DATEADD(DAY, -1, GETDATE())),
(7, N'tarek.naggar',    N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'طارق إبراهيم النجار',      N'كاشير',   1, DATEADD(DAY, -12, GETDATE()), DATEADD(DAY, -1, GETDATE())),
(8, N'yasmine.sherif',  N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'ياسمين علي الشريف',        N'كاشير',   1, DATEADD(DAY, -10, GETDATE()), DATEADD(DAY, -2, GETDATE()));

SET IDENTITY_INSERT [dbo].[Users] OFF;

-- ============================================================================
-- 5. إدخال الأقسام والتصنيفات (Categories)
-- ============================================================================
SET IDENTITY_INSERT [dbo].[Categories] ON;

INSERT INTO [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES
(1,  N'ألبان ومنتجات الأجبان'),
(2,  N'مياه ومشروبات وعصائر'),
(3,  N'سناكس ومقرمشات وشيبسي'),
(4,  N'بسكويت وشوكولاتة وحلويات'),
(5,  N'زيوت وسمن ومواد تموينية'),
(6,  N'مكرونة وأرز وبقوليات'),
(7,  N'معلبات وتونة وصلصات'),
(8,  N'منظفات وعناية منزلية'),
(9,  N'عناية شخصية ونظافة'),
(10, N'إلكترونيات وإكسسوارات');

SET IDENTITY_INSERT [dbo].[Categories] OFF;

-- ============================================================================
-- 6. إدخال الشركات والموردين الواقعيين (Suppliers)
-- ============================================================================
SET IDENTITY_INSERT [dbo].[Suppliers] ON;

INSERT INTO [dbo].[Suppliers] ([SupplierId], [SupplierName], [Phone], [Address], [CreatedAt]) VALUES
(1,  N'شركة جهينة للصناعات الغذائية',         N'01012345678', N'المنطقة الصناعية الثالثة، مدينة 6 أكتوبر، الجيزة', DATEADD(DAY, -30, GETDATE())),
(2,  N'شركة شيبسي للصناعات الغذائية (PepsiCo)', N'01123456789', N'المنطقة الصناعية الأولى، مدينة العبور، القليوبية', DATEADD(DAY, -30, GETDATE())),
(3,  N'شركة المراعي مصر للتجارة والتوزيع',    N'01234567890', N'التجمع الخامس، شارع التسعين، القاهرة الجديدة',    DATEADD(DAY, -30, GETDATE())),
(4,  N'شركة كوكاكولا هيلينك مصر للمشروبات',   N'01098765432', N'شارع النصر، مدينة نصر، القاهرة',               DATEADD(DAY, -30, GETDATE())),
(5,  N'شركة يونيليفر مشرق للصناعة والتجارة',  N'01187654321', N'مصر الجديدة، ميدان الحجاز، القاهرة',              DATEADD(DAY, -30, GETDATE())),
(6,  N'شركة إيديتا للصناعات الغذائية',        N'01276543210', N'المجمع الصناعي، المنطقة الثانية، 6 أكتوبر',      DATEADD(DAY, -30, GETDATE())),
(7,  N'شركة صافولا للأغذية (عافية والملكة)',   N'01144332211', N'المنطقة الصناعية الأولى، العاشر من رمضان',        DATEADD(DAY, -30, GETDATE())),
(8,  N'شركة هاينز مصر للصناعات الغذائية',     N'01223344556', N'المنطقة الحرة العامة، مدينة نصر، القاهرة',       DATEADD(DAY, -30, GETDATE())),
(9,  N'شركة بروكتر آند جامبل مصر (P&G)',     N'01033221100', N'القطامية، المنطقة الصناعية، القاهرة',             DATEADD(DAY, -30, GETDATE())),
(10, N'مجموعة العربي للتجارة والتوزيع',       N'01055566778', N'شارع عبد العزيز، الموسكي، العتبة، القاهرة',       DATEADD(DAY, -30, GETDATE()));

SET IDENTITY_INSERT [dbo].[Suppliers] OFF;

-- ============================================================================
-- 7. إدخال المنتجات الواقعية الكبيرة والمتنوعة (Products)
-- ============================================================================
SET IDENTITY_INSERT [dbo].[Products] ON;

INSERT INTO [dbo].[Products] ([ProductId], [Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert], [CreatedAt]) VALUES
-- ألبان وأجبان (Category 1)
(1,  N'622100100001', N'حليب جهينة كامل الدسم 1 لتر',       1, 35.00,  44.00, 85, 15, DATEADD(DAY, -30, GETDATE())),
(2,  N'622100100002', N'حليب جهينة خالي الدسم 1 لتر',       1, 35.00,  44.00, 45, 10, DATEADD(DAY, -30, GETDATE())),
(3,  N'622100100003', N'زبادي جهينة طبيعي 105 جم',         1, 6.50,   9.00,  120, 20, DATEADD(DAY, -30, GETDATE())),
(4,  N'622100100004', N'حليب المراعي كامل الدسم 1 لتر',     1, 36.00,  45.00, 60, 15, DATEADD(DAY, -30, GETDATE())),
(5,  N'622100100005', N'جبنة فيتا دومتي بلس 500 جم',       1, 32.00,  40.00, 50, 10, DATEADD(DAY, -30, GETDATE())),
(6,  N'622100100006', N'جبنة براميلي فلفل المراعي 500 جم', 1, 48.00,  62.00, 35, 8,  DATEADD(DAY, -30, GETDATE())),
(7,  N'622100100007', N'جبنة كيري مربعات 8 قطع',           1, 42.00,  55.00, 40, 10, DATEADD(DAY, -30, GETDATE())),
(8,  N'622100100008', N'جبنة رومي قديم ممتاز (ربع كيلو)',    1, 75.00,  98.00, 18, 5,  DATEADD(DAY, -30, GETDATE())),
(9,  N'622100100009', N'جبنة موزاريلا دومتي مبشورة 300 جم', 1, 45.00,  58.00, 25, 8,  DATEADD(DAY, -30, GETDATE())),

-- مياه ومشروبات وعصائر (Category 2)
(10, N'622100200001', N'مياه معدنية نستله 1.5 لتر',        2, 7.50,   12.00, 140, 25, DATEADD(DAY, -30, GETDATE())),
(11, N'622100200002', N'مياه معدنية نستله 600 مل',         2, 4.50,   7.00,  180, 30, DATEADD(DAY, -30, GETDATE())),
(12, N'622100200003', N'كانز كوكاكولا أحمر 330 مل',        2, 12.00,  18.00, 95,  20, DATEADD(DAY, -30, GETDATE())),
(13, N'622100200004', N'كانز كوكاكولا زيرو 330 مل',        2, 12.00,  18.00, 50,  15, DATEADD(DAY, -30, GETDATE())),
(14, N'622100200005', N'كانز سبرايت ليمون 330 مل',         2, 12.00,  18.00, 65,  15, DATEADD(DAY, -30, GETDATE())),
(15, N'622100200006', N'كانز شويبس جولد ليمون ونعناع',      2, 13.00,  19.00, 45,  10, DATEADD(DAY, -30, GETDATE())),
(16, N'622100200007', N'عصير جهينة مانجو بيور 1 لتر',       2, 28.00,  38.00, 40,  10, DATEADD(DAY, -30, GETDATE())),
(17, N'622100200008', N'عصير جهينة برتقال 1 لتر',          2, 26.00,  35.00, 35,  10, DATEADD(DAY, -30, GETDATE())),
(18, N'622100200009', N'عصير بيتي تروبيكانا تفاح 1 لتر',     2, 25.00,  34.00, 30,  10, DATEADD(DAY, -30, GETDATE())),
(19, N'622100200010', N'مشروب طاقة ريد بول كانز 250 مل',    2, 45.00,  60.00, 4,   10, DATEADD(DAY, -30, GETDATE())), -- تنبيه نقص مخزون

-- سناكس ومقرمشات وشيبسي (Category 3)
(20, N'622100300001', N'شيبسي جبنة متبلة عائلي',           3, 11.50,  15.00, 90,  20, DATEADD(DAY, -30, GETDATE())),
(21, N'622100300002', N'شيبسي شطة وليمون سوبر جامبو',      3, 14.50,  20.00, 75,  15, DATEADD(DAY, -30, GETDATE())),
(22, N'622100300003', N'شيبسي طماطم عائلي',                3, 11.50,  15.00, 60,  15, DATEADD(DAY, -30, GETDATE())),
(23, N'622100300004', N'دوريتوس جبنة ناتشو حار جامبو',     3, 15.00,  22.00, 55,  12, DATEADD(DAY, -30, GETDATE())),
(24, N'622100300005', N'شيتوس جبنة كرانشي وسط',           3, 7.50,   10.00, 80,  15, DATEADD(DAY, -30, GETDATE())),
(25, N'622100300006', N'بي bake سناكس بالزعتر والزيتون',    3, 9.00,   13.00, 40,  10, DATEADD(DAY, -30, GETDATE())),

-- بسكويت وشوكولاتة وحلويات (Category 4)
(26, N'622100400001', N'شوكولاتة كادبوري ديري ميلك 90 جم', 4, 38.00,  50.00, 65,  15, DATEADD(DAY, -30, GETDATE())),
(27, N'622100400002', N'شوكولاتة جلاكسي سادة 40 جم',        4, 20.00,  28.00, 80,  15, DATEADD(DAY, -30, GETDATE())),
(28, N'622100400003', N'ويفر كيت كات 4 أصابع',             4, 18.00,  25.00, 70,  15, DATEADD(DAY, -30, GETDATE())),
(29, N'622100400004', N'مولتو ماجنم شوكولاتة بندق',        4, 10.00,  15.00, 85,  20, DATEADD(DAY, -30, GETDATE())),
(30, N'622100400005', N'هوهوز كيك شوكولاتة كبير',          4, 6.00,   9.00,  110, 20, DATEADD(DAY, -30, GETDATE())),
(31, N'622100400006', N'بسكويت أوريو الأصلي 6 قطع',        4, 8.00,   12.00, 95,  15, DATEADD(DAY, -30, GETDATE())),
(32, N'622100400007', N'بسكويت لوكس شاي سادة باكيت',        4, 14.00,  20.00, 50,  10, DATEADD(DAY, -30, GETDATE())),

-- زيوت وسمن ومواد تموينية (Category 5)
(33, N'622100500001', N'زيت عباد الشمس عافية 800 مل',      5, 78.00,  95.00, 45,  10, DATEADD(DAY, -30, GETDATE())),
(34, N'622100500002', N'زيت ذرة كريستال نقي 800 مل',       5, 92.00,  115.00,35,  10, DATEADD(DAY, -30, GETDATE())),
(35, N'622100500003', N'سمن نباتي روابي بطعم القشطة 700 جم',5, 68.00,  85.00, 30,  8,  DATEADD(DAY, -30, GETDATE())),
(36, N'622100500004', N'سكر الأسرة نقي 1 كجم',             5, 30.00,  38.00, 100, 25, DATEADD(DAY, -30, GETDATE())),
(37, N'622100500005', N'شاي ليبتون ناعم أحمر 250 جم',       5, 62.00,  78.00, 45,  10, DATEADD(DAY, -30, GETDATE())),
(38, N'622100500006', N'شاي العروسة ناعم فاخر 250 جم',      5, 50.00,  65.00, 70,  15, DATEADD(DAY, -30, GETDATE())),
(39, N'622100500007', N'نسكافيه كلاسيك جولد برطمان 100 جم', 5, 110.00, 145.00,18,  5,  DATEADD(DAY, -30, GETDATE())),

-- مكرونة وأرز وبقوليات (Category 6)
(40, N'622100600001', N'أرز مصري المطبخ درجة أولى 1 كجم',   6, 30.00,  38.00, 80,  20, DATEADD(DAY, -30, GETDATE())),
(41, N'622100600002', N'أرز بسمتي هندي ذهبي 1 كجم',        6, 75.00,  95.00, 30,  8,  DATEADD(DAY, -30, GETDATE())),
(42, N'622100600003', N'مكرونة الملكة قلم 400 جم',         6, 11.50,  15.00, 110, 20, DATEADD(DAY, -30, GETDATE())),
(43, N'622100600004', N'مكرونة الملكة سباجيتي 400 جم',      6, 11.50,  15.00, 95,  20, DATEADD(DAY, -30, GETDATE())),
(44, N'622100600005', N'مكرونة ريجينا فرن 400 جم',          6, 19.00,  26.00, 45,  10, DATEADD(DAY, -30, GETDATE())),
(45, N'622100600006', N'شعرية الملكة سريعة الطهي 400 جم',   6, 11.50,  15.00, 70,  15, DATEADD(DAY, -30, GETDATE())),

-- معلبات وصلصات (Category 7)
(46, N'622100700001', N'صلصة طماطم هاينز برطمان 360 جم',    7, 24.00,  32.00, 55,  12, DATEADD(DAY, -30, GETDATE())),
(47, N'622100700002', N'تونة صن شاين قطع سهلة الفتح 185 جم', 7, 52.00,  68.00, 40,  10, DATEADD(DAY, -30, GETDATE())),
(48, N'622100700003', N'فول مدمس سادة حدائق كاليفورنيا 400 جم',7, 18.00, 25.00, 60, 15, DATEADD(DAY, -30, GETDATE())),
(49, N'622100700004', N'مايونيز هاينز ضغاط 280 جم',        7, 34.00,  45.00, 30,  8,  DATEADD(DAY, -30, GETDATE())),

-- منظفات وعناية منزلية (Category 8)
(50, N'622100800001', N'مسحوق غسيل أوتوماتيك أريال 2.5 كجم', 8, 175.00, 220.00,25,  6,  DATEADD(DAY, -30, GETDATE())),
(51, N'622100800002', N'مسحوق غسيل أوتوماتيك تايد 2.5 كجم',  8, 155.00, 195.00,20,  6,  DATEADD(DAY, -30, GETDATE())),
(52, N'622100800003', N'سائل غسيل الأطباق فيري ليمون 650 مل',8, 38.00,  48.00, 50,  10, DATEADD(DAY, -30, GETDATE())),
(53, N'622100800004', N'منظف ومطهر ديتول الأصلي 500 مل',    8, 65.00,  85.00, 22,  6,  DATEADD(DAY, -30, GETDATE())),
(54, N'622100800005', N'مناديل زينة سحب كلاسيك 550 منديل',  8, 22.00,  30.00, 65,  15, DATEADD(DAY, -30, GETDATE())),

-- عناية شخصية (Category 9)
(55, N'622100900001', N'شامبو هيد آند شولدرز كلاسيك 400 مل', 9, 78.00,  98.00, 25,  6,  DATEADD(DAY, -30, GETDATE())),
(56, N'622100900002', N'شامبو بانتين بديل الزيت 360 مل',     9, 72.00,  92.00, 20,  6,  DATEADD(DAY, -30, GETDATE())),
(57, N'622100900003', N'صابون دوف للجمال أبيض 135 جم',      9, 24.00,  32.00, 60,  12, DATEADD(DAY, -30, GETDATE())),
(58, N'622100900004', N'معجون أسنان سيجنال توتال 100 مل',   9, 28.00,  38.00, 45,  10, DATEADD(DAY, -30, GETDATE())),

-- إلكترونيات وإكسسوارات (Category 10)
(59, N'622101000001', N'كابل شحن سريع Type-C أنكر مضفر 1م', 10, 85.00, 130.00,18,  5,  DATEADD(DAY, -30, GETDATE())),
(60, N'622101000002', N'رأس شاحن سريع 20 وات PD تورنيدو',  10, 120.00, 180.00,3,   5,  DATEADD(DAY, -30, GETDATE())), -- تنبيه مخزون حرج
(61, N'622101000003', N'سماعة أذن سلكية سامسونج أصلية 3.5مم',10, 65.00, 95.00,  15,  5,  DATEADD(DAY, -30, GETDATE())),
(62, N'622101000004', N'حجارة بطارية قلم إنرجايزر AA (4 قطع)',10, 55.00, 75.00, 30,  8,  DATEADD(DAY, -30, GETDATE()));

SET IDENTITY_INSERT [dbo].[Products] OFF;

-- ============================================================================
-- 8. إدخال فواتير المشتريات (Purchases & PurchaseDetails)
-- ============================================================================
SET IDENTITY_INSERT [dbo].[Purchases] ON;

INSERT INTO [dbo].[Purchases] ([PurchaseId], [SupplierId], [PurchaseDate], [TotalAmount], [Notes]) VALUES
(1, 1, DATEADD(DAY, -20, GETDATE()), 7025.00, N'توريد دفعة منتجات ألبان وزبادي جهينة'),
(2, 2, DATEADD(DAY, -18, GETDATE()), 3215.00, N'توريد دفعة مقرمشات وشيبسي شيبسي بيبسيكو'),
(3, 4, DATEADD(DAY, -15, GETDATE()), 3550.00, N'توريد مشروبات غازية وكانز كوكاكولا وشويبس'),
(4, 7, DATEADD(DAY, -12, GETDATE()), 8940.00, N'توريد زيوت عافية وسمن ومكرونة الملكة'),
(5, 9, DATEADD(DAY, -8,  GETDATE()), 6820.00, N'توريد مساحيق أريال وتايد وشامبوهات P&G'),
(6, 10,DATEADD(DAY, -4,  GETDATE()), 3850.00, N'توريد كابلات وإكسسوارات وشواحن العربي');

SET IDENTITY_INSERT [dbo].[Purchases] OFF;

SET IDENTITY_INSERT [dbo].[PurchaseDetails] ON;

INSERT INTO [dbo].[PurchaseDetails] ([DetailId], [PurchaseId], [ProductId], [Quantity], [UnitPrice], [LineTotal]) VALUES
-- فاتورة 1 (جهينة)
(1, 1, 1, 100, 35.00, 3500.00),
(2, 1, 2, 50,  35.00, 1750.00),
(3, 1, 3, 150, 6.50,  975.00),
(4, 1, 16, 25, 28.00, 700.00),
(5, 1, 17, 4,  25.00, 100.00),

-- فاتورة 2 (شيبسي)
(6, 2, 20, 100, 11.50, 1150.00),
(7, 2, 21, 80,  14.50, 1160.00),
(8, 2, 23, 60,  15.00, 905.00),

-- فاتورة 3 (كوكاكولا)
(9,  3, 12, 120, 12.00, 1440.00),
(10, 3, 13, 60,  12.00, 720.00),
(11, 3, 14, 80,  12.00, 960.00),
(12, 3, 15, 33,  13.00, 430.00),

-- فاتورة 4 (صافولا والملكة)
(13, 4, 33, 50,  78.00, 3900.00),
(14, 4, 42, 120, 11.50, 1380.00),
(15, 4, 43, 100, 11.50, 1150.00),
(16, 4, 36, 83,  30.00, 2510.00),

-- فاتورة 5 (P&G)
(17, 5, 50, 25, 175.00, 4375.00),
(18, 5, 55, 25, 78.00,  1950.00),
(19, 5, 57, 20, 24.50,  495.00),

-- فاتورة 6 (العربي إلكترونيات)
(20, 6, 59, 20, 85.00,  1700.00),
(21, 6, 60, 5,  120.00, 600.00),
(22, 6, 61, 15, 65.00,  975.00),
(23, 6, 62, 10, 57.50,  575.00);

SET IDENTITY_INSERT [dbo].[PurchaseDetails] OFF;

-- ============================================================================
-- 9. إدخال فواتير مبيعات واقعية متنوعة (Sales & SaleDetails)
-- مرتبطة بمستخدمين وكاشيريين حقيقيين وطرق دفع مختلفة
-- ============================================================================
SET IDENTITY_INSERT [dbo].[Sales] ON;

INSERT INTO [dbo].[Sales] ([SaleId], [UserId], [SaleDate], [TotalAmount], [Discount], [TaxAmount], [FinalAmount], [PaidAmount], [ChangeAmount], [PaymentMethod], [ReturnStatus], [TotalRefunded]) VALUES
(1,  4, DATEADD(HOUR, -70, GETDATE()), 152.00, 0.00, 0.00, 152.00, 200.00, 48.00, N'نقدي',         N'مكتملة',     0.00),
(2,  4, DATEADD(HOUR, -65, GETDATE()), 89.00,  0.00, 0.00, 89.00,  100.00, 11.00, N'نقدي',         N'مكتملة',     0.00),
(3,  5, DATEADD(HOUR, -50, GETDATE()), 345.00, 15.00, 0.00, 330.00, 330.00, 0.00,  N'بطاقة ائتمان', N'مكتملة',     0.00),
(4,  5, DATEADD(HOUR, -45, GETDATE()), 210.00, 0.00, 0.00, 210.00, 250.00, 40.00, N'نقدي',         N'مكتملة',     0.00),
(5,  6, DATEADD(HOUR, -30, GETDATE()), 175.00, 5.00,  0.00, 170.00, 200.00, 30.00, N'نقدي',         N'مرتجع جزئي', 44.00),
(6,  6, DATEADD(HOUR, -25, GETDATE()), 480.00, 20.00, 0.00, 460.00, 500.00, 40.00, N'نقدي',         N'مكتملة',     0.00),
(7,  4, DATEADD(HOUR, -12, GETDATE()), 115.00, 0.00, 0.00, 115.00, 115.00, 0.00,  N'بطاقة ائتمان', N'مكتملة',     0.00),
(8,  5, DATEADD(HOUR, -8,  GETDATE()), 260.00, 10.00, 0.00, 250.00, 300.00, 50.00, N'نقدي',         N'مكتملة',     0.00),
(9,  7, DATEADD(HOUR, -4,  GETDATE()), 94.00,  0.00, 0.00, 94.00,  100.00, 6.00,  N'نقدي',         N'مكتملة',     0.00),
(10, 4, DATEADD(MINUTE, -40, GETDATE()), 185.00, 0.00, 0.00, 185.00, 200.00, 15.00, N'نقدي',       N'مكتملة',     0.00);

SET IDENTITY_INSERT [dbo].[Sales] OFF;

SET IDENTITY_INSERT [dbo].[SaleDetails] ON;

INSERT INTO [dbo].[SaleDetails] ([DetailId], [SaleId], [ProductId], [Quantity], [ReturnedQuantity], [UnitPrice], [LineTotal]) VALUES
-- فاتورة 1
(1,  1, 1,  2, 0, 44.00, 88.00),
(2,  1, 10, 2, 0, 12.00, 24.00),
(3,  1, 20, 2, 0, 15.00, 30.00),
(4,  1, 31, 1, 0, 10.00, 10.00),

-- فاتورة 2
(5,  2, 3,  3, 0, 9.00,  27.00),
(6,  2, 12, 2, 0, 18.00, 36.00),
(7,  2, 26, 1, 0, 26.00, 26.00),

-- فاتورة 3
(8,  3, 33, 2, 0, 95.00, 190.00),
(9,  3, 36, 2, 0, 38.00, 76.00),
(10, 3, 37, 1, 0, 79.00, 79.00),

-- فاتورة 4
(11, 4, 40, 2, 0, 38.00, 76.00),
(12, 4, 42, 4, 0, 15.00, 60.00),
(13, 4, 46, 2, 0, 32.00, 64.00),
(14, 4, 11, 1, 0, 10.00, 10.00),

-- فاتورة 5 (فيها مرتجع حبة لبن)
(15, 5, 1,  2, 1, 44.00, 88.00),
(16, 5, 21, 2, 0, 20.00, 40.00),
(17, 5, 27, 1, 0, 28.00, 28.00),
(18, 5, 12, 1, 0, 19.00, 19.00),

-- فاتورة 6
(19, 6, 50, 1, 0, 220.00, 220.00),
(20, 6, 52, 2, 0, 48.00,  96.00),
(21, 6, 55, 1, 0, 98.00,  98.00),
(22, 6, 57, 2, 0, 33.00,  66.00),

-- فاتورة 7
(23, 7, 34, 1, 0, 115.00, 115.00),

-- فاتورة 8
(24, 8, 59, 1, 0, 130.00, 130.00),
(25, 8, 62, 1, 0, 75.00,  75.00),
(26, 8, 28, 2, 0, 25.00,  50.00),
(27, 8, 10, 1, 0, 5.00,   5.00),

-- فاتورة 9
(28, 9, 5,  1, 0, 40.00, 40.00),
(29, 9, 29, 2, 0, 15.00, 30.00),
(30, 9, 14, 1, 0, 18.00, 18.00),
(31, 9, 11, 1, 0, 6.00,  6.00),

-- فاتورة 10
(32, 10, 4,  2, 0, 45.00, 90.00),
(33, 10, 16, 1, 0, 38.00, 38.00),
(34, 10, 23, 1, 0, 22.00, 22.00),
(35, 10, 30, 3, 0, 9.00,  27.00),
(36, 10, 11, 1, 0, 8.00,  8.00);

SET IDENTITY_INSERT [dbo].[SaleDetails] OFF;

-- ============================================================================
-- 10. إدخال مرتجع مبيعات واقعي (SalesReturns & SalesReturnDetails)
-- ============================================================================
SET IDENTITY_INSERT [dbo].[SalesReturns] ON;

INSERT INTO [dbo].[SalesReturns] ([ReturnId], [SaleId], [UserId], [ReturnDate], [TotalRefundAmount], [Reason]) VALUES
(1, 5, 6, DATEADD(HOUR, -28, GETDATE()), 44.00, N'تم إرجاع عبوة حليب واحدة لعدم الحاجة واسترداد قيمتها');

SET IDENTITY_INSERT [dbo].[SalesReturns] OFF;

SET IDENTITY_INSERT [dbo].[SalesReturnDetails] ON;

INSERT INTO [dbo].[SalesReturnDetails] ([ReturnDetailId], [ReturnId], [DetailId], [ProductId], [ReturnedQuantity], [UnitPrice], [RefundAmount]) VALUES
(1, 1, 15, 1, 1, 44.00, 44.00);

SET IDENTITY_INSERT [dbo].[SalesReturnDetails] OFF;

-- ============================================================================
-- 11. إدخال سجلات الورديات والدوام للموظفين الحقيقيين (Shifts)
-- ============================================================================
SET IDENTITY_INSERT [dbo].[Shifts] ON;

INSERT INTO [dbo].[Shifts] ([ShiftId], [UserId], [ClockInTime], [ClockOutTime], [Notes]) VALUES
-- ورديات سابقة مكتملة
(1, 4, DATEADD(DAY, -4, DATEADD(HOUR, 8, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY, -4, DATEADD(HOUR, 16, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), N'وردية صباحية منتظمة - مبيعات ممتازة'),
(2, 5, DATEADD(DAY, -4, DATEADD(HOUR, 16, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY, -4, DATEADD(HOUR, 23, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), N'وردية مسائية - تم جرد الخزينة بنجاح'),
(3, 6, DATEADD(DAY, -3, DATEADD(HOUR, 8, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY, -3, DATEADD(HOUR, 16, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), N'وردية صباحية صالة 1'),
(4, 7, DATEADD(DAY, -3, DATEADD(HOUR, 16, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY, -3, DATEADD(HOUR, 23, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), N'وردية مسائية مع استلام بضاعة'),
(5, 4, DATEADD(DAY, -2, DATEADD(HOUR, 8, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY, -2, DATEADD(HOUR, 16, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), N'وردية صباحية كاملة'),
(6, 5, DATEADD(DAY, -2, DATEADD(HOUR, 16, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY, -2, DATEADD(HOUR, 23, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), N'وردية مسائية'),
(7, 6, DATEADD(DAY, -1, DATEADD(HOUR, 8, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY, -1, DATEADD(HOUR, 16, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), N'وردية صباحية - تم التعامل مع مرتجع زبون'),
(8, 7, DATEADD(DAY, -1, DATEADD(HOUR, 16, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY, -1, DATEADD(HOUR, 23, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), N'وردية مسائية جرد وتصفية خزينة'),

-- ورديات اليوم الحالية (نشطة وغير منتهية أو مكتملة صباحاً)
(9,  4, DATEADD(HOUR, -4, GETDATE()), NULL, N'وردية الصالة الأولى المباشرة - قيد العمل الآن'),
(10, 2, DATEADD(HOUR, -5, GETDATE()), NULL, N'متابعة الإدارة والعمليات اليومية - دوام حالي');

SET IDENTITY_INSERT [dbo].[Shifts] OFF;

-- ============================================================================
-- 12. تسجيل رقم إصدار المخطط المحدث
-- ============================================================================
INSERT INTO [dbo].[__SchemaVersion] ([VersionNumber], [AppliedAt]) VALUES (3, GETDATE());

GO
