using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace DDDPPP.Chap19.NHibernateExample.Application.Application.Queries
{
    public class BidHistoryQuery
    {
        private readonly ISession _session;

        public BidHistoryQuery(ISession session)
        {
            _session = session;         
        }

        public IEnumerable<BidInformation> BidHistoryFor(Guid auctionId)
        {
            var status = _session
                       .CreateSQLQuery(String.Format("SELECT [BidderId] as Bidder,[Bid] as AmountBid ,TimeOfBid FROM [BidHistory] Where AuctionId = '{0}' Order By Bid Desc, TimeOfBid Asc", auctionId))
                       .SetResultTransformer(Transformers.AliasToBean<BidInformation>());

            return status.List<BidInformation>(); 
        }
    }
}
