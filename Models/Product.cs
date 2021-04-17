using System.Collections.Generic;

#nullable disable

namespace Models
{
    public partial class Product
    {
        public Product()
        {
            Discounts = new HashSet<Discount>();
            Sales = new HashSet<Sale>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Style { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public int QtyOnHand { get; set; }
        public decimal CommissionPercentage { get; set; }

        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}