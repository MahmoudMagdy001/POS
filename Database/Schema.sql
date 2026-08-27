-- ============================================================================
-- قاعدة بيانات نظام نقاط البيع وإدارة المخزون (POS_DB)
-- Microsoft SQL Server / LocalDB
-- ============================================================================

-- 1. إنشاء قاعدة البيانات إن لم تكن موجودة
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'POS_DB')
BEGIN
    CREATE DATABASE POS_DB;
END
GO

USE POS_DB;
GO

-- ============================================================================
-- 2. جدول المستخدمين (Users)
-- ============================================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Users] (
        [UserId]       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Username]     NVARCHAR(50)      NOT NULL UNIQUE,
        [PasswordHash] NVARCHAR(256)     NOT NULL, -- تشفير SHA-256
        [FullName]     NVARCHAR(100)     NOT NULL,
        [Role]         NVARCHAR(50)      NOT NULL DEFAULT N'كاشير', -- 'Admin', 'Cashier', 'مدير', 'كاشير'
        [IsActive]     BIT               NOT NULL DEFAULT 1,
        [CreatedAt]    DATETIME          NOT NULL DEFAULT GETDATE(),
        [LastLogin]    DATETIME          NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Users_Username' AND object_id = OBJECT_ID(N'[dbo].[Users]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Users_Username] ON [dbo].[Users] ([Username]);
END
GO

-- ============================================================================
-- 3. جدول الأقسام والفئات (Categories)
-- ============================================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Categories] (
        [CategoryId]   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [CategoryName] NVARCHAR(100)     NOT NULL UNIQUE
    );
END
GO

-- ============================================================================
-- 4. جدول المنتجات والمخزون (Products)
-- ============================================================================
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

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Products_Barcode' AND object_id = OBJECT_ID(N'[dbo].[Products]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Products_Barcode] ON [dbo].[Products] ([Barcode]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Products_ProductName' AND object_id = OBJECT_ID(N'[dbo].[Products]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Products_ProductName] ON [dbo].[Products] ([ProductName]);
END
GO

-- ============================================================================
-- 5. جدول الموردين (Suppliers)
-- ============================================================================
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

-- ============================================================================
-- 6. جدول فواتير المشتريات (Purchases)
-- ============================================================================
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

-- ============================================================================
-- 7. جدول تفاصيل فواتير المشتريات (PurchaseDetails)
-- ============================================================================
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

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_PurchaseDetails_PurchaseId' AND object_id = OBJECT_ID(N'[dbo].[PurchaseDetails]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_PurchaseDetails_PurchaseId] ON [dbo].[PurchaseDetails] ([PurchaseId]);
END
GO

-- ============================================================================
-- 8. جدول فواتير المبيعات (Sales - POS)
-- ============================================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sales]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Sales] (
        [SaleId]        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [UserId]        INT               NULL,
        [SaleDate]      DATETIME          NOT NULL DEFAULT GETDATE(),
        [TotalAmount]   DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        [Discount]      DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        [FinalAmount]   DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        [PaidAmount]    DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        [ChangeAmount]  DECIMAL(18,2)     NOT NULL DEFAULT 0.00,
        [PaymentMethod] NVARCHAR(50)      NOT NULL DEFAULT N'نقدي',
        CONSTRAINT [FK_Sales_Users] FOREIGN KEY ([UserId]) 
            REFERENCES [dbo].[Users] ([UserId]) ON DELETE SET NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Sales_SaleDate' AND object_id = OBJECT_ID(N'[dbo].[Sales]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Sales_SaleDate] ON [dbo].[Sales] ([SaleDate]);
END
GO

-- ============================================================================
-- 9. جدول تفاصيل فواتير المبيعات (SaleDetails)
-- ============================================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaleDetails]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SaleDetails] (
        [DetailId]  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [SaleId]    INT               NOT NULL,
        [ProductId] INT               NOT NULL,
        [Quantity]  INT               NOT NULL,
        [UnitPrice] DECIMAL(18,2)     NOT NULL,
        [LineTotal] DECIMAL(18,2)     NOT NULL,
        CONSTRAINT [FK_SaleDetails_Sales] FOREIGN KEY ([SaleId]) 
            REFERENCES [dbo].[Sales] ([SaleId]) ON DELETE CASCADE,
        CONSTRAINT [FK_SaleDetails_Products] FOREIGN KEY ([ProductId]) 
            REFERENCES [dbo].[Products] ([ProductId])
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_SaleDetails_SaleId' AND object_id = OBJECT_ID(N'[dbo].[SaleDetails]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SaleDetails_SaleId] ON [dbo].[SaleDetails] ([SaleId]);
END
GO

