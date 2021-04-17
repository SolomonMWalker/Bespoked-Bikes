using System;
using System.Collections.Generic;

#nullable disable

namespace Models
{
    public partial class Salesperson
    {
        public Salesperson()
        {
            Sales = new HashSet<Sale>();
        }

        public int SalespersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string Manager { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}