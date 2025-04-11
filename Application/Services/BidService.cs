using Application.DTOs.Bids;
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
    public class BidService : Service, IBidService
    {
        public BidService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<ResponseDTOBid> PlaceOrderBid(PlaceOrderBidDTO dto)
        {
            if (await CheckValidBidAmount(dto.BidAmount) == false)
            {
                throw new Exception("The Bid Ammount order must be greater than current price in session now");
            }
            var data = new Bid
            {
                BuyerId = dto.BuyerId,
                AuctionSessionId = dto.AuctionSessionId,
                BidAmount = dto.BidAmount,
                BidDate = DateTime.Now
            };
            await _unitOfWork.Bids.AddAsync(data);
            await _unitOfWork.CommitAsync();
            var bid = await _unitOfWork.Bids.GetBidById(data.Id);
            return new ResponseDTOBid
            {
                BuyerId = dto.BuyerId,
                BidAmount = bid.BidAmount,
                BidDate = bid.BidDate,
                BuyerName = bid.Buyer?.Account?.UserName!
            };
        }
        private async Task<bool> CheckValidBidAmount(double amount)
        {
            var check  = await _unitOfWork.Bids.GetSingle(e => e.BidAmount >= amount);
            return check == null;

        }

        
    }
}
