using Application.DTOs.Account;
using Application.Interface.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IAuthenticateService _authenticateService;
        public AccountController(IAccountService accountService, IAuthenticateService authenticateService)
        {
            _accountService = accountService;
            _authenticateService = authenticateService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(new CreateAccountDTO());
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginDTORequest());
        }
        [HttpPost]
        public async Task<IActionResult> Register(CreateAccountDTO dto)
        {
            try
            {
                var response =await _accountService.CreateAccountAsync(dto);
                return RedirectToAction("login");
            }catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View(dto);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTORequest dto)
        {
            try
            {
                await _authenticateService.AuthenticateCretial(dto);
                return RedirectToAction("Index", "Home");
            }catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View(dto);
            }
        }

        [HttpGet("account/confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string UserId,string token)
        {
            try
            {
                await _accountService.ConfirmEmail(UserId, token);
                return View();
            }catch(Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authenticateService.Logout();
            return RedirectToAction("Login");
        }
    }
}