-- ============================================================================
-- 10. البيانات الأولية باللغة العربية (Seed Data)
-- ============================================================================

-- إضافة المستخدمين الافتراضيين
IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [Username] = 'admin')
BEGIN
    INSERT INTO [dbo].[Users] ([Username], [PasswordHash], [FullName], [Role], [IsActive])
    VALUES (
        N'admin',
        N'240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', -- admin123
        N'مدير النظام العام',
        N'Admin',
        1
    );
END
ELSE
BEGIN
    UPDATE [dbo].[Users] SET [FullName] = N'مدير النظام العام' WHERE [Username] = 'admin';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [Username] = 'cashier')
BEGIN
    INSERT INTO [dbo].[Users] ([Username], [PasswordHash], [FullName], [Role], [IsActive])
    VALUES (
        N'cashier',
        N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', -- cashier123
        N'كاشير الصالة الرئيسي',
        N'Cashier',
        1
    );
END
ELSE
BEGIN
    UPDATE [dbo].[Users] SET [FullName] = N'كاشير الصالة الرئيسي' WHERE [Username] = 'cashier';
END

-- إضافة الأقسام باللغة العربية
IF NOT EXISTS (SELECT 1 FROM [dbo].[Categories] WHERE [CategoryName] = N'مشروبات ومياه')
    INSERT INTO [dbo].[Categories] ([CategoryName]) VALUES (N'مشروبات ومياه');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Categories] WHERE [CategoryName] = N'سناكس ومقرمشات')
    INSERT INTO [dbo].[Categories] ([CategoryName]) VALUES (N'سناكس ومقرمشات');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Categories] WHERE [CategoryName] = N'ألبان وجبن')
    INSERT INTO [dbo].[Categories] ([CategoryName]) VALUES (N'ألبان وجبن');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Categories] WHERE [CategoryName] = N'إلكترونيات وإكسسوارات')
    INSERT INTO [dbo].[Categories] ([CategoryName]) VALUES (N'إلكترونيات وإكسسوارات');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Categories] WHERE [CategoryName] = N'منظفات وعناية منزلية')
    INSERT INTO [dbo].[Categories] ([CategoryName]) VALUES (N'منظفات وعناية منزلية');

-- إضافة الموردين باللغة العربية
IF NOT EXISTS (SELECT 1 FROM [dbo].[Suppliers] WHERE [SupplierName] = N'شركة الأهرام للتوزيع والتوريدات')
    INSERT INTO [dbo].[Suppliers] ([SupplierName], [Phone], [Address]) 
    VALUES (N'شركة الأهرام للتوزيع والتوريدات', N'01001234567', N'المنطقة الصناعية - القاهرة');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Suppliers] WHERE [SupplierName] = N'مؤسسة الدلتا للمواد الغذائية')
    INSERT INTO [dbo].[Suppliers] ([SupplierName], [Phone], [Address]) 
    VALUES (N'مؤسسة الدلتا للمواد الغذائية', N'01129876543', N'مجمع المخازن اللوجستية - الإسكندرية');

-- إضافة وتحديث المنتجات باللغة العربية
IF NOT EXISTS (SELECT 1 FROM [dbo].[Products] WHERE [Barcode] = N'6221001001')
BEGIN
    INSERT INTO [dbo].[Products] ([Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert])
    VALUES (N'6221001001', N'مياه معدنية 1.5 لتر', 1, 8.00, 12.00, 50, 10);
END
ELSE
BEGIN
    UPDATE [dbo].[Products] SET [ProductName] = N'مياه معدنية 1.5 لتر' WHERE [Barcode] = N'6221001001';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Products] WHERE [Barcode] = N'6221001002')
BEGIN
    INSERT INTO [dbo].[Products] ([Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert])
    VALUES (N'6221001002', N'كانز كولا 330 مل', 1, 12.00, 18.00, 40, 10);
END
ELSE
BEGIN
    UPDATE [dbo].[Products] SET [ProductName] = N'كانز كولا 330 مل' WHERE [Barcode] = N'6221001002';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Products] WHERE [Barcode] = N'6221001003')
BEGIN
    INSERT INTO [dbo].[Products] ([Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert])
    VALUES (N'6221001003', N'شيبسي عائلي بالجبنة المتبلة', 2, 10.00, 15.00, 25, 8);
END
ELSE
BEGIN
    UPDATE [dbo].[Products] SET [ProductName] = N'شيبسي عائلي بالجبنة المتبلة' WHERE [Barcode] = N'6221001003';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Products] WHERE [Barcode] = N'6221001004')
