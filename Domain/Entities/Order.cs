using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public required string BuyerId { get; set; }

        public int AuctionSessionId { get; set; }

        public double Total {  get; set; }

        public DateTime DateOrder {  get; set; }

        public OrderStatus OrderStatus { get; set; }  
        
        public virtual Product? Product { get; set; }

        public virtual Buyer? Buyer { get; set; }

        public virtual AuctionSession? AuctionSession { get; set; }





    }
}
