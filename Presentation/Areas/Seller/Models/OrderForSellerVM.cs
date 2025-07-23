using Domain.Enum;

namespace Presentation.Areas.Seller.Models
{
    public class OrderForSellerVM
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; } = "";
        public string ProductImage { get; set; } = "";
        public string BuyerName { get; set; } = "";
        public double Total { get; set; }
        public DateTime DateOrder { get; set; }
        public OrderStatus Status { get; set; }
        public string AuctionSessionLink { get; set; } = "";
    }

}
