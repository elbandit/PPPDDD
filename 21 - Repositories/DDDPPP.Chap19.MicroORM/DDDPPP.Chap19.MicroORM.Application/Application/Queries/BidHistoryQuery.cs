using System;
using System.Collections.Generic;
using DDDPPP.Chap19.MicroORM.Application.Infrastructure;
using DDDPPP.Chap19.MicroORM.Application.Model.BidHistory;

namespace DDDPPP.Chap19.MicroORM.Application.Application.Queries
{
    public class BidHistoryQuery
    {
        private readonly IBidHistoryRepository _bidHistory;

        public BidHistoryQuery(IBidHistoryRepository bidHistory)
        {
            _bidHistory = bidHistory;         
        }

        public IEnumerable<BidInformation> BidHistoryFor(Guid auctionId)
        {
            var bidHistory = _bidHistory.FindBy(auctionId);

            return Convert(bidHistory.ShowAllBids());
        }

        public IEnumerable<BidInformation> Convert(IEnumerable<Bid> bids)
        {
            var bidInfo = new List<BidInformation>();

            foreach (var bid in bids)
            {
                bidInfo.Add(new BidInformation() { Bidder = bid.Bidder, AmountBid = bid.AmountBid.GetSnapshot().Value, TimeOfBid = bid.TimeOfBid });
            }

            return bidInfo;
        }
    }
}
