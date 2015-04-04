using System;
using System.Collections.Generic;
using DDDPPP.Chap21.NHibernateExample.Application.Model.BidHistory;
using NHibernate;

namespace DDDPPP.Chap21.NHibernateExample.Application.Infrastructure
{
    public class BidHistoryRepository : IBidHistoryRepository
    {
        private readonly ISession _session;

        public BidHistoryRepository(ISession session)
        {
            _session = session;
        }

        public int NoOfBidsFor(Guid autionId)
        {
            var sql = String.Format("SELECT Count(*) FROM BidHistory Where AuctionId = '{0}'", autionId);
            var query = _session.CreateSQLQuery(sql);
            var result = query.UniqueResult();
           
            return Convert.ToInt32(result);                                                
        }

        public void Add(Bid bid)
        {                       
            _session.Save(bid);
        }
    }
}
