using System;

namespace DDDPPP.Chap19.RavenDBExample.Application.Model.BidHistory
{
    public interface IBidHistoryRepository
    {
        int NoOfBidsFor(Guid autionId);
        void Add(Bid bid);
        BidHistory FindBy(Guid auctionId);
    }
}
