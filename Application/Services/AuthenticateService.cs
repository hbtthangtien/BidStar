using Application.DTOs.Account;
using Application.Interface.IExternalService;
using Application.Interface.IServices;
using Application.UnitOfWork;
using AutoMapper;
using Domain.ExceptionCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthenticateService : Service, IAuthenticateService
    {
        private readonly IAccountService _accountService;
        public AuthenticateService(IUnitOfWork unitOfWork, IMapper mapper,
            IAccountService accountService) : base(unitOfWork, mapper)
        {
            _accountService = accountService;
        }

        public async Task AuthenticateCretial(LoginDTORequest request)
        {
            var account = (request.UsernameOrEmail!.Contains('@'))
                        ? await _unitOfWork.Accounts.UserManager.FindByEmailAsync(request.UsernameOrEmail)
                        : await _unitOfWork.Accounts.UserManager.FindByNameAsync(request.UsernameOrEmail)
                        ?? throw new LoginException("Username or Email is not existed!");
            var checkPassword = await _unitOfWork.Accounts.SignInManager.CheckPasswordSignInAsync(account!, request.Password!, false);
            if(!checkPassword.Succeeded)
            {
                throw new LoginException("Password is not correct!");
            }
            else
            {
                var checkEmail = await _unitOfWork.Accounts.UserManager.IsEmailConfirmedAsync(account!);
                if (!checkEmail)
                {
                    await _accountService.SendEmailConfirmAsync(account!);
                }
                else
                {
                    await _unitOfWork.Accounts.SignInManager
                        .PasswordSignInAsync(account!, request.Password!,false,lockoutOnFailure:false);
                }
            }
        }

        public async Task Logout()
        {
            await _unitOfWork.Accounts.SignInManager.SignOutAsync();
        }
    }
}
