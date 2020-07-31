using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matthewpbaileydesigns.Core.ViewModels
{
    public class CartSummary
    {
        public int CartCount { get; set; }
        public decimal CartTotal { get; set; }

        public CartSummary(int cartCount, decimal cartTotal)
        {
            this.CartCount = cartCount;
            this.CartTotal = cartTotal;
        }

        public CartSummary()
        { 
            
        }
    }
}
