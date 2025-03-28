using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IRepository
{
    public interface IAccountRepository : IRepository<Account>
    {
        public UserManager<Account> UserManager { get; }

        public SignInManager<Account> SignInManager { get; }

    }
}
