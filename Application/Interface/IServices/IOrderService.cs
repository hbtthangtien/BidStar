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

        public Task CreateOrder(int auctionId);
    }
}
