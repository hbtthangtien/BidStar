using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string? ImageUrl { get; set; }

        public double StartPrice { get; set; }

        public ProductStatus ProductStatus { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
