using System;
using DDDPPP.Chap21.RavenDBExample.Application.Infrastructure;

namespace DDDPPP.Chap21.RavenDBExample.Application.Model.Auction
{
    public class Auction : Entity<Guid>
    {
        private Auction() { }

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

        public Money StartingPrice { get; private set; }
        public WinningBid WinningBid { get; private set; }
        public DateTime EndsAt { get; private set; }

        public bool StillInProgress(DateTime currentTime)
        {
            return (EndsAt > currentTime);
        }

        public void PlaceBidFor(Offer offer, DateTime currentTime)
        {
            if (StillInProgress(currentTime))
            {
                if (FirstOffer())
                    PlaceABidForTheFirst(offer);
                else if (BidderIsIncreasingMaximumBidToNew(offer))
                    WinningBid = WinningBid.RaiseMaximumBidTo(offer.MaximumBid);
                else if (WinningBid.CanBeExceededBy(offer.MaximumBid))
                {
                    var newBids = new AutomaticBidder().GenerateNextSequenceOfBidsAfter(offer, WinningBid);

                    foreach (var bid in newBids)
                        Place(bid);
                }
            }
        }

        private bool BidderIsIncreasingMaximumBidToNew(Offer offer)
        {
            return WinningBid.WasMadeBy(offer.Bidder) && offer.MaximumBid.IsGreaterThan(WinningBid.MaximumBid);
        }

        public bool HasBeenBidOn()
        {
            return WinningBid == null;
        }   

        private bool FirstOffer()
        {
            return WinningBid == null;
        }

        private void PlaceABidForTheFirst(Offer offer)
        {
            if (offer.MaximumBid.IsGreaterThanOrEqualTo(StartingPrice))
                Place(new WinningBid(offer.Bidder, offer.MaximumBid, StartingPrice, offer.TimeOfOffer));
        }

        private void Place(WinningBid newBid)
        {
            if (!FirstOffer() && WinningBid.WasMadeBy(newBid.Bidder))
                DomainEvents.Raise(new OutBid(Id, WinningBid.Bidder));

            WinningBid = newBid;
            DomainEvents.Raise(new BidPlaced(Id, newBid.Bidder, newBid.CurrentAuctionPrice.Amount, newBid.TimeOfBid));
        }
    }
}
