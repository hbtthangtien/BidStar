using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Enum;
using Microsoft.Extensions.Hosting;
using Persistence.DatabaseConfig;

namespace Presentation.BackgroundServices
{
    public class AuctionSessionStatusBackground : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AuctionSessionStatusBackground(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<BidStarContext>();
                var scheduler = scope.ServiceProvider.GetRequiredService<AuctionSessionScheduler>();
                var now = DateTime.Now;
                var auctions = db.AuctionSessions
                    .Where(a => a.AuctionSatus == Domain.Enum.AuctionSatus.Scheduled && a.StartTime <= now)
                    .ToList();
                foreach (var a in auctions) a.AuctionSatus = AuctionSatus.Ongoing;
                var auctions2 = db.AuctionSessions
                    .Where(a => a.AuctionSatus == Domain.Enum.AuctionSatus.Ongoing && a.EndTime <= now)
                    .ToList();
                foreach (var a in auctions2)
                {
                    scheduler.ForceEndAuctionTime(a.Id);
                }

                await db.SaveChangesAsync();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
