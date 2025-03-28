using Application.Interface.IRepository;
using Domain.Entities;
using Persistence.DatabaseConfig;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class JoinningRepository : Repository<Joinning>, IJoinningRepository
    {
        public JoinningRepository(BidStarContext context) : base(context)
        {
        }
    }
}
