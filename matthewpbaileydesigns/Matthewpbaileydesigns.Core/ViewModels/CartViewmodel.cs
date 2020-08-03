using Matthewpbaileydesigns.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matthewpbaileydesigns.Core.ViewModels
{
    public class CartViewmodel
    {
        public List<CartItemViewModel> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}
