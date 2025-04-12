using Application.DTOs.AuctionSession;
using Application.Interface.IServices;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Infrustucture.Hubs
{
    public class AuctionHub : Hub
    {
        private static readonly ConcurrentDictionary<string, List<UserInfor>> AuctionGroups = new();

        private readonly IAuctionSessionService _auctionService;

        public AuctionHub(IAuctionSessionService auctionService)
        {
            _auctionService = auctionService;
        }

        public override Task OnConnectedAsync()
        {
            // Optional: log connection
            return base.OnConnectedAsync();
        }

        public async Task JoinAuction(int auctionId)
        {
            string groupName = $"auction_{auctionId}";
            string connectionId = Context.ConnectionId;
            string userId = Context.UserIdentifier ?? connectionId;
            string userName = Context.User?.Identity?.Name ?? $"Guest_{connectionId[..6]}";

            string sellerId = await _auctionService.GetSellerIdByAuctionId(auctionId);
            bool isHost = userId == sellerId;

            await Groups.AddToGroupAsync(connectionId, groupName);

            var userInfo = new UserInfor
            {
                ConnectionId = connectionId,
                UserName = userName,
                IsHost = isHost
            };

            AuctionGroups.AddOrUpdate(groupName,
                _ => new List<UserInfor> { userInfo },
                (_, list) =>
                {
                    if (!list.Any(u => u.ConnectionId == connectionId))
                        list.Add(userInfo);
                    return list;
                });

            // Gửi vai trò cho client
            await Clients.Client(connectionId).SendAsync("SetRole", isHost);

            // Cập nhật danh sách cho cả group
            await UpdateGroupState(groupName, auctionId);
        }

        public async Task KickUser(string connectionId, int auctionId)
        {
            string groupName = $"auction_{auctionId}";
            string callerId = Context.UserIdentifier ?? Context.ConnectionId;
            string sellerId = await _auctionService.GetSellerIdByAuctionId(auctionId);

            if (callerId != sellerId)
            {
                await Clients.Caller.SendAsync("KickFailed", "Bạn không có quyền thực hiện.");
                return;
            }

            await Groups.RemoveFromGroupAsync(connectionId, groupName);

            if (AuctionGroups.TryGetValue(groupName, out var users))
            {
                users.RemoveAll(u => u.ConnectionId == connectionId);
            }

            await Clients.Client(connectionId).SendAsync("YouHaveBeenKicked", auctionId);
            await UpdateGroupState(groupName, auctionId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            var affectedGroups = new List<(string group, int auctionId)>();

            foreach (var kvp in AuctionGroups)
            {
                var removed = kvp.Value.RemoveAll(u => u.ConnectionId == connectionId);
                if (removed > 0 && int.TryParse(kvp.Key.Split('_')[1], out int auctionId))
                {
                    affectedGroups.Add((kvp.Key, auctionId));
                }
            }

            foreach (var (groupName, auctionId) in affectedGroups)
            {
                await UpdateGroupState(groupName, auctionId);
            }

            await base.OnDisconnectedAsync(exception);
        }

        private async Task UpdateGroupState(string groupName, int auctionId)
        {
            if (AuctionGroups.TryGetValue(groupName, out var users))
            {
                await Clients.Group(groupName).SendAsync("UpdateParticipantList", auctionId, users);
            }
        }
    }
}
