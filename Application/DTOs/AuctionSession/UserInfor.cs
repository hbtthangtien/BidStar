using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AuctionSession
{
    public class UserInfor
    {
        public string ConnectionId { get; set; } = default!;
        public string UserName { get; set; } = default!;

        public string Fullname { get; set; } = default!;
        public bool IsHost { get; set; }
    }
}
