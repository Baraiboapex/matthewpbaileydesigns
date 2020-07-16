﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matthewpbaileydesigns.Core.Models
{
    public class Product
    {
        public string Id { get; set; }
        [StringLength(20)]
        [DebuggerDisplay("Product Name")]
        public string Name { get; set; }
        [Range(0,100000)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

        public Product() {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
