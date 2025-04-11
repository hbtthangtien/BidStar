using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IRepository
{
    public interface IBidRepository : IRepository<Bid>
    {
        public Task<Bid> GetBidById(int id);
    }
}
