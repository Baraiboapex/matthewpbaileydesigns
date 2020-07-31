using Matthewpbaileydesigns.Core.Contracts;
using Matthewpbaileydesigns.Core.Models;
using Matthewpbaileydesigns.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Matthewpbaileydesigns.Services
{
    public class CartService : ICartService
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

            return cart;
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

        public void AddToCart(HttpContextBase httpContext, string productId)
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

        public void RemoveFromCart(HttpContextBase httpContext, string itemId)
        {
            Cart cart = GetCart(httpContext, true);
            CartItem item = cart.cartItems.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
            {
                cart.cartItems.Remove(item);
                cartContext.Commit();
            }
        }

        public List<CartItemViewModel> GetCartItems(HttpContextBase httpContext)
        {
            Cart cart = GetCart(httpContext, false);

            if (cart != null)
            {
                var result = (
                    from c in cart.cartItems
                    join p in productContext.Collection()
                    on c.ProductId equals p.Id
                    select new CartItemViewModel()
                    {
                        Id = c.Id,
                        Quantity = c.Quantity,
                        Name = p.Name,
                        Image = p.Image,
                        Price = p.Price
                    }
                ).ToList();

                return result;
            }
            else
            {
                return new List<CartItemViewModel>();
            }
        }

        public CartSummary GetCartSummary(HttpContextBase httpContext)
        {
            Cart cart = GetCart(httpContext, false);
            CartSummary cartSummary = new CartSummary(0,0);
            if (cart != null)
            {
                int? cartCount = (from item in cart.cartItems select item.Quantity).Sum();

                decimal? cartTotal = (from item in cart.cartItems 
                                      join p in productContext.Collection() 
                                      on item.ProductId equals p.Id 
                                      select item.Quantity * p.Price
                                      ).Sum();

                cartSummary.CartCount = cartCount ?? 0;
                cartSummary.CartTotal = cartTotal ?? decimal.Zero;

                return cartSummary;
            }
            else
            {
                return cartSummary;
            }
        }
    }
}
