-- ============================================================================
-- قاعدة بيانات نظام نقاط البيع وإدارة المخزون (POS_DB) - النسخة الإنتاجية النظيفة
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

-- 1. جدول المستخدمين (Users)
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

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Users_Username' AND object_id = OBJECT_ID(N'[dbo].[Users]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Users_Username] ON [dbo].[Users] ([Username]);
END
GO

-- 2. جدول الأقسام والفئات (Categories)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Categories] (
        [CategoryId]   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [CategoryName] NVARCHAR(100)     NOT NULL UNIQUE
    );
END
GO

-- 3. جدول المنتجات والمخزون (Products)
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

-- 4. جدول الموردين (Suppliers)
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

-- 5. جدول فواتير المشتريات (Purchases)
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

-- 6. جدول تفاصيل فواتير المشتريات (PurchaseDetails)
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

-- 7. جدول فواتير المبيعات (Sales - POS)
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

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Sales_SaleDate' AND object_id = OBJECT_ID(N'[dbo].[Sales]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Sales_SaleDate] ON [dbo].[Sales] ([SaleDate]);
END
GO

-- 8. جدول تفاصيل فواتير المبيعات (SaleDetails)
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

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_SaleDetails_SaleId' AND object_id = OBJECT_ID(N'[dbo].[SaleDetails]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SaleDetails_SaleId] ON [dbo].[SaleDetails] ([SaleId]);
END
GO

-- 9. جدول مرتجعات المبيعات (SalesReturns & SalesReturnDetails)
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

-- 10. جدول إعدادات النظام (SystemSettings)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemSettings]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SystemSettings] (
        [SettingKey]   NVARCHAR(50)  NOT NULL PRIMARY KEY,
        [SettingValue] NVARCHAR(MAX) NULL
    );
END
GO

-- 11. جدول الورديات (Shifts)
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

-- 12. الفهارس المتقدمة
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

-- ============================================================================
-- 13. البيانات الأولية الوحيدة (مستخدم المدير admin / admin123 وإعدادات النظام)
-- ============================================================================

-- إعدادات النظام الافتراضية
MERGE INTO [dbo].[SystemSettings] AS target
USING (VALUES 
    (N'StoreName', N'نظام إدارة نقاط البيع'),
    (N'StoreSubtitle', N'إدارة المبيعات والمخازن والتحصيل'),
    (N'StorePhone', N''),
    (N'StoreAddress', N''),
    (N'TaxNumber', N''),
    (N'ReceiptHeader', N'أهلاً وسهلاً بكم'),
    (N'ReceiptFooter', N'شكراً لتعاملكم معنا • البضاعة المباعة ترد وتستبدل خلال 14 يوماً بالفاتورة'),
    (N'CurrencySymbol', N'ج.م'),
    (N'VatRate', N'0.00'),
    (N'DefaultMinStock', N'5'),
    (N'EnablePrintPreview', N'True'),
    (N'AutoPrintOnSale', N'False'),
    (N'AllowNegativeStock', N'False')
) AS source (SettingKey, SettingValue)
ON target.SettingKey = source.SettingKey
WHEN NOT MATCHED THEN
    INSERT (SettingKey, SettingValue) VALUES (source.SettingKey, source.SettingValue);
GO

-- إنشاء حساب الأدمن الافتراضي فقط (Username: admin, Password: admin123)
IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [Username] = N'admin')
BEGIN
    INSERT INTO [dbo].[Users] ([Username], [PasswordHash], [FullName], [Role], [IsActive], [CreatedAt])
    VALUES (
        N'admin', 
        N'240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', -- SHA-256 لـ admin123
        N'المدير العام', 
        N'Admin', 
        1, 
        GETDATE()
    );
END
GO
