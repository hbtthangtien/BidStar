using Application.DTOs.AuctionSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IServices
{
    public interface IAuctionSessionService
    {
        public Task<List<AuctionSessionDTO>> GetAllAuctionBySellerId(string sellerId);

        public Task CreateAuctionSessionBySeller(AuctionSessionDTO dto);

        public Task<AuctionSessionDTODetail> AuctionSessionDTODetail(int auctionId);

        public Task<List<AuctionSessionDTO>> GetAllAuction();

        public Task JoinAuctionSession(int auctionId, string connectionId);

        public Task<string> GetSellerIdByAuctionId(int auctionId);

        public Task UpdateCurrentPrice(int auctionId, double price);

        public Task UpdateWinner(int auctionId, string winnerId);
    }
}
