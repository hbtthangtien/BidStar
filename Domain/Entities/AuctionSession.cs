using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuctionSession
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateOnly DateStart { get; set; }

        public DateOnly DateEnd { get; set; }
        public double CurrentPrice { get; set; }
        public string? WinnerId { get; set; }
        public double BaseBalance { get; set; }
        public virtual AuctionSatus AuctionSatus { get; set; }
        public virtual Buyer? Winner { get; set; }
        public virtual Product? Product { get; set; }
        public virtual ICollection<Joinning> Joinnings { get; set; } = new List<Joinning>()!;

        public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>()!;

    }
}
