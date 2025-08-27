using Application.DTOs.Product;
using Application.Interface.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Areas.Seller.Controllers
{
    [Area("seller")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        
        public async Task<IActionResult> Index()
        {
            var sellerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var products = await _productService.GetProductsBySellerIdAsync(sellerId!);
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            ViewBag.Categories = categories;
            return View(new ProductDTOCreate());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTOCreate dto, List<IFormFile> ImageFiles)
        {
            var sellerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            dto.SellerId = sellerId;
            await _productService.CreateProductBySellerId(dto,ImageFiles);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product =await _productService.GetProductDetailById(id);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductDetailById(id);
            var categories = await _categoryService.GetAllCategoryAsync();
            ViewBag.Categories = categories;
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTODetail dto, IFormFile ImageFile)
        {
            await _productService.EditProductById(dto,ImageFile);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductById(id);
            return RedirectToAction("Index");
        }
    }
}
