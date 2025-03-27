using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Seller
    {
        public required string AccountId { get; set; }

        public virtual Account? Account { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>()!;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>()!;    
    }
}
