using System;
using System.Collections.Generic;
using DDDPPP.Chap19.RavenDBExample.Application.Infrastructure;

namespace DDDPPP.Chap19.RavenDBExample.Application.Model.Auction
{
    public class WinningBid : ValueObject<WinningBid>
    {
        private WinningBid() { }

        public WinningBid(Guid bidder, Money maximumBid, Money bid, DateTime timeOfBid)
        {
            if (bidder == Guid.Empty)
                throw new ArgumentNullException("Bidder cannot be null");

            if (maximumBid == null)
                throw new ArgumentNullException("MaximumBid cannot be null");

            if (timeOfBid == DateTime.MinValue)
                throw new ArgumentNullException("TimeOfBid must have a value");

            Bidder = bidder;
            MaximumBid = maximumBid;
            TimeOfBid = timeOfBid;
            CurrentAuctionPrice = new Price(bid);
        }

        public Guid Bidder { get; private set; }
        public Money MaximumBid { get; private set; }
        public DateTime TimeOfBid { get; private set; }
        public Price CurrentAuctionPrice { get; private set; }

        public WinningBid RaiseMaximumBidTo(Money newAmount)
        {
            if (newAmount.IsGreaterThan(MaximumBid))
                return new WinningBid(Bidder, newAmount, CurrentAuctionPrice.Amount, DateTime.Now);
            else
                throw new ApplicationException("Maximum bid increase must be larger than current maximum bid.");
        }

        public bool WasMadeBy(Guid bidder)
        {
            return Bidder.Equals(bidder);
        }

        public bool CanBeExceededBy(Money offer)
        {
            return CurrentAuctionPrice.CanBeExceededBy(offer);
        }

        public bool HasNotReachedMaximumBid()
        {
            return MaximumBid.IsGreaterThan(CurrentAuctionPrice.Amount);
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object>() { Bidder, MaximumBid, TimeOfBid, CurrentAuctionPrice };
        }
    }
}
