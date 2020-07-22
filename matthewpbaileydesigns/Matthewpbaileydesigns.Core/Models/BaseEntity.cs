using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matthewpbaileydesigns.Core.Models
{
    public abstract class BaseEntity
    {
       public string Id { get; set; }
       public DateTime CreatedAt { get; set; }

        public BaseEntity() {
            this.Id = new Guid().ToString();
            this.CreatedAt = DateTime.Now;
        }
    }
}
