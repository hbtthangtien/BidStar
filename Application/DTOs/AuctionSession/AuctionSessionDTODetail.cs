using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AuctionSession
{
    public class AuctionSessionDTODetail
    {
        public int Id { get; set; }

        public string SellerId { get; set; }
        public int? ProductId { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateOnly DateStart { get; set; }

        public DateOnly DateEnd { get; set; }
        public double CurrentPrice { get; set; }
        public string? WinnerId { get; set; }
        public double BaseBalance { get; set; }

        public double DisplayPrice => (BaseBalance != 0 && Product != null) ? CurrentPrice : Product!.StartPrice;
        public virtual AuctionSatus AuctionSatus { get; set; }
        public virtual Buyer? Winner { get; set; }
        public virtual Domain.Entities.Product? Product { get; set; }
        public virtual Seller? Seller { get; set; }

        public virtual ICollection<Joinning> Joinnings { get; set; } = new List<Joinning>()!;

        public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>()!;
    }
}
