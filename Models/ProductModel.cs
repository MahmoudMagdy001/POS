using System;

namespace POS
{
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
}
