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
    public class CustomersController : Controller
    {
        private CustomerService customerService;
        public CustomersController(CustomerService cService)
        {
            customerService = cService;
        }

        // GET: CustomersController
        public ActionResult Index()
        {
            var customers = customerService.GetCustomers();
            return View("DisplayCustomersView", customers);
        }

    }
}
