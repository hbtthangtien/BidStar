using Application.DTOs.Account;
using Application.Interface.IExternalService;
using Application.Interface.IServices;
using Application.UnitOfWork;
using AutoMapper;
using Domain.Constant;
using Domain.Entities;
using Domain.ExceptionCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AccountService : Service, IAccountService
    {
        private readonly ISenderService _sender;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, ISenderService sender) : base(unitOfWork, mapper)
        {
            _sender = sender;
        }

        public async Task<bool> CheckValidBalance(string userId, double amount)
        {
            bool validBalance = await _unitOfWork.Accounts.HasAnyAsync(e => e.Balance >= amount && e.Id == userId);
            return validBalance;
        }

        public async Task ConfirmEmail(string UserId, string token)
        {
            var User = await _unitOfWork.Accounts.UserManager.FindByIdAsync(UserId)
                ?? throw new Exception("User is not existed!");
            var confirmEmail = await _unitOfWork.Accounts.UserManager.ConfirmEmailAsync(User, token);
            if(!confirmEmail.Succeeded)
            {
                throw new Exception("Token is not valid!!!");
            }
        }

        public async Task<CreateAccountDTO> CreateAccountAsync(CreateAccountDTO dto)
        {
            if(dto.Password == dto.ConfirmPassword)
            {
                var entityUser = new Account
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                    Balance = 0
                };
                var create = await _unitOfWork.Accounts.UserManager.CreateAsync(entityUser, dto.Password!);
                if (!create.Succeeded)
                {
                    throw new CreateException(create.Errors);
                }
                await _unitOfWork.Accounts.UserManager
                    .AddToRoleAsync(entityUser, (dto.UserRole == Domain.Enum.UserRole.Buyer) ? UserRole.BUYER : UserRole.SELLER);
                if(dto.UserRole == Domain.Enum.UserRole.Buyer)
                {
                    var buyer = new Buyer { BuyerId = entityUser.Id };
                    await _unitOfWork.Buyers.AddAsync(buyer);
                }
                else
                {
                    var seller = new Seller { AccountId = entityUser.Id };
                    await _unitOfWork.Sellers.AddAsync (seller);
                }
                await _unitOfWork.CommitAsync();
                await SendEmailConfirmAsync(entityUser);
                return dto;
            }
            else
            {
                throw new CreateException("Password and Confirm password is not correct!");
            }
            
        }

        public async Task<ProfileDTO> GetProfileAsync(string userId)
        {
            var user = await _unitOfWork.Accounts.UserManager.FindByIdAsync(userId);
            var profile = new ProfileDTO
            {
                Address = user.Address,
                Balance = (double)user.Balance!,
                Email = user.Email,
                Username = user.UserName
            };
            return profile;

        }

        public async Task SendEmailConfirmAsync(Account account)
        {
            string token = await _unitOfWork.Accounts.UserManager.GenerateEmailConfirmationTokenAsync(account);
            UriBuilder uriBuilder = LinkConstant.UriBuilder(account.Id, token, "confirm-email");
            var link = uriBuilder.ToString();
            var Body = EmailBody.CONFIRM_EMAIL(account.Email!,link);
            await _sender.SendMailAsync(EmailSubject.CONFIRM_EMAIL, EmailBody.CONFIRM_EMAIL(account.Email!,link), account.Email!);
        }

        public async Task UpdateBalance(string userId, double amount)
        {
            var user = await _unitOfWork.Accounts.UserManager.FindByIdAsync(userId);
            user.Balance += amount;
            await _unitOfWork.CommitAsync();
        }
    }
}
