using Data;
using Models;
using System.Collections.Generic;

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