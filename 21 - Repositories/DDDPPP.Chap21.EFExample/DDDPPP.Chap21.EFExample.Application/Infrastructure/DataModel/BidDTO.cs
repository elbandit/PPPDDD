using System;
using System.Collections.Generic;

namespace DDDPPP.Chap21.EFExample.Application.Infrastructure.DataModel
{
    public partial class BidDTO
    {
        public System.Guid Id { get; set; }
        public System.Guid AuctionId { get; set; }
        public System.Guid BidderId { get; set; }
        public decimal Bid { get; set; }
        public System.DateTime TimeOfBid { get; set; }
    }
}
