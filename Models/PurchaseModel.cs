using System;

namespace POS
{
    public class PurchaseModel
    {
        public int PurchaseId { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; }
    }
}
