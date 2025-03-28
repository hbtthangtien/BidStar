using Application.DTOs.Vnpay;
using Application.Interface.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var list = await _paymentService.GetAllPaymentByUserId(userId!);
            return View(list);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RechargeBalance(double Amount)
        {
            string url = await _paymentService.CreatePaymentAsync(new VnpayDTORequest { Amount = Amount}, HttpContext);
            return Redirect(url);
        }

        [HttpGet]
        public async Task<IActionResult> Callback()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _paymentService.ExecutePaymentAsync(Request.Query,userId!);
            return RedirectToAction("index");
        }
    }
}
