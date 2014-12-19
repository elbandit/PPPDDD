using System.Collections.Generic;

namespace PPPDDDChap05.DomainModel.Model
{
    public class AutomaticBidder 
    {
        public IEnumerable<WinningBid> GenerateNextSequenceOfBidsAfter(Bid offer, WinningBid currentWinningBid)
        {
            var bids = new List<WinningBid>();

            if (currentWinningBid.MaximumBid.IsGreaterThanOrEqualTo(offer.MaximumBid))
            {
                var bidFromOffer = new WinningBid(offer.Bidder, offer.MaximumBid, offer.MaximumBid, offer.TimeOfOffer);
                bids.Add(bidFromOffer);

                bids.Add(CalculateNextBid(bidFromOffer, new Bid(currentWinningBid.Bidder, currentWinningBid.MaximumBid, currentWinningBid.TimeOfBid)));               
            }
            else
            {
                if (currentWinningBid.HasNotReachedMaximumBid())
                {
                    var currentBiddersLastBid = new WinningBid(currentWinningBid.Bidder, currentWinningBid.MaximumBid, currentWinningBid.MaximumBid, currentWinningBid.TimeOfBid);
                    bids.Add(currentBiddersLastBid);

                    bids.Add(CalculateNextBid(currentBiddersLastBid, offer));                   
                }
                else
                    bids.Add(new WinningBid(offer.Bidder, currentWinningBid.CurrentAuctionPrice.BidIncrement(), offer.MaximumBid, offer.TimeOfOffer));
            }

            return bids;
        }

        private WinningBid CalculateNextBid(WinningBid winningbid, Bid offer)
        {
            WinningBid bid;

            if (winningbid.CanBeExceededBy(offer.MaximumBid))
                bid = new WinningBid(offer.Bidder, offer.MaximumBid, winningbid.CurrentAuctionPrice.BidIncrement(), offer.TimeOfOffer);
            else
                bid = new WinningBid(offer.Bidder, offer.MaximumBid, offer.MaximumBid, offer.TimeOfOffer);

            return bid;
        }
    }
}
