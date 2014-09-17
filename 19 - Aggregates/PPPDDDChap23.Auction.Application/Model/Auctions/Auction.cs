using System;
using PPPDDDChap23.Auction.Application.Infrastructure;
using PPPDDDChap23.Auction.Application.Model.Auctions.QandA;
using PPPDDDChap23.Auction.Application.Model.Members.Watching;
using PPPDDDChap23.Auction.Application.Model.Items;

namespace PPPDDDChap23.Auction.Application.Model.Auctions
{
    public class Auction : Entity<Guid>
    {
        private Auction() { }

        public Auction(Guid id, Guid sellerId, Money startingPrice, DateTime endsAt, Item item)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Auction Id cannot be null");

            if (sellerId == Guid.Empty)
                throw new ArgumentNullException("Seller Id cannot be null");

            if (startingPrice == null)
                throw new ArgumentNullException("Starting Price cannot be null");

            if (endsAt == DateTime.MinValue)
                throw new ArgumentNullException("EndsAt must have a value");

            Id = id;
            StartingPrice = startingPrice;
            EndsAt = endsAt;              
        }

        //public Email ContactSeller(string question)
        //{ 
            // You'll receive the message in your Messages inbox and your personal email account. 
            // When you respond, you can choose to post the question and answer to your listing 
            // so all buyers can see it. Once you post the answer, you can't change or remove it.
        //}

        public WatchedItem Watch(Guid MemberId)
        {
            return new WatchedItem(this.Id, MemberId);
        }

       // public PaymentMethod AddPaymentMethod()
       // { }

       // public PostLocation AddPostLocation()
       // { }

        public Question AskQuestion(Guid MemberId)
        {   
            return new Question(Guid.NewGuid(), this.Id, MemberId);
        }

        public void ReduceTheStartingPrice()
        { 
            // Only if no bids and more than 12 hours left
        }

        private Guid sellerId { get; set; }

        private string Title { get; set; }

        private string Description { get; set; }
        private DateTime EndsAt { get; set; }



        public void AmendDescription(string description)
        {
            // http://pages.ebay.co.uk/help/sell/revising_restrictions.html
            // if (currentTime.
            // Throw New ItemRevisionEvent(Description), DateTime currentTime

            Description = description;
        }

        private Money StartingPrice { get; set; }
        private WinningBid WinningBid { get; set; }
        

        // Fixed Price or 
        // Auction
        // Once someone bids, the Buy it now option disappears. 
        // The listing then proceeds as a regular auction-style listing, with the item going to the highest bidder. 
        // (If the auction has a reserve price, the Buy it now option will be available until the reserve price is met.)
        // http://sellercentre.ebay.co.uk/add-buy-it-now-price-auction

        public void BestOffer()
        {
            // http://ocsnext.ebay.co.uk/ocs/sc
            // for Buy It Now listing only
        }
       
        private bool StillInProgress(DateTime currentTime)
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
