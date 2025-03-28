using Application.Interface.IExternalService;
using Application.Interface.IRepository;
using Application.Interface.IServices;
using Application.Services;
using Application.UnitOfWork;
using AutoMapper;
using Domain.Configs;
using Domain.Entities;
using Infrustucture.ExternalService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustucture.DIConfig
{
    public static class Config
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IAccountRepository,AccountRepository>();
            services.AddScoped<IAuctionSessionRepository,AuctionSessionRepository>();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped<IBuyerRepository, BuyerRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IJoinningRepository, JoinningRepository>();  
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository,PaymentRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISellerRepository,SellerRepository>();

        }

        public static void AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuctionSessionService, AuctionSessionService>();
            services.AddScoped<IBidService, BidService>();
            services.AddScoped<IBuyerService, BuyerService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IJoinningService, JoinningService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISellerService, SellerService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
        }
        public static void AddOtherService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationMapper));
            services.AddScoped<ISenderService,EmailService>();
        }
        public static void InitialValueConfig(this IServiceCollection services,IConfiguration configuration)
        {
            var config = configuration.GetSection("EmailConfig");
            services.Configure<EmailConfig>(config);
        }
    }
}
