using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Buyer
    {
        public required string BuyerId;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>()!;

        public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>()!;

        public virtual ICollection<Joinning> Joinnings { get; set; } = new HashSet<Joinning>()!;



    }
}
