using Application.Interface.IServices;
using Application.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : Service, IOrderService
    {
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public Task CompletedOrder(int auctionId, string buyerId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateOrder(int auctionId)
        {
            var session = await _unitOfWork.AuctionSessions.GetSingle(e => e.Id == auctionId);
            var order = new Order
            {
                BuyerId = session!.WinnerId!,
                SellerId = session!.SellerId!,
                DateOrder = DateTime.Now,
                ProductId = (int)session.ProductId!,
                Total = session.CurrentPrice,
                OrderStatus = Domain.Enum.OrderStatus.Paid,
                AuctionSessionId = auctionId
            };
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<Order>> GetWinOrder(string userId)
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            orders = orders.Where(e => e.BuyerId == userId).ToList();
            return (List<Order>)orders;
        }
    }
}
