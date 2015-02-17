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

    public void PlaceBidFor(Bid bid, DateTime currentTime)
    {
        if (StillInProgress(currentTime))
        {
            if (IsFirstBid())
                RegisterFirst(bid);
            else if (BidderIsIncreasingMaximumBid(bid))
                WinningBid = WinningBid.RaiseMaximumBidTo(bid.MaximumBid);
            else if (WinningBid.CanMeetOrExceedBidIncrement(bid.MaximumBid))
            {
                Place(WinningBid.DetermineWinningBidIncrement(bid));
            }                                                   
        }     
    }

    private bool BidderIsIncreasingMaximumBid(Bid bid)
    {
        return WinningBid.WasMadeBy(bid.Bidder) && bid.MaximumBid.IsGreaterThan(WinningBid.MaximumBid);
    }

    private bool IsFirstBid()
    {
        return WinningBid == null;
    }

    private void RegisterFirst(Bid bid)
    {
        if (IsFirstBid() && bid.MaximumBid.IsGreaterThanOrEqualTo(StartingPrice)) 
            Place(new WinningBid(bid.Bidder, bid.MaximumBid, StartingPrice, bid.TimeOfOffer));            
    }

    private void Place(WinningBid newBid)
    {
        Bids.Add(new HistoricalBid(newBid.Bidder, newBid.MaximumBid, newBid.TimeOfBid));
        WinningBid = newBid;            
    }
}
}
