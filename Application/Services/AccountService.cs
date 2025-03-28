using Application.DTOs.Account;
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
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<CreateAccountDTO> CreateAccount(CreateAccountDTO dto)
        {
            if(dto.Password == dto.ConfirmPassword)
            {
                var entityUser = _mapper.Map<Account>(dto);
                var create = await _unitOfWork.Accounts.UserManager.CreateAsync(entityUser, dto.Password!);
                if (!create.Succeeded)
                {
                    throw new CreateException(create.Errors);
                }
                await _unitOfWork.Accounts.UserManager
                    .AddToRoleAsync(entityUser, (dto.UserRole == Domain.Enum.UserRole.Buyer) ? UserRole.SELLER : UserRole.BUYER);
                return dto;
            }
            else
            {
                throw new CreateException("Password and Confirm password is not correct!");
            }
            
        }
    }
}
