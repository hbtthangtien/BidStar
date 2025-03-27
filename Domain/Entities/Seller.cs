using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Seller
    {
        public virtual ICollection<Product> Products { get; set; } = new List<Product>()!;
    }
}
