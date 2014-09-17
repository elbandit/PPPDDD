using System;
using System.Collections.Generic;
using Raven.Client;
using DDDPPP.Chap19.RavenDBExample.Application.Model.Auction;
using DDDPPP.Chap19.RavenDBExample.Application.Model.BidHistory;
using DDDPPP.Chap19.RavenDBExample.Application.Infrastructure;

namespace DDDPPP.Chap19.RavenDBExample.Application.Application.Queries
{
    public class AuctionStatusQuery
    {
        private readonly IAuctionRepository _auctions;
        private readonly IBidHistoryRepository _bidHistory;
        private readonly IClock _clock;

        public AuctionStatusQuery(IAuctionRepository auctions, 
                                  IBidHistoryRepository bidHistory,
                                  IClock clock)
        {
            _auctions = auctions;
            _bidHistory = bidHistory;
            _clock = clock;
        }

        public AuctionStatus AuctionStatus(Guid auctionId)
        {
            var auction = _auctions.FindBy(auctionId);

            var status = new AuctionStatus();

            status.AuctionEnds = auction.EndsAt;
            status.Id = auction.Id;

            if (auction.HasBeenBidOn())
            {
                status.CurrentPrice = auction.WinningBid.CurrentAuctionPrice.Amount.Value;
                status.WinningBidderId = auction.WinningBid.Bidder;
            }

            status.TimeRemaining = TimeRemaining(auction.EndsAt);
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
