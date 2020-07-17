using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matthewpbaileydesigns.Core.Models;

namespace Matthewpbaileydesigns.Core.ViewModels
{
    public class ProductManageViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
