using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BidController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("bid/placeBid")]
        public Task<IActionResult> PlaceBid()
        {

        }
    }
}
