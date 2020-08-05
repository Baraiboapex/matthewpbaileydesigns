using System;
using System.Linq;
using System.Web.Mvc;
using Matthewpbaileydesigns.Core.Contracts;
using Matthewpbaileydesigns.Core.ViewModels;
using Matthewpbaileydesigns.Core.Models;
using Matthewpbaileydesigns.Services;
using Matthewpbaileydesigns.WebUI.Controllers;
using Matthewpbaileydesigns.WebUI.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matthewpbaileydesigns.WebUI.Tests.Controllers
{
    [TestClass]
    public class CartControllerTest
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            IRepository<Cart> carts = new MockContext<Cart>();
            IRepository<Product> products = new MockContext<Product>();

            var httpContext = new MockHttpContext();

            ///Cart Service Test
            ICartService cartService = new CartService(products, carts);
            
            cartService.AddToCart(httpContext, "1");

            Cart cart = carts.Collection().FirstOrDefault();

            Assert.IsNotNull(cart);
            Assert.AreEqual(1,cart.cartItems.Count());
            Assert.AreEqual("1", cart.cartItems.ToList().FirstOrDefault().ProductId);
            ///Eof Cart Service Test

            ///Cart Controller Test
            var controller = new CartController(cartService);

            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            controller.AddToCart("2");

            Assert.IsNotNull(cart);
            Assert.AreEqual(2, cart.cartItems.Count());
            Assert.AreEqual("2", cart.cartItems.ToList().Where(item => item.ProductId == "2").FirstOrDefault().ProductId);
            ///Cart Controller Test
        }

        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            IRepository<Cart> carts = new MockContext<Cart>();
            IRepository<Product> products = new MockContext<Product>();

            products.Insert(new Product() { Id = "1", Price=15.00m});
            products.Insert(new Product() { Id = "2", Price = 5.00m });

            Cart cart = new Cart();
            cart.cartItems.Add(new CartItem() { ProductId = "1", Quantity = 1 });
            cart.cartItems.Add(new CartItem() { ProductId = "2", Quantity = 2 });
            carts.Insert(cart);

            ICartService cartService = new CartService(products, carts);

            var controller = new CartController(cartService);
            var httpContext = new MockHttpContext();

            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("cartService") { Value = cart.Id});
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            var result = controller.CartSummary() as PartialViewResult;
            var cartSummary = (CartSummary)result.ViewData.Model;

            Assert.AreEqual(3, cartSummary.CartCount);
            Assert.AreEqual(25.00m, cartSummary.CartTotal);
        }
    }
}
