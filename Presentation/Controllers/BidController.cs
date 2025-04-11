using Application.DTOs.Bids;
using Application.Interface.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BidController : Controller
    {
        private readonly IBidService _bidService;

        public BidController(IBidService bidService)
        {
            _bidService = bidService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("bid/placeBid")]
        public async Task<IActionResult> PlaceBid([FromBody]PlaceOrderBidDTO dto)
        {
            var data = await _bidService.PlaceOrderBid(dto);
            return Ok(data);
        }
    }
}
