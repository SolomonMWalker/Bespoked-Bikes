using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if(queries.IsSalespersonUnique(s))
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
