using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class ProductsController : Controller
    {
        private ProductService productService;
        public ProductsController(ProductService pService)
        {
            productService = pService;
        }

        // GET: ProductsController
        public ActionResult Index()
        {
            var products = productService.GetProducts();
            return View("DisplayProductsView", products);
        }


        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = productService.GetProduct(id);
            return View("EditProductView", product);
        }

        public ActionResult Update(int id, IFormCollection collection)
        {
            var product = new Product
            {
                ProductId = Int32.Parse(collection["ProductId"]),
                Name = collection["Name"],
                Manufacturer = collection["Manufacturer"],
                Style = collection["Style"],
                PurchasePrice = Decimal.Parse(collection["PurchasePrice"]),
                SalePrice = Decimal.Parse(collection["SalePrice"]),
                QtyOnHand = Int32.Parse(collection["QtyOnHand"]),
                CommissionPercentage = Decimal.Parse(collection["CommissionPercentage"])
            };

            try
            {
                if (!productService.UpdateProduct(product))
                {
                    throw (new Exception("Failure to update product. Check the database connection or if you are creating a duplicate salesperson."));
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
