using System;

namespace POS
{
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
}
