using System;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using DDDPPP.Chap19.NHibernateExample.Application.Model.BidHistory;
using DDDPPP.Chap19.NHibernateExample.Application.Infrastructure;

namespace DDDPPP.Chap19.NHibernateExample.Application.Application.Queries
{
    public class AuctionStatusQuery
    {
        private readonly ISession _session;
        private readonly IBidHistoryRepository _bidHistory;
        private readonly IClock _clock;

        public AuctionStatusQuery(ISession session, 
                                   IBidHistoryRepository bidHistory,
                                   IClock clock)
        {
            _session = session;
            _bidHistory = bidHistory;
            _clock = clock;
        }

        public AuctionStatus AuctionStatus(Guid auctionId)
        {
            var status = _session
                       .CreateSQLQuery(String.Format("select Id, CurrentPrice, BidderMemberId as WinningBidderId, AuctionEnds from Auctions Where Id = '{0}'", auctionId))
                       .SetResultTransformer(Transformers.AliasToBean<AuctionStatus>())
                       .UniqueResult<AuctionStatus>();

            status.TimeRemaining = TimeRemaining(status.AuctionEnds);
            status.NumberOfBids = _bidHistory.NoOfBidsFor(auctionId);

            return status;
        }

        public TimeSpan TimeRemaining(DateTime AuctionEnds)
        {
            if (_clock.Time() < AuctionEnds)
                return AuctionEnds.Subtract(_clock.Time());
            else
                return new TimeSpan();
        }
    }
}
