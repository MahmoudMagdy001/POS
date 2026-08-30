using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public static class DbHelper
    {
        private static readonly string DefaultMasterConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=master;Integrated Security=True;TrustServerCertificate=True;";

        private static readonly string DefaultAppConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=POS_DB;Integrated Security=True;TrustServerCertificate=True;";

        public static string GetConnectionString()
        {
            var configConn = ConfigurationManager.ConnectionStrings["POS_DB"];
            if (configConn != null && !string.IsNullOrWhiteSpace(configConn.ConnectionString))
            {
                return configConn.ConnectionString;
            }
            return DefaultAppConnectionString;
        }

        public static string ComputeSha256Hash(string rawData)
        {
            if (string.IsNullOrEmpty(rawData)) return string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder(bytes.Length * 2);
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        #region In-Memory Fast Caching Layer

        private static readonly object _settingsLock = new object();
        private static SystemSettingsModel _cachedSettings = null;

        private static readonly object _categoriesLock = new object();
        private static List<CategoryModel> _cachedCategories = null;

        private static readonly ConcurrentDictionary<string, ProductModel> _productBarcodeCache =
            new ConcurrentDictionary<string, ProductModel>(StringComparer.OrdinalIgnoreCase);

        private static readonly ConcurrentDictionary<int, ProductModel> _productIdCache =
            new ConcurrentDictionary<int, ProductModel>();

        public static void InvalidateSettingsCache()
        {
            lock (_settingsLock)
            {
                _cachedSettings = null;
            }
        }

        public static void InvalidateCategoriesCache()
        {
            lock (_categoriesLock)
            {
                _cachedCategories = null;
            }
        }

        public static void InvalidateProductsCache(string barcode = null, int? productId = null)
        {
            if (barcode != null)
                _productBarcodeCache.TryRemove(barcode, out _);
            if (productId.HasValue)
                _productIdCache.TryRemove(productId.Value, out _);

            if (barcode == null && !productId.HasValue)
            {
                _productBarcodeCache.Clear();
                _productIdCache.Clear();
            }
        }

        public static void CacheProduct(ProductModel prod)
        {
            if (prod == null) return;
            if (!string.IsNullOrWhiteSpace(prod.Barcode))
                _productBarcodeCache[prod.Barcode] = prod;
            if (prod.ProductId > 0)
                _productIdCache[prod.ProductId] = prod;
        }

        #endregion

        #region Database Initialization

        private const int CurrentSchemaVersion = 3;

        public static void InitializeDatabase()
        {
            try
            {
                // 1. Ensure POS_DB exists on SQL Server
                using (SqlConnection masterConn = new SqlConnection(DefaultMasterConnectionString))
                {
                    masterConn.Open();
                    string checkDbQuery = "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'POS_DB') CREATE DATABASE POS_DB;";
                    using (SqlCommand cmd = new SqlCommand(checkDbQuery, masterConn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                // 2. Fast check schema version before executing full DDL script
                using (SqlConnection appConn = new SqlConnection(GetConnectionString()))
                {
                    appConn.Open();

                    string versionCheck = @"
                        IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__SchemaVersion]') AND type in (N'U'))
                            SELECT TOP 1 VersionNumber FROM [dbo].[__SchemaVersion] ORDER BY AppliedAt DESC;
                        ELSE
                            SELECT 0;";

                    int existingVersion = 0;
                    using (SqlCommand cmd = new SqlCommand(versionCheck, appConn))
                    {
                        object res = cmd.ExecuteScalar();
                        if (res != null && res != DBNull.Value)
                            existingVersion = Convert.ToInt32(res);
                    }

                    if (existingVersion >= CurrentSchemaVersion)
                    {
                        return; // Database schema and indexes are already up-to-date!
                    }

                    // Execute full schema, migrations, seed data, and indexes
                    string schemaQuery = @"
                        -- Users
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

                            INSERT INTO [dbo].[Users] ([Username], [PasswordHash], [FullName], [Role], [IsActive])
                            VALUES (N'admin', N'240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', N'مدير النظام العام', N'Admin', 1);

                            INSERT INTO [dbo].[Users] ([Username], [PasswordHash], [FullName], [Role], [IsActive])
                            VALUES (N'cashier', N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', N'كاشير الصالة الرئيسي', N'Cashier', 1);
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Users_Username' AND object_id = OBJECT_ID(N'[dbo].[Users]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_Users_Username] ON [dbo].[Users] ([Username]);
                        END;

                        -- Categories
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[Categories] (
                                [CategoryId]   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                                [CategoryName] NVARCHAR(100)     NOT NULL UNIQUE
                            );

                            INSERT INTO [dbo].[Categories] ([CategoryName]) VALUES (N'مشروبات ومياه');
                            INSERT INTO [dbo].[Categories] ([CategoryName]) VALUES (N'سناكس ومقرمشات');
                            INSERT INTO [dbo].[Categories] ([CategoryName]) VALUES (N'ألبان وجبن');
                            INSERT INTO [dbo].[Categories] ([CategoryName]) VALUES (N'إلكترونيات وإكسسوارات');
                            INSERT INTO [dbo].[Categories] ([CategoryName]) VALUES (N'منظفات وعناية منزلية');
                        END;

                        -- Products
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

                            INSERT INTO [dbo].[Products] ([Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert])
                            VALUES (N'6221001001', N'مياه معدنية 1.5 لتر', 1, 8.00, 12.00, 50, 10);
                            INSERT INTO [dbo].[Products] ([Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert])
                            VALUES (N'6221001002', N'كانز كولا 330 مل', 1, 12.00, 18.00, 40, 10);
                            INSERT INTO [dbo].[Products] ([Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert])
                            VALUES (N'6221001003', N'شيبسي عائلي بالجبنة المتبلة', 2, 10.00, 15.00, 25, 8);
                            INSERT INTO [dbo].[Products] ([Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert])
                            VALUES (N'6221001004', N'حليب طازج كامل الدسم 1 لتر', 3, 30.00, 42.00, 4, 10);
                            INSERT INTO [dbo].[Products] ([Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert])
                            VALUES (N'6221001005', N'كابل شحن سريع Type-C', 4, 45.00, 75.00, 3, 5);
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Products_Barcode' AND object_id = OBJECT_ID(N'[dbo].[Products]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_Products_Barcode] ON [dbo].[Products] ([Barcode]);
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Products_ProductName' AND object_id = OBJECT_ID(N'[dbo].[Products]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_Products_ProductName] ON [dbo].[Products] ([ProductName]);
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Products_CategoryId' AND object_id = OBJECT_ID(N'[dbo].[Products]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_Products_CategoryId] ON [dbo].[Products] ([CategoryId]) 
                            INCLUDE ([ProductName], [SellPrice], [StockQuantity], [Barcode]);
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Products_StockAlert' AND object_id = OBJECT_ID(N'[dbo].[Products]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_Products_StockAlert] ON [dbo].[Products] ([StockQuantity], [MinStockAlert]) 
                            INCLUDE ([ProductName], [Barcode], [BuyPrice], [SellPrice], [CategoryId]);
                        END;

                        -- Suppliers
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Suppliers]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[Suppliers] (
                                [SupplierId]   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                                [SupplierName] NVARCHAR(150)     NOT NULL,
                                [Phone]        NVARCHAR(20)      NULL,
                                [Address]      NVARCHAR(250)     NULL,
                                [CreatedAt]    DATETIME          NOT NULL DEFAULT GETDATE()
                            );

                            INSERT INTO [dbo].[Suppliers] ([SupplierName], [Phone], [Address]) 
                            VALUES (N'شركة الأهرام للتوزيع والتوريدات', N'01001234567', N'المنطقة الصناعية - القاهرة');
                            INSERT INTO [dbo].[Suppliers] ([SupplierName], [Phone], [Address]) 
                            VALUES (N'مؤسسة الدلتا للمواد الغذائية', N'01129876543', N'مجمع المخازن اللوجستية - الإسكندرية');
                        END;

                        -- Purchases
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
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Purchases_SupplierId' AND object_id = OBJECT_ID(N'[dbo].[Purchases]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_Purchases_SupplierId] ON [dbo].[Purchases] ([SupplierId]);
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Purchases_PurchaseDate' AND object_id = OBJECT_ID(N'[dbo].[Purchases]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_Purchases_PurchaseDate] ON [dbo].[Purchases] ([PurchaseDate]) 
                            INCLUDE ([TotalAmount], [SupplierId]);
                        END;

                        -- PurchaseDetails
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
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_PurchaseDetails_PurchaseId' AND object_id = OBJECT_ID(N'[dbo].[PurchaseDetails]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_PurchaseDetails_PurchaseId] ON [dbo].[PurchaseDetails] ([PurchaseId]);
                        END;

                        -- Sales
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
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Sales_SaleDate' AND object_id = OBJECT_ID(N'[dbo].[Sales]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_Sales_SaleDate] ON [dbo].[Sales] ([SaleDate]);
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Sales_SaleDate_Covering' AND object_id = OBJECT_ID(N'[dbo].[Sales]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_Sales_SaleDate_Covering] ON [dbo].[Sales] ([SaleDate]) 
                            INCLUDE ([SaleId], [UserId], [FinalAmount], [TotalRefunded], [ReturnStatus], [PaymentMethod]);
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Sales_UserId' AND object_id = OBJECT_ID(N'[dbo].[Sales]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_Sales_UserId] ON [dbo].[Sales] ([UserId]);
                        END;

                        -- SaleDetails
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
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_SaleDetails_SaleId' AND object_id = OBJECT_ID(N'[dbo].[SaleDetails]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_SaleDetails_SaleId] ON [dbo].[SaleDetails] ([SaleId]);
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_SaleDetails_ProductId' AND object_id = OBJECT_ID(N'[dbo].[SaleDetails]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_SaleDetails_ProductId] ON [dbo].[SaleDetails] ([ProductId]);
                        END;

                        -- SalesReturns
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
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_SalesReturns_SaleId' AND object_id = OBJECT_ID(N'[dbo].[SalesReturns]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_SalesReturns_SaleId] ON [dbo].[SalesReturns] ([SaleId]);
                        END;

                        -- SalesReturnDetails
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
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_SalesReturnDetails_ReturnId' AND object_id = OBJECT_ID(N'[dbo].[SalesReturnDetails]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_SalesReturnDetails_ReturnId] ON [dbo].[SalesReturnDetails] ([ReturnId]);
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_SalesReturnDetails_ProductId' AND object_id = OBJECT_ID(N'[dbo].[SalesReturnDetails]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_SalesReturnDetails_ProductId] ON [dbo].[SalesReturnDetails] ([ProductId]);
                        END;

                        -- SystemSettings
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemSettings]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[SystemSettings] (
                                [SettingKey]   NVARCHAR(50)  NOT NULL PRIMARY KEY,
                                [SettingValue] NVARCHAR(MAX) NULL
                            );
                        END;

                        -- Shifts (Attendance / الوردية والحضور والانصراف)
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
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Shifts_UserId' AND object_id = OBJECT_ID(N'[dbo].[Shifts]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_Shifts_UserId] ON [dbo].[Shifts] ([UserId])
                            INCLUDE ([ClockInTime], [ClockOutTime]);
                        END;

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Shifts_ClockInTime' AND object_id = OBJECT_ID(N'[dbo].[Shifts]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_Shifts_ClockInTime] ON [dbo].[Shifts] ([ClockInTime])
                            INCLUDE ([UserId], [ClockOutTime]);
                        END;

                        -- Schema Version Tracker
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__SchemaVersion]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[__SchemaVersion] (
                                [VersionNumber] INT NOT NULL PRIMARY KEY,
                                [AppliedAt]     DATETIME NOT NULL DEFAULT GETDATE()
                            );
                        END;

                        IF NOT EXISTS (SELECT 1 FROM [dbo].[__SchemaVersion] WHERE VersionNumber = 3)
                        BEGIN
                            INSERT INTO [dbo].[__SchemaVersion] (VersionNumber, AppliedAt) VALUES (3, GETDATE());
                        END;
                    ";

                    using (SqlCommand cmd = new SqlCommand(schemaQuery, appConn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Database init error: " + ex.Message);
            }
        }

        #endregion

        #region User Authentication & Management

        public static UserModel GetUserById(int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "SELECT UserId, Username, FullName, Role, IsActive, CreatedAt, LastLogin FROM [dbo].[Users] WHERE UserId = @UserId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new UserModel
                                {
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    Username = reader["Username"].ToString(),
                                    FullName = reader["FullName"].ToString(),
                                    Role = reader["Role"].ToString(),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    CreatedAt = reader["CreatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["CreatedAt"]) : null,
                                    LastLogin = reader["LastLogin"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["LastLogin"]) : null
                                };
                            }
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        public static async Task<UserModel> GetUserByIdAsync(int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = "SELECT UserId, Username, FullName, Role, IsActive, CreatedAt, LastLogin FROM [dbo].[Users] WHERE UserId = @UserId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            if (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                return new UserModel
                                {
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    Username = reader["Username"].ToString(),
                                    FullName = reader["FullName"].ToString(),
                                    Role = reader["Role"].ToString(),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    CreatedAt = reader["CreatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["CreatedAt"]) : null,
                                    LastLogin = reader["LastLogin"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["LastLogin"]) : null
                                };
                            }
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        public static (bool Success, string Message, UserModel User) Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return (false, "يرجى إدخال اسم المستخدم وكلمة المرور.", null);
            }

            string passwordHash = ComputeSha256Hash(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT UserId, Username, FullName, Role, IsActive, CreatedAt, LastLogin 
                        FROM [dbo].[Users] 
                        WHERE Username = @Username AND PasswordHash = @PasswordHash";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username.Trim();
                        cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 256).Value = passwordHash;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bool isActive = Convert.ToBoolean(reader["IsActive"]);
                                if (!isActive)
                                {
                                    return (false, "هذا الحساب موقوف حالياً. يرجى مراجعة مدير النظام.", null);
                                }

                                UserModel user = new UserModel
                                {
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    Username = reader["Username"].ToString(),
                                    FullName = reader["FullName"].ToString(),
                                    Role = reader["Role"].ToString(),
                                    IsActive = isActive,
                                    CreatedAt = reader["CreatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["CreatedAt"]) : null,
                                    LastLogin = reader["LastLogin"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["LastLogin"]) : null
                                };

                                reader.Close();

                                using (SqlCommand updateCmd = new SqlCommand("UPDATE [dbo].[Users] SET LastLogin = GETDATE() WHERE UserId = @UserId", conn))
                                {
                                    updateCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = user.UserId;
                                    updateCmd.ExecuteNonQuery();
                                }

                                return (true, "تم تسجيل الدخول بنجاح.", user);
                            }
                            else
                            {
                                return (false, "اسم المستخدم أو كلمة المرور غير صحيحة.", null);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return (false, "خطأ في الاتصال بقاعدة البيانات: " + ex.Message, null);
            }
            catch (Exception ex)
            {
                return (false, "حدث خطأ غير متوقع: " + ex.Message, null);
            }
        }

        public static async Task<(bool Success, string Message, UserModel User)> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return (false, "يرجى إدخال اسم المستخدم وكلمة المرور.", null);
            }

            string passwordHash = ComputeSha256Hash(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = @"
                        SELECT UserId, Username, FullName, Role, IsActive, CreatedAt, LastLogin 
                        FROM [dbo].[Users] 
                        WHERE Username = @Username AND PasswordHash = @PasswordHash";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username.Trim();
                        cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 256).Value = passwordHash;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            if (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                bool isActive = Convert.ToBoolean(reader["IsActive"]);
                                if (!isActive)
                                {
                                    return (false, "هذا الحساب موقوف حالياً. يرجى مراجعة مدير النظام.", null);
                                }

                                UserModel user = new UserModel
                                {
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    Username = reader["Username"].ToString(),
                                    FullName = reader["FullName"].ToString(),
                                    Role = reader["Role"].ToString(),
                                    IsActive = isActive,
                                    CreatedAt = reader["CreatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["CreatedAt"]) : null,
                                    LastLogin = reader["LastLogin"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["LastLogin"]) : null
                                };

                                reader.Close();

                                using (SqlCommand updateCmd = new SqlCommand("UPDATE [dbo].[Users] SET LastLogin = GETDATE() WHERE UserId = @UserId", conn))
                                {
                                    updateCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = user.UserId;
                                    await updateCmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                                }

                                return (true, "تم تسجيل الدخول بنجاح.", user);
                            }
                            else
                            {
                                return (false, "اسم المستخدم أو كلمة المرور غير صحيحة.", null);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return (false, "خطأ في الاتصال بقاعدة البيانات: " + ex.Message, null);
            }
            catch (Exception ex)
            {
                return (false, "حدث خطأ غير متوقع: " + ex.Message, null);
            }
        }

        public static (bool Success, string Message, int NewUserId) CreateUser(string username, string password, string fullName, string role, bool isActive = true)
        {
            if (string.IsNullOrWhiteSpace(username))
                return (false, "اسم المستخدم مطلوب.", 0);
            if (string.IsNullOrWhiteSpace(password))
                return (false, "كلمة المرور مطلوبة.", 0);
            if (password.Length < 4)
                return (false, "كلمة المرور يجب أن لا تقل عن 4 خانات.", 0);
            if (string.IsNullOrWhiteSpace(fullName))
                return (false, "الاسم الكامل مطلوب.", 0);
            if (string.IsNullOrWhiteSpace(role))
                role = "Cashier";

            string trimmedUsername = username.Trim();
            string trimmedFullName = fullName.Trim();
            string passwordHash = ComputeSha256Hash(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(1) FROM [dbo].[Users] WHERE Username = @Username";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = trimmedUsername;
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            return (false, $"اسم المستخدم '{trimmedUsername}' مستخدم بالفعل، يرجى اختيار اسم آخر.", 0);
                        }
                    }

                    string insertQuery = @"
                        INSERT INTO [dbo].[Users] ([Username], [PasswordHash], [FullName], [Role], [IsActive], [CreatedAt])
                        VALUES (@Username, @PasswordHash, @FullName, @Role, @IsActive, GETDATE());
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = trimmedUsername;
                        insertCmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 256).Value = passwordHash;
                        insertCmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value = trimmedFullName;
                        insertCmd.Parameters.Add("@Role", SqlDbType.NVarChar, 50).Value = role;
                        insertCmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = isActive;

                        int newId = (int)insertCmd.ExecuteScalar();
                        return (true, $"تم إنشاء المستخدم '{trimmedUsername}' بنجاح.", newId);
                    }
                }
            }
            catch (SqlException ex)
            {
                return (false, "خطأ قاعدة البيانات: " + ex.Message, 0);
            }
            catch (Exception ex)
            {
                return (false, "خطأ: " + ex.Message, 0);
            }
        }

        public static DataTable GetAllUsers(string searchTerm = "")
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "SELECT UserId, Username, FullName, Role, IsActive, CreatedAt, LastLogin FROM [dbo].[Users]";

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        query += " WHERE Username LIKE @Search OR FullName LIKE @Search OR Role LIKE @Search";
                    }

                    query += " ORDER BY UserId ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 100).Value = "%" + searchTerm.Trim() + "%";
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        public static async Task<DataTable> GetAllUsersAsync(string searchTerm = "")
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = "SELECT UserId, Username, FullName, Role, IsActive, CreatedAt, LastLogin FROM [dbo].[Users]";

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        query += " WHERE Username LIKE @Search OR FullName LIKE @Search OR Role LIKE @Search";
                    }

                    query += " ORDER BY UserId ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 100).Value = "%" + searchTerm.Trim() + "%";
                        }

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        public static (bool Success, string Message) UpdateUser(int userId, string fullName, string role, bool isActive, string newPassword = null)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return (false, "الاسم الكامل لا يمكن أن يكون فارغاً.");

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query;
                    if (!string.IsNullOrWhiteSpace(newPassword))
                    {
                        if (newPassword.Length < 4)
                            return (false, "كلمة المرور يجب أن لا تقل عن 4 خانات.");

                        query = @"
                            UPDATE [dbo].[Users] 
                            SET FullName = @FullName, Role = @Role, IsActive = @IsActive, PasswordHash = @PasswordHash 
                            WHERE UserId = @UserId";
                    }
                    else
                    {
                        query = @"
                            UPDATE [dbo].[Users] 
                            SET FullName = @FullName, Role = @Role, IsActive = @IsActive 
                            WHERE UserId = @UserId";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value = fullName.Trim();
                        cmd.Parameters.Add("@Role", SqlDbType.NVarChar, 50).Value = role;
                        cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = isActive;
                        if (!string.IsNullOrWhiteSpace(newPassword))
                        {
                            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 256).Value = ComputeSha256Hash(newPassword);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            return (true, "تم تحديث بيانات المستخدم بنجاح.");
                        return (false, "لم يتم العثور على المستخدم.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "خطأ: " + ex.Message);
            }
        }

        public static (bool Success, string Message) DeleteUser(int userId, int currentUserId)
        {
            if (userId == currentUserId)
                return (false, "لا يمكنك حذف حسابك الحالي الذي قمت بتسجيل الدخول به.");

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "DELETE FROM [dbo].[Users] WHERE UserId = @UserId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                            return (true, "تم حذف المستخدم بنجاح.");
                        return (false, "المستخدم غير موجود.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "خطأ: " + ex.Message);
            }
        }

        public static (bool Success, string Message) ToggleUserActive(int userId, int currentUserId, bool newStatus)
        {
            if (userId == currentUserId && !newStatus)
                return (false, "لا يمكنك إيقاف حسابك الحالي.");

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "UPDATE [dbo].[Users] SET IsActive = @IsActive WHERE UserId = @UserId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = newStatus;
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                            return (true, $"تم تغيير حالة المستخدم إلى {(newStatus ? "نشط" : "معطل")}.");
                        return (false, "المستخدم غير موجود.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "خطأ: " + ex.Message);
            }
        }

        #endregion

        #region Categories Management

        public static List<CategoryModel> GetAllCategories()
        {
            lock (_categoriesLock)
            {
                if (_cachedCategories != null)
                    return new List<CategoryModel>(_cachedCategories);
            }

            List<CategoryModel> categories = new List<CategoryModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "SELECT CategoryId, CategoryName FROM [dbo].[Categories] ORDER BY CategoryName ASC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new CategoryModel
                            {
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                CategoryName = reader["CategoryName"].ToString()
                            });
                        }
                    }
                }

                lock (_categoriesLock)
                {
                    _cachedCategories = new List<CategoryModel>(categories);
                }
            }
            catch { }
            return categories;
        }

        public static async Task<List<CategoryModel>> GetAllCategoriesAsync()
        {
            lock (_categoriesLock)
            {
                if (_cachedCategories != null)
                    return new List<CategoryModel>(_cachedCategories);
            }

            List<CategoryModel> categories = new List<CategoryModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = "SELECT CategoryId, CategoryName FROM [dbo].[Categories] ORDER BY CategoryName ASC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            categories.Add(new CategoryModel
                            {
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                CategoryName = reader["CategoryName"].ToString()
                            });
                        }
                    }
                }

                lock (_categoriesLock)
                {
                    _cachedCategories = new List<CategoryModel>(categories);
                }
            }
            catch { }
            return categories;
        }

        public static (bool Success, string Message, int CategoryId) SaveCategory(string categoryName, int? categoryId = null)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return (false, "اسم القسم لا يمكن أن يكون فارغاً.", 0);

            string name = categoryName.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    if (!categoryId.HasValue || categoryId.Value <= 0)
                    {
                        string check = "SELECT COUNT(1) FROM [dbo].[Categories] WHERE CategoryName = @CategoryName";
                        using (SqlCommand chkCmd = new SqlCommand(check, conn))
                        {
                            chkCmd.Parameters.Add("@CategoryName", SqlDbType.NVarChar, 100).Value = name;
                            if ((int)chkCmd.ExecuteScalar() > 0)
                                return (false, "هذا القسم موجود بالفعل.", 0);
                        }

                        string insert = "INSERT INTO [dbo].[Categories] (CategoryName) VALUES (@CategoryName); SELECT CAST(SCOPE_IDENTITY() AS INT);";
                        using (SqlCommand cmd = new SqlCommand(insert, conn))
                        {
                            cmd.Parameters.Add("@CategoryName", SqlDbType.NVarChar, 100).Value = name;
                            int newId = (int)cmd.ExecuteScalar();
                            InvalidateCategoriesCache();
                            return (true, "تمت إضافة القسم بنجاح.", newId);
                        }
                    }
                    else
                    {
                        string update = "UPDATE [dbo].[Categories] SET CategoryName = @CategoryName WHERE CategoryId = @CategoryId";
                        using (SqlCommand cmd = new SqlCommand(update, conn))
                        {
                            cmd.Parameters.Add("@CategoryName", SqlDbType.NVarChar, 100).Value = name;
                            cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId.Value;
                            cmd.ExecuteNonQuery();
                            InvalidateCategoriesCache();
                            return (true, "تم تعديل القسم بنجاح.", categoryId.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "خطأ: " + ex.Message, 0);
            }
        }

        public static (bool Success, string Message) DeleteCategory(int categoryId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "DELETE FROM [dbo].[Categories] WHERE CategoryId = @CategoryId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId;
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            InvalidateCategoriesCache();
                            return (true, "تم حذف القسم بنجاح.");
                        }
                        return (false, "القسم غير موجود.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "خطأ: " + ex.Message);
            }
        }

        #endregion

        #region Products Management

        public static DataTable GetAllProductsDataTable(string searchTerm = "", int? categoryId = null, bool lowStockOnly = false)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    StringBuilder query = new StringBuilder(@"
                        SELECT 
                            p.ProductId, 
                            p.Barcode, 
                            p.ProductName, 
                            p.CategoryId, 
                            ISNULL(c.CategoryName, N'عام / غير مصنف') AS CategoryName, 
                            p.BuyPrice, 
                            p.SellPrice, 
                            p.StockQuantity, 
                            p.MinStockAlert, 
                            p.CreatedAt,
                            CASE WHEN p.StockQuantity <= p.MinStockAlert THEN 1 ELSE 0 END AS IsLowStock
                        FROM [dbo].[Products] p
                        LEFT JOIN [dbo].[Categories] c ON p.CategoryId = c.CategoryId
                        WHERE 1=1 ");

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        query.Append(" AND (p.Barcode LIKE @Search OR p.ProductName LIKE @Search) ");
                    }

                    if (categoryId.HasValue && categoryId.Value > 0)
                    {
                        query.Append(" AND p.CategoryId = @CategoryId ");
                    }

                    if (lowStockOnly)
                    {
                        query.Append(" AND p.StockQuantity <= p.MinStockAlert ");
                    }

                    query.Append(" ORDER BY p.ProductName ASC");

                    using (SqlCommand cmd = new SqlCommand(query.ToString(), conn))
                    {
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 150).Value = "%" + searchTerm.Trim() + "%";
                        }
                        if (categoryId.HasValue && categoryId.Value > 0)
                        {
                            cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId.Value;
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        public static async Task<DataTable> GetAllProductsDataTableAsync(string searchTerm = "", int? categoryId = null, bool lowStockOnly = false)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    StringBuilder query = new StringBuilder(@"
                        SELECT 
                            p.ProductId, 
                            p.Barcode, 
                            p.ProductName, 
                            p.CategoryId, 
                            ISNULL(c.CategoryName, N'عام / غير مصنف') AS CategoryName, 
                            p.BuyPrice, 
                            p.SellPrice, 
                            p.StockQuantity, 
                            p.MinStockAlert, 
                            p.CreatedAt,
                            CASE WHEN p.StockQuantity <= p.MinStockAlert THEN 1 ELSE 0 END AS IsLowStock
                        FROM [dbo].[Products] p
                        LEFT JOIN [dbo].[Categories] c ON p.CategoryId = c.CategoryId
                        WHERE 1=1 ");

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        query.Append(" AND (p.Barcode LIKE @Search OR p.ProductName LIKE @Search) ");
                    }

                    if (categoryId.HasValue && categoryId.Value > 0)
                    {
                        query.Append(" AND p.CategoryId = @CategoryId ");
                    }

                    if (lowStockOnly)
                    {
                        query.Append(" AND p.StockQuantity <= p.MinStockAlert ");
                    }

                    query.Append(" ORDER BY p.ProductName ASC");

                    using (SqlCommand cmd = new SqlCommand(query.ToString(), conn))
                    {
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 150).Value = "%" + searchTerm.Trim() + "%";
                        }
                        if (categoryId.HasValue && categoryId.Value > 0)
                        {
                            cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId.Value;
                        }

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        public static ProductModel GetProductByBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode)) return null;

            string cleanBarcode = barcode.Trim();
            if (_productBarcodeCache.TryGetValue(cleanBarcode, out ProductModel cached))
            {
                return cached;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT p.ProductId, p.Barcode, p.ProductName, p.CategoryId, ISNULL(c.CategoryName, '') AS CategoryName, 
                               p.BuyPrice, p.SellPrice, p.StockQuantity, p.MinStockAlert, p.CreatedAt
                        FROM [dbo].[Products] p
                        LEFT JOIN [dbo].[Categories] c ON p.CategoryId = c.CategoryId
                        WHERE p.Barcode = @Barcode";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = cleanBarcode;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var prod = new ProductModel
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString(),
                                    ProductName = reader["ProductName"].ToString(),
                                    CategoryId = reader["CategoryId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["CategoryId"]) : null,
                                    CategoryName = reader["CategoryName"].ToString(),
                                    BuyPrice = Convert.ToDecimal(reader["BuyPrice"]),
                                    SellPrice = Convert.ToDecimal(reader["SellPrice"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                                    MinStockAlert = Convert.ToInt32(reader["MinStockAlert"]),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                                };
                                CacheProduct(prod);
                                return prod;
                            }
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        public static async Task<ProductModel> GetProductByBarcodeAsync(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode)) return null;

            string cleanBarcode = barcode.Trim();
            if (_productBarcodeCache.TryGetValue(cleanBarcode, out ProductModel cached))
            {
                return cached;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = @"
                        SELECT p.ProductId, p.Barcode, p.ProductName, p.CategoryId, ISNULL(c.CategoryName, '') AS CategoryName, 
                               p.BuyPrice, p.SellPrice, p.StockQuantity, p.MinStockAlert, p.CreatedAt
                        FROM [dbo].[Products] p
                        LEFT JOIN [dbo].[Categories] c ON p.CategoryId = c.CategoryId
                        WHERE p.Barcode = @Barcode";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = cleanBarcode;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            if (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                var prod = new ProductModel
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString(),
                                    ProductName = reader["ProductName"].ToString(),
                                    CategoryId = reader["CategoryId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["CategoryId"]) : null,
                                    CategoryName = reader["CategoryName"].ToString(),
                                    BuyPrice = Convert.ToDecimal(reader["BuyPrice"]),
                                    SellPrice = Convert.ToDecimal(reader["SellPrice"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                                    MinStockAlert = Convert.ToInt32(reader["MinStockAlert"]),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                                };
                                CacheProduct(prod);
                                return prod;
                            }
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        public static ProductModel GetProductById(int productId)
        {
            if (_productIdCache.TryGetValue(productId, out ProductModel cached))
            {
                return cached;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT p.ProductId, p.Barcode, p.ProductName, p.CategoryId, ISNULL(c.CategoryName, '') AS CategoryName, 
                               p.BuyPrice, p.SellPrice, p.StockQuantity, p.MinStockAlert, p.CreatedAt
                        FROM [dbo].[Products] p
                        LEFT JOIN [dbo].[Categories] c ON p.CategoryId = c.CategoryId
                        WHERE p.ProductId = @ProductId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var prod = new ProductModel
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString(),
                                    ProductName = reader["ProductName"].ToString(),
                                    CategoryId = reader["CategoryId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["CategoryId"]) : null,
                                    CategoryName = reader["CategoryName"].ToString(),
                                    BuyPrice = Convert.ToDecimal(reader["BuyPrice"]),
                                    SellPrice = Convert.ToDecimal(reader["SellPrice"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                                    MinStockAlert = Convert.ToInt32(reader["MinStockAlert"]),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                                };
                                CacheProduct(prod);
                                return prod;
                            }
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        public static async Task<ProductModel> GetProductByIdAsync(int productId)
        {
            if (_productIdCache.TryGetValue(productId, out ProductModel cached))
            {
                return cached;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = @"
                        SELECT p.ProductId, p.Barcode, p.ProductName, p.CategoryId, ISNULL(c.CategoryName, '') AS CategoryName, 
                               p.BuyPrice, p.SellPrice, p.StockQuantity, p.MinStockAlert, p.CreatedAt
                        FROM [dbo].[Products] p
                        LEFT JOIN [dbo].[Categories] c ON p.CategoryId = c.CategoryId
                        WHERE p.ProductId = @ProductId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            if (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                var prod = new ProductModel
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString(),
                                    ProductName = reader["ProductName"].ToString(),
                                    CategoryId = reader["CategoryId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["CategoryId"]) : null,
                                    CategoryName = reader["CategoryName"].ToString(),
                                    BuyPrice = Convert.ToDecimal(reader["BuyPrice"]),
                                    SellPrice = Convert.ToDecimal(reader["SellPrice"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                                    MinStockAlert = Convert.ToInt32(reader["MinStockAlert"]),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                                };
                                CacheProduct(prod);
                                return prod;
                            }
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        public static (bool Success, string Message, int ProductId) SaveProduct(ProductModel product)
        {
            if (product == null)
                return (false, "بيانات المنتج مفقودة.", 0);

            if (string.IsNullOrWhiteSpace(product.Barcode))
                return (false, "الباركود مطلوب.", 0);

            if (string.IsNullOrWhiteSpace(product.ProductName))
                return (false, "اسم المنتج مطلوب.", 0);

            if (product.SellPrice < 0 || product.BuyPrice < 0)
                return (false, "الأسعار لا يمكن أن تكون أرقاماً سالبة.", 0);

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    if (product.ProductId <= 0)
                    {
                        string checkBarcode = "SELECT COUNT(1) FROM [dbo].[Products] WHERE Barcode = @Barcode";
                        using (SqlCommand chkCmd = new SqlCommand(checkBarcode, conn))
                        {
                            chkCmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = product.Barcode.Trim();
                            if ((int)chkCmd.ExecuteScalar() > 0)
                                return (false, $"الباركود '{product.Barcode}' مسجل بالفعل لمنتج آخر.", 0);
                        }

                        string insert = @"
                            INSERT INTO [dbo].[Products] (Barcode, ProductName, CategoryId, BuyPrice, SellPrice, StockQuantity, MinStockAlert, CreatedAt)
                            VALUES (@Barcode, @ProductName, @CategoryId, @BuyPrice, @SellPrice, @StockQuantity, @MinStockAlert, GETDATE());
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        using (SqlCommand cmd = new SqlCommand(insert, conn))
                        {
                            cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = product.Barcode.Trim();
                            cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, 150).Value = product.ProductName.Trim();
                            cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = product.CategoryId.HasValue && product.CategoryId.Value > 0 ? (object)product.CategoryId.Value : DBNull.Value;
                            cmd.Parameters.Add("@BuyPrice", SqlDbType.Decimal).Value = product.BuyPrice;
                            cmd.Parameters.Add("@SellPrice", SqlDbType.Decimal).Value = product.SellPrice;
                            cmd.Parameters.Add("@StockQuantity", SqlDbType.Int).Value = product.StockQuantity;
                            cmd.Parameters.Add("@MinStockAlert", SqlDbType.Int).Value = product.MinStockAlert;

                            int newId = (int)cmd.ExecuteScalar();
                            product.ProductId = newId;
                            CacheProduct(product);
                            return (true, "تم حفظ المنتج الجديد بنجاح.", newId);
                        }
                    }
                    else
                    {
                        string checkBarcode = "SELECT COUNT(1) FROM [dbo].[Products] WHERE Barcode = @Barcode AND ProductId <> @ProductId";
                        using (SqlCommand chkCmd = new SqlCommand(checkBarcode, conn))
                        {
                            chkCmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = product.Barcode.Trim();
                            chkCmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = product.ProductId;
                            if ((int)chkCmd.ExecuteScalar() > 0)
                                return (false, $"الباركود '{product.Barcode}' مسجل بالفعل لمنتج آخر.", 0);
                        }

                        string update = @"
                            UPDATE [dbo].[Products]
                            SET Barcode = @Barcode,
                                ProductName = @ProductName,
                                CategoryId = @CategoryId,
                                BuyPrice = @BuyPrice,
                                SellPrice = @SellPrice,
                                StockQuantity = @StockQuantity,
                                MinStockAlert = @MinStockAlert
                            WHERE ProductId = @ProductId";

                        using (SqlCommand cmd = new SqlCommand(update, conn))
                        {
                            cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = product.ProductId;
                            cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = product.Barcode.Trim();
                            cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, 150).Value = product.ProductName.Trim();
                            cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = product.CategoryId.HasValue && product.CategoryId.Value > 0 ? (object)product.CategoryId.Value : DBNull.Value;
                            cmd.Parameters.Add("@BuyPrice", SqlDbType.Decimal).Value = product.BuyPrice;
                            cmd.Parameters.Add("@SellPrice", SqlDbType.Decimal).Value = product.SellPrice;
                            cmd.Parameters.Add("@StockQuantity", SqlDbType.Int).Value = product.StockQuantity;
                            cmd.Parameters.Add("@MinStockAlert", SqlDbType.Int).Value = product.MinStockAlert;

                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                InvalidateProductsCache(product.Barcode, product.ProductId);
                                CacheProduct(product);
                                return (true, "تم تحديث بيانات المنتج بنجاح.", product.ProductId);
                            }
                            return (false, "المنتج غير موجود.", 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "خطأ: " + ex.Message, 0);
            }
        }

        public static (bool Success, string Message) DeleteProduct(int productId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    string checkRef = @"
                        SELECT (SELECT COUNT(1) FROM [dbo].[SaleDetails] WHERE ProductId = @ProductId) +
                               (SELECT COUNT(1) FROM [dbo].[PurchaseDetails] WHERE ProductId = @ProductId)";

                    using (SqlCommand chkCmd = new SqlCommand(checkRef, conn))
                    {
                        chkCmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;
                        int refCount = Convert.ToInt32(chkCmd.ExecuteScalar());
                        if (refCount > 0)
                        {
                            return (false, "لا يمكن حذف هذا المنتج لوجود حركات بيع أو شراء سابقة مرتبطة به.");
                        }
                    }

                    string query = "DELETE FROM [dbo].[Products] WHERE ProductId = @ProductId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            InvalidateProductsCache(null, productId);
                            return (true, "تم حذف المنتج بنجاح.");
                        }
                        return (false, "المنتج غير موجود.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "خطأ: " + ex.Message);
            }
        }

        public static string GenerateUniqueBarcode()
        {
            string candidate;
            Random rnd = new Random();
            do
            {
                candidate = "622" + rnd.Next(1000000, 9999999).ToString();
            }
            while (GetProductByBarcode(candidate) != null);

            return candidate;
        }

        #endregion

        #region Suppliers Management

        public static List<SupplierModel> GetAllSuppliersList()
        {
            List<SupplierModel> suppliers = new List<SupplierModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "SELECT SupplierId, SupplierName, Phone, Address, CreatedAt FROM [dbo].[Suppliers] ORDER BY SupplierName ASC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(new SupplierModel
                            {
                                SupplierId = Convert.ToInt32(reader["SupplierId"]),
                                SupplierName = reader["SupplierName"].ToString(),
                                Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : "",
                                Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : "",
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                            });
                        }
                    }
                }
            }
            catch { }
            return suppliers;
        }

        public static DataTable GetAllSuppliersDataTable(string searchTerm = "")
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "SELECT SupplierId, SupplierName, Phone, Address, CreatedAt FROM [dbo].[Suppliers]";
                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        query += " WHERE SupplierName LIKE @Search OR Phone LIKE @Search OR Address LIKE @Search";
                    }
                    query += " ORDER BY SupplierName ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 150).Value = "%" + searchTerm.Trim() + "%";
                        }
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        public static (bool Success, string Message, int SupplierId) SaveSupplier(SupplierModel supplier)
        {
            if (supplier == null || string.IsNullOrWhiteSpace(supplier.SupplierName))
                return (false, "اسم المورد مطلوب.", 0);

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    if (supplier.SupplierId <= 0)
                    {
                        string insert = @"
                            INSERT INTO [dbo].[Suppliers] (SupplierName, Phone, Address, CreatedAt)
                            VALUES (@SupplierName, @Phone, @Address, GETDATE());
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        using (SqlCommand cmd = new SqlCommand(insert, conn))
                        {
                            cmd.Parameters.Add("@SupplierName", SqlDbType.NVarChar, 150).Value = supplier.SupplierName.Trim();
                            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = (object)supplier.Phone?.Trim() ?? DBNull.Value;
                            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 250).Value = (object)supplier.Address?.Trim() ?? DBNull.Value;

                            int newId = (int)cmd.ExecuteScalar();
                            return (true, "تمت إضافة المورد بنجاح.", newId);
                        }
                    }
                    else
                    {
                        string update = @"
                            UPDATE [dbo].[Suppliers]
                            SET SupplierName = @SupplierName,
                                Phone = @Phone,
                                Address = @Address
                            WHERE SupplierId = @SupplierId";

                        using (SqlCommand cmd = new SqlCommand(update, conn))
                        {
                            cmd.Parameters.Add("@SupplierId", SqlDbType.Int).Value = supplier.SupplierId;
                            cmd.Parameters.Add("@SupplierName", SqlDbType.NVarChar, 150).Value = supplier.SupplierName.Trim();
                            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = (object)supplier.Phone?.Trim() ?? DBNull.Value;
                            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 250).Value = (object)supplier.Address?.Trim() ?? DBNull.Value;

                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                                return (true, "تم تحديث بيانات المورد بنجاح.", supplier.SupplierId);
                            return (false, "المورد غير موجود.", 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "خطأ: " + ex.Message, 0);
            }
        }

        public static (bool Success, string Message) DeleteSupplier(int supplierId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "DELETE FROM [dbo].[Suppliers] WHERE SupplierId = @SupplierId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@SupplierId", SqlDbType.Int).Value = supplierId;
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                            return (true, "تم حذف المورد بنجاح.");
                        return (false, "المورد غير موجود.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "خطأ: " + ex.Message);
            }
        }

        #endregion

        #region Sales Management & POS Checkout Transaction (Single Round-Trip Batched)

        public static (bool Success, string Message, int SaleId) ProcessSaleTransaction(SaleModel sale, List<CartItemModel> items)
        {
            if (sale == null)
                return (false, "بيانات الفاتورة مفقودة.", 0);

            if (items == null || items.Count == 0)
                return (false, "سلة المشتريات فارغة، يرجى إضافة منتجات لإتمام البيع.", 0);

            if (sale.PaidAmount < sale.FinalAmount)
                return (false, $"المبلغ المدفوع ({sale.PaidAmount:N2} ج.م) أقل من إجمالي الفاتورة المطلوب ({sale.FinalAmount:N2} ج.م).", 0);

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    StringBuilder batchSql = new StringBuilder();
                    batchSql.AppendLine("BEGIN TRANSACTION;");
                    batchSql.AppendLine("DECLARE @AvailStock INT = 0;");
                    batchSql.AppendLine("DECLARE @InsuffProduct NVARCHAR(150) = NULL;");

                    for (int i = 0; i < items.Count; i++)
                    {
                        batchSql.AppendLine($"SELECT @AvailStock = StockQuantity, @InsuffProduct = ProductName FROM [dbo].[Products] WITH (UPDLOCK, ROWLOCK) WHERE ProductId = @PId_{i};");
                        batchSql.AppendLine($"IF @AvailStock IS NULL BEGIN ROLLBACK TRANSACTION; SELECT -2 AS ResultStatus, @PId_{i} AS MissingId, 0 AS AvailableStock, 0 AS RequestedQty, '' AS ProductName; RETURN; END;");
                        batchSql.AppendLine($"IF @AvailStock < @Qty_{i} BEGIN ROLLBACK TRANSACTION; SELECT -1 AS ResultStatus, 0 AS MissingId, @AvailStock AS AvailableStock, @Qty_{i} AS RequestedQty, @InsuffProduct AS ProductName; RETURN; END;");
                    }

                    batchSql.AppendLine(@"
                        INSERT INTO [dbo].[Sales] 
                            (UserId, SaleDate, TotalAmount, Discount, TaxAmount, FinalAmount, PaidAmount, ChangeAmount, PaymentMethod)
                        VALUES 
                            (@UserId, @SaleDate, @TotalAmount, @Discount, @TaxAmount, @FinalAmount, @PaidAmount, @ChangeAmount, @PaymentMethod);
                        DECLARE @NewSaleId INT = CAST(SCOPE_IDENTITY() AS INT);");

                    batchSql.AppendLine("INSERT INTO [dbo].[SaleDetails] (SaleId, ProductId, Quantity, UnitPrice, LineTotal) VALUES ");
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (i > 0) batchSql.Append(", ");
                        batchSql.Append($"(@NewSaleId, @PId_{i}, @Qty_{i}, @Price_{i}, @LineTotal_{i})");
                    }
                    batchSql.AppendLine(";");

                    for (int i = 0; i < items.Count; i++)
                    {
                        batchSql.AppendLine($"UPDATE [dbo].[Products] SET StockQuantity = StockQuantity - @Qty_{i} WHERE ProductId = @PId_{i};");
                    }

                    batchSql.AppendLine("COMMIT TRANSACTION;");
                    batchSql.AppendLine("SELECT @NewSaleId AS ResultStatus, 0 AS MissingId, 0 AS AvailableStock, 0 AS RequestedQty, '' AS ProductName;");

                    using (SqlCommand cmd = new SqlCommand(batchSql.ToString(), conn))
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = sale.UserId.HasValue ? (object)sale.UserId.Value : DBNull.Value;
                        cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime).Value = sale.SaleDate == default ? DateTime.Now : sale.SaleDate;
                        cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = sale.TotalAmount;
                        cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = sale.Discount;
                        cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = sale.TaxAmount;
                        cmd.Parameters.Add("@FinalAmount", SqlDbType.Decimal).Value = sale.FinalAmount;
                        cmd.Parameters.Add("@PaidAmount", SqlDbType.Decimal).Value = sale.PaidAmount;
                        cmd.Parameters.Add("@ChangeAmount", SqlDbType.Decimal).Value = sale.ChangeAmount;
                        cmd.Parameters.Add("@PaymentMethod", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(sale.PaymentMethod) ? "نقدي" : sale.PaymentMethod;

                        for (int i = 0; i < items.Count; i++)
                        {
                            cmd.Parameters.Add($"@PId_{i}", SqlDbType.Int).Value = items[i].ProductId;
                            cmd.Parameters.Add($"@Qty_{i}", SqlDbType.Int).Value = items[i].Quantity;
                            cmd.Parameters.Add($"@Price_{i}", SqlDbType.Decimal).Value = items[i].UnitPrice;
                            cmd.Parameters.Add($"@LineTotal_{i}", SqlDbType.Decimal).Value = items[i].LineTotal;
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int status = Convert.ToInt32(reader["ResultStatus"]);
                                if (status > 0)
                                {
                                    InvalidateProductsCache();
                                    return (true, $"تم إتمام الفاتورة #{status:D5} وخصم المخزون بنجاح.", status);
                                }
                                else if (status == -1)
                                {
                                    string pName = reader["ProductName"].ToString();
                                    int avail = Convert.ToInt32(reader["AvailableStock"]);
                                    int req = Convert.ToInt32(reader["RequestedQty"]);
                                    return (false, $"الكمية غير متوفرة في المخزن للمنتج '{pName}'. المتاح: {avail}، المطلوب: {req}.", 0);
                                }
                                else
                                {
                                    int missingId = Convert.ToInt32(reader["MissingId"]);
                                    return (false, $"المنتج ذو الرقم {missingId} غير موجود في قاعدة البيانات.", 0);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "فشلت عملية البيع: " + ex.Message, 0);
            }
            return (false, "فشلت عملية البيع لسبب غير معروف.", 0);
        }

        public static async Task<(bool Success, string Message, int SaleId)> ProcessSaleTransactionAsync(SaleModel sale, List<CartItemModel> items)
        {
            if (sale == null)
                return (false, "بيانات الفاتورة مفقودة.", 0);

            if (items == null || items.Count == 0)
                return (false, "سلة المشتريات فارغة، يرجى إضافة منتجات لإتمام البيع.", 0);

            if (sale.PaidAmount < sale.FinalAmount)
                return (false, $"المبلغ المدفوع ({sale.PaidAmount:N2} ج.م) أقل من إجمالي الفاتورة المطلوب ({sale.FinalAmount:N2} ج.م).", 0);

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);

                    StringBuilder batchSql = new StringBuilder();
                    batchSql.AppendLine("BEGIN TRANSACTION;");
                    batchSql.AppendLine("DECLARE @AvailStock INT = 0;");
                    batchSql.AppendLine("DECLARE @InsuffProduct NVARCHAR(150) = NULL;");

                    for (int i = 0; i < items.Count; i++)
                    {
                        batchSql.AppendLine($"SELECT @AvailStock = StockQuantity, @InsuffProduct = ProductName FROM [dbo].[Products] WITH (UPDLOCK, ROWLOCK) WHERE ProductId = @PId_{i};");
                        batchSql.AppendLine($"IF @AvailStock IS NULL BEGIN ROLLBACK TRANSACTION; SELECT -2 AS ResultStatus, @PId_{i} AS MissingId, 0 AS AvailableStock, 0 AS RequestedQty, '' AS ProductName; RETURN; END;");
                        batchSql.AppendLine($"IF @AvailStock < @Qty_{i} BEGIN ROLLBACK TRANSACTION; SELECT -1 AS ResultStatus, 0 AS MissingId, @AvailStock AS AvailableStock, @Qty_{i} AS RequestedQty, @InsuffProduct AS ProductName; RETURN; END;");
                    }

                    batchSql.AppendLine(@"
                        INSERT INTO [dbo].[Sales] 
                            (UserId, SaleDate, TotalAmount, Discount, TaxAmount, FinalAmount, PaidAmount, ChangeAmount, PaymentMethod)
                        VALUES 
                            (@UserId, @SaleDate, @TotalAmount, @Discount, @TaxAmount, @FinalAmount, @PaidAmount, @ChangeAmount, @PaymentMethod);
                        DECLARE @NewSaleId INT = CAST(SCOPE_IDENTITY() AS INT);");

                    batchSql.AppendLine("INSERT INTO [dbo].[SaleDetails] (SaleId, ProductId, Quantity, UnitPrice, LineTotal) VALUES ");
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (i > 0) batchSql.Append(", ");
                        batchSql.Append($"(@NewSaleId, @PId_{i}, @Qty_{i}, @Price_{i}, @LineTotal_{i})");
                    }
                    batchSql.AppendLine(";");

                    for (int i = 0; i < items.Count; i++)
                    {
                        batchSql.AppendLine($"UPDATE [dbo].[Products] SET StockQuantity = StockQuantity - @Qty_{i} WHERE ProductId = @PId_{i};");
                    }

                    batchSql.AppendLine("COMMIT TRANSACTION;");
                    batchSql.AppendLine("SELECT @NewSaleId AS ResultStatus, 0 AS MissingId, 0 AS AvailableStock, 0 AS RequestedQty, '' AS ProductName;");

                    using (SqlCommand cmd = new SqlCommand(batchSql.ToString(), conn))
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = sale.UserId.HasValue ? (object)sale.UserId.Value : DBNull.Value;
                        cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime).Value = sale.SaleDate == default ? DateTime.Now : sale.SaleDate;
                        cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = sale.TotalAmount;
                        cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = sale.Discount;
                        cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal).Value = sale.TaxAmount;
                        cmd.Parameters.Add("@FinalAmount", SqlDbType.Decimal).Value = sale.FinalAmount;
                        cmd.Parameters.Add("@PaidAmount", SqlDbType.Decimal).Value = sale.PaidAmount;
                        cmd.Parameters.Add("@ChangeAmount", SqlDbType.Decimal).Value = sale.ChangeAmount;
                        cmd.Parameters.Add("@PaymentMethod", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(sale.PaymentMethod) ? "نقدي" : sale.PaymentMethod;

                        for (int i = 0; i < items.Count; i++)
                        {
                            cmd.Parameters.Add($"@PId_{i}", SqlDbType.Int).Value = items[i].ProductId;
                            cmd.Parameters.Add($"@Qty_{i}", SqlDbType.Int).Value = items[i].Quantity;
                            cmd.Parameters.Add($"@Price_{i}", SqlDbType.Decimal).Value = items[i].UnitPrice;
                            cmd.Parameters.Add($"@LineTotal_{i}", SqlDbType.Decimal).Value = items[i].LineTotal;
                        }

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            if (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                int status = Convert.ToInt32(reader["ResultStatus"]);
                                if (status > 0)
                                {
                                    InvalidateProductsCache();
                                    return (true, $"تم إتمام الفاتورة #{status:D5} وخصم المخزون بنجاح.", status);
                                }
                                else if (status == -1)
                                {
                                    string pName = reader["ProductName"].ToString();
                                    int avail = Convert.ToInt32(reader["AvailableStock"]);
                                    int req = Convert.ToInt32(reader["RequestedQty"]);
                                    return (false, $"الكمية غير متوفرة في المخزن للمنتج '{pName}'. المتاح: {avail}، المطلوب: {req}.", 0);
                                }
                                else
                                {
                                    int missingId = Convert.ToInt32(reader["MissingId"]);
                                    return (false, $"المنتج ذو الرقم {missingId} غير موجود في قاعدة البيانات.", 0);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "فشلت عملية البيع: " + ex.Message, 0);
            }
            return (false, "فشلت عملية البيع لسبب غير معروف.", 0);
        }

        public static (DateTime Start, DateTime End) GetDateRangeBoundaries(string dateFilter, DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate.HasValue && toDate.HasValue)
            {
                return (fromDate.Value.Date, toDate.Value.Date.AddDays(1).AddTicks(-1));
            }

            DateTime now = DateTime.Now;
            DateTime todayStart = now.Date;
            DateTime todayEnd = todayStart.AddDays(1).AddTicks(-1);

            if (dateFilter == "اليوم" || string.Equals(dateFilter, "Today", StringComparison.OrdinalIgnoreCase))
            {
                return (todayStart, todayEnd);
            }
            else if (dateFilter == "هذا الأسبوع" || dateFilter == "الاسبوع" || dateFilter == "الأسبوع" || string.Equals(dateFilter, "This Week", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "Week", StringComparison.OrdinalIgnoreCase))
            {
                return (now.AddDays(-7), now);
            }
            else if (dateFilter == "هذا الشهر" || dateFilter == "الشهر" || string.Equals(dateFilter, "This Month", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "Month", StringComparison.OrdinalIgnoreCase))
            {
                return (now.AddMonths(-1), now);
            }
            else
            {
                // All time
                return (new DateTime(2000, 1, 1), now.AddDays(1));
            }
        }

        public static DataTable GetAllSalesDataTable(string dateFilter = "اليوم", DateTime? fromDate = null, DateTime? toDate = null, string searchTerm = "")
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    var range = GetDateRangeBoundaries(dateFilter, fromDate, toDate);
                    bool isAllTime = dateFilter == "الكل" || dateFilter == "كل الفترات" || string.Equals(dateFilter, "All Time", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "All", StringComparison.OrdinalIgnoreCase);

                    StringBuilder query = new StringBuilder(@"
                        SELECT 
                            s.SaleId, 
                            s.SaleDate, 
                            ISNULL(u.FullName, N'مدير النظام') AS CashierName, 
                            s.TotalAmount, 
                            s.Discount, 
                            ISNULL(s.TaxAmount, 0) AS TaxAmount,
                            s.FinalAmount, 
                            ISNULL(s.TotalRefunded, 0) AS TotalRefunded,
                            (s.FinalAmount - ISNULL(s.TotalRefunded, 0)) AS NetFinalAmount,
                            s.PaidAmount, 
                            s.ChangeAmount, 
                            s.PaymentMethod,
                            ISNULL(s.ReturnStatus, N'مكتملة') AS ReturnStatus,
                            ISNULL(itemsSummary.ItemsCount, 0) AS ItemsCount
                        FROM [dbo].[Sales] s
                        LEFT JOIN [dbo].[Users] u ON s.UserId = u.UserId
                        LEFT JOIN (
                            SELECT SaleId, COUNT(1) AS ItemsCount
                            FROM [dbo].[SaleDetails]
                            GROUP BY SaleId
                        ) itemsSummary ON s.SaleId = itemsSummary.SaleId
                        WHERE 1=1 ");

                    if (!isAllTime)
                    {
                        query.Append(" AND s.SaleDate >= @FromDate AND s.SaleDate <= @ToDate ");
                    }

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        string cleanSearch = searchTerm.Trim().TrimStart('#');
                        if (int.TryParse(cleanSearch, out int searchId))
                        {
                            query.Append(" AND s.SaleId = @SearchId ");
                        }
                        else
                        {
                            query.Append(" AND CAST(s.SaleId AS NVARCHAR(20)) LIKE @Search ");
                        }
                    }

                    query.Append(" ORDER BY s.SaleId DESC");

                    using (SqlCommand cmd = new SqlCommand(query.ToString(), conn))
                    {
                        if (!isAllTime)
                        {
                            cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = range.Start;
                            cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = range.End;
                        }

                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            string cleanSearch = searchTerm.Trim().TrimStart('#');
                            if (int.TryParse(cleanSearch, out int searchId))
                            {
                                cmd.Parameters.Add("@SearchId", SqlDbType.Int).Value = searchId;
                            }
                            else
                            {
                                cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 100).Value = "%" + cleanSearch + "%";
                            }
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        public static async Task<DataTable> GetAllSalesDataTableAsync(string dateFilter = "اليوم", DateTime? fromDate = null, DateTime? toDate = null, string searchTerm = "")
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);

                    var range = GetDateRangeBoundaries(dateFilter, fromDate, toDate);
                    bool isAllTime = dateFilter == "الكل" || dateFilter == "كل الفترات" || string.Equals(dateFilter, "All Time", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "All", StringComparison.OrdinalIgnoreCase);

                    StringBuilder query = new StringBuilder(@"
                        SELECT 
                            s.SaleId, 
                            s.SaleDate, 
                            ISNULL(u.FullName, N'مدير النظام') AS CashierName, 
                            s.TotalAmount, 
                            s.Discount, 
                            ISNULL(s.TaxAmount, 0) AS TaxAmount,
                            s.FinalAmount, 
                            ISNULL(s.TotalRefunded, 0) AS TotalRefunded,
                            (s.FinalAmount - ISNULL(s.TotalRefunded, 0)) AS NetFinalAmount,
                            s.PaidAmount, 
                            s.ChangeAmount, 
                            s.PaymentMethod,
                            ISNULL(s.ReturnStatus, N'مكتملة') AS ReturnStatus,
                            ISNULL(itemsSummary.ItemsCount, 0) AS ItemsCount
                        FROM [dbo].[Sales] s
                        LEFT JOIN [dbo].[Users] u ON s.UserId = u.UserId
                        LEFT JOIN (
                            SELECT SaleId, COUNT(1) AS ItemsCount
                            FROM [dbo].[SaleDetails]
                            GROUP BY SaleId
                        ) itemsSummary ON s.SaleId = itemsSummary.SaleId
                        WHERE 1=1 ");

                    if (!isAllTime)
                    {
                        query.Append(" AND s.SaleDate >= @FromDate AND s.SaleDate <= @ToDate ");
                    }

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        string cleanSearch = searchTerm.Trim().TrimStart('#');
                        if (int.TryParse(cleanSearch, out int searchId))
                        {
                            query.Append(" AND s.SaleId = @SearchId ");
                        }
                        else
                        {
                            query.Append(" AND CAST(s.SaleId AS NVARCHAR(20)) LIKE @Search ");
                        }
                    }

                    query.Append(" ORDER BY s.SaleId DESC");

                    using (SqlCommand cmd = new SqlCommand(query.ToString(), conn))
                    {
                        if (!isAllTime)
                        {
                            cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = range.Start;
                            cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = range.End;
                        }

                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            string cleanSearch = searchTerm.Trim().TrimStart('#');
                            if (int.TryParse(cleanSearch, out int searchId))
                            {
                                cmd.Parameters.Add("@SearchId", SqlDbType.Int).Value = searchId;
                            }
                            else
                            {
                                cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 100).Value = "%" + cleanSearch + "%";
                            }
                        }

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        public static DataTable GetSaleDetailsDataTable(int saleId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            sd.DetailId,
                            sd.SaleId,
                            sd.ProductId,
                            p.Barcode,
                            p.ProductName,
                            sd.Quantity,
                            ISNULL(sd.ReturnedQuantity, 0) AS ReturnedQuantity,
                            (sd.Quantity - ISNULL(sd.ReturnedQuantity, 0)) AS ActiveQuantity,
                            sd.UnitPrice,
                            sd.LineTotal
                        FROM [dbo].[SaleDetails] sd
                        INNER JOIN [dbo].[Products] p ON sd.ProductId = p.ProductId
                        WHERE sd.SaleId = @SaleId
                        ORDER BY sd.DetailId ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@SaleId", SqlDbType.Int).Value = saleId;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        public static async Task<DataTable> GetSaleDetailsDataTableAsync(int saleId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = @"
                        SELECT 
                            sd.DetailId,
                            sd.SaleId,
                            sd.ProductId,
                            p.Barcode,
                            p.ProductName,
                            sd.Quantity,
                            ISNULL(sd.ReturnedQuantity, 0) AS ReturnedQuantity,
                            (sd.Quantity - ISNULL(sd.ReturnedQuantity, 0)) AS ActiveQuantity,
                            sd.UnitPrice,
                            sd.LineTotal
                        FROM [dbo].[SaleDetails] sd
                        INNER JOIN [dbo].[Products] p ON sd.ProductId = p.ProductId
                        WHERE sd.SaleId = @SaleId
                        ORDER BY sd.DetailId ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@SaleId", SqlDbType.Int).Value = saleId;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        public static List<ReturnItemModel> GetSaleDetailsForReturn(int saleId)
        {
            var list = new List<ReturnItemModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            sd.DetailId,
                            sd.ProductId,
                            p.Barcode,
                            p.ProductName,
                            sd.UnitPrice,
                            sd.Quantity AS OriginalQuantity,
                            ISNULL(sd.ReturnedQuantity, 0) AS AlreadyReturnedQuantity
                        FROM [dbo].[SaleDetails] sd
                        INNER JOIN [dbo].[Products] p ON sd.ProductId = p.ProductId
                        WHERE sd.SaleId = @SaleId
                        ORDER BY sd.DetailId ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@SaleId", SqlDbType.Int).Value = saleId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new ReturnItemModel
                                {
                                    DetailId = Convert.ToInt32(reader["DetailId"]),
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString(),
                                    ProductName = reader["ProductName"].ToString(),
                                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                    OriginalQuantity = Convert.ToInt32(reader["OriginalQuantity"]),
                                    AlreadyReturnedQuantity = Convert.ToInt32(reader["AlreadyReturnedQuantity"]),
                                    ReturnQuantity = 0
                                };
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch { }
            return list;
        }

        public static async Task<List<ReturnItemModel>> GetSaleDetailsForReturnAsync(int saleId)
        {
            var list = new List<ReturnItemModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = @"
                        SELECT 
                            sd.DetailId,
                            sd.ProductId,
                            p.Barcode,
                            p.ProductName,
                            sd.UnitPrice,
                            sd.Quantity AS OriginalQuantity,
                            ISNULL(sd.ReturnedQuantity, 0) AS AlreadyReturnedQuantity
                        FROM [dbo].[SaleDetails] sd
                        INNER JOIN [dbo].[Products] p ON sd.ProductId = p.ProductId
                        WHERE sd.SaleId = @SaleId
                        ORDER BY sd.DetailId ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@SaleId", SqlDbType.Int).Value = saleId;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                var item = new ReturnItemModel
                                {
                                    DetailId = Convert.ToInt32(reader["DetailId"]),
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString(),
                                    ProductName = reader["ProductName"].ToString(),
                                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                    OriginalQuantity = Convert.ToInt32(reader["OriginalQuantity"]),
                                    AlreadyReturnedQuantity = Convert.ToInt32(reader["AlreadyReturnedQuantity"]),
                                    ReturnQuantity = 0
                                };
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch { }
            return list;
        }

        public static (bool Success, string Message, int ReturnId) ProcessSaleReturnTransaction(int saleId, int? userId, string reason, List<ReturnItemModel> returnItems)
        {
            if (returnItems == null || returnItems.Count == 0)
                return (false, "لم يتم تحديد أي أصناف للإرجاع.", 0);

            var itemsToReturn = returnItems.FindAll(x => x.ReturnQuantity > 0);
            if (itemsToReturn.Count == 0)
                return (false, "يرجى تحديد كمية إرجاع أكبر من الصفر لصنف واحد على الأقل.", 0);

            decimal totalRefund = 0;
            foreach (var it in itemsToReturn)
            {
                if (it.ReturnQuantity > it.AvailableToReturn)
                {
                    return (false, $"كمية الإرجاع للصنف ({it.ProductName}) أكبر من الكمية المتاحة للإرجاع ({it.AvailableToReturn}).", 0);
                }
                totalRefund += it.RefundAmount;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    StringBuilder batchSql = new StringBuilder();
                    batchSql.AppendLine("BEGIN TRANSACTION;");
                    batchSql.AppendLine(@"
                        INSERT INTO [dbo].[SalesReturns] (SaleId, UserId, ReturnDate, TotalRefundAmount, Reason)
                        VALUES (@SaleId, @UserId, GETDATE(), @TotalRefundAmount, @Reason);
                        DECLARE @NewReturnId INT = CAST(SCOPE_IDENTITY() AS INT);");

                    for (int i = 0; i < itemsToReturn.Count; i++)
                    {
                        var it = itemsToReturn[i];
                        batchSql.AppendLine($@"
                            INSERT INTO [dbo].[SalesReturnDetails] (ReturnId, DetailId, ProductId, ReturnedQuantity, UnitPrice, RefundAmount)
                            VALUES (@NewReturnId, @DetId_{i}, @ProdId_{i}, @RetQty_{i}, @Price_{i}, @Refund_{i});

                            UPDATE [dbo].[SaleDetails]
                            SET ReturnedQuantity = ISNULL(ReturnedQuantity, 0) + @RetQty_{i}
                            WHERE DetailId = @DetId_{i};

                            UPDATE [dbo].[Products]
                            SET StockQuantity = StockQuantity + @RetQty_{i}
                            WHERE ProductId = @ProdId_{i};");
                    }

                    batchSql.AppendLine(@"
                        UPDATE [dbo].[Sales]
                        SET TotalRefunded = ISNULL(TotalRefunded, 0) + @TotalRefundAmount,
                            ReturnStatus = CASE 
                                WHEN (SELECT ISNULL(SUM(Quantity - ISNULL(ReturnedQuantity, 0)), 0) FROM [dbo].[SaleDetails] WHERE SaleId = @SaleId) <= 0 
                                THEN N'مرتجع بالكامل' 
                                ELSE N'مرتجع جزئي' 
                            END
                        WHERE SaleId = @SaleId;

                        COMMIT TRANSACTION;
                        SELECT @NewReturnId;");

                    using (SqlCommand cmd = new SqlCommand(batchSql.ToString(), conn))
                    {
                        cmd.Parameters.Add("@SaleId", SqlDbType.Int).Value = saleId;
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = (object)userId ?? DBNull.Value;
                        cmd.Parameters.Add("@TotalRefundAmount", SqlDbType.Decimal).Value = totalRefund;
                        cmd.Parameters.Add("@Reason", SqlDbType.NVarChar, 250).Value = (object)reason ?? DBNull.Value;

                        for (int i = 0; i < itemsToReturn.Count; i++)
                        {
                            var it = itemsToReturn[i];
                            cmd.Parameters.Add($"@DetId_{i}", SqlDbType.Int).Value = it.DetailId > 0 ? (object)it.DetailId : DBNull.Value;
                            cmd.Parameters.Add($"@ProdId_{i}", SqlDbType.Int).Value = it.ProductId;
                            cmd.Parameters.Add($"@RetQty_{i}", SqlDbType.Int).Value = it.ReturnQuantity;
                            cmd.Parameters.Add($"@Price_{i}", SqlDbType.Decimal).Value = it.UnitPrice;
                            cmd.Parameters.Add($"@Refund_{i}", SqlDbType.Decimal).Value = it.RefundAmount;
                        }

                        int returnId = Convert.ToInt32(cmd.ExecuteScalar());
                        InvalidateProductsCache();
                        return (true, "تمت عملية إرجاع الأصناف وإعادة البضاعة للمخزن واسترداد المبلغ بنجاح.", returnId);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "فشلت عملية الإرجاع: " + ex.Message, 0);
            }
        }

        public static SaleModel GetSaleById(int saleId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT s.SaleId, s.UserId, ISNULL(u.FullName, N'مدير النظام') AS CashierName, 
                               s.SaleDate, s.TotalAmount, s.Discount, ISNULL(s.TaxAmount, 0) AS TaxAmount, s.FinalAmount, 
                               ISNULL(s.TotalRefunded, 0) AS TotalRefunded,
                               ISNULL(s.ReturnStatus, N'مكتملة') AS ReturnStatus,
                               s.PaidAmount, s.ChangeAmount, s.PaymentMethod
                        FROM [dbo].[Sales] s
                        LEFT JOIN [dbo].[Users] u ON s.UserId = u.UserId
                        WHERE s.SaleId = @SaleId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@SaleId", SqlDbType.Int).Value = saleId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new SaleModel
                                {
                                    SaleId = Convert.ToInt32(reader["SaleId"]),
                                    UserId = reader["UserId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["UserId"]) : null,
                                    CashierName = reader["CashierName"].ToString(),
                                    SaleDate = Convert.ToDateTime(reader["SaleDate"]),
                                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                    Discount = Convert.ToDecimal(reader["Discount"]),
                                    TaxAmount = reader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TaxAmount"]) : 0.00m,
                                    FinalAmount = Convert.ToDecimal(reader["FinalAmount"]),
                                    TotalRefunded = Convert.ToDecimal(reader["TotalRefunded"]),
                                    ReturnStatus = reader["ReturnStatus"].ToString(),
                                    PaidAmount = Convert.ToDecimal(reader["PaidAmount"]),
                                    ChangeAmount = Convert.ToDecimal(reader["ChangeAmount"]),
                                    PaymentMethod = reader["PaymentMethod"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        #endregion

        #region Purchases Management & Transaction (Single Round-Trip Batched)

        public static (bool Success, string Message, int PurchaseId) ProcessPurchaseTransaction(PurchaseModel purchase, List<PurchaseDetailModel> items, bool updateBuyPrice = true)
        {
            if (purchase == null)
                return (false, "بيانات فاتورة الشراء مفقودة.", 0);

            if (items == null || items.Count == 0)
                return (false, "يجب إضافة صنف واحد على الأقل لفاتورة الشراء.", 0);

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    StringBuilder batchSql = new StringBuilder();
                    batchSql.AppendLine("BEGIN TRANSACTION;");
                    batchSql.AppendLine(@"
                        INSERT INTO [dbo].[Purchases] (SupplierId, PurchaseDate, TotalAmount, Notes)
                        VALUES (@SupplierId, @PurchaseDate, @TotalAmount, @Notes);
                        DECLARE @NewPurchaseId INT = CAST(SCOPE_IDENTITY() AS INT);");

                    batchSql.AppendLine("INSERT INTO [dbo].[PurchaseDetails] (PurchaseId, ProductId, Quantity, UnitPrice, LineTotal) VALUES ");
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (i > 0) batchSql.Append(", ");
                        batchSql.Append($"(@NewPurchaseId, @ProdId_{i}, @Qty_{i}, @Price_{i}, @LineTotal_{i})");
                    }
                    batchSql.AppendLine(";");

                    for (int i = 0; i < items.Count; i++)
                    {
                        batchSql.AppendLine($@"
                            UPDATE [dbo].[Products]
                            SET StockQuantity = StockQuantity + @Qty_{i},
                                BuyPrice = CASE WHEN @UpdateBuyPrice = 1 THEN @Price_{i} ELSE BuyPrice END
                            WHERE ProductId = @ProdId_{i};");
                    }

                    batchSql.AppendLine("COMMIT TRANSACTION;");
                    batchSql.AppendLine("SELECT @NewPurchaseId;");

                    using (SqlCommand cmd = new SqlCommand(batchSql.ToString(), conn))
                    {
                        cmd.Parameters.Add("@SupplierId", SqlDbType.Int).Value = purchase.SupplierId.HasValue && purchase.SupplierId.Value > 0 ? (object)purchase.SupplierId.Value : DBNull.Value;
                        cmd.Parameters.Add("@PurchaseDate", SqlDbType.DateTime).Value = purchase.PurchaseDate == default ? DateTime.Now : purchase.PurchaseDate;
                        cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = purchase.TotalAmount;
                        cmd.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = (object)purchase.Notes ?? DBNull.Value;
                        cmd.Parameters.Add("@UpdateBuyPrice", SqlDbType.Bit).Value = updateBuyPrice;

                        for (int i = 0; i < items.Count; i++)
                        {
                            cmd.Parameters.Add($"@ProdId_{i}", SqlDbType.Int).Value = items[i].ProductId;
                            cmd.Parameters.Add($"@Qty_{i}", SqlDbType.Int).Value = items[i].Quantity;
                            cmd.Parameters.Add($"@Price_{i}", SqlDbType.Decimal).Value = items[i].UnitPrice;
                            cmd.Parameters.Add($"@LineTotal_{i}", SqlDbType.Decimal).Value = items[i].LineTotal;
                        }

                        int purchaseId = Convert.ToInt32(cmd.ExecuteScalar());
                        InvalidateProductsCache();
                        return (true, $"تم حفظ فاتورة المشتريات #{purchaseId:D5} وزيادة رصيد المخزون بنجاح.", purchaseId);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "فشلت عملية حفظ فاتورة الشراء: " + ex.Message, 0);
            }
        }

        public static DataTable GetAllPurchasesDataTable()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            p.PurchaseId, 
                            p.PurchaseDate, 
                            ISNULL(s.SupplierName, N'مورد عام / نقدي') AS SupplierName, 
                            p.TotalAmount, 
                            p.Notes,
                            ISNULL(itemSummary.ItemsCount, 0) AS ItemsCount
                        FROM [dbo].[Purchases] p
                        LEFT JOIN [dbo].[Suppliers] s ON p.SupplierId = s.SupplierId
                        LEFT JOIN (
                            SELECT PurchaseId, COUNT(1) AS ItemsCount
                            FROM [dbo].[PurchaseDetails]
                            GROUP BY PurchaseId
                        ) itemSummary ON p.PurchaseId = itemSummary.PurchaseId
                        ORDER BY p.PurchaseId DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch { }
            return dt;
        }

        public static async Task<DataTable> GetAllPurchasesDataTableAsync()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = @"
                        SELECT 
                            p.PurchaseId, 
                            p.PurchaseDate, 
                            ISNULL(s.SupplierName, N'مورد عام / نقدي') AS SupplierName, 
                            p.TotalAmount, 
                            p.Notes,
                            ISNULL(itemSummary.ItemsCount, 0) AS ItemsCount
                        FROM [dbo].[Purchases] p
                        LEFT JOIN [dbo].[Suppliers] s ON p.SupplierId = s.SupplierId
                        LEFT JOIN (
                            SELECT PurchaseId, COUNT(1) AS ItemsCount
                            FROM [dbo].[PurchaseDetails]
                            GROUP BY PurchaseId
                        ) itemSummary ON p.PurchaseId = itemSummary.PurchaseId
                        ORDER BY p.PurchaseId DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch { }
            return dt;
        }

        public static DataTable GetPurchaseDetailsDataTable(int purchaseId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            pd.DetailId,
                            pd.PurchaseId,
                            pd.ProductId,
                            p.Barcode,
                            p.ProductName,
                            pd.Quantity,
                            pd.UnitPrice,
                            pd.LineTotal
                        FROM [dbo].[PurchaseDetails] pd
                        INNER JOIN [dbo].[Products] p ON pd.ProductId = p.ProductId
                        WHERE pd.PurchaseId = @PurchaseId
                        ORDER BY pd.DetailId ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@PurchaseId", SqlDbType.Int).Value = purchaseId;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        public static async Task<DataTable> GetPurchaseDetailsDataTableAsync(int purchaseId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = @"
                        SELECT 
                            pd.DetailId,
                            pd.PurchaseId,
                            pd.ProductId,
                            p.Barcode,
                            p.ProductName,
                            pd.Quantity,
                            pd.UnitPrice,
                            pd.LineTotal
                        FROM [dbo].[PurchaseDetails] pd
                        INNER JOIN [dbo].[Products] p ON pd.ProductId = p.ProductId
                        WHERE pd.PurchaseId = @PurchaseId
                        ORDER BY pd.DetailId ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@PurchaseId", SqlDbType.Int).Value = purchaseId;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        #endregion

        #region Executive Dashboard Analytics (Consolidated Single Round-Trip Batch)

        public static DashboardStatsModel GetDashboardKPIs(string dateFilter = "الأسبوع")
        {
            DashboardStatsModel stats = new DashboardStatsModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    var range = GetDateRangeBoundaries(dateFilter, null, null);
                    bool isAllTime = dateFilter == "الكل" || dateFilter == "كل الفترات" || string.Equals(dateFilter, "All Time", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "All", StringComparison.OrdinalIgnoreCase);

                    string salesDateCond = isAllTime ? "1=1" : "s.SaleDate >= @FromDate AND s.SaleDate <= @ToDate";
                    string purchasesDateCond = isAllTime ? "1=1" : "p.PurchaseDate >= @FromDate AND p.PurchaseDate <= @ToDate";

                    string batchSql = $@"
                        -- 1. Sales KPI
                        SELECT 
                            ISNULL(SUM(s.FinalAmount - ISNULL(s.TotalRefunded, 0)), 0) AS TotalRevenue,
                            COUNT(1) AS TotalTransactions
                        FROM [dbo].[Sales] s
                        WHERE {salesDateCond};

                        -- 2. Purchases KPI
                        SELECT 
                            ISNULL(SUM(p.TotalAmount), 0) AS TotalPurchases,
                            COUNT(1) AS TotalPurchaseInvoices
                        FROM [dbo].[Purchases] p
                        WHERE {purchasesDateCond};

                        -- 3. COGS
                        SELECT 
                            ISNULL(SUM((sd.Quantity - ISNULL(sd.ReturnedQuantity, 0)) * ISNULL(pr.BuyPrice, 0)), 0) AS CostOfGoodsSold
                        FROM [dbo].[SaleDetails] sd
                        INNER JOIN [dbo].[Sales] s ON sd.SaleId = s.SaleId
                        INNER JOIN [dbo].[Products] pr ON sd.ProductId = pr.ProductId
                        WHERE {salesDateCond};

                        -- 4. Inventory Valuation
                        SELECT 
                            ISNULL(SUM(StockQuantity), 0) AS TotalUnitsInStock,
                            ISNULL(SUM(StockQuantity * BuyPrice), 0) AS InventoryCostValue,
                            ISNULL(SUM(StockQuantity * SellPrice), 0) AS InventorySellValue,
                            COUNT(CASE WHEN StockQuantity <= MinStockAlert THEN 1 END) AS LowStockCount
                        FROM [dbo].[Products];

                        -- 5. Active Cashiers
                        SELECT COUNT(1) AS ActiveCashiers FROM [dbo].[Users] WHERE IsActive = 1;
                    ";

                    using (SqlCommand cmd = new SqlCommand(batchSql, conn))
                    {
                        if (!isAllTime)
                        {
                            cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = range.Start;
                            cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = range.End;
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Result 1: Sales
                            if (reader.Read())
                            {
                                stats.TotalSalesRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                                stats.TotalTransactionsCount = Convert.ToInt32(reader["TotalTransactions"]);
                            }

                            // Result 2: Purchases
                            if (reader.NextResult() && reader.Read())
                            {
                                stats.TotalPurchasesAmount = Convert.ToDecimal(reader["TotalPurchases"]);
                                stats.TotalPurchaseInvoicesCount = Convert.ToInt32(reader["TotalPurchaseInvoices"]);
                            }

                            // Result 3: COGS
                            if (reader.NextResult() && reader.Read())
                            {
                                stats.CostOfGoodsSold = Convert.ToDecimal(reader["CostOfGoodsSold"]);
                            }

                            // Result 4: Inventory
                            if (reader.NextResult() && reader.Read())
                            {
                                stats.TotalProductsInStock = Convert.ToInt32(reader["TotalUnitsInStock"]);
                                stats.InventoryCostValue = Convert.ToDecimal(reader["InventoryCostValue"]);
                                stats.InventorySellValue = Convert.ToDecimal(reader["InventorySellValue"]);
                                stats.LowStockItemsCount = Convert.ToInt32(reader["LowStockCount"]);
                            }

                            // Result 5: Cashiers
                            if (reader.NextResult() && reader.Read())
                            {
                                stats.ActiveCashiersCount = Convert.ToInt32(reader["ActiveCashiers"]);
                            }
                        }
                    }

                    stats.NetProfit = stats.TotalSalesRevenue - stats.CostOfGoodsSold;
                    stats.ProfitMarginPct = stats.TotalSalesRevenue > 0
                        ? Math.Round((stats.NetProfit / stats.TotalSalesRevenue) * 100m, 1)
                        : 0;
                }
            }
            catch { }
            return stats;
        }

        public static async Task<DashboardStatsModel> GetDashboardKPIsAsync(string dateFilter = "الأسبوع")
        {
            DashboardStatsModel stats = new DashboardStatsModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    var range = GetDateRangeBoundaries(dateFilter, null, null);
                    bool isAllTime = dateFilter == "الكل" || dateFilter == "كل الفترات" || string.Equals(dateFilter, "All Time", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "All", StringComparison.OrdinalIgnoreCase);

                    string salesDateCond = isAllTime ? "1=1" : "s.SaleDate >= @FromDate AND s.SaleDate <= @ToDate";
                    string purchasesDateCond = isAllTime ? "1=1" : "p.PurchaseDate >= @FromDate AND p.PurchaseDate <= @ToDate";

                    string batchSql = $@"
                        -- 1. Sales KPI
                        SELECT 
                            ISNULL(SUM(s.FinalAmount - ISNULL(s.TotalRefunded, 0)), 0) AS TotalRevenue,
                            COUNT(1) AS TotalTransactions
                        FROM [dbo].[Sales] s
                        WHERE {salesDateCond};

                        -- 2. Purchases KPI
                        SELECT 
                            ISNULL(SUM(p.TotalAmount), 0) AS TotalPurchases,
                            COUNT(1) AS TotalPurchaseInvoices
                        FROM [dbo].[Purchases] p
                        WHERE {purchasesDateCond};

                        -- 3. COGS
                        SELECT 
                            ISNULL(SUM((sd.Quantity - ISNULL(sd.ReturnedQuantity, 0)) * ISNULL(pr.BuyPrice, 0)), 0) AS CostOfGoodsSold
                        FROM [dbo].[SaleDetails] sd
                        INNER JOIN [dbo].[Sales] s ON sd.SaleId = s.SaleId
                        INNER JOIN [dbo].[Products] pr ON sd.ProductId = pr.ProductId
                        WHERE {salesDateCond};

                        -- 4. Inventory Valuation
                        SELECT 
                            ISNULL(SUM(StockQuantity), 0) AS TotalUnitsInStock,
                            ISNULL(SUM(StockQuantity * BuyPrice), 0) AS InventoryCostValue,
                            ISNULL(SUM(StockQuantity * SellPrice), 0) AS InventorySellValue,
                            COUNT(CASE WHEN StockQuantity <= MinStockAlert THEN 1 END) AS LowStockCount
                        FROM [dbo].[Products];

                        -- 5. Active Cashiers
                        SELECT COUNT(1) AS ActiveCashiers FROM [dbo].[Users] WHERE IsActive = 1;
                    ";

                    using (SqlCommand cmd = new SqlCommand(batchSql, conn))
                    {
                        if (!isAllTime)
                        {
                            cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = range.Start;
                            cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = range.End;
                        }

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            // Result 1: Sales
                            if (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                stats.TotalSalesRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                                stats.TotalTransactionsCount = Convert.ToInt32(reader["TotalTransactions"]);
                            }

                            // Result 2: Purchases
                            if (await reader.NextResultAsync().ConfigureAwait(false) && await reader.ReadAsync().ConfigureAwait(false))
                            {
                                stats.TotalPurchasesAmount = Convert.ToDecimal(reader["TotalPurchases"]);
                                stats.TotalPurchaseInvoicesCount = Convert.ToInt32(reader["TotalPurchaseInvoices"]);
                            }

                            // Result 3: COGS
                            if (await reader.NextResultAsync().ConfigureAwait(false) && await reader.ReadAsync().ConfigureAwait(false))
                            {
                                stats.CostOfGoodsSold = Convert.ToDecimal(reader["CostOfGoodsSold"]);
                            }

                            // Result 4: Inventory
                            if (await reader.NextResultAsync().ConfigureAwait(false) && await reader.ReadAsync().ConfigureAwait(false))
                            {
                                stats.TotalProductsInStock = Convert.ToInt32(reader["TotalUnitsInStock"]);
                                stats.InventoryCostValue = Convert.ToDecimal(reader["InventoryCostValue"]);
                                stats.InventorySellValue = Convert.ToDecimal(reader["InventorySellValue"]);
                                stats.LowStockItemsCount = Convert.ToInt32(reader["LowStockCount"]);
                            }

                            // Result 5: Cashiers
                            if (await reader.NextResultAsync().ConfigureAwait(false) && await reader.ReadAsync().ConfigureAwait(false))
                            {
                                stats.ActiveCashiersCount = Convert.ToInt32(reader["ActiveCashiers"]);
                            }
                        }
                    }

                    stats.NetProfit = stats.TotalSalesRevenue - stats.CostOfGoodsSold;
                    stats.ProfitMarginPct = stats.TotalSalesRevenue > 0
                        ? Math.Round((stats.NetProfit / stats.TotalSalesRevenue) * 100m, 1)
                        : 0;
                }
            }
            catch { }
            return stats;
        }

        public static DataTable GetTopSellingProducts(int topN = 5, string dateFilter = "الأسبوع")
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    var range = GetDateRangeBoundaries(dateFilter, null, null);
                    bool isAllTime = dateFilter == "الكل" || dateFilter == "كل الفترات" || string.Equals(dateFilter, "All Time", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "All", StringComparison.OrdinalIgnoreCase);

                    string dateCondition = isAllTime ? "1=1" : "s.SaleDate >= @FromDate AND s.SaleDate <= @ToDate";

                    string query = $@"
                        SELECT TOP ({topN})
                            p.ProductName,
                            p.Barcode,
                            ISNULL(c.CategoryName, N'عام') AS CategoryName,
                            SUM(sd.Quantity) AS UnitsSold,
                            SUM(sd.LineTotal) AS TotalRevenue
                        FROM [dbo].[SaleDetails] sd
                        INNER JOIN [dbo].[Sales] s ON sd.SaleId = s.SaleId
                        INNER JOIN [dbo].[Products] p ON sd.ProductId = p.ProductId
                        LEFT JOIN [dbo].[Categories] c ON p.CategoryId = c.CategoryId
                        WHERE {dateCondition}
                        GROUP BY p.ProductName, p.Barcode, c.CategoryName
                        ORDER BY UnitsSold DESC, TotalRevenue DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!isAllTime)
                        {
                            cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = range.Start;
                            cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = range.End;
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        public static async Task<DataTable> GetTopSellingProductsAsync(int topN = 5, string dateFilter = "الأسبوع")
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);

                    var range = GetDateRangeBoundaries(dateFilter, null, null);
                    bool isAllTime = dateFilter == "الكل" || dateFilter == "كل الفترات" || string.Equals(dateFilter, "All Time", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "All", StringComparison.OrdinalIgnoreCase);

                    string dateCondition = isAllTime ? "1=1" : "s.SaleDate >= @FromDate AND s.SaleDate <= @ToDate";

                    string query = $@"
                        SELECT TOP ({topN})
                            p.ProductName,
                            p.Barcode,
                            ISNULL(c.CategoryName, N'عام') AS CategoryName,
                            SUM(sd.Quantity) AS UnitsSold,
                            SUM(sd.LineTotal) AS TotalRevenue
                        FROM [dbo].[SaleDetails] sd
                        INNER JOIN [dbo].[Sales] s ON sd.SaleId = s.SaleId
                        INNER JOIN [dbo].[Products] p ON sd.ProductId = p.ProductId
                        LEFT JOIN [dbo].[Categories] c ON p.CategoryId = c.CategoryId
                        WHERE {dateCondition}
                        GROUP BY p.ProductName, p.Barcode, c.CategoryName
                        ORDER BY UnitsSold DESC, TotalRevenue DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!isAllTime)
                        {
                            cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = range.Start;
                            cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = range.End;
                        }

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        public static DataTable GetRecentTransactions(int topN = 10)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = $@"
                        SELECT TOP ({topN})
                            s.SaleId,
                            s.SaleDate,
                            ISNULL(u.FullName, N'مدير النظام') AS Cashier,
                            s.FinalAmount,
                            s.PaymentMethod,
                            ISNULL(itemSummary.ItemsCount, 0) AS ItemsCount
                        FROM [dbo].[Sales] s
                        LEFT JOIN [dbo].[Users] u ON s.UserId = u.UserId
                        LEFT JOIN (
                            SELECT SaleId, COUNT(1) AS ItemsCount
                            FROM [dbo].[SaleDetails]
                            GROUP BY SaleId
                        ) itemSummary ON s.SaleId = itemSummary.SaleId
                        ORDER BY s.SaleId DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch { }
            return dt;
        }

        public static async Task<DataTable> GetRecentTransactionsAsync(int topN = 10)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = $@"
                        SELECT TOP ({topN})
                            s.SaleId,
                            s.SaleDate,
                            ISNULL(u.FullName, N'مدير النظام') AS Cashier,
                            s.FinalAmount,
                            s.PaymentMethod,
                            ISNULL(itemSummary.ItemsCount, 0) AS ItemsCount
                        FROM [dbo].[Sales] s
                        LEFT JOIN [dbo].[Users] u ON s.UserId = u.UserId
                        LEFT JOIN (
                            SELECT SaleId, COUNT(1) AS ItemsCount
                            FROM [dbo].[SaleDetails]
                            GROUP BY SaleId
                        ) itemSummary ON s.SaleId = itemSummary.SaleId
                        ORDER BY s.SaleId DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch { }
            return dt;
        }

        public static DataTable GetUrgentLowStockProducts(int topN = 10)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = $@"
                        SELECT TOP ({topN})
                            p.ProductId,
                            p.Barcode,
                            p.ProductName,
                            ISNULL(c.CategoryName, N'عام') AS CategoryName,
                            p.StockQuantity,
                            p.MinStockAlert,
                            p.BuyPrice,
                            p.SellPrice
                        FROM [dbo].[Products] p
                        LEFT JOIN [dbo].[Categories] c ON p.CategoryId = c.CategoryId
                        WHERE p.StockQuantity <= p.MinStockAlert
                        ORDER BY p.StockQuantity ASC, p.ProductName ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch { }
            return dt;
        }

        public static async Task<DataTable> GetUrgentLowStockProductsAsync(int topN = 10)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = $@"
                        SELECT TOP ({topN})
                            p.ProductId,
                            p.Barcode,
                            p.ProductName,
                            ISNULL(c.CategoryName, N'عام') AS CategoryName,
                            p.StockQuantity,
                            p.MinStockAlert,
                            p.BuyPrice,
                            p.SellPrice
                        FROM [dbo].[Products] p
                        LEFT JOIN [dbo].[Categories] c ON p.CategoryId = c.CategoryId
                        WHERE p.StockQuantity <= p.MinStockAlert
                        ORDER BY p.StockQuantity ASC, p.ProductName ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch { }
            return dt;
        }

        #endregion

        #region System Settings & Database Management (Cached)

        public static SystemSettingsModel GetSystemSettings()
        {
            lock (_settingsLock)
            {
                if (_cachedSettings != null)
                    return _cachedSettings;
            }

            var settings = new SystemSettingsModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "SELECT SettingKey, SettingValue FROM [dbo].[SystemSettings]";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        while (reader.Read())
                        {
                            string k = reader["SettingKey"].ToString();
                            string v = reader["SettingValue"] != DBNull.Value ? reader["SettingValue"].ToString() : "";
                            dict[k] = v;
                        }

                        if (dict.TryGetValue("StoreName", out var storeName) && !string.IsNullOrWhiteSpace(storeName))
                            settings.StoreName = storeName;
                        if (dict.TryGetValue("StoreSubtitle", out var storeSub) && !string.IsNullOrWhiteSpace(storeSub))
                            settings.StoreSubtitle = storeSub;
                        if (dict.TryGetValue("StorePhone", out var storePhone))
                            settings.StorePhone = storePhone;
                        if (dict.TryGetValue("StoreAddress", out var storeAddr))
                            settings.StoreAddress = storeAddr;
                        if (dict.TryGetValue("TaxNumber", out var taxNum))
                            settings.TaxNumber = taxNum;
                        if (dict.TryGetValue("ReceiptHeader", out var rHeader) && !string.IsNullOrWhiteSpace(rHeader))
                            settings.ReceiptHeader = rHeader;
                        if (dict.TryGetValue("ReceiptFooter", out var rFooter) && !string.IsNullOrWhiteSpace(rFooter))
                            settings.ReceiptFooter = rFooter;
                        if (dict.TryGetValue("CurrencySymbol", out var curr) && !string.IsNullOrWhiteSpace(curr))
                            settings.CurrencySymbol = curr;
                        if (dict.TryGetValue("VatRate", out var vatStr) && decimal.TryParse(vatStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var vat))
                            settings.VatRate = vat;
                        if (dict.TryGetValue("DefaultMinStock", out var minStockStr) && int.TryParse(minStockStr, out var minStock))
                            settings.DefaultMinStock = minStock;
                        if (dict.TryGetValue("EnablePrintPreview", out var prevStr) && bool.TryParse(prevStr, out var prev))
                            settings.EnablePrintPreview = prev;
                        if (dict.TryGetValue("AutoPrintOnSale", out var autoStr) && bool.TryParse(autoStr, out var autoP))
                            settings.AutoPrintOnSale = autoP;
                        if (dict.TryGetValue("AllowNegativeStock", out var negStr) && bool.TryParse(negStr, out var neg))
                            settings.AllowNegativeStock = neg;
                    }
                }

                lock (_settingsLock)
                {
                    _cachedSettings = settings;
                }
            }
            catch { }
            return settings;
        }

        public static async Task<SystemSettingsModel> GetSystemSettingsAsync()
        {
            lock (_settingsLock)
            {
                if (_cachedSettings != null)
                    return _cachedSettings;
            }

            var settings = new SystemSettingsModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = "SELECT SettingKey, SettingValue FROM [dbo].[SystemSettings]";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            string k = reader["SettingKey"].ToString();
                            string v = reader["SettingValue"] != DBNull.Value ? reader["SettingValue"].ToString() : "";
                            dict[k] = v;
                        }

                        if (dict.TryGetValue("StoreName", out var storeName) && !string.IsNullOrWhiteSpace(storeName))
                            settings.StoreName = storeName;
                        if (dict.TryGetValue("StoreSubtitle", out var storeSub) && !string.IsNullOrWhiteSpace(storeSub))
                            settings.StoreSubtitle = storeSub;
                        if (dict.TryGetValue("StorePhone", out var storePhone))
                            settings.StorePhone = storePhone;
                        if (dict.TryGetValue("StoreAddress", out var storeAddr))
                            settings.StoreAddress = storeAddr;
                        if (dict.TryGetValue("TaxNumber", out var taxNum))
                            settings.TaxNumber = taxNum;
                        if (dict.TryGetValue("ReceiptHeader", out var rHeader) && !string.IsNullOrWhiteSpace(rHeader))
                            settings.ReceiptHeader = rHeader;
                        if (dict.TryGetValue("ReceiptFooter", out var rFooter) && !string.IsNullOrWhiteSpace(rFooter))
                            settings.ReceiptFooter = rFooter;
                        if (dict.TryGetValue("CurrencySymbol", out var curr) && !string.IsNullOrWhiteSpace(curr))
                            settings.CurrencySymbol = curr;
                        if (dict.TryGetValue("VatRate", out var vatStr) && decimal.TryParse(vatStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var vat))
                            settings.VatRate = vat;
                        if (dict.TryGetValue("DefaultMinStock", out var minStockStr) && int.TryParse(minStockStr, out var minStock))
                            settings.DefaultMinStock = minStock;
                        if (dict.TryGetValue("EnablePrintPreview", out var prevStr) && bool.TryParse(prevStr, out var prev))
                            settings.EnablePrintPreview = prev;
                        if (dict.TryGetValue("AutoPrintOnSale", out var autoStr) && bool.TryParse(autoStr, out var autoP))
                            settings.AutoPrintOnSale = autoP;
                        if (dict.TryGetValue("AllowNegativeStock", out var negStr) && bool.TryParse(negStr, out var neg))
                            settings.AllowNegativeStock = neg;
                    }
                }

                lock (_settingsLock)
                {
                    _cachedSettings = settings;
                }
            }
            catch { }
            return settings;
        }

        public static (bool Success, string Message) SaveSystemSettings(SystemSettingsModel settings)
        {
            if (settings == null) return (false, "بيانات الإعدادات غير صالحة.");
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    var pairs = new Dictionary<string, string>
                    {
                        { "StoreName", settings.StoreName ?? "" },
                        { "StoreSubtitle", settings.StoreSubtitle ?? "" },
                        { "StorePhone", settings.StorePhone ?? "" },
                        { "StoreAddress", settings.StoreAddress ?? "" },
                        { "TaxNumber", settings.TaxNumber ?? "" },
                        { "ReceiptHeader", settings.ReceiptHeader ?? "" },
                        { "ReceiptFooter", settings.ReceiptFooter ?? "" },
                        { "CurrencySymbol", settings.CurrencySymbol ?? "ج.م" },
                        { "VatRate", settings.VatRate.ToString(CultureInfo.InvariantCulture) },
                        { "DefaultMinStock", settings.DefaultMinStock.ToString() },
                        { "EnablePrintPreview", settings.EnablePrintPreview.ToString() },
                        { "AutoPrintOnSale", settings.AutoPrintOnSale.ToString() },
                        { "AllowNegativeStock", settings.AllowNegativeStock.ToString() }
                    };

                    StringBuilder batchSql = new StringBuilder();
                    batchSql.AppendLine("BEGIN TRANSACTION;");

                    int idx = 0;
                    foreach (var kvp in pairs)
                    {
                        batchSql.AppendLine($@"
                            IF EXISTS (SELECT 1 FROM [dbo].[SystemSettings] WHERE SettingKey = @Key_{idx})
                                UPDATE [dbo].[SystemSettings] SET SettingValue = @Val_{idx} WHERE SettingKey = @Key_{idx}
                            ELSE
                                INSERT INTO [dbo].[SystemSettings] (SettingKey, SettingValue) VALUES (@Key_{idx}, @Val_{idx});");
                        idx++;
                    }

                    batchSql.AppendLine("COMMIT TRANSACTION;");

                    using (SqlCommand cmd = new SqlCommand(batchSql.ToString(), conn))
                    {
                        idx = 0;
                        foreach (var kvp in pairs)
                        {
                            cmd.Parameters.Add($"@Key_{idx}", SqlDbType.NVarChar, 50).Value = kvp.Key;
                            cmd.Parameters.Add($"@Val_{idx}", SqlDbType.NVarChar, -1).Value = kvp.Value;
                            idx++;
                        }

                        cmd.ExecuteNonQuery();
                    }

                    lock (_settingsLock)
                    {
                        _cachedSettings = settings;
                    }

                    return (true, "تم حفظ وتحديث إعدادات النظام بنجاح.");
                }
            }
            catch (Exception ex)
            {
                return (false, "خطأ بالاتصال بقاعدة البيانات: " + ex.Message);
            }
        }

        public static (bool Success, string Message) BackupDatabase(string backupFilePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(backupFilePath))
                    return (false, "يرجى تحديد مسار صالح لحفظ ملف النسخة الاحتياطية.");

                string dir = Path.GetDirectoryName(backupFilePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        BACKUP DATABASE [POS_DB] 
                        TO DISK = @BackupPath 
                        WITH FORMAT, INIT, NAME = N'POS_DB Full Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.Add("@BackupPath", SqlDbType.NVarChar).Value = backupFilePath;
                        cmd.ExecuteNonQuery();
                    }
                }
                return (true, $"تم إنشاء النسخة الاحتياطية بنجاح وحفظها في:\n{backupFilePath}");
            }
            catch (Exception ex)
            {
                return (false, "فشل إنشاء النسخة الاحتياطية: " + ex.Message);
            }
        }

        public static (bool Success, string Message) RestoreDatabase(string backupFilePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(backupFilePath) || !File.Exists(backupFilePath))
                    return (false, "ملف النسخة الاحتياطية المحدد غير موجود.");

                using (SqlConnection conn = new SqlConnection(DefaultMasterConnectionString))
                {
                    conn.Open();
                    string query = @"
                        ALTER DATABASE [POS_DB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                        RESTORE DATABASE [POS_DB] FROM DISK = @BackupPath WITH REPLACE;
                        ALTER DATABASE [POS_DB] SET MULTI_USER;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandTimeout = 180;
                        cmd.Parameters.Add("@BackupPath", SqlDbType.NVarChar).Value = backupFilePath;
                        cmd.ExecuteNonQuery();
                    }
                }

                InvalidateSettingsCache();
                InvalidateCategoriesCache();
                InvalidateProductsCache();

                return (true, "تمت استعادة قاعدة البيانات بنجاح من النسخة الاحتياطية!");
            }
            catch (Exception ex)
            {
                return (false, "فشلت استعادة قاعدة البيانات: " + ex.Message);
            }
        }

        public static (bool Success, string Message) ClearTransactionHistory(string adminUsername, string adminPassword)
        {
            try
            {
                var auth = Authenticate(adminUsername, adminPassword);
                if (!auth.Success || auth.User == null || (!string.Equals(auth.User.Role, "Admin", StringComparison.OrdinalIgnoreCase) && auth.User.Role != "مدير"))
                {
                    return (false, "كلمة مرور المشرف غير صحيحة أو ليس لديك صلاحية مدير النظام.");
                }

                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string sql = @"
                        BEGIN TRANSACTION;
                        DELETE FROM [dbo].[SalesReturnDetails];
                        DELETE FROM [dbo].[SalesReturns];
                        DELETE FROM [dbo].[SaleDetails];
                        DELETE FROM [dbo].[Sales];
                        DELETE FROM [dbo].[PurchaseDetails];
                        DELETE FROM [dbo].[Purchases];
                        DBCC CHECKIDENT ('[dbo].[Sales]', RESEED, 0);
                        DBCC CHECKIDENT ('[dbo].[SaleDetails]', RESEED, 0);
                        DBCC CHECKIDENT ('[dbo].[Purchases]', RESEED, 0);
                        DBCC CHECKIDENT ('[dbo].[PurchaseDetails]', RESEED, 0);
                        DBCC CHECKIDENT ('[dbo].[SalesReturns]', RESEED, 0);
                        DBCC CHECKIDENT ('[dbo].[SalesReturnDetails]', RESEED, 0);
                        COMMIT TRANSACTION;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InvalidateProductsCache();
                    return (true, "تم تصفير سجلات المبيعات والمشتريات والمرتجعات بالكامل وبدء الترقيم من 1.");
                }
            }
            catch (Exception ex)
            {
                return (false, "خطأ أثناء التنفيذ: " + ex.Message);
            }
        }

        #endregion

        #region Shift / Attendance Management (الورديات والحضور والانصراف)

        /// <summary>
        /// تسجيل حضور موظف (بداية وردية جديدة)
        /// </summary>
        public static (bool Success, string Message, int ShiftId) ClockIn(int userId, string notes = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    string checkQuery = @"SELECT COUNT(1) FROM [dbo].[Shifts] WHERE UserId = @UserId AND ClockOutTime IS NULL";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        int openShifts = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (openShifts > 0)
                            return (false, "هذا الموظف لديه وردية مفتوحة بالفعل. يرجى تسجيل الانصراف أولاً.", 0);
                    }

                    string insertQuery = @"INSERT INTO [dbo].[Shifts] (UserId, ClockInTime, Notes) 
                                           VALUES (@UserId, GETDATE(), @Notes);
                                           SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = (object)notes ?? DBNull.Value;
                        int shiftId = Convert.ToInt32(cmd.ExecuteScalar());
                        return (true, "تم تسجيل الحضور بنجاح.", shiftId);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "خطأ في تسجيل الحضور: " + ex.Message, 0);
            }
        }

        /// <summary>
        /// تسجيل انصراف موظف (إغلاق الوردية المفتوحة)
        /// </summary>
        public static (bool Success, string Message) ClockOut(int userId, string notes = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"UPDATE [dbo].[Shifts] SET ClockOutTime = GETDATE(), 
                                     Notes = CASE WHEN @Notes IS NOT NULL THEN ISNULL(Notes + N' | ', N'') + @Notes ELSE Notes END
                                     WHERE UserId = @UserId AND ClockOutTime IS NULL";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = (object)notes ?? DBNull.Value;
                        int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                            return (false, "لا توجد وردية مفتوحة لهذا الموظف.");
                        return (true, "تم تسجيل الانصراف بنجاح.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "خطأ في تسجيل الانصراف: " + ex.Message);
            }
        }

        /// <summary>
        /// التحقق مما إذا كان الموظف لديه وردية مفتوحة حالياً
        /// </summary>
        public static ShiftModel GetActiveShift(int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"SELECT s.ShiftId, s.UserId, u.FullName, u.Username, s.ClockInTime, s.ClockOutTime, s.Notes
                                     FROM [dbo].[Shifts] s
                                     INNER JOIN [dbo].[Users] u ON s.UserId = u.UserId
                                     WHERE s.UserId = @UserId AND s.ClockOutTime IS NULL";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new ShiftModel
                                {
                                    ShiftId = Convert.ToInt32(reader["ShiftId"]),
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    FullName = reader["FullName"].ToString(),
                                    Username = reader["Username"].ToString(),
                                    ClockInTime = Convert.ToDateTime(reader["ClockInTime"]),
                                    ClockOutTime = null,
                                    Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null
                                };
                            }
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// جلب سجل الورديات مع فلتر حسب الموظف والتاريخ
        /// </summary>
        public static async Task<DataTable> GetShiftsAsync(int? userIdFilter = null, DateTime? dateFrom = null, DateTime? dateTo = null, string searchTerm = "")
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = @"
                        SELECT 
                            s.ShiftId,
                            s.UserId,
                            u.FullName,
                            u.Username,
                            s.ClockInTime,
                            s.ClockOutTime,
                            CASE 
                                WHEN s.ClockOutTime IS NOT NULL THEN 
                                    RIGHT('0' + CAST(DATEDIFF(HOUR, s.ClockInTime, s.ClockOutTime) AS NVARCHAR), 2) + N':' + 
                                    RIGHT('0' + CAST(DATEDIFF(MINUTE, s.ClockInTime, s.ClockOutTime) % 60 AS NVARCHAR), 2)
                                ELSE N'وردية مفتوحة'
                            END AS Duration,
                            CASE 
                                WHEN s.ClockOutTime IS NOT NULL THEN 
                                    CAST(DATEDIFF(MINUTE, s.ClockInTime, s.ClockOutTime) / 60.0 AS DECIMAL(10,2))
                                ELSE 0
                            END AS TotalHours,
                            s.Notes
                        FROM [dbo].[Shifts] s
                        INNER JOIN [dbo].[Users] u ON s.UserId = u.UserId
                        WHERE 1=1";

                    if (userIdFilter.HasValue)
                        query += " AND s.UserId = @UserId";
                    if (dateFrom.HasValue)
                        query += " AND s.ClockInTime >= @DateFrom";
                    if (dateTo.HasValue)
                        query += " AND s.ClockInTime < DATEADD(DAY, 1, @DateTo)";
                    if (!string.IsNullOrWhiteSpace(searchTerm))
                        query += " AND (u.FullName LIKE @Search OR u.Username LIKE @Search)";

                    query += " ORDER BY s.ClockInTime DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (userIdFilter.HasValue)
                            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userIdFilter.Value;
                        if (dateFrom.HasValue)
                            cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dateFrom.Value.Date;
                        if (dateTo.HasValue)
                            cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dateTo.Value.Date;
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                            cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 200).Value = "%" + searchTerm + "%";

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        /// <summary>
        /// جلب ملخص ساعات العمل لكل موظف خلال فترة محددة
        /// </summary>
        public static async Task<DataTable> GetShiftsSummaryAsync(DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = @"
                        SELECT 
                            u.UserId,
                            u.FullName,
                            COUNT(s.ShiftId) AS TotalShifts,
                            ISNULL(CAST(SUM(CASE WHEN s.ClockOutTime IS NOT NULL THEN DATEDIFF(MINUTE, s.ClockInTime, s.ClockOutTime) ELSE 0 END) / 60.0 AS DECIMAL(10,2)), 0) AS TotalHours,
                            ISNULL(CAST(AVG(CASE WHEN s.ClockOutTime IS NOT NULL THEN CAST(DATEDIFF(MINUTE, s.ClockInTime, s.ClockOutTime) AS FLOAT) ELSE NULL END) / 60.0 AS DECIMAL(10,2)), 0) AS AvgHoursPerShift,
                            MAX(s.ClockInTime) AS LastClockIn
                        FROM [dbo].[Users] u
                        LEFT JOIN [dbo].[Shifts] s ON u.UserId = s.UserId
                        WHERE u.IsActive = 1";

                    if (dateFrom.HasValue)
                        query += " AND (s.ClockInTime IS NULL OR s.ClockInTime >= @DateFrom)";
                    if (dateTo.HasValue)
                        query += " AND (s.ClockInTime IS NULL OR s.ClockInTime < DATEADD(DAY, 1, @DateTo))";

                    query += @"
                        GROUP BY u.UserId, u.FullName
                        ORDER BY TotalHours DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (dateFrom.HasValue)
                            cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dateFrom.Value.Date;
                        if (dateTo.HasValue)
                            cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dateTo.Value.Date;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        /// <summary>
        /// حذف سجل وردية محدد (للمدير فقط)
        /// </summary>
        public static (bool Success, string Message) DeleteShift(int shiftId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "DELETE FROM [dbo].[Shifts] WHERE ShiftId = @ShiftId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@ShiftId", SqlDbType.Int).Value = shiftId;
                        int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                            return (false, "لم يتم العثور على سجل الوردية المحدد.");
                        return (true, "تم حذف سجل الوردية بنجاح.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "خطأ في حذف سجل الوردية: " + ex.Message);
            }
        }

        /// <summary>
        /// تعديل سجل وردية محدد (تعديل أوقات الحضور والانصراف يدوياً)
        /// </summary>
        public static (bool Success, string Message) UpdateShift(int shiftId, DateTime clockInTime, DateTime? clockOutTime, string notes)
        {
            try
            {
                if (clockOutTime.HasValue && clockOutTime.Value <= clockInTime)
                    return (false, "وقت الانصراف يجب أن يكون بعد وقت الحضور.");

                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"UPDATE [dbo].[Shifts] SET ClockInTime = @ClockIn, ClockOutTime = @ClockOut, Notes = @Notes WHERE ShiftId = @ShiftId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@ShiftId", SqlDbType.Int).Value = shiftId;
                        cmd.Parameters.Add("@ClockIn", SqlDbType.DateTime).Value = clockInTime;
                        cmd.Parameters.Add("@ClockOut", SqlDbType.DateTime).Value = (object)clockOutTime ?? DBNull.Value;
                        cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = (object)notes ?? DBNull.Value;
                        int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                            return (false, "لم يتم العثور على سجل الوردية المحدد.");
                        return (true, "تم تحديث سجل الوردية بنجاح.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "خطأ في تحديث سجل الوردية: " + ex.Message);
            }
        }

        /// <summary>
        /// جلب قائمة المستخدمين النشطين للاختيار في الورديات
        /// </summary>
        public static async Task<DataTable> GetActiveUsersForShiftsAsync()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    string query = @"SELECT UserId, FullName, Username FROM [dbo].[Users] WHERE IsActive = 1 ORDER BY FullName";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        #endregion
    }
}
