using System;

namespace POS
{
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
}
