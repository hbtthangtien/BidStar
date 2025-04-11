using Application.DTOs.AuctionSession;
using Application.Interface.IServices;
using Application.UnitOfWork;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class AuctionSessionService : Service, IAuctionSessionService
    {
        public AuctionSessionService(IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<AuctionSessionDTODetail> AuctionSessionDTODetail(int auctionId)
        {
            var data = await _unitOfWork.AuctionSessions.GetSingle(e => e.Id == auctionId);
            var dto = _mapper.Map<AuctionSessionDTODetail>(data);
            return dto;
        }

        public async Task CreateAuctionSessionBySeller(AuctionSessionDTO dto)
        {
            var data = _mapper.Map<AuctionSession>(dto);
            if (dto.StartTime >= DateTime.Now)
            {
                data.AuctionSatus = Domain.Enum.AuctionSatus.Scheduled;
            }
            else if( dto.StartTime <= DateTime.Now && dto.EndTime >= DateTime.Now )
            {
                data.AuctionSatus = Domain.Enum.AuctionSatus.Ongoing;
            }
            await _unitOfWork.AuctionSessions.AddAsync(data);
            var product = await _unitOfWork.Products.GetSingle(e => e.Id == dto.ProductId);
            product!.ProductStatus = Domain.Enum.ProductStatus.Approved;
            _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<AuctionSessionDTO>> GetAllAuction()
        {
            var data = await _unitOfWork.AuctionSessions.GetAllAsync();
            var auctions = _mapper.Map<List<AuctionSessionDTO>>(data);
            return auctions;
        }

        public async Task<List<AuctionSessionDTO>> GetAllAuctionBySellerId(string sellerId)
        {
            var data = await _unitOfWork.AuctionSessions.FindAsync(e => e.SellerId == sellerId);
            var auctions = _mapper.Map<List<AuctionSessionDTO>>(data);
            return auctions;
        }

        public async Task JoinAuctionSession(int auctionId, string connectionId)
        {
          
           
        }

        public async Task<string> GetSellerIdByAuctionId(int auctionId)
        {
            var data =  await _unitOfWork.AuctionSessions.GetSingle(e => e.Id == auctionId);
            return data.SellerId;
        }
    }
}
