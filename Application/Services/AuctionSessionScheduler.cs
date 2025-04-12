using Application.Interface.IServices;
using Infrustucture.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.BackgroundServices
{
    public class AuctionSessionScheduler
    {
        private readonly IServiceProvider _serviceProvider;

        public AuctionSessionScheduler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ScheduleAuctionSessionEndTime(int auctionId, DateTime endTime)
        {
            var delay = endTime - DateTime.Now;
            if(delay<= TimeSpan.Zero)
            {
                _ = EndAuctionSession(auctionId);
                return;
            }
            var timer = new System.Timers.Timer(delay.TotalMilliseconds) 
            {
                AutoReset = false
            };
            // add event when timer is end
            timer.Elapsed += async (s, e) =>
            {
                await EndAuctionSession(auctionId);
                timer.Dispose();
            };
            timer.Start();
        }
        public void ForceEndAuctionTime(int auctionId)
        {
            _ = EndAuctionSession(auctionId);
        }
        private async Task EndAuctionSession(int sessionId)
        {
            using var scope = _serviceProvider.CreateScope();
            var auctionService = scope.ServiceProvider.GetRequiredService<IAuctionSessionService>();
            var hub = scope.ServiceProvider.GetRequiredService<IHubContext<AuctionHub>>();
            bool isSolved = await auctionService.SolveAuctionCompleted(sessionId);
            if(!isSolved)
            {
                return;
            }
            await hub.Clients.Group($"auction_{sessionId}").SendAsync("AuctionEnd",sessionId);
            await hub.Clients.All.SendAsync("AuctionEnd",sessionId);
        }
    }
}
