using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public required string SellerId { get; set; }

        public int? CategoryId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double StartPrice { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? CreatedDate { get; set; }

        public ProductStatus ProductStatus { get; set; }

        public virtual Seller? Seller { get; set; } 

        public virtual Category? Category { get; set;}

        public virtual ICollection<ImageProduct> ImageProducts { get; set; } = new List<ImageProduct>();
    }
}
