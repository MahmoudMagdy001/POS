namespace POS
{
    public class CartItemModel
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal => UnitPrice * Quantity;
        public int AvailableStock { get; set; }
    }
}
