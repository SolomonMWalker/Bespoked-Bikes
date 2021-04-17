using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            queries.AddSale(s);
        }
    }
}