BEGIN
    INSERT INTO [dbo].[Products] ([Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert])
    VALUES (N'6221001004', N'حليب طازج كامل الدسم 1 لتر', 3, 30.00, 42.00, 4, 10); -- مخزون حرج للتنبيه
END
ELSE
BEGIN
    UPDATE [dbo].[Products] SET [ProductName] = N'حليب طازج كامل الدسم 1 لتر' WHERE [Barcode] = N'6221001004';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Products] WHERE [Barcode] = N'6221001005')
BEGIN
    INSERT INTO [dbo].[Products] ([Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert])
    VALUES (N'6221001005', N'كابل شحن سريع Type-C', 4, 45.00, 75.00, 3, 5); -- مخزون حرج للتنبيه
END
ELSE
BEGIN
    UPDATE [dbo].[Products] SET [ProductName] = N'كابل شحن سريع Type-C' WHERE [Barcode] = N'6221001005';
END
GO

-- ============================================================================
-- 11. جدول مرتجعات المبيعات (SalesReturns & SalesReturnDetails)
-- ============================================================================
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Sales]') AND name = 'ReturnStatus')
BEGIN
    ALTER TABLE [dbo].[Sales] ADD [ReturnStatus] NVARCHAR(50) NOT NULL DEFAULT N'مكتملة';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Sales]') AND name = 'TotalRefunded')
BEGIN
    ALTER TABLE [dbo].[Sales] ADD [TotalRefunded] DECIMAL(18,2) NOT NULL DEFAULT 0.00;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SaleDetails]') AND name = 'ReturnedQuantity')
BEGIN
    ALTER TABLE [dbo].[SaleDetails] ADD [ReturnedQuantity] INT NOT NULL DEFAULT 0;
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

-- ============================================================================
-- 12. جدول إعدادات النظام العامة (SystemSettings)
-- ============================================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemSettings]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SystemSettings] (
        [SettingKey]   NVARCHAR(50)  NOT NULL PRIMARY KEY,
        [SettingValue] NVARCHAR(MAX) NULL
    );
END
GO

-- ============================================================================
-- 13. الفهارس المتقدمة لتحسين سرعة الاستعلامات والعمليات (High-Performance Indexes)
-- ============================================================================
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Products_CategoryId' AND object_id = OBJECT_ID(N'[dbo].[Products]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Products_CategoryId] ON [dbo].[Products] ([CategoryId]) 
    INCLUDE ([ProductName], [SellPrice], [StockQuantity], [Barcode]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Products_StockAlert' AND object_id = OBJECT_ID(N'[dbo].[Products]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Products_StockAlert] ON [dbo].[Products] ([StockQuantity], [MinStockAlert]) 
    INCLUDE ([ProductName], [Barcode], [BuyPrice], [SellPrice], [CategoryId]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Purchases_SupplierId' AND object_id = OBJECT_ID(N'[dbo].[Purchases]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Purchases_SupplierId] ON [dbo].[Purchases] ([SupplierId]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Purchases_PurchaseDate' AND object_id = OBJECT_ID(N'[dbo].[Purchases]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Purchases_PurchaseDate] ON [dbo].[Purchases] ([PurchaseDate]) 
    INCLUDE ([TotalAmount], [SupplierId]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Sales_UserId' AND object_id = OBJECT_ID(N'[dbo].[Sales]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Sales_UserId] ON [dbo].[Sales] ([UserId]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Sales_SaleDate_Covering' AND object_id = OBJECT_ID(N'[dbo].[Sales]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Sales_SaleDate_Covering] ON [dbo].[Sales] ([SaleDate]) 
    INCLUDE ([SaleId], [UserId], [FinalAmount], [TotalRefunded], [ReturnStatus], [PaymentMethod]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_SalesReturns_SaleId' AND object_id = OBJECT_ID(N'[dbo].[SalesReturns]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SalesReturns_SaleId] ON [dbo].[SalesReturns] ([SaleId]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_SalesReturnDetails_ReturnId' AND object_id = OBJECT_ID(N'[dbo].[SalesReturnDetails]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SalesReturnDetails_ReturnId] ON [dbo].[SalesReturnDetails] ([ReturnId]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_SalesReturnDetails_ProductId' AND object_id = OBJECT_ID(N'[dbo].[SalesReturnDetails]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SalesReturnDetails_ProductId] ON [dbo].[SalesReturnDetails] ([ProductId]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_SaleDetails_ProductId' AND object_id = OBJECT_ID(N'[dbo].[SaleDetails]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SaleDetails_ProductId] ON [dbo].[SaleDetails] ([ProductId]);
END
GO



