using System;
using System.Collections.Generic;
using System.Linq;
namespace PPPDDDChap05.DomainModel.Model
{
    public class WinningBid 
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
        
        public Guid AuctionId { get; private set; }
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

        public WinningBid DetermineWinningBidIncrement(Bid newbid)
        {
            if (this.CanMeetOrExceedBidIncrement(this.MaximumBid) && this.CanMeetOrExceedBidIncrement(newbid.MaximumBid))
            {
                return DetermineWinnerFromProxyBidding(this, newbid);
            }
            else if (this.CanMeetOrExceedBidIncrement(newbid.MaximumBid))
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

                if (nextIncrement.CanMeetOrExceedBidIncrement(newbid.MaximumBid))
                    return CreateNewBid(newbid.Bidder, nextIncrement.CurrentAuctionPrice.BidIncrement(), newbid.MaximumBid, newbid.TimeOfOffer);
                else
                    return CreateNewBid(newbid.Bidder, newbid.MaximumBid, newbid.MaximumBid, newbid.TimeOfOffer);
            }
            else
            {
                nextIncrement = CreateNewBid(newbid.Bidder, newbid.MaximumBid, newbid.MaximumBid, newbid.TimeOfOffer);

                if (nextIncrement.CanMeetOrExceedBidIncrement(winningBid.MaximumBid))
                    return CreateNewBid(winningBid.Bidder, nextIncrement.CurrentAuctionPrice.BidIncrement(), winningBid.MaximumBid, winningBid.TimeOfBid);
                else
                    return CreateNewBid(winningBid.Bidder, winningBid.MaximumBid, winningBid.MaximumBid, winningBid.TimeOfBid);
            }
        }

        private WinningBid CreateNewBid(Guid bidder, Money bid, Money maxBid, DateTime timeOfBid)
        {          
            return new WinningBid(bidder, bid, maxBid, timeOfBid);
        }

        private bool MaxBidCanBeExceededBy(Money bid)
        {
            return !this.MaximumBid.IsGreaterThanOrEqualTo(bid);
        }

        public bool CanMeetOrExceedBidIncrement(Money offer)
        {
            return CurrentAuctionPrice.CanBeExceededBy(offer);
        }

        public bool HasNotReachedMaximumBid()
        {
            return MaximumBid.IsGreaterThan(CurrentAuctionPrice.Amount);
        }
    }
}
