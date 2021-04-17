using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return View();
        }

        // POST: SalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
