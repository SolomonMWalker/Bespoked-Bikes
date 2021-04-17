using Microsoft.AspNetCore.Mvc;
using Service;

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