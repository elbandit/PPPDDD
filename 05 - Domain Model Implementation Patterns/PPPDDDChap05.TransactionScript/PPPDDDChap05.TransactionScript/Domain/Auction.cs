using System;

namespace PPPDDDChap05.TransactionScript.Domain
{
    public class Auction 
    {
        public Guid Id { get; set; }
        public Guid ListingId { get; set; }
        public DateTime EndsAt { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal WinningBid { get; set; }
        public decimal WinninBidderMaximumBid { get; set; }
        public Guid WinningBidder { get; set; }                    
    }
}
