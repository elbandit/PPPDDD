using System;

namespace DDDPPP.Chap21.RavenDBExample.Application.Application.Queries
{
    public class AuctionStatus
    {
        public Guid Id { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime AuctionEnds { get; set; }
        public Guid WinningBidderId { get; set; }
        public int NumberOfBids { get; set; }
        public TimeSpan TimeRemaining { get; set; }
    }
}
