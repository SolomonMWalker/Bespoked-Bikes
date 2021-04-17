using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CustomerService
    {
        private static Queries queries;

        public CustomerService()
        {
            queries = queries ?? new Queries();
        }

        public List<Customer> GetCustomers()
        {
            return queries.GetCustomers();
        }
    }
}
