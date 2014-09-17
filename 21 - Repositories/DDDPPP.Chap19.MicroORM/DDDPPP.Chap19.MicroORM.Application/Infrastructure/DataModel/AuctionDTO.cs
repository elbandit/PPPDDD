using System;
using System.Collections.Generic;

namespace DDDPPP.Chap19.MicroORM.Application.Infrastructure.DataModel
{
    public partial class AuctionDTO : IAggregateDataModel 
    {
        public System.Guid Id { get; set; }
        public decimal StartingPrice { get; set; }
        public DateTime AuctionEnds { get; set; }

        public Nullable<System.Guid> BidderMemberId { get; set; }
        public System.DateTime? TimeOfBid { get; set; }
        public Nullable<decimal> MaximumBid { get; set; }
        public Nullable<decimal> CurrentPrice { get; set; }

        public int Version { get; set; }
    }
}
