using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class Queries
    {
        //only need one instance for db accessing
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

        public Salesperson GetSalesperson(int salespersonId)
        {
            return DBContext.Salespeople.Where(x => x.SalespersonId == salespersonId).FirstOrDefault();
        }

        //as long as a single field below is unique, returns true
        public bool IsSalespersonUnique(Salesperson sp)
        {
            var isFirstNameUnique = DBContext.Salespeople.Where(s => s.FirstName == sp.FirstName).Count() == 0;
            var isLastNameUnique = DBContext.Salespeople.Where(s => s.LastName == sp.LastName).Count() == 0;
            var isAddressUnique = DBContext.Salespeople.Where(s => s.Address == sp.Address).Count() == 0;
            var isPhoneNumberUnique = DBContext.Salespeople.Where(s => s.Phone == sp.Phone).Count() == 0;
            var isStartDateUnique = DBContext.Salespeople.Where(s => s.StartDate == sp.StartDate).Count() == 0;

            if (isFirstNameUnique || isLastNameUnique || isAddressUnique || isPhoneNumberUnique || isStartDateUnique)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //allows addition or creation, but the app only supports updating at the moment
        public void AddOrUpateSalesperson(Salesperson sp)
        {
            if (DBContext.Salespeople.Select(x => x.SalespersonId).Contains(sp.SalespersonId))
            {
                var salesperson = (
                    from s in DBContext.Salespeople
                    where s.SalespersonId == sp.SalespersonId
                    select s).SingleOrDefault();
                salesperson.FirstName = sp.FirstName;
                salesperson.LastName = sp.LastName;
                salesperson.Address = sp.Address;
                salesperson.Phone = sp.Phone;
                salesperson.StartDate = sp.StartDate;
                salesperson.TerminationDate = sp.TerminationDate;
                salesperson.Manager = sp.Manager;
            }
            else
            {
                DBContext.Salespeople.Add(sp);
            }
            DBContext.SaveChanges();
        }

        public List<Product> GetProducts()
        {
            var productsQuery = from p in DBContext.Products select p;
            var productsList = productsQuery.ToList();
            return productsList;
        }

        public Product GetProduct(int productId)
        {
            return DBContext.Products.Where(x => x.ProductId == productId).FirstOrDefault();
        }

        //as long as a single field below is unique, returns true
        public bool IsProductUnique(Product product)
        {
            var isNameUnique = DBContext.Products.Where(p => p.Name == product.Name).Count() == 0;
            var isManufacturerUnique = DBContext.Products.Where(p => p.Manufacturer == product.Manufacturer).Count() == 0;
            var isSalePriceUnique = DBContext.Products.Where(p => p.SalePrice == product.SalePrice).Count() == 0;
            var isPurchasePriceUnique = DBContext.Products.Where(p => p.PurchasePrice == product.PurchasePrice).Count() == 0;
            var isStyleUnique = DBContext.Products.Where(p => p.Style == product.Style).Count() == 0;

            if (isNameUnique || isManufacturerUnique || isSalePriceUnique || isPurchasePriceUnique || isStyleUnique)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //allows addition or creation, but the app only supports updating at the moment
        public void AddOrUpdateProduct(Product p)
        {
            if (DBContext.Products.Select(x => x.ProductId).Contains(p.ProductId))
            {
                var product = (
                    from pr in DBContext.Products
                    where pr.ProductId == p.ProductId
                    select pr).SingleOrDefault();
                product.Manufacturer = p.Manufacturer;
                product.Style = p.Style;
                product.SalePrice = p.SalePrice;
                product.PurchasePrice = p.PurchasePrice;
                product.QtyOnHand = p.QtyOnHand;
                product.CommissionPercentage = p.CommissionPercentage;
                product.Name = p.Name;
            }
            else
            {
                DBContext.Products.Add(p);
            }

            DBContext.SaveChanges();
        }

        public List<Customer> GetCustomers()
        {
            var customersQuery = from c in DBContext.Customers select c;
            var customersList = customersQuery.ToList();
            return customersList;
        }

        //uses optional parameters and null coalesces to effectively allow passing or not passing parameters
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

        //Used to increment new sale ids
        public int GetLastSaleId()
        {
            var lastSaleId = DBContext.Sales.Select(x => x.SaleId).ToList().Max();
            return lastSaleId;
        }

        //for sales creation
        public List<int> GetProductIds()
        {
            return DBContext.Products.Select(x => x.ProductId).ToList();
        }

        //for sales creation
        public List<int> GetSalespeopleIds()
        {
            return DBContext.Salespeople.Select(x => x.SalespersonId).ToList();
        }

        //for sales creation
        public List<int> GetCustomerIds()
        {
            return DBContext.Customers.Select(x => x.CustomerId).ToList();
        }

        public List<SalespersonCommissionReport> GetQuarterlyCommissionReport(DateTime startDate, DateTime endDate, int quarter, int year)
        {
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