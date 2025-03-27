using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Joinning
    {
        public int Id { get; set; }

        public required string BuyerId { get; set; }

        public int AuctionSessionId { get; set; }

        public DateTime TimeJoin { get; set; } = DateTime.Now;

        public DateTime TimeOut { get; set; } = DateTime.Now;

        public virtual Buyer? Buyer { get; set; }   

        public virtual AuctionSession? AuctionSession { get; set; }
    }
}
