using Application.Interface.IServices;
using Infrustucture.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class AuctionSessionController : Controller
    {
        private IAuctionSessionService _auctionSessionService;
        private IJoinningService _joinningService;
        public AuctionSessionController(IAuctionSessionService auctionSessionService,
            IJoinningService joinningService,
            IHubContext<AuctionHub> hubContext)
        {
            _auctionSessionService = auctionSessionService;
            _joinningService = joinningService;
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
    }
}
