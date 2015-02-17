using System;

namespace DDDPPP.Chap21.EFExample.Application.Model.Auction
{
    public class WinningBidSnapshot
    {
        public Guid BiddersId { get; set; }
        public DateTime TimeOfBid { get; set; }
        public decimal BiddersMaximumBid { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
