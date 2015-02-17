using System;

namespace DDDPPP.Chap21.NHibernateExample.Application.Model.BidHistory
{
    public interface IBidHistoryRepository
    {
        int NoOfBidsFor(Guid autionId);
        void Add(Bid bid);
    }
}
