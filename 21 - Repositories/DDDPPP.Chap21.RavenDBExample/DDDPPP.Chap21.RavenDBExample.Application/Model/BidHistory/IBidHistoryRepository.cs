using System;

namespace DDDPPP.Chap21.RavenDBExample.Application.Model.BidHistory
{
    public interface IBidHistoryRepository
    {
        int NoOfBidsFor(Guid autionId);
        void Add(Bid bid);
        BidHistory FindBy(Guid auctionId);
    }
}
