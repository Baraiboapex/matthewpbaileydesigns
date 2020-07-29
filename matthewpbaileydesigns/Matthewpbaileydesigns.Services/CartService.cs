using Matthewpbaileydesigns.Core.Contracts;
using Matthewpbaileydesigns.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Matthewpbaileydesigns.Services
{
    public class CartService
    {
        IRepository<Product> productContext;
        IRepository<Cart> cartContext;

        public const string CartSession = "cartService";

        public CartService(IRepository<Product> products,IRepository<Cart> cart) 
        {
            productContext = products;
            cartContext = cart;
        }

        private Cart GetCart(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(CartSession);

            Cart cart = new Cart();

            if (cookie != null)
            {
                string cartId = cookie.Value;
                if (!string.IsNullOrEmpty(cartId))
                {
                    cart = cartContext.Find(cartId);
                }
                else
                {
                    cart = CreateNewCart(httpContext);
                }
            }
            else 
            {
                if (createIfNull)
                {
                    cart = CreateNewCart(httpContext);
                }
            }
        }

        private Cart CreateNewCart(HttpContextBase httpContext)
        {
            Cart cart = new Cart();
            cartContext.Insert(cart);
            cartContext.Commit();

            HttpCookie cookie = new HttpCookie(CartSession);
            cookie.Value = cart.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return cart;
        }

        public void AddToBasket(HttpContextBase httpContext, string productId)
        {
            Cart cart = GetCart(httpContext, true);
            CartItem item = cart.cartItems.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            {
                item = new CartItem()
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = 1
                };
            }
            else
            {
                item.Quantity += 1;
            }

            cartContext.Commit();
        }

        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Cart cart = GetCart(httpContext, true);
            CartItem item = cart.cartItems.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
            {
                cart.cartItems.Remove(item);
                cartContext.Commit();
            }
        }
    }
}
