using System;
using System.Collections.Generic;

#nullable disable

namespace Models
{
    public partial class Sale
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int SalespersonId { get; set; }
        public int CustomerId { get; set; }
        public DateTime SalesDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        public virtual Salesperson Salesperson { get; set; }
    }
}
