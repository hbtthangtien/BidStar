using Application.Interface.IServices;
using Application.Services;
using Domain.Enum;
using Infrustucture.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Presentation.Models;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class AuctionSessionController : Controller
    {
        private IAuctionSessionService _auctionSessionService;
        private IJoinningService _joinningService;
        private IOrderService _orderService;
        private IAccountService _accountService;
        public AuctionSessionController(IAuctionSessionService auctionSessionService,
            IJoinningService joinningService,
            IHubContext<AuctionHub> hubContext,
            IOrderService orderService,
            IAccountService accountService)
        {
            _auctionSessionService = auctionSessionService;
            _joinningService = joinningService;
           _orderService = orderService;
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _auctionSessionService.GetAllAuction();
            return View(data);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var buyerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _joinningService.JoinningSession(buyerId!, id);
                var dto = await _auctionSessionService.AuctionSessionDTODetail(id);
                ViewData["AuctionId"] = id;
                return View(dto);
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> CheckBalance(int auctionId)
        {
            var buyerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var buyer = await _accountService.GetProfileAsync(buyerId);
            var auction = await _auctionSessionService.GetAuctionById(auctionId);
            double requiredAmount = auction.BaseBalance > 0 ? auction.BaseBalance : auction.CurrentPrice;
            double balance = buyer.Balance;

            bool isEnough = balance >= requiredAmount;
            return Json(new
            {
                isEnough,
                balance,
                requiredAmount,
                message = isEnough ? "" : "Số dư của bạn không đủ (" + balance.ToString("N0") + " đ). Cần tối thiểu " + requiredAmount.ToString("N0") + " đ!"
            });
        }

        public async Task<IActionResult> Wins()
        {
            var buyerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var wins = await _orderService.GetWinOrder(buyerId!);
            var vm = wins.Select(o => new WinCardVM
            {
                OrderId = o.Id,
                AuctionId = o.AuctionSessionId,
                ProductName = o.Product?.Name,
                ProductImage = o.Product?.ImageUrl ?? "/images/no-img.png",
                FinalPrice = o.Total,
                SellerName = o.Seller?.Account?.Fullname ?? o.SellerId,
                Paid = o.OrderStatus == OrderStatus.Paid
            }).ToList();

            return View(vm);
        }

    }
}
