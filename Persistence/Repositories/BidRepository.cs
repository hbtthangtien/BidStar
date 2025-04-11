using Application.Interface.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseConfig;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BidRepository : Repository<Bid>, IBidRepository
    {
        public BidRepository(BidStarContext context) : base(context)
        {
        }

        public async Task<Bid> GetBidById(int id)
        {
            var data = await _context.Bids.Include(e => e.Buyer)
                .Include(e => e.AuctionSession)
                .SingleOrDefaultAsync(e => e.Id == id);
            return data;
        }
    }
}
