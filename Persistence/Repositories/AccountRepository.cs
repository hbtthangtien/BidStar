using Application.Interface.IRepository;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Persistence.DatabaseConfig;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(BidStarContext context,
            UserManager<Account> userManager,
            SignInManager<Account> signInManager) : base(context)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public UserManager<Account> UserManager { get; private set; }

        public SignInManager<Account> SignInManager { get; private set; }
    }
}
