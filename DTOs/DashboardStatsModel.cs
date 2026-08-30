namespace POS
{
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
}
