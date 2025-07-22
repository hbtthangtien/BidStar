using Application.Interface.IServices;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuctionSessionService _auctionService;

        public HomeController(ILogger<HomeController> logger, IAuctionSessionService auctionSessionService)
        {
            _logger = logger;
            _auctionService = auctionSessionService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var auctions = await _auctionService.GetActiveAuctionsAsync();
            var model = new BuyerViewModel
            {
                Auctions = auctions.Select(a => new AuctionProductViewModel
                {
                    AuctionId = a.Id,
                    ProductName = a.Product?.Name ?? "(Chưa đặt tên)",
                    ProductImage = a.Product?.ImageUrl ?? "/images/no-image.png",
                    CurrentPrice = a.CurrentPrice,
                    BidCount = a.Bids?.Count ?? 0,
                    EndTime = a.EndTime,
                    StartTime = a.StartTime,
                    AuctionStatus = a.AuctionSatus
                }).ToList()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
