using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Bids
{
    public class ResponseDTOBid
    {
        public required string BuyerId { get; set; }

        public double BidAmount { get; set; }

        public DateTime BidDate { get; set; }

        public string BuyerName { get; set; } = default!;
    }
}
