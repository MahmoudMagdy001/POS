using System;
using System.Collections.Generic;

namespace POS
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }

    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public override string ToString()
        {
            return CategoryName;
        }
    }

    public class ProductModel
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int StockQuantity { get; set; }
        public int MinStockAlert { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool IsLowStock => StockQuantity <= MinStockAlert;
    }

    public class SupplierModel
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return SupplierName;
        }
    }

    public class CartItemModel
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal => UnitPrice * Quantity;
        public int AvailableStock { get; set; }
    }

    public class SaleModel
    {
        public int SaleId { get; set; }
        public int? UserId { get; set; }
        public string CashierName { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxAmount { get; set; } = 0.00m;
        public decimal FinalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal ChangeAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string ReturnStatus { get; set; } = "مكتملة"; // 'مكتملة', 'مرتجع جزئي', 'مرتجع بالكامل'
        public decimal TotalRefunded { get; set; } = 0.00m;
        public decimal NetFinalAmount => Math.Max(0, FinalAmount - TotalRefunded);
    }

    public class SaleDetailModel
    {
        public int DetailId { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public int Quantity { get; set; }
        public int ReturnedQuantity { get; set; } = 0;
        public int RemainingQuantity => Math.Max(0, Quantity - ReturnedQuantity);
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class ReturnItemModel
    {
        public int DetailId { get; set; }
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int OriginalQuantity { get; set; }
        public int AlreadyReturnedQuantity { get; set; }
        public int AvailableToReturn => Math.Max(0, OriginalQuantity - AlreadyReturnedQuantity);
        public int ReturnQuantity { get; set; }
        public decimal RefundAmount => UnitPrice * ReturnQuantity;
    }

    public class SalesReturnModel
    {
        public int ReturnId { get; set; }
        public int SaleId { get; set; }
        public int? UserId { get; set; }
        public string CashierName { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalRefundAmount { get; set; }
        public string Reason { get; set; }
        public List<ReturnItemModel> Items { get; set; } = new List<ReturnItemModel>();
    }

    public class PurchaseModel
    {
        public int PurchaseId { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; }
    }

    public class PurchaseDetailModel
    {
        public int DetailId { get; set; }
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class DashboardStatsModel
    {
        public decimal TotalSalesRevenue { get; set; }
        public int TotalTransactionsCount { get; set; }
        public decimal TotalPurchasesAmount { get; set; }
        public int TotalPurchaseInvoicesCount { get; set; }
        public decimal CostOfGoodsSold { get; set; }
        public decimal NetProfit { get; set; }
        public decimal ProfitMarginPct { get; set; }
        public int TotalProductsInStock { get; set; }
        public decimal InventoryCostValue { get; set; }
        public decimal InventorySellValue { get; set; }
        public int LowStockItemsCount { get; set; }
        public int ActiveCashiersCount { get; set; }
    }

    public class SystemSettingsModel
    {
        public string StoreName { get; set; } = "سوبر ماركت وسوق العائلة";
        public string StoreSubtitle { get; set; } = "إدارة المبيعات والمخازن والتحصيل";
        public string StorePhone { get; set; } = "01001234567";
        public string StoreAddress { get; set; } = "القاهرة، جمهورية مصر العربية";
        public string TaxNumber { get; set; } = "300-125-987";
        public string ReceiptHeader { get; set; } = "فاتورة مبيعات ضريبية مبسطة";
        public string ReceiptFooter { get; set; } = "الأسعار تشمل ضريبة القيمة المضافة • البضاعة المباعة ترد وتستبدل خلال 14 يوماً بالفاتورة";
        public string CurrencySymbol { get; set; } = "ج.م";
        public decimal VatRate { get; set; } = 0.00m;
        public int DefaultMinStock { get; set; } = 5;
        public bool EnablePrintPreview { get; set; } = true;
        public bool AutoPrintOnSale { get; set; } = false;
        public bool AllowNegativeStock { get; set; } = false;
    }
}
