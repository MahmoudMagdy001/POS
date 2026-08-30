namespace POS
{
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
