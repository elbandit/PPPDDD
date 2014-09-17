using System;
using System.Collections.Generic;
using System.Linq;
using DDDPPP.Chap19.RavenDBExample.Application.Model.BidHistory;
using Raven.Client;

namespace DDDPPP.Chap19.RavenDBExample.Application.Infrastructure
{
    public class BidHistoryRepository : IBidHistoryRepository
    {
        private readonly IDocumentSession _documentSession;

        public BidHistoryRepository(IDocumentSession documentSession)
        { 
            _documentSession = documentSession;
        }

        public int NoOfBidsFor(Guid autionId)
        {            
            
            var count = _documentSession.Query<BidHistory_NumberOfBids.ReduceResult, BidHistory_NumberOfBids>()
                            .Customize(x => x.WaitForNonStaleResultsAsOfNow())
                            .FirstOrDefault(x => x.AuctionId == autionId)
                                ?? new BidHistory_NumberOfBids.ReduceResult();

            return count.Count;                                           
        }

        public void Add(Bid bid)
        {
            _documentSession.Store(bid); 
        }

        public Model.BidHistory.BidHistory FindBy(Guid auctionId)
        {
            var bids = _documentSession.Query<Bid>()
                                .Customize(x => x.WaitForNonStaleResultsAsOfNow())
                                .Where(x => x.AuctionId == auctionId)                                
                                .ToList();

            return new Model.BidHistory.BidHistory(bids);
        }
    }
}
