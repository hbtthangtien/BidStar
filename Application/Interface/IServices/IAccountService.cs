using Application.DTOs.Account;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IAccountService
    {
        public Task<CreateAccountDTO> CreateAccountAsync(CreateAccountDTO dto);

        public Task ConfirmEmail(string UserId, string token);

        public Task SendEmailConfirmAsync(Account account);

        public Task<ProfileDTO> GetProfileAsync(string userId);
    }
}
