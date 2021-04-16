using System;
using System.Collections.Generic;

#nullable disable

namespace Models
{
    public partial class Customer
    {
        public Customer()
        {
            Sales = new HashSet<Sale>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime? StartDate { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
