using Application.DTOs.Bids;
using Application.Interface.IServices;
using Application.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.ExceptionCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BidService : Service, IBidService
    {
        private readonly IAccountService _accountService;
        private readonly IAuctionSessionService _auctionSessionService;
        public BidService(IUnitOfWork unitOfWork, IMapper mapper,
            IAccountService accountService,
            IAuctionSessionService auctionSessionService) : base(unitOfWork, mapper)
        {
            _accountService = accountService;
            _auctionSessionService = auctionSessionService;
        }

        public async Task<ResponseDTOBid> PlaceOrderBid(PlaceOrderBidDTO dto)
        {
            await EnsureBalanceValid(dto.BidAmount,dto.BuyerId,dto.AuctionSessionId);
            var data = new Bid
            {
                BuyerId = dto.BuyerId,
                AuctionSessionId = dto.AuctionSessionId,
                BidAmount = dto.BidAmount,
                BidDate = DateTime.Now
            };
            await _unitOfWork.Bids.AddAsync(data);           
            await _accountService.UpdateBalance(dto.BuyerId,-dto.BidAmount);
            await _auctionSessionService.UpdateCurrentPrice(dto.AuctionSessionId, dto.BidAmount);
            await _auctionSessionService.UpdateWinner(dto.AuctionSessionId,dto.BuyerId);
            var bid = await _unitOfWork.Bids.GetBidById(data.Id);
            await _unitOfWork.CommitAsync();
            return new ResponseDTOBid
            {
                BuyerId = dto.BuyerId,
                BidAmount = bid.BidAmount,
                BidDate = bid.BidDate,
                BuyerName = bid.Buyer?.Account?.UserName!
            };
        }
        private async Task<bool> CheckValidBidAmount(double amount, int auctionId)
        {
            var check  = await _unitOfWork.Bids.GetSingle(e => e.BidAmount >= amount && e.Id == auctionId);
            return check != null;

        }
        private async Task EnsureBalanceValid(double amount, string userId,int auctionId)
        {
            if (await CheckValidBidAmount(amount,auctionId) == false)
            {
                throw new PlaceOrderBidException("The Bid Ammount order must be greater than current price in session now");
            }
            if (await _accountService.CheckValidBalance(userId, amount) == false)
            {
                throw new PlaceOrderBidException("Your balance is not enough to place, Place add more to order!!!");
            }
        }
        

    }
}
