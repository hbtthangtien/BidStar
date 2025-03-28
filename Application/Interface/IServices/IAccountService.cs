using Application.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IAccountService
    {
        public Task<CreateAccountDTO> CreateAccount(CreateAccountDTO dto);
    }
}
