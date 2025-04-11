using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Bids
{
    public class PlaceOrderBidDTO
    {
        public int AuctionSessionId {  get; set; }

        public string BuyerId { get; set; }

        public double BidAmount { get; set; }

        public DateTime BidDate { get; set; }
    }
}
