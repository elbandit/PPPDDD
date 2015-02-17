using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDPPP.Chap21.EFExample.Application.Model.Auction
{
    public class BidPlaced
    {
        public BidPlaced(Guid auctionId, Guid bidderId, Money amountBid, DateTime timeOfBid)
        {
            if (auctionId == Guid.Empty)
                throw new ArgumentNullException("Auction Id cannot be null");

            if (bidderId == Guid.Empty)
                throw new ArgumentNullException("Bidder Id cannot be null");

            if (amountBid == null)
                throw new ArgumentNullException("AmountBid cannot be null");

            if (timeOfBid == DateTime.MinValue)
                throw new ArgumentNullException("TimeOfBid must have a value");

            AuctionId = auctionId;
            Bidder = bidderId;
            AmountBid = amountBid;
            TimeOfBid = timeOfBid;
        }

        public Guid AuctionId { get; private set; }
        public Guid Bidder { get; private set; }
        public Money AmountBid { get; private set; }
        public DateTime TimeOfBid { get; private set; }
        
    }
}
