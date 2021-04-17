using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;
using System;

namespace Client.Controllers
{
    public class SalespeopleController : Controller
    {
        private SalespersonService salespersonService;

        public SalespeopleController(SalespersonService spService)
        {
            salespersonService = spService;
        }

        // GET: SalespeopleController
        public ActionResult Index()
        {
            var salespeople = salespersonService.GetSalespeople();
            return View("DisplaySalespeopleView", salespeople);
        }

        // GET: SalespeopleController/Edit/5
        public ActionResult Edit(int id)
        {
            var salesperson = salespersonService.GetSalesperson(id);
            return View("EditSalespersonView", salesperson);
        }

        // POST: SalespeopleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, IFormCollection collection)
        {
            var startDate = DateTime.Parse(collection["StartDate"]);
            DateTime? terminationDate;
            if (String.IsNullOrEmpty(collection["TerminationDate"]))
            {
                terminationDate = null;
            }
            else
            {
                terminationDate = DateTime.Parse(collection["TerminationDate"]);
            }

            var salesperson = new Salesperson
            {
                SalespersonId = Int32.Parse(collection["SalespersonId"]),
                FirstName = collection["FirstName"],
                LastName = collection["LastName"],
                Address = collection["Address"],
                Phone = collection["Phone"],
                StartDate = startDate,
                TerminationDate = terminationDate,
                Manager = collection["Manager"]
            };

            try
            {
                if (!salespersonService.UpdateSalesperson(salesperson))
                {
                    throw (new Exception("Failure to update salesperson. Check the database connection or if you are creating a duplicate salesperson."));
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                var error = new Models.ErrorViewModel();
                error.ErrorMessage = e.Message;
                return View("Error", error);
            }
        }
    }
}