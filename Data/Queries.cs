using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    class Queries
    {
        private static DatabaseContext DBContext;

        public Queries()
        {
            if (DBContext == null)
            {
                DBContext = new DatabaseContext();
            }
        }


        public List<Salesperson> GetSalespeople()
        {
            IQueryable<Salesperson> salespeopleQuery = from s in DBContext.Salespeople select s;
            var salespeopleList = salespeopleQuery.ToList();
            return salespeopleList;
        }

        public bool IsSalespersonUnique(Salesperson sp)
        {
            var isFirstNameUnique = DBContext.Salespeople.Where(s => s.FirstName == sp.FirstName).Count() == 0;
            var isLastNameUnique = DBContext.Salespeople.Where(s => s.LastName == sp.LastName).Count() == 0;
            var isAddressUnique = DBContext.Salespeople.Where(s => s.Address == sp.Address).Count() == 0;
            var isPhoneNumberUnique = DBContext.Salespeople.Where(s => s.Phone == sp.Phone).Count() == 0;
            var isStartDateUnique = DBContext.Salespeople.Where(s => s.StartDate == sp.StartDate).Count() == 0;

            if (isFirstNameUnique && isLastNameUnique && isAddressUnique && isPhoneNumberUnique && isStartDateUnique)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddSalesperson(Salesperson sp)
        {
            DBContext.Salespeople.Add(sp);
            DBContext.SaveChanges();
        }

        public List<Product> GetProducts()
        {
            var productsQuery = from p in DBContext.Products select p;
            var productsList = productsQuery.ToList();
            return productsList;
        }

        public bool IsProductUnique(Product product)
        {
            var isNameUnique = DBContext.Products.Where(p => p.Name == product.Name).Count() == 0;
            var isManufacturerUnique = DBContext.Products.Where(p => p.Manufacturer == product.Manufacturer).Count() == 0;
            var isSalePriceUnique = DBContext.Products.Where(p => p.SalePrice == product.SalePrice).Count() == 0;
            var isPurchasePriceUnique = DBContext.Products.Where(p => p.PurchasePrice == product.PurchasePrice).Count() == 0;
            var isStyleUnique = DBContext.Products.Where(p => p.Style == product.Style).Count() == 0;

            if (isNameUnique && isManufacturerUnique && isSalePriceUnique && isPurchasePriceUnique && isStyleUnique)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddProduct(Product p)
        {
            DBContext.Add(p);
            DBContext.SaveChanges();
        }

        public List<Customer> GetCustomers()
        {
            var customersQuery = from c in DBContext.Customers select c;
            var customersList = customersQuery.ToList();
            return customersList;
        }

        public List<DisplaySale> GetSales(DateTime? startDateRangeDate = null, DateTime? endDateRangeDate = null)
        {
            var startDateNotNull = startDateRangeDate ?? DateTime.MinValue;
            var endDateNotNull = endDateRangeDate ?? DateTime.Now;

            var salesQuery =
                from s in DBContext.Sales
                join p in DBContext.Products on s.ProductId equals p.ProductId
                where s.SalesDate >= startDateNotNull && s.SalesDate <= endDateNotNull
                select new DisplaySale
                {
                    ProductName = p.Name,
                    ProductId = s.ProductId,
                    CustomerId = s.CustomerId,
                    SaleDate = s.SalesDate,
                    SalePrice = (float)p.SalePrice,
                    SalespersonId = s.SalespersonId,
                    SalespersonCommission = (float)(p.SalePrice * p.CommissionPercentage)
                };

            var salesList = salesQuery.ToList();
            return salesList;
        }

        public void AddSale(Sale s)
        {
            DBContext.Sales.Add(s);
            DBContext.SaveChanges();
        }

        public List<SalespersonCommissionReport> GetQuarterlyCommissionReport(int quarter, int year)
        {
            DateTime startDate, endDate;

            switch(quarter)
            {
                case 1:
                    startDate = new DateTime(year, 1, 1);
                    endDate = new DateTime(year, 3, 31);
                    break;
                case 2:
                    startDate = new DateTime(year, 4, 1);
                    endDate = new DateTime(year, 6, 30);
                    break;
                case 3:
                    startDate = new DateTime(year, 7, 1);
                    endDate = new DateTime(year, 9, 30);
                    break;
                default:
                    startDate = new DateTime(year, 10, 1);
                    endDate = new DateTime(year, 12, 31);
                    break;
            }

            var sales = GetSales(startDate, endDate);

            var salesInQuarterQuery =
                from s in sales
                where s.SaleDate >= startDate && s.SaleDate <= endDate
                group new
                {
                    s.ProductId,
                    s.SalespersonId,
                    s.SaleDate,
                    s.SalePrice,
                    s.SalespersonCommission
                } by s.SalespersonId into spgrp
                select spgrp;

            var commissionReportQuery =
                from spgrp in salesInQuarterQuery
                select new SalespersonCommissionReport
                {
                    SalespersonId = spgrp.Key,
                    SalespersonFullName = DBContext.Salespeople.Where(s => s.SalespersonId == spgrp.Key).FirstOrDefault().FirstName + " "
                        + DBContext.Salespeople.Where(s => s.SalespersonId == spgrp.Key).FirstOrDefault().LastName,
                    Quarter = quarter,
                    Year = year,
                    SalesCommissionAmount = spgrp.Where(x => x.SalespersonId == spgrp.Key).Sum(x => x.SalespersonCommission)
                };

            return commissionReportQuery.ToList();
        }

    }
}
