using System;

namespace POS
{
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
}
