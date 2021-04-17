using Data;
using Models;
using System.Collections.Generic;

namespace Service
{
    public class SalespersonService
    {
        private static Queries queries;

        public SalespersonService()
        {
            queries = queries ?? new Queries();
        }

        public List<Salesperson> GetSalespeople()
        {
            return queries.GetSalespeople();
        }

        public Salesperson GetSalesperson(int salespersonId)
        {
            return queries.GetSalesperson(salespersonId);
        }

        public bool UpdateSalesperson(Salesperson s)
        {
            if (queries.IsSalespersonUnique(s))
            {
                queries.AddOrUpateSalesperson(s);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}