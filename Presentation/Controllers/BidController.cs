using Application.DTOs.Bids;
using Application.Interface.IServices;
using Domain.ExceptionCustom;
using Infrustucture.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class BidController : Controller
    {
        private readonly IBidService _bidService;
        private readonly IHubContext<AuctionHub> _hubContext;
        public BidController(IBidService bidService, IHubContext<AuctionHub> hubContext)
        {
            _bidService = bidService;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("bid/placeBid")]
        public async Task<IActionResult> PlaceBid([FromBody]PlaceOrderBidDTO dto)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            dto.BuyerId = userId;
            try
            {
                var data = await _bidService.PlaceOrderBid(dto);
                await _hubContext.Clients.Group($"auction_{dto.AuctionSessionId}").SendAsync("NewBids",data);
                return Ok(data);
            }
            catch(PlaceOrderBidException ex)
            {
                return BadRequest(new
                {
                    Message =  ex.Message,
                    Status = 400
                });
            }catch(Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    Status = 400
                });
            }
            
            
        }
    }
}
