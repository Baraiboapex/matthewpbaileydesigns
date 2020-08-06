using Matthewpbaileydesigns.Core.Contracts;
using Matthewpbaileydesigns.Core.Models;
using Matthewpbaileydesigns.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matthewpbaileydesigns.Services
{
    public class OrderService : IOrderService
    {
        IRepository<Order> OrderContext;
        public OrderService(IRepository<Order> OrderContext)
        {
            this.OrderContext = OrderContext;
        }

        public void CreateOrder(Order baseOrder, List<CartItemViewModel> cartItems)
        {
            foreach (var item in cartItems)
            {
                baseOrder.OrderItems.Add(new OrderItem()
                {
                    ProductId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    ProductName = item.Name,
                    Quantity = item.Quantity
                });

                OrderContext.Insert(baseOrder);
                OrderContext.Commit();
            }
        }
    }
}
