using Application.Interface.IServices;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Seller.Models;
using System.Security.Claims;

namespace Presentation.Areas.Seller.Controllers
{
    [Area("seller")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var sellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetOrdersBySellerAsync(sellerId);

            var vm = orders.Select(o => new OrderForSellerVM
            {
                OrderId = o.Id,
                ProductName = o.Product?.Name,
                ProductImage = o.Product?.ImageUrl ?? "/images/no-img.png",
                BuyerName = o.Buyer?.Account?.UserName ?? o.BuyerId,
                Total = o.Total,
                DateOrder = o.DateOrder,
                Status = o.OrderStatus,
                AuctionSessionLink = $"/AuctionSession/DetailAuction/{o.AuctionSessionId}"
            }).ToList();

            return View(vm);
        }
    }
}
