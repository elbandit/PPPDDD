using System;
using PPPDDDChap19.eBidder.Listings.Application.Infrastructure;

namespace PPPDDDChap19.eBidder.Listings.Model.Auctions
{
    public class Auction : Entity<Guid>
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
            
            Id = id;
            ListingId = listingId;
            StartingPrice = startingPrice;
            EndsAt = endsAt;            
        }

        public void ReduceTheStartingPrice()
        { 
            // Only if no bids and more than 12 hours left
        }

        private Guid ListingId { get; set; }
        private DateTime EndsAt { get; set; }       
        private Money StartingPrice { get; set; }
        private WinningBid WinningBid { get; set; }
        private bool HasEnded { get; set; }
        
        // Fixed Price or 
        // Auction
        // Once someone bids, the Buy it now option disappears. 
        // The listing then proceeds as a regular auction-style listing, with the item going to the highest bidder. 
        // (If the auction has a reserve price, the Buy it now option will be available until the reserve price is met.)
        // http://sellercentre.ebay.co.uk/add-buy-it-now-price-auction
       
        private bool StillInProgress(DateTime currentTime)
        {
            return (EndsAt > currentTime);             
        }

        public bool CanPlaceBid()
        { 
            return HasEnded == false;        
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
