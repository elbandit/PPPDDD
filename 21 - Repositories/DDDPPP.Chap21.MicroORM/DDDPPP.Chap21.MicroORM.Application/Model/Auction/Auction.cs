using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDPPP.Chap21.MicroORM.Application.Infrastructure;

namespace DDDPPP.Chap21.MicroORM.Application.Model.Auction
{
    public class Auction : Entity<Guid>
    {        
        public Auction(Guid id, Money startingPrice, DateTime endsAt)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Auction Id cannot be null");

            if (startingPrice == null)
                throw new ArgumentNullException("Starting Price cannot be null");

            if (endsAt == DateTime.MinValue)
                throw new ArgumentNullException("EndsAt must have a value");

            Id = id;
            StartingPrice = startingPrice;
            EndsAt = endsAt;
        }

        private Auction(AuctionSnapshot snapshot)
        {
            this.Id = snapshot.Id;
            this.StartingPrice = new Money(snapshot.StartingPrice);
            this.EndsAt = snapshot.EndsAt;
            this.Version = snapshot.Version;

            if (snapshot.WinningBid != null)                          
                CurrentWinningBid = WinningBid.CreateFrom(snapshot.WinningBid);            
        }

        public static Auction CreateFrom(AuctionSnapshot snapshot)
        {
            return new Auction(snapshot);
        }

        private Money StartingPrice { get; set; }
        private WinningBid CurrentWinningBid { get; set; }
        private DateTime EndsAt { get; set; }

        public AuctionSnapshot GetSnapshot()
        {
            var snapshot = new AuctionSnapshot();
            snapshot.Id = this.Id;
            snapshot.StartingPrice = this.StartingPrice.GetSnapshot().Value;
            snapshot.EndsAt = this.EndsAt;
            snapshot.Version = this.Version;

            if (HasACurrentBid())
                snapshot.WinningBid = CurrentWinningBid.GetSnapshot();            

            return snapshot;
        }

        private bool HasACurrentBid()
        {
            return CurrentWinningBid != null;
        }

        private bool StillInProgress(DateTime currentTime)
        {
            return (EndsAt > currentTime);
        }

        public void PlaceBidFor(Bid bid, DateTime currentTime)
        {          
            if (StillInProgress(currentTime))
            {
                if (FirstOffer())
                    PlaceABidForTheFirst(bid);
                else if (BidderIsIncreasingMaximumBid(bid))
                    CurrentWinningBid = CurrentWinningBid.RaiseMaximumBidTo(bid.MaximumBid);
                else if (CurrentWinningBid.CanBeExceededBy(bid.MaximumBid))
                    Set(CurrentWinningBid.DetermineWinningBidIncrement(bid));                
            }
        }

        private bool BidderIsIncreasingMaximumBid(Bid bid)
        {
            return CurrentWinningBid.WasMadeBy(bid.Bidder) && bid.MaximumBid.IsGreaterThan(CurrentWinningBid.MaximumBid);
        }

        private bool FirstOffer()
        {
            return CurrentWinningBid == null;
        }

        private void PlaceABidForTheFirst(Bid offer)
        {
            if (offer.MaximumBid.IsGreaterThanOrEqualTo(StartingPrice))
            {
                DomainEvents.Raise(new BidPlaced(this.Id, offer.Bidder, StartingPrice, offer.TimeOfOffer));

                Set(new WinningBid(Id, offer.Bidder, offer.MaximumBid, StartingPrice, offer.TimeOfOffer));
            }
        }

        private void Set(WinningBid newBid)
        {
            if (!FirstOffer() && CurrentWinningBid.WasMadeBy(newBid.Bidder))
                DomainEvents.Raise(new OutBid(Id, CurrentWinningBid.Bidder));

            CurrentWinningBid = newBid;            
        }
    }


}
