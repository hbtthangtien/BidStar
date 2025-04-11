using Infrustucture.DIConfig;
using Persistence.DatabaseConfig;
using Presentation.BackgroundServices;
using Infrustucture.Hubs;
namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDatabaseConfig(builder.Configuration);
            builder.Services.AddIdentityConfig();
            builder.Services.AddApplicationService();
            builder.Services.AddPersistence();
            builder.Services.AddOtherService();
            builder.Services.InitialValueConfig(builder.Configuration);
            builder.Services.AddHostedService<AuctionSessionStatusBackground>();
            builder.Services.AddSignalR();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapHub<AuctionHub>("/auctionHub");
            app.MapControllerRoute(
               name: "seller",
               pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
               );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
