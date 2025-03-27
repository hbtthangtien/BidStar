using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bid
    {
        public int Id { get; set; }

        public int AuctionSessionId { get; set; }

        public required string BuyerId { get; set; }

        public double BidAmount { get; set; }

        public DateTime BidDate { get; set; } = DateTime.Now;

        public virtual AuctionSession? AuctionSession { get; set; }

        public virtual Buyer? Buyer { get; set; }
    }
}
