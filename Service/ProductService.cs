﻿using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService
    {
        private static Queries queries;

        public ProductService()
        {
            queries = queries ?? new Queries();
        }

        public List<Product> GetProducts()
        {
            return queries.GetProducts();
        }

        public Product GetProduct(int productId)
        {
            return queries.GetProduct(productId);
        }

        public bool UpdateProduct(Product p)
        {
            if(queries.IsProductUnique(p))
            {
                queries.AddOrUpdateProduct(p);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
