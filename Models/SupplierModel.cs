using System;

namespace POS
{
    public class SupplierModel
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return SupplierName;
        }
    }
}
