using System;

namespace PPPDDDChap19.eBidder.Store.Application.Model.Auctions.BidHistory
{
    public interface IBidHistoryRepository
    {
        int NoOfBidsFor(Guid autionId);
        void Add(Bid bid);
    }
}
