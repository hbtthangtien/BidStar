using Application.DTOs.AuctionSession;
using Application.Interface.IServices;
using Infrustucture.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Presentation.Areas.Seller.Controllers
{
    [Area("seller")]
    [Authorize]
    public class AuctionSessionController : Controller
    {
        private readonly IAuctionSessionService _auctionSessionService;
        private readonly IProductService _productService;
        private readonly IHubContext<AuctionHub> _hubContext;
        public AuctionSessionController(IAuctionSessionService auctionSessionService,
            IProductService productService,
            IHubContext<AuctionHub> hub)
        {
            _auctionSessionService = auctionSessionService;
            _productService = productService;
            _hubContext = hub;

        }
        public async Task<IActionResult> Index()
        {
            var sellerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var list =await _auctionSessionService.GetAllAuctionBySellerId(sellerId!);
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> CreateList()
        {
            var sellerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var list = await _productService.GetProductsPendingBySellerIdAsync(sellerId!);
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> CreateAuction(int productId)
        {
            var sellerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var list = await _productService.GetProductsPendingBySellerIdAsync(sellerId!);
            ViewBag.Products = list;
            return View(new AuctionSessionDTO());
        }
        [HttpPost]
        public async Task<IActionResult> CreateAuction(AuctionSessionDTO dto)
        {
            var sellerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            dto.SellerId = sellerId!;
            await _auctionSessionService.CreateAuctionSessionBySeller(dto);
            await _hubContext.Clients.All.SendAsync("addSession");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DetailAuction(int id)
        {
           var auction = await _auctionSessionService.AuctionSessionDTODetail(id);
            return View(auction);
        }
    }
}
