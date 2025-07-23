using Application.Interface.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Areas.Seller.Controllers
{
    [Area("seller")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IAuctionSessionService _auctionSessionService;
        private readonly IOrderService _orderService;
        public HomeController(IAuctionSessionService auctionSessionService, IOrderService orderService, IProductService productService)
        {
            _auctionSessionService = auctionSessionService;
            _productService = productService;
            _orderService = orderService;

        }
        public async Task<IActionResult> IndexAsync()
        {
            var sellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var productCount = await _productService.CountProductsBySellerAsync(sellerId);
            var auctionCount = await _auctionSessionService.CountAuctionsBySellerAsync(sellerId);
            var orderCount = await _orderService.CountOrdersBySellerAsync(sellerId);

            ViewBag.ProductCount = productCount;
            ViewBag.AuctionCount = auctionCount;
            ViewBag.OrderCount = orderCount;

            return View();
        }
    }
}
