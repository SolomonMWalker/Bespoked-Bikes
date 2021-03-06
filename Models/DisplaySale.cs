using System;

namespace Models
{
    //contains the value we want to display in the DisplaySalesView
    public class DisplaySale
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public float SalePrice { get; set; }
        public int SalespersonId { get; set; }
        public float SalespersonCommission { get; set; }
    }
}