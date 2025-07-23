using Application.DTOs.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IProductService
    {
        public Task<List<ProductDTO>> GetProductsBySellerIdAsync(string sellerId);

        public Task CreateProductBySellerId(ProductDTOCreate dto, IFormFile file);

        public Task<ProductDTODetail> GetProductDetailById(int id);

        public Task EditProductById(ProductDTODetail dto, IFormFile file);

        public Task DeleteProductById(int id);
        public Task<List<ProductDTO>> GetProductsPendingBySellerIdAsync(string sellerId);
        Task<dynamic> CountProductsBySellerAsync(string? sellerId);
    }
}
