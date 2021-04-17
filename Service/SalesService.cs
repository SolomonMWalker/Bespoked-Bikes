using Data;
using Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public class SalesService
    {
        private static Queries queries;

        public SalesService()
        {
            queries = queries ?? new Queries();
        }

        public List<DisplaySale> GetSales(DateTime? startDate = null, DateTime? endDate = null)
        {
            return queries.GetSales(startDate, endDate);
        }

        public void CreateSale(Sale s)
        {
            s.SaleId = queries.GetLastSaleId() + 1;
            queries.AddSale(s);
        }

        public List<int> GetProductIds()
        {
            return queries.GetProductIds();
        }

        public List<int> GetSalespersonIds()
        {
            return queries.GetSalespeopleIds();
        }

        public List<int> GetCustomerIds()
        {
            return queries.GetCustomerIds();
        }
    }
}