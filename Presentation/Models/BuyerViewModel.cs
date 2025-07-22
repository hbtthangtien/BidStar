using Domain.Enum;

public class BuyerViewModel
{
    public List<AuctionProductViewModel> Auctions { get; set; }
    // Có thể thêm Orders, Notifications,...
}
public class AuctionProductViewModel
{
    public int AuctionId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
    public double CurrentPrice { get; set; }
    public int BidCount { get; set; }
    public DateTime StartTime { get; set; }     // Thêm
    public DateTime EndTime { get; set; }       // Thêm
    public AuctionSatus AuctionStatus { get; set; }   // Scheduled, Ongoing, Completed, Paid
    public bool IsOngoing => DateTime.Now >= StartTime && DateTime.Now < EndTime;
}
