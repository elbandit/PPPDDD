using System;
using System.Collections.Generic;

namespace PPPDDDChap05.DomainModel.Model
{
    public class Auction 
    {
        private Auction() { }

        public Auction(Guid id, Guid listingId, Money startingPrice, DateTime endsAt)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Auction Id cannot be null");

            if (startingPrice == null)
                throw new ArgumentNullException("Starting Price cannot be null");

            if (endsAt == DateTime.MinValue)
                throw new ArgumentNullException("EndsAt must have a value");

            if (listingId == Guid.Empty)
                throw new ArgumentNullException("Lisitng Id cannot be null");
            
            Id = id;
            ListingId = listingId;
            StartingPrice = startingPrice;
            EndsAt = endsAt;            
        }

        private Guid Id { get; set; }
        private IList<HistoricalBid> Bids { get; set; }         
        private Guid ListingId { get; set; }
        private DateTime EndsAt { get; set; }       
        private Money StartingPrice { get; set; }
        private WinningBid WinningBid { get; set; }
        private bool HasEnded { get; set; }
       
        private bool StillInProgress(DateTime currentTime)
        {
            return (EndsAt > currentTime);             
        }

        public bool CanPlaceBid()
        { 
            return HasEnded == false;        
        }

        public void PlaceBidFor(Bid offer, DateTime currentTime)
        {
            if (StillInProgress(currentTime))
            {
                if (FirstOffer())
                    PlaceABidForTheFirst(offer);
                else if (BidderIsIncreasingMaximumBid(offer))
                    WinningBid = WinningBid.RaiseMaximumBidTo(offer.MaximumBid);
                else if (WinningBid.CanBeExceededBy(offer.MaximumBid))
                {
                    Place(WinningBid.DetermineWinningBidIncrement(offer));                        
                }                                                   
            }     
        }

        private bool BidderIsIncreasingMaximumBid(Bid offer)
        {
            return WinningBid.WasMadeBy(offer.Bidder) && offer.MaximumBid.IsGreaterThan(WinningBid.MaximumBid);
        }

        private bool FirstOffer()
        {
            return WinningBid == null;
        }

        private void PlaceABidForTheFirst(Bid offer)
        {
            if (offer.MaximumBid.IsGreaterThanOrEqualTo(StartingPrice))
                Place(new WinningBid(offer.Bidder, offer.MaximumBid, StartingPrice, offer.TimeOfOffer));            
        }

        private void Place(WinningBid newBid)
        {
            Bids.Add(new HistoricalBid(newBid.Bidder, newBid.MaximumBid, newBid.TimeOfBid));
            WinningBid = newBid;            
        }
    }
}
