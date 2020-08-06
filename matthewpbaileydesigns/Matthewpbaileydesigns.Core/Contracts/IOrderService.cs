using Matthewpbaileydesigns.Core.Models;
using Matthewpbaileydesigns.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matthewpbaileydesigns.Core.Contracts
{
    public interface IOrderService
    {
        void CreateOrder(Order baseOrder, List<CartItemViewModel> cartItems);
    }
}
