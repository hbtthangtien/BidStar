using Application.DTOs.Account;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
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
        public IActionResult Register(CreateAccountDTO dto)
        {
            return Ok(dto);
        }
        [HttpPost]
        public IActionResult Login(LoginDTORequest dto)
        {
            return Ok(dto);
        }
    }
}
