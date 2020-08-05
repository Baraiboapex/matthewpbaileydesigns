using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Matthewpbaileydesigns.WebUI;
using Matthewpbaileydesigns.WebUI.Controllers;
using Matthewpbaileydesigns.Core.Contracts;
using Matthewpbaileydesigns.Core.Models;
using Matthewpbaileydesigns.WebUI.Tests.Mocks;
using Matthewpbaileydesigns.Core.ViewModels;

namespace Matthewpbaileydesigns.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexPageReturnsProducts()
        {
            IRepository<Product> productContext = new MockContext<Product>();
            IRepository<ProductCategory> categoryContext = new MockContext<ProductCategory>();

            productContext.Insert(new Product());

            HomeController controller = new HomeController(productContext, categoryContext);

            var result = controller.Index() as ViewResult;

            var viewModel = (ProductListViewModel)result.ViewData.Model;

            Assert.AreEqual(1, viewModel.Product.Count());
        }

    }
}
