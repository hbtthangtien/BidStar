using Application.Interface.IRepository;
using Application.UnitOfWork;
using Persistence.DatabaseConfig;

namespace Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BidStarContext _context;
        public IAccountRepository Accounts { get; private set; }

        public IAuctionSessionRepository AuctionSessions { get; private set; }

        public IBidRepository Bids { get; private set; }

        public IBuyerRepository Buyers { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public IProductRepository Products { get; private set; }

        public ISellerRepository Sellers { get; private set; }

        public IPaymentRepository Payments { get; private set; }

        public IJoinningRepository Joinnings { get; private set; }

        public UnitOfWork(IAccountRepository accounts,
            IAuctionSessionRepository auctionSessions,
            IBidRepository bids, IBuyerRepository buyers,
            ICategoryRepository categories,
            IOrderRepository orders,
            IProductRepository products,
            ISellerRepository sellers,
            IPaymentRepository payments,
            IJoinningRepository joinnings,
            BidStarContext context)
        {
            Accounts = accounts;
            AuctionSessions = auctionSessions;
            Bids = bids;
            Buyers = buyers;
            Categories = categories;
            Orders = orders;
            Products = products;
            Sellers = sellers;
            Payments = payments;
            Joinnings = joinnings;
            _context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
