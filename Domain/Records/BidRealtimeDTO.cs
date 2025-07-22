using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Records
{
    public record BidRealtimeDTO
    (
        int AuctionSessionId,
        double BidAmount,
        int BidCount,
        AuctionSatus AuctionStatus
    );
}
