using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Service;
using System;
using System.Linq;

namespace Client.Controllers
{
    public class SalesController : Controller
    {
        private SalesService salesService;

        public SalesController(SalesService sService)
        {
            salesService = sService;
        }

        // GET: SalesController
        public ActionResult Index()
        {
            var sales = salesService.GetSales();
            return View("DisplaySalesView", sales);
        }

        public ActionResult FilteredIndex(DateTime startDate, DateTime endDate)
        {
            var sales = salesService.GetSales(startDate, endDate);
            return View("DisplaySalesView", sales);
        }

        // GET: SalesController/Create
        public ActionResult Create()
        {
            ViewBag.ProductIds = salesService.GetProductIds().Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() });
            ViewBag.SalespersonIds = salesService.GetSalespersonIds().Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() });
            ViewBag.CustomerIds = salesService.GetCustomerIds().Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() });
            return View("CreateSaleView");
        }

        // POST: SalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSale(IFormCollection collection)
        {
            var sale = new Sale
            {
                ProductId = Int32.Parse(collection["ProductId"]),
                SalespersonId = Int32.Parse(collection["SalespersonId"]),
                CustomerId = Int32.Parse(collection["CustomerId"]),
                SalesDate = DateTime.Parse(collection["SalesDate"])
            };

            try
            {
                salesService.CreateSale(sale);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                var error = new Models.ErrorViewModel();
                error.ErrorMessage = "Failure to create sale. Check the database connection or if you are entering valid values.";
                return View("Error", error);
            }
        }
    }
}