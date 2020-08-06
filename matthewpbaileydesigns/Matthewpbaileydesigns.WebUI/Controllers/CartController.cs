using Matthewpbaileydesigns.Core.Contracts;
using Matthewpbaileydesigns.Core.Models;
using Matthewpbaileydesigns.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Matthewpbaileydesigns.WebUI.Controllers
{
    public class CartController : Controller
    {
        ICartService cartService;
        IOrderService orderService;

        public CartController(ICartService cartService, IOrderService orderService)
        {
            this.cartService = cartService;
            this.orderService = orderService;
        }

        // GET: Cart
        public ActionResult Index()
        {
            var cartItems = cartService.GetCartItems(this.HttpContext).ToList();
            var cartTotal = (from p in cartItems select p.Price * p.Quantity).Sum();

            if (cartItems != null)
            {
                CartViewmodel currentCart = new CartViewmodel()
                {
                    CartItems = cartItems,
                    CartTotal = cartTotal
                };

                return View(currentCart);
            }
            else
            {
                return HttpNotFound("");
            }


        }

        public ActionResult AddToCart(string prodId)
        {
            cartService.AddToCart(this.HttpContext, prodId);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(string prodId)
        {
            cartService.RemoveFromCart(this.HttpContext, prodId);
            return RedirectToAction("Index");
        }

        public PartialViewResult CartSummary()
        {
            var cartSummary = cartService.GetCartSummary(this.HttpContext);

            return PartialView(cartSummary);
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckOut(Order order)
        {
            var cartItems = cartService.GetCartItems(this.HttpContext);
            order.OrderStatus = "Order Created";

            order.OrderStatus = "Payment Processed";
            orderService.CreateOrder(order, cartItems);
            cartService.ClearCart(this.HttpContext);

            return RedirectToAction("ThankYou", new { OrderId = order.Id });
        }

        public ActionResult ThankYou(string orderId)
        {
            ViewBag.OrderId = orderId;

            return View();
        }
    }
}