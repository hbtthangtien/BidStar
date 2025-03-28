using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DatabaseConfig
{
    public static class DbConfig
    {
        public static void AddIdentityConfig(this IServiceCollection services)
        {
            services.AddIdentity<Account,IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;

                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<BidStarContext>()
                .AddDefaultTokenProviders();            
        }
        public static void AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BidStarContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MyDatabase"));
            });
        }
    }
}
