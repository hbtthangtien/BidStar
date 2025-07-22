namespace Presentation.Models
{
    public class WinCardVM
    {
        public int OrderId { get; set; }
        public int AuctionId { get; set; }
        public string ProductName { get; set; } = "";
        public string ProductImage { get; set; } = "";
        public double FinalPrice { get; set; }
        public string SellerName { get; set; } = "";
        public bool Paid { get; set; }
    }
}
