using System;

namespace POS
{
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
}
