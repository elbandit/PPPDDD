using System;

namespace PPPDDDChap19.eBidder.Listings.Model.Auctions.BidHistory
{
    public interface IBidHistoryRepository
    {
        int NoOfBidsFor(Guid autionId);
        void Add(Bid bid);
    }
}
