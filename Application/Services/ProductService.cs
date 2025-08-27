using Application.DTOs.Product;
using Application.Interface.IExternalService;
using Application.Interface.IServices;
using Application.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : Service, IProductService
    {
        private readonly ICloudinaryService _cloudineryService;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinary) : base(unitOfWork, mapper)
        {
            _cloudineryService = cloudinary;
        }

        public async Task<dynamic> CountProductsBySellerAsync(string? sellerId)
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var numberOfProduct = products.Where(e => e.SellerId == sellerId).Count();
            return numberOfProduct;
        }

        public async Task CreateProductBySellerId(ProductDTOCreate dto, List<IFormFile> files)
        {
            var product = _mapper.Map<Product>(dto);
            var listImage = new List<ImageProduct>();
            foreach (var file in files)
            {
                string imageUrl = await _cloudineryService.UploadImageFileAsync(file);
                product.ImageUrl = imageUrl;
                var image = new ImageProduct
                {
                    ProductId = product.Id,
                    Url = imageUrl,
                };
                listImage.Add(image);
            }
            product.ImageProducts = listImage;
            
            
            product.CreatedDate = DateTime.Now;
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CommitAsync(); 
        }

        public async Task DeleteProductById(int id)
        {
            var product = await _unitOfWork.Products.GetSingle(e => e.Id == id);
            _unitOfWork.Products.RemoveAsync(product!);
            await _unitOfWork.CommitAsync();
        }

        public async Task EditProductById(ProductDTODetail dto, IFormFile file)
        {
            var product = await _unitOfWork.Products.GetSingle(e => e.Id == dto.Id);
            if(file != null)
            {
                string url = await _cloudineryService.UploadImageFileAsync(file);
                product!.ImageUrl = url;
            }
            product!.StartPrice = dto.StartPrice;
            product.CategoryId = dto.CategoryId;
            product.Description = dto.Description;
            product.Name = dto.Name;
            _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ProductDTODetail> GetProductDetailById(int id)
        {
            var data =await _unitOfWork.Products.GetSingle(e => e.Id == id);
            var productDTO = _mapper.Map<ProductDTODetail>(data);
            return productDTO;
        }

        public async Task<List<ProductDTO>> GetProductsBySellerIdAsync(string selerId)
        {
            var data =await _unitOfWork.Products.FindAsync(e => e.SellerId == selerId);
            var products = _mapper.Map<List<ProductDTO>>(data);
            return products;
        }

        public async Task<List<ProductDTO>> GetProductsPendingBySellerIdAsync(string sellerId)
        {
            var data = await _unitOfWork.Products.FindAsync(e => e.SellerId == sellerId 
            && e.ProductStatus == Domain.Enum.ProductStatus.Pending);
            var products = _mapper.Map<List<ProductDTO>>(data);
            return products;
        }
    }
}
