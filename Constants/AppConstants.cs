namespace POS.Constants
{
    public static class AppRoles
    {
        public const string Admin = "Admin";
        public const string Cashier = "Cashier";
        public const string AdminArabic = "مدير";
        public const string CashierArabic = "كاشير";

        public static bool IsAdmin(string role)
        {
            if (string.IsNullOrEmpty(role)) return false;
            return string.Equals(role, Admin, System.StringComparison.OrdinalIgnoreCase) || role == AdminArabic;
        }
    }

    public static class PaymentMethods
    {
        public const string Cash = "نقدي";
        public const string Card = "فيزا / بطاقة";
        public const string Credit = "آجل";
    }

    public static class ReturnStatuses
    {
        public const string Completed = "مكتملة";
        public const string PartialReturn = "مرتجع جزئي";
        public const string FullReturn = "مرتجع بالكامل";
    }

    public static class AppDefaults
    {
        public const string AppName = "نظام نقاط البيع وإدارة المخازن";
        public const string DefaultCurrency = "ج.م";
        public const int DefaultMinStock = 5;
    }
}
