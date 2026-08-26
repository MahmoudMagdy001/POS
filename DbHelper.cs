using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

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
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        #region Database Initialization

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

                // 2. Ensure all tables, indexes, constraints & Arabic seed data exist
                using (SqlConnection appConn = new SqlConnection(GetConnectionString()))
                {
                    appConn.Open();
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
                        END
                        ELSE
                        BEGIN
                            UPDATE [dbo].[Users] SET [FullName] = N'مدير النظام العام' WHERE [Username] = 'admin' AND [FullName] LIKE '%Administrator%';
                            UPDATE [dbo].[Users] SET [FullName] = N'كاشير الصالة الرئيسي' WHERE [Username] = 'cashier' AND [FullName] LIKE '%Cashier%';
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
                        END;

                        -- Translate any previous english categories
                        UPDATE [dbo].[Categories] SET [CategoryName] = N'مشروبات ومياه' WHERE [CategoryName] = 'Beverages' OR [CategoryName] LIKE '%Beverage%';
                        UPDATE [dbo].[Categories] SET [CategoryName] = N'سناكس ومقرمشات' WHERE [CategoryName] = 'Snacks & Confectionery' OR [CategoryName] LIKE '%Snack%';
                        UPDATE [dbo].[Categories] SET [CategoryName] = N'ألبان وجبن' WHERE [CategoryName] = 'Dairy & Eggs' OR [CategoryName] LIKE '%Dairy%';
                        UPDATE [dbo].[Categories] SET [CategoryName] = N'إلكترونيات وإكسسوارات' WHERE [CategoryName] = 'Electronics & Accessories' OR [CategoryName] LIKE '%Electron%';

                        -- Remove duplicate categories if created with english names
                        DELETE FROM [dbo].[Categories] WHERE [CategoryName] IN ('Beverages', 'Snacks & Confectionery', 'Dairy & Eggs', 'Electronics & Accessories');

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
                        END;

                        IF NOT EXISTS (SELECT 1 FROM [dbo].[Products] WHERE [Barcode] = N'6221001001')
                            INSERT INTO [dbo].[Products] ([Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert])
                            VALUES (N'6221001001', N'مياه معدنية 1.5 لتر', 1, 8.00, 12.00, 50, 10);
                        ELSE
                            UPDATE [dbo].[Products] SET [ProductName] = N'مياه معدنية 1.5 لتر' WHERE [Barcode] = N'6221001001' AND [ProductName] LIKE '%Mineral%';

                        IF NOT EXISTS (SELECT 1 FROM [dbo].[Products] WHERE [Barcode] = N'6221001002')
                            INSERT INTO [dbo].[Products] ([Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert])
                            VALUES (N'6221001002', N'كانز كولا 330 مل', 1, 12.00, 18.00, 40, 10);
                        ELSE
                            UPDATE [dbo].[Products] SET [ProductName] = N'كانز كولا 330 مل' WHERE [Barcode] = N'6221001002' AND [ProductName] LIKE '%Cola%';

                        IF NOT EXISTS (SELECT 1 FROM [dbo].[Products] WHERE [Barcode] = N'6221001003')
                            INSERT INTO [dbo].[Products] ([Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert])
                            VALUES (N'6221001003', N'شيبسي عائلي بالجبنة المتبلة', 2, 10.00, 15.00, 25, 8);
                        ELSE
                            UPDATE [dbo].[Products] SET [ProductName] = N'شيبسي عائلي بالجبنة المتبلة' WHERE [Barcode] = N'6221001003' AND [ProductName] LIKE '%Chips%';

                        IF NOT EXISTS (SELECT 1 FROM [dbo].[Products] WHERE [Barcode] = N'6221001004')
                            INSERT INTO [dbo].[Products] ([Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert])
                            VALUES (N'6221001004', N'حليب طازج كامل الدسم 1 لتر', 3, 30.00, 42.00, 4, 10);
                        ELSE
                            UPDATE [dbo].[Products] SET [ProductName] = N'حليب طازج كامل الدسم 1 لتر' WHERE [Barcode] = N'6221001004' AND [ProductName] LIKE '%Milk%';

                        IF NOT EXISTS (SELECT 1 FROM [dbo].[Products] WHERE [Barcode] = N'6221001005')
                            INSERT INTO [dbo].[Products] ([Barcode], [ProductName], [CategoryId], [BuyPrice], [SellPrice], [StockQuantity], [MinStockAlert])
                            VALUES (N'6221001005', N'كابل شحن سريع Type-C', 4, 45.00, 75.00, 3, 5);
                        ELSE
                            UPDATE [dbo].[Products] SET [ProductName] = N'كابل شحن سريع Type-C' WHERE [Barcode] = N'6221001005' AND [ProductName] LIKE '%Cable%';

                        -- Map products to Arabic category IDs
                        UPDATE [dbo].[Products] SET [CategoryId] = (SELECT TOP 1 CategoryId FROM [dbo].[Categories] WHERE CategoryName = N'مشروبات ومياه') WHERE [Barcode] IN ('6221001001', '6221001002');
                        UPDATE [dbo].[Products] SET [CategoryId] = (SELECT TOP 1 CategoryId FROM [dbo].[Categories] WHERE CategoryName = N'سناكس ومقرمشات') WHERE [Barcode] = '6221001003';
                        UPDATE [dbo].[Products] SET [CategoryId] = (SELECT TOP 1 CategoryId FROM [dbo].[Categories] WHERE CategoryName = N'ألبان وجبن') WHERE [Barcode] = '6221001004';
                        UPDATE [dbo].[Products] SET [CategoryId] = (SELECT TOP 1 CategoryId FROM [dbo].[Categories] WHERE CategoryName = N'إلكترونيات وإكسسوارات') WHERE [Barcode] = '6221001005';

                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Products_Barcode' AND object_id = OBJECT_ID(N'[dbo].[Products]'))
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_Products_Barcode] ON [dbo].[Products] ([Barcode]);
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
                        END
                        ELSE
                        BEGIN
                            UPDATE [dbo].[Suppliers] SET [SupplierName] = N'شركة الأهرام للتوزيع والتوريدات', [Phone] = N'01001234567', [Address] = N'المنطقة الصناعية - القاهرة' WHERE [SupplierName] LIKE '%Ahram%' OR [SupplierName] LIKE '%الأهرام%';
                            UPDATE [dbo].[Suppliers] SET [SupplierName] = N'مؤسسة الدلتا للمواد الغذائية', [Phone] = N'01129876543', [Address] = N'مجمع المخازن اللوجستية - الإسكندرية' WHERE [SupplierName] LIKE '%Delta%' OR [SupplierName] LIKE '%الدلتا%';
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

                        -- Sales
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
                        END;

                        -- SaleDetails
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaleDetails]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[SaleDetails] (
                                [DetailId]  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                                [SaleId]    INT               NOT NULL,
                                [ProductId] INT               NOT NULL,
                                [Quantity]  INT               NOT NULL,
                                [ReturnedQuantity] INT        NOT NULL DEFAULT 0,
                                [UnitPrice] DECIMAL(18,2)     NOT NULL,
                                [LineTotal] DECIMAL(18,2)     NOT NULL,
                                CONSTRAINT [FK_SaleDetails_Sales] FOREIGN KEY ([SaleId]) 
                                    REFERENCES [dbo].[Sales] ([SaleId]) ON DELETE CASCADE,
                                CONSTRAINT [FK_SaleDetails_Products] FOREIGN KEY ([ProductId]) 
                                    REFERENCES [dbo].[Products] ([ProductId])
                            );
                        END;

                        -- Migrations for Returns
                        IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Sales]') AND name = 'ReturnStatus')
                        BEGIN
                            ALTER TABLE [dbo].[Sales] ADD [ReturnStatus] NVARCHAR(50) NOT NULL DEFAULT N'مكتملة';
                        END;

                        IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Sales]') AND name = 'TotalRefunded')
                        BEGIN
                            ALTER TABLE [dbo].[Sales] ADD [TotalRefunded] DECIMAL(18,2) NOT NULL DEFAULT 0.00;
                        END;

                        IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SaleDetails]') AND name = 'ReturnedQuantity')
                        BEGIN
                            ALTER TABLE [dbo].[SaleDetails] ADD [ReturnedQuantity] INT NOT NULL DEFAULT 0;
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

                                // Update LastLogin
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

                    string checkQuery = "SELECT COUNT(1) FROM [dbo].[Users] WHERE LOWER(Username) = LOWER(@Username)";
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
                        string check = "SELECT COUNT(1) FROM [dbo].[Categories] WHERE LOWER(CategoryName) = LOWER(@CategoryName)";
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
                            return (true, "تم حذف القسم بنجاح.");
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

        public static ProductModel GetProductByBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode)) return null;

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
                        cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = barcode.Trim();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new ProductModel
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
                                return new ProductModel
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
                                return (true, "تم تحديث بيانات المنتج بنجاح.", product.ProductId);
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
                            return (true, "تم حذف المنتج بنجاح.");
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

        #region Sales Management & POS Checkout Transaction

        public static (bool Success, string Message, int SaleId) ProcessSaleTransaction(SaleModel sale, List<CartItemModel> items)
        {
            if (sale == null)
                return (false, "بيانات الفاتورة مفقودة.", 0);

            if (items == null || items.Count == 0)
                return (false, "سلة المشتريات فارغة، يرجى إضافة منتجات لإتمام البيع.", 0);

            if (sale.PaidAmount < sale.FinalAmount)
                return (false, $"المبلغ المدفوع ({sale.PaidAmount:N2} ج.م) أقل من إجمالي الفاتورة المطلوب ({sale.FinalAmount:N2} ج.م).", 0);

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        // 1. التحقق من توفر الكمية الكافية لكل منتج في المخزون
                        foreach (var item in items)
                        {
                            string checkStockSql = "SELECT StockQuantity, ProductName FROM [dbo].[Products] WITH (UPDLOCK, ROWLOCK) WHERE ProductId = @ProductId";
                            using (SqlCommand cmd = new SqlCommand(checkStockSql, conn, transaction))
                            {
                                cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = item.ProductId;
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        int currentStock = Convert.ToInt32(reader["StockQuantity"]);
                                        string productName = reader["ProductName"].ToString();

                                        if (currentStock < item.Quantity)
                                        {
                                            reader.Close();
                                            transaction.Rollback();
                                            return (false, $"الكمية غير متوفرة في المخزن للمنتج '{productName}'. المتاح: {currentStock}، المطلوب: {item.Quantity}.", 0);
                                        }
                                    }
                                    else
                                    {
                                        reader.Close();
                                        transaction.Rollback();
                                        return (false, $"المنتج ذو الرقم {item.ProductId} غير موجود في قاعدة البيانات.", 0);
                                    }
                                }
                            }
                        }

                        // 2. إدراج رأس الفاتورة
                        string insertSaleSql = @"
                            INSERT INTO [dbo].[Sales] 
                                (UserId, SaleDate, TotalAmount, Discount, FinalAmount, PaidAmount, ChangeAmount, PaymentMethod)
                            VALUES 
                                (@UserId, @SaleDate, @TotalAmount, @Discount, @FinalAmount, @PaidAmount, @ChangeAmount, @PaymentMethod);
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        int saleId;
                        using (SqlCommand cmd = new SqlCommand(insertSaleSql, conn, transaction))
                        {
                            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = sale.UserId.HasValue ? (object)sale.UserId.Value : DBNull.Value;
                            cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime).Value = sale.SaleDate == default ? DateTime.Now : sale.SaleDate;
                            cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = sale.TotalAmount;
                            cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = sale.Discount;
                            cmd.Parameters.Add("@FinalAmount", SqlDbType.Decimal).Value = sale.FinalAmount;
                            cmd.Parameters.Add("@PaidAmount", SqlDbType.Decimal).Value = sale.PaidAmount;
                            cmd.Parameters.Add("@ChangeAmount", SqlDbType.Decimal).Value = sale.ChangeAmount;
                            cmd.Parameters.Add("@PaymentMethod", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(sale.PaymentMethod) ? "نقدي" : sale.PaymentMethod;

                            saleId = (int)cmd.ExecuteScalar();
                        }

                        // 3. إدراج تفاصيل الفاتورة وخصم الكميات من المخزون
                        string insertDetailSql = @"
                            INSERT INTO [dbo].[SaleDetails] (SaleId, ProductId, Quantity, UnitPrice, LineTotal)
                            VALUES (@SaleId, @ProductId, @Quantity, @UnitPrice, @LineTotal);";

                        string deductStockSql = @"
                            UPDATE [dbo].[Products]
                            SET StockQuantity = StockQuantity - @Quantity
                            WHERE ProductId = @ProductId;";

                        foreach (var item in items)
                        {
                            using (SqlCommand cmd = new SqlCommand(insertDetailSql, conn, transaction))
                            {
                                cmd.Parameters.Add("@SaleId", SqlDbType.Int).Value = saleId;
                                cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = item.ProductId;
                                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = item.Quantity;
                                cmd.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = item.UnitPrice;
                                cmd.Parameters.Add("@LineTotal", SqlDbType.Decimal).Value = item.LineTotal;
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = new SqlCommand(deductStockSql, conn, transaction))
                            {
                                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = item.Quantity;
                                cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = item.ProductId;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // تأكيد المعاملة
                        transaction.Commit();
                        return (true, $"تم إتمام الفاتورة #{saleId:D5} وخصم المخزون بنجاح.", saleId);
                    }
                    catch (Exception ex)
                    {
                        try { transaction.Rollback(); } catch { }
                        return (false, "فشلت عملية البيع: " + ex.Message, 0);
                    }
                }
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
                    StringBuilder query = new StringBuilder(@"
                        SELECT 
                            s.SaleId, 
                            s.SaleDate, 
                            ISNULL(u.FullName, N'مدير النظام') AS CashierName, 
                            s.TotalAmount, 
                            s.Discount, 
                            s.FinalAmount, 
                            ISNULL(s.TotalRefunded, 0) AS TotalRefunded,
                            (s.FinalAmount - ISNULL(s.TotalRefunded, 0)) AS NetFinalAmount,
                            s.PaidAmount, 
                            s.ChangeAmount, 
                            s.PaymentMethod,
                            ISNULL(s.ReturnStatus, N'مكتملة') AS ReturnStatus,
                            (SELECT COUNT(1) FROM [dbo].[SaleDetails] sd WHERE sd.SaleId = s.SaleId) AS ItemsCount
                        FROM [dbo].[Sales] s
                        LEFT JOIN [dbo].[Users] u ON s.UserId = u.UserId
                        WHERE 1=1 ");

                    if (fromDate.HasValue && toDate.HasValue)
                    {
                        query.Append(" AND s.SaleDate >= @FromDate AND s.SaleDate <= @ToDate ");
                    }
                    else if (dateFilter == "اليوم" || string.Equals(dateFilter, "Today", StringComparison.OrdinalIgnoreCase))
                    {
                        query.Append(" AND CAST(s.SaleDate AS DATE) = CAST(GETDATE() AS DATE) ");
                    }
                    else if (dateFilter == "هذا الأسبوع" || dateFilter == "الاسبوع" || string.Equals(dateFilter, "This Week", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "Week", StringComparison.OrdinalIgnoreCase))
                    {
                        query.Append(" AND s.SaleDate >= DATEADD(day, -7, GETDATE()) ");
                    }
                    else if (dateFilter == "هذا الشهر" || dateFilter == "الشهر" || string.Equals(dateFilter, "This Month", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "Month", StringComparison.OrdinalIgnoreCase))
                    {
                        query.Append(" AND s.SaleDate >= DATEADD(month, -1, GETDATE()) ");
                    }

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        query.Append(" AND (CAST(s.SaleId AS NVARCHAR(20)) LIKE @Search OR u.FullName LIKE @Search OR s.PaymentMethod LIKE @Search OR s.ReturnStatus LIKE @Search) ");
                    }

                    query.Append(" ORDER BY s.SaleId DESC");

                    using (SqlCommand cmd = new SqlCommand(query.ToString(), conn))
                    {
                        if (fromDate.HasValue && toDate.HasValue)
                        {
                            cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate.Value;
                            cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = toDate.Value;
                        }

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

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Insert into SalesReturns
                        string insertReturnSql = @"
                            INSERT INTO [dbo].[SalesReturns] (SaleId, UserId, ReturnDate, TotalRefundAmount, Reason)
                            VALUES (@SaleId, @UserId, GETDATE(), @TotalRefundAmount, @Reason);
                            SELECT SCOPE_IDENTITY();";

                        int returnId = 0;
                        using (SqlCommand cmd = new SqlCommand(insertReturnSql, conn, transaction))
                        {
                            cmd.Parameters.Add("@SaleId", SqlDbType.Int).Value = saleId;
                            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = (object)userId ?? DBNull.Value;
                            cmd.Parameters.Add("@TotalRefundAmount", SqlDbType.Decimal).Value = totalRefund;
                            cmd.Parameters.Add("@Reason", SqlDbType.NVarChar, 250).Value = (object)reason ?? DBNull.Value;
                            returnId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // 2. Insert Return Details and Update Stock & SaleDetails
                        foreach (var item in itemsToReturn)
                        {
                            string insertDetailSql = @"
                                INSERT INTO [dbo].[SalesReturnDetails] (ReturnId, DetailId, ProductId, ReturnedQuantity, UnitPrice, RefundAmount)
                                VALUES (@ReturnId, @DetailId, @ProductId, @ReturnedQuantity, @UnitPrice, @RefundAmount);";

                            using (SqlCommand cmd = new SqlCommand(insertDetailSql, conn, transaction))
                            {
                                cmd.Parameters.Add("@ReturnId", SqlDbType.Int).Value = returnId;
                                cmd.Parameters.Add("@DetailId", SqlDbType.Int).Value = item.DetailId > 0 ? (object)item.DetailId : DBNull.Value;
                                cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = item.ProductId;
                                cmd.Parameters.Add("@ReturnedQuantity", SqlDbType.Int).Value = item.ReturnQuantity;
                                cmd.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = item.UnitPrice;
                                cmd.Parameters.Add("@RefundAmount", SqlDbType.Decimal).Value = item.RefundAmount;
                                cmd.ExecuteNonQuery();
                            }

                            // Update SaleDetails.ReturnedQuantity
                            string updateSaleDetailSql = @"
                                UPDATE [dbo].[SaleDetails]
                                SET ReturnedQuantity = ISNULL(ReturnedQuantity, 0) + @ReturnedQuantity
                                WHERE DetailId = @DetailId;";

                            using (SqlCommand cmd = new SqlCommand(updateSaleDetailSql, conn, transaction))
                            {
                                cmd.Parameters.Add("@ReturnedQuantity", SqlDbType.Int).Value = item.ReturnQuantity;
                                cmd.Parameters.Add("@DetailId", SqlDbType.Int).Value = item.DetailId;
                                cmd.ExecuteNonQuery();
                            }

                            // Return quantity to Products Stock
                            string updateStockSql = @"
                                UPDATE [dbo].[Products]
                                SET StockQuantity = StockQuantity + @ReturnedQuantity
                                WHERE ProductId = @ProductId;";

                            using (SqlCommand cmd = new SqlCommand(updateStockSql, conn, transaction))
                            {
                                cmd.Parameters.Add("@ReturnedQuantity", SqlDbType.Int).Value = item.ReturnQuantity;
                                cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = item.ProductId;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // 3. Update Sales Header status & TotalRefunded
                        string updateSaleSql = @"
                            UPDATE [dbo].[Sales]
                            SET TotalRefunded = ISNULL(TotalRefunded, 0) + @RefundAmount,
                                ReturnStatus = CASE 
                                    WHEN (SELECT ISNULL(SUM(Quantity - ISNULL(ReturnedQuantity, 0)), 0) FROM [dbo].[SaleDetails] WHERE SaleId = @SaleId) <= 0 
                                    THEN N'مرتجع بالكامل' 
                                    ELSE N'مرتجع جزئي' 
                                END
                            WHERE SaleId = @SaleId;";

                        using (SqlCommand cmd = new SqlCommand(updateSaleSql, conn, transaction))
                        {
                            cmd.Parameters.Add("@RefundAmount", SqlDbType.Decimal).Value = totalRefund;
                            cmd.Parameters.Add("@SaleId", SqlDbType.Int).Value = saleId;
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return (true, "تمت عملية إرجاع الأصناف وإعادة البضاعة للمخزن واسترداد المبلغ بنجاح.", returnId);
                    }
                    catch (Exception ex)
                    {
                        try { transaction.Rollback(); } catch { }
                        return (false, "فشلت عملية الإرجاع: " + ex.Message, 0);
                    }
                }
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
                               s.SaleDate, s.TotalAmount, s.Discount, s.FinalAmount, 
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

        #region Purchases Management & Transaction

        public static (bool Success, string Message, int PurchaseId) ProcessPurchaseTransaction(PurchaseModel purchase, List<PurchaseDetailModel> items, bool updateBuyPrice = true)
        {
            if (purchase == null)
                return (false, "بيانات فاتورة الشراء مفقودة.", 0);

            if (items == null || items.Count == 0)
                return (false, "يجب إضافة صنف واحد على الأقل لفاتورة الشراء.", 0);

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        // 1. إدراج رأس فاتورة الشراء
                        string insertPurchaseSql = @"
                            INSERT INTO [dbo].[Purchases] (SupplierId, PurchaseDate, TotalAmount, Notes)
                            VALUES (@SupplierId, @PurchaseDate, @TotalAmount, @Notes);
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        int purchaseId;
                        using (SqlCommand cmd = new SqlCommand(insertPurchaseSql, conn, transaction))
                        {
                            cmd.Parameters.Add("@SupplierId", SqlDbType.Int).Value = purchase.SupplierId.HasValue && purchase.SupplierId.Value > 0 ? (object)purchase.SupplierId.Value : DBNull.Value;
                            cmd.Parameters.Add("@PurchaseDate", SqlDbType.DateTime).Value = purchase.PurchaseDate == default ? DateTime.Now : purchase.PurchaseDate;
                            cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = purchase.TotalAmount;
                            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = (object)purchase.Notes ?? DBNull.Value;

                            purchaseId = (int)cmd.ExecuteScalar();
                        }

                        // 2. إدراج تفاصيل الفاتورة وزيادة رصيد المخزون وتحديث سعر الشراء
                        string insertDetailSql = @"
                            INSERT INTO [dbo].[PurchaseDetails] (PurchaseId, ProductId, Quantity, UnitPrice, LineTotal)
                            VALUES (@PurchaseId, @ProductId, @Quantity, @UnitPrice, @LineTotal);";

                        string incrementStockSql = @"
                            UPDATE [dbo].[Products]
                            SET StockQuantity = StockQuantity + @Quantity,
                                BuyPrice = CASE WHEN @UpdateBuyPrice = 1 THEN @UnitPrice ELSE BuyPrice END
                            WHERE ProductId = @ProductId;";

                        foreach (var item in items)
                        {
                            using (SqlCommand cmd = new SqlCommand(insertDetailSql, conn, transaction))
                            {
                                cmd.Parameters.Add("@PurchaseId", SqlDbType.Int).Value = purchaseId;
                                cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = item.ProductId;
                                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = item.Quantity;
                                cmd.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = item.UnitPrice;
                                cmd.Parameters.Add("@LineTotal", SqlDbType.Decimal).Value = item.LineTotal;
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = new SqlCommand(incrementStockSql, conn, transaction))
                            {
                                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = item.Quantity;
                                cmd.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = item.UnitPrice;
                                cmd.Parameters.Add("@UpdateBuyPrice", SqlDbType.Bit).Value = updateBuyPrice;
                                cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = item.ProductId;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return (true, $"تم حفظ فاتورة المشتريات #{purchaseId:D5} وزيادة رصيد المخزون بنجاح.", purchaseId);
                    }
                    catch (Exception ex)
                    {
                        try { transaction.Rollback(); } catch { }
                        return (false, "فشلت عملية حفظ فاتورة الشراء: " + ex.Message, 0);
                    }
                }
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
                            (SELECT COUNT(1) FROM [dbo].[PurchaseDetails] pd WHERE pd.PurchaseId = p.PurchaseId) AS ItemsCount
                        FROM [dbo].[Purchases] p
                        LEFT JOIN [dbo].[Suppliers] s ON p.SupplierId = s.SupplierId
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

        #endregion

        #region Executive Dashboard Analytics

        public static DashboardStatsModel GetDashboardKPIs(string dateFilter = "الأسبوع")
        {
            DashboardStatsModel stats = new DashboardStatsModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    string salesDateCondition = "s.SaleDate >= DATEADD(day, -7, GETDATE())";
                    string purchasesDateCondition = "p.PurchaseDate >= DATEADD(day, -7, GETDATE())";

                    if (dateFilter == "الأسبوع" || dateFilter == "الاسبوع" || dateFilter == "هذا الأسبوع" || string.Equals(dateFilter, "This Week", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "Week", StringComparison.OrdinalIgnoreCase))
                    {
                        salesDateCondition = "s.SaleDate >= DATEADD(day, -7, GETDATE())";
                        purchasesDateCondition = "p.PurchaseDate >= DATEADD(day, -7, GETDATE())";
                    }
                    else if (dateFilter == "الشهر" || dateFilter == "هذا الشهر" || string.Equals(dateFilter, "This Month", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "Month", StringComparison.OrdinalIgnoreCase))
                    {
                        salesDateCondition = "s.SaleDate >= DATEADD(month, -1, GETDATE())";
                        purchasesDateCondition = "p.PurchaseDate >= DATEADD(month, -1, GETDATE())";
                    }
                    else if (dateFilter == "الكل" || dateFilter == "كل الفترات" || string.Equals(dateFilter, "All Time", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "All", StringComparison.OrdinalIgnoreCase))
                    {
                        salesDateCondition = "1=1";
                        purchasesDateCondition = "1=1";
                    }
                    else if (dateFilter == "اليوم" || string.Equals(dateFilter, "Today", StringComparison.OrdinalIgnoreCase))
                    {
                        salesDateCondition = "CAST(s.SaleDate AS DATE) = CAST(GETDATE() AS DATE)";
                        purchasesDateCondition = "CAST(p.PurchaseDate AS DATE) = CAST(GETDATE() AS DATE)";
                    }

                    // 1. حساب صافي إجمالي المبيعات وعدد المعاملات
                    string salesKpiSql = $@"
                        SELECT 
                            ISNULL(SUM(s.FinalAmount - ISNULL(s.TotalRefunded, 0)), 0) AS TotalRevenue,
                            COUNT(1) AS TotalTransactions
                        FROM [dbo].[Sales] s
                        WHERE {salesDateCondition}";

                    using (SqlCommand cmd = new SqlCommand(salesKpiSql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stats.TotalSalesRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                            stats.TotalTransactionsCount = Convert.ToInt32(reader["TotalTransactions"]);
                        }
                    }

                    // 2. حساب إجمالي المشتريات المنفذة في نفس الفترة
                    string purchasesKpiSql = $@"
                        SELECT 
                            ISNULL(SUM(p.TotalAmount), 0) AS TotalPurchases,
                            COUNT(1) AS TotalPurchaseInvoices
                        FROM [dbo].[Purchases] p
                        WHERE {purchasesDateCondition}";

                    using (SqlCommand cmd = new SqlCommand(purchasesKpiSql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stats.TotalPurchasesAmount = Convert.ToDecimal(reader["TotalPurchases"]);
                            stats.TotalPurchaseInvoicesCount = Convert.ToInt32(reader["TotalPurchaseInvoices"]);
                        }
                    }

                    // 3. حساب تكلفة البضاعة المباعة (Cost of Goods Sold - COGS) بعد استبعاد المرتجع
                    string cogsKpiSql = $@"
                        SELECT 
                            ISNULL(SUM((sd.Quantity - ISNULL(sd.ReturnedQuantity, 0)) * ISNULL(pr.BuyPrice, 0)), 0) AS CostOfGoodsSold
                        FROM [dbo].[SaleDetails] sd
                        INNER JOIN [dbo].[Sales] s ON sd.SaleId = s.SaleId
                        INNER JOIN [dbo].[Products] pr ON sd.ProductId = pr.ProductId
                        WHERE {salesDateCondition}";

                    using (SqlCommand cmd = new SqlCommand(cogsKpiSql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stats.CostOfGoodsSold = Convert.ToDecimal(reader["CostOfGoodsSold"]);
                        }
                    }

                    // 4. حساب صافي الربح والمكسب وهامش الربح
                    stats.NetProfit = stats.TotalSalesRevenue - stats.CostOfGoodsSold;
                    if (stats.TotalSalesRevenue > 0)
                    {
                        stats.ProfitMarginPct = Math.Round((stats.NetProfit / stats.TotalSalesRevenue) * 100m, 1);
                    }
                    else
                    {
                        stats.ProfitMarginPct = 0;
                    }

                    // 5. حساب إحصائيات وقيمة المخزون الإجمالية
                    string productsKpiSql = @"
                        SELECT 
                            ISNULL(SUM(StockQuantity), 0) AS TotalUnitsInStock,
                            ISNULL(SUM(StockQuantity * BuyPrice), 0) AS InventoryCostValue,
                            ISNULL(SUM(StockQuantity * SellPrice), 0) AS InventorySellValue,
                            COUNT(CASE WHEN StockQuantity <= MinStockAlert THEN 1 END) AS LowStockCount
                        FROM [dbo].[Products]";

                    using (SqlCommand cmd = new SqlCommand(productsKpiSql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stats.TotalProductsInStock = Convert.ToInt32(reader["TotalUnitsInStock"]);
                            stats.InventoryCostValue = Convert.ToDecimal(reader["InventoryCostValue"]);
                            stats.InventorySellValue = Convert.ToDecimal(reader["InventorySellValue"]);
                            stats.LowStockItemsCount = Convert.ToInt32(reader["LowStockCount"]);
                        }
                    }

                    // 6. عدد المستخدمين النشطين
                    string activeCashiersSql = "SELECT COUNT(1) FROM [dbo].[Users] WHERE IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(activeCashiersSql, conn))
                    {
                        stats.ActiveCashiersCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
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

                    string dateCondition = "s.SaleDate >= DATEADD(day, -7, GETDATE())";
                    if (dateFilter == "الأسبوع" || dateFilter == "الاسبوع" || dateFilter == "هذا الأسبوع" || string.Equals(dateFilter, "This Week", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "Week", StringComparison.OrdinalIgnoreCase))
                        dateCondition = "s.SaleDate >= DATEADD(day, -7, GETDATE())";
                    else if (dateFilter == "الشهر" || dateFilter == "هذا الشهر" || string.Equals(dateFilter, "This Month", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "Month", StringComparison.OrdinalIgnoreCase))
                        dateCondition = "s.SaleDate >= DATEADD(month, -1, GETDATE())";
                    else if (dateFilter == "الكل" || dateFilter == "كل الفترات" || string.Equals(dateFilter, "All Time", StringComparison.OrdinalIgnoreCase) || string.Equals(dateFilter, "All", StringComparison.OrdinalIgnoreCase))
                        dateCondition = "1=1";
                    else if (dateFilter == "اليوم" || string.Equals(dateFilter, "Today", StringComparison.OrdinalIgnoreCase))
                        dateCondition = "CAST(s.SaleDate AS DATE) = CAST(GETDATE() AS DATE)";

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
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
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
                            (SELECT COUNT(1) FROM [dbo].[SaleDetails] sd WHERE sd.SaleId = s.SaleId) AS ItemsCount
                        FROM [dbo].[Sales] s
                        LEFT JOIN [dbo].[Users] u ON s.UserId = u.UserId
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

        #endregion
    }
}
