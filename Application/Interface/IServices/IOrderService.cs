using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IOrderService
    {
        public Task CompletedOrder(int auctionId, string buyerId);
        Task<dynamic> CountOrdersBySellerAsync(string? sellerId);
        public Task CreateOrder(int auctionId);
        public Task<List<Order>> GetWinOrder(string userId);
        Task<List<Order>> GetOrdersBySellerAsync(string sellerId);
    }
}
