using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client.Indexes;
using Raven.Client.Document;
using Raven.Abstractions.Indexing;
using DDDPPP.Chap19.RavenDBExample.Application.Model.BidHistory;
using DDDPPP.Chap19.RavenDBExample.Application.Application;

namespace DDDPPP.Chap19.RavenDBExample.Application.Infrastructure
{
    public class BidHistory_NumberOfBids : AbstractIndexCreationTask<Bid, BidHistory_NumberOfBids.ReduceResult>
    {
        public class ReduceResult
        {
            public Guid AuctionId { get; set; }
            public int Count { get; set; }
        }

        public BidHistory_NumberOfBids()
        {
            Map = bids => from bid in bids
                  select new {bid.AuctionId, Count = 1};

            Reduce = results => from result in results
                                group result by result.AuctionId into g
                                select new { AuctionId = g.Key, Count = g.Sum(x => x.Count) };
        }
    }
}
