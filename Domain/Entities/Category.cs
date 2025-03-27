using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; } 

        public required string Name { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>()!;
    }
}
