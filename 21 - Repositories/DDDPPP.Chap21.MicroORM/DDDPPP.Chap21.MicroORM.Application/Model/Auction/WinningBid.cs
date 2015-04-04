using System;
using System.Collections.Generic;
using System.Linq;
using DDDPPP.Chap21.MicroORM.Application.Infrastructure;

namespace DDDPPP.Chap21.MicroORM.Application.Model.Auction
{
    public class WinningBid : ValueObject<WinningBid>
    {
        private WinningBid() { }

        public WinningBid(Guid auctionId, Guid bidder, Money maximumBid, Money bid, DateTime timeOfBid)
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
            AuctionId = auctionId;           
        }

        public Guid AuctionId { get; private set; }
        public Guid Bidder { get; private set; }
        public Money MaximumBid { get; private set; }
        public DateTime TimeOfBid { get; private set; }
        public Price CurrentAuctionPrice { get; private set; }

        public WinningBid RaiseMaximumBidTo(Money newAmount)
        {
            if (newAmount.IsGreaterThan(MaximumBid))
                return new WinningBid(AuctionId, Bidder, newAmount, CurrentAuctionPrice.Amount, DateTime.Now);
            else
                throw new ApplicationException("Maximum bid increase must be larger than current maximum bid.");
        }

        public bool WasMadeBy(Guid bidder)
        {
            return Bidder.Equals(bidder);
        }

        public WinningBid DetermineWinningBidIncrement(Bid newbid)
        {            
            if (this.CanBeExceededBy(this.MaximumBid) && this.CanBeExceededBy(newbid.MaximumBid))
            {
                return DetermineWinnerFromProxyBidding(this, newbid);
            }
            else if (this.CanBeExceededBy(newbid.MaximumBid))
            {
                return CreateNewBid(newbid.Bidder, CurrentAuctionPrice.BidIncrement(), newbid.MaximumBid, newbid.TimeOfOffer);
            }
            else            
                return this;               
        }

        private WinningBid DetermineWinnerFromProxyBidding(WinningBid winningBid, Bid newbid)
        {
            WinningBid nextIncrement;

            if (winningBid.MaxBidCanBeExceededBy(newbid.MaximumBid))
            {
                nextIncrement = CreateNewBid(this.Bidder, this.MaximumBid, this.MaximumBid, this.TimeOfBid);
                
                if (nextIncrement.CanBeExceededBy(newbid.MaximumBid))                
                    return CreateNewBid(newbid.Bidder, nextIncrement.CurrentAuctionPrice.BidIncrement(), newbid.MaximumBid, newbid.TimeOfOffer);                
                else                
                    return CreateNewBid(newbid.Bidder, newbid.MaximumBid, newbid.MaximumBid, newbid.TimeOfOffer);                
            }
            else
            {
                nextIncrement = CreateNewBid(newbid.Bidder, newbid.MaximumBid, newbid.MaximumBid, newbid.TimeOfOffer);

                if (nextIncrement.CanBeExceededBy(winningBid.MaximumBid))
                    return CreateNewBid(winningBid.Bidder, nextIncrement.CurrentAuctionPrice.BidIncrement(), winningBid.MaximumBid, winningBid.TimeOfBid);
                else
                    return CreateNewBid(winningBid.Bidder, winningBid.MaximumBid, winningBid.MaximumBid, winningBid.TimeOfBid); 
            }                       
        }

        private WinningBid CreateNewBid(Guid bidder, Money bid, Money maxBid, DateTime timeOfBid)
        {
            DomainEvents.Raise(new BidPlaced(AuctionId, bidder, bid, timeOfBid));

            return new WinningBid(AuctionId, bidder, bid, maxBid, timeOfBid);
        }

        private bool MaxBidCanBeExceededBy(Money bid)
        {
            return !this.MaximumBid.IsGreaterThanOrEqualTo(bid);
        }

        public bool CanBeExceededBy(Money offer)
        {
            return CurrentAuctionPrice.CanBeExceededBy(offer);
        }

        public bool HasNotReachedMaximumBid()
        {
            return MaximumBid.IsGreaterThan(CurrentAuctionPrice.Amount);
        }

        public WinningBidSnapshot GetSnapshot()
        {
            var snapshot = new WinningBidSnapshot();

            snapshot.AuctionId = AuctionId;
            snapshot.BiddersId = this.Bidder;
            snapshot.BiddersMaximumBid = this.MaximumBid.GetSnapshot().Value;
            snapshot.CurrentPrice = this.CurrentAuctionPrice.Amount.GetSnapshot().Value;
            snapshot.TimeOfBid = this.TimeOfBid;

            return snapshot;
        }

        public static WinningBid CreateFrom(WinningBidSnapshot bidSnapShot)
        {
            return new WinningBid(bidSnapShot.AuctionId, bidSnapShot.BiddersId, new Money(bidSnapShot.BiddersMaximumBid), new Money(bidSnapShot.CurrentPrice), bidSnapShot.TimeOfBid);
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object>() { Bidder, MaximumBid, TimeOfBid, CurrentAuctionPrice };
        }
    }
}
