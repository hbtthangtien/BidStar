using Application.Interface.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IAccountRepository Accounts { get; }

        public IAuctionSessionRepository AuctionSessions { get; }

        public IBidRepository Bids { get; }

        public IBuyerRepository Buyers { get; }

        public ICategoryRepository Categories { get; }

        public IOrderRepository Orders { get; }

        public IProductRepository Products { get; }

        public ISellerRepository Sellers { get; }

        public IPaymentRepository Payments { get; }

        public IJoinningRepository Joinnings { get; }

        public Task<int> CommitAsync();
    }
}
