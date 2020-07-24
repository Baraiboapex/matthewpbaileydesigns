using Matthewpbaileydesigns.Core.Contracts;
using Matthewpbaileydesigns.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Matthewpbaileydesigns.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategoryContext;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> categoryContext)
        {
            context = productContext;
            productCategoryContext = categoryContext;
        }

        public ActionResult Index()
        {
            var products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Details(string Id)
        {
            Product productToView = context.Find(Id);

            if (productToView == null)
            {
                return HttpNotFound("Product not found");
            }
            else 
            {
                return View(productToView);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}