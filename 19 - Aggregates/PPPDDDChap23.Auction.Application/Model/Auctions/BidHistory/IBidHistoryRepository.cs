using System;

namespace PPPDDDChap23.Auction.Application.Model.Auctions.BidHistory
{
    public interface IBidHistoryRepository
    {
        int NoOfBidsFor(Guid autionId);
        void Add(Bid bid);
    }
}
