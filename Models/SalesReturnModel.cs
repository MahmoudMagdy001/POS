using System;
using System.Collections.Generic;

namespace POS
{
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
}
