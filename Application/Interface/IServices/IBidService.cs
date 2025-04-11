using Application.DTOs.Bids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IBidService
    {
        public Task<ResponseDTOBid> PlaceOrderBid(PlaceOrderBidDTO dto);
    }
}
