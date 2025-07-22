using Application.DTOs.AuctionSession;
using Application.Interface.IServices;
using Application.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Paginated;
using Presentation.BackgroundServices;
using System.Linq.Expressions;

namespace Application.Services
{
    public class AuctionSessionService : Service, IAuctionSessionService
    {
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;
        private readonly AuctionSessionScheduler _auctionScheduler;
        public AuctionSessionService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IOrderService orderService,
            IAccountService accountService,
            AuctionSessionScheduler auctionSessionScheduler) : base(unitOfWork, mapper)
        {
            _orderService = orderService;
            _accountService = accountService;
            _auctionScheduler = auctionSessionScheduler;
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
            _auctionScheduler.ScheduleAuctionSessionEndTime(data.Id, data.EndTime);
        }

        public async Task<List<AuctionSessionDTO>> GetAllAuction()
        {
            var data = await _unitOfWork.AuctionSessions.GetAllAsync();
            data = data.Where(e => e.AuctionSatus == Domain.Enum.AuctionSatus.Ongoing);
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

        public async Task UpdateCurrentPrice(int auctionId, double price)
        {
            var auction = await _unitOfWork.AuctionSessions.GetSingle(e => e.Id == auctionId);
            var currentPrice = auction!.CurrentPrice;
            auction.CurrentPrice = price;
            _unitOfWork.AuctionSessions.UpdateAsync(auction);
            // refund money for before winner
            if(auction.WinnerId != null)
            {
                await _accountService.UpdateBalance(auction.WinnerId!, currentPrice);
            }           
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateWinner(int auctionId, string winnerId)
        {
            var auction = await _unitOfWork.AuctionSessions.GetSingle(e => e.Id == auctionId);
            auction.WinnerId = winnerId;
            _unitOfWork.AuctionSessions.UpdateAsync(auction);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> SolveAuctionCompleted(int sessionId)
        {
            var session = await _unitOfWork.AuctionSessions.GetSingle(e => e.Id == sessionId);
            if(session == null || session.AuctionSatus != Domain.Enum.AuctionSatus.Ongoing)
            {
                return false;
            }
            session.AuctionSatus = Domain.Enum.AuctionSatus.Completed;
            if(session.WinnerId != null)
            {
                await _orderService.CreateOrder(sessionId);
                // update balance for seller
                await _accountService.UpdateBalance(session.SellerId, session.CurrentPrice*95/100);
                
            }
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<PaginatedList<AuctionSession>> GetAllAuction(string searchString="", string sortOrder = "", int pageNumber = 1, int pageSize = 5)
        {
            Expression<Func<AuctionSession, bool>> filter =( e => (e.Product!.Name!.Contains(searchString)));
            Expression<Func<AuctionSession, object>> sortBy = e => e.BaseBalance;
            var query = _unitOfWork.AuctionSessions.GetPaginatedList(filter, sortBy, true);
            return await PaginatedList<AuctionSession>.CreateAsync(query,pageNumber,pageSize);
        }

        public async Task<IEnumerable<AuctionSession>> GetActiveAuctionsAsync()
        {
            return await _unitOfWork.AuctionSessions.GetAllAsync();
        }

        public async Task<AuctionSession> GetAuctionById(int auctionId)
        {
            return await _unitOfWork.AuctionSessions.GetSingle(e => e.Id == auctionId) ??
                throw new Exception("Not found");
        }
    }
}
