using System;
using System.Collections.Generic;
using DDDPPP.Chap19.MicroORM.Application.Model.Auction;
using DDDPPP.Chap19.MicroORM.Application.Model.BidHistory;
using DDDPPP.Chap19.MicroORM.Application.Infrastructure;

namespace DDDPPP.Chap19.MicroORM.Application.Application.Queries
{
    public class AuctionStatusQuery
    {
        private readonly IAuctionRepository _auctions;
        private readonly IBidHistoryRepository _bidHistory;
        private readonly IClock _clock;

        public AuctionStatusQuery(IAuctionRepository auctions, IBidHistoryRepository bidHistory, IClock clock)
        {
            _auctions = auctions;
            _bidHistory = bidHistory;
            _clock = clock;
        }

        public AuctionStatus AuctionStatus(Guid auctionId)
        {            
            var auction = _auctions.FindBy(auctionId);

            var snapshot = auction.GetSnapshot();

            return ConvertToStatus(snapshot);
        }

        public AuctionStatus ConvertToStatus(AuctionSnapshot snapshot)
        {
            var status = new AuctionStatus();

            status.AuctionEnds = snapshot.EndsAt;            
            status.Id = snapshot.Id;
            status.TimeRemaining = TimeRemaining(snapshot.EndsAt);

            if (snapshot.WinningBid != null)
            {
                status.NumberOfBids = _bidHistory.NoOfBidsFor(snapshot.Id);
                status.WinningBidderId = snapshot.WinningBid.BiddersId;
                status.CurrentPrice = snapshot.WinningBid.CurrentPrice;
            }
            
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
