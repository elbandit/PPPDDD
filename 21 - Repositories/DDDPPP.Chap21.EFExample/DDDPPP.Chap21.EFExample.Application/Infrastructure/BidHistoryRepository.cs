using System;
using System.Collections.Generic;
using System.Linq;
using DDDPPP.Chap21.EFExample.Application.Model.BidHistory;
using DDDPPP.Chap21.EFExample.Application.Model.Auction;
using DDDPPP.Chap21.EFExample.Application.Infrastructure.DataModel;

namespace DDDPPP.Chap21.EFExample.Application.Infrastructure
{
    public class BidHistoryRepository : IBidHistoryRepository
    {
        private readonly AuctionDatabaseContext _auctionExampleContext;

        public BidHistoryRepository(AuctionDatabaseContext auctionExampleContext)
        {
            _auctionExampleContext = auctionExampleContext;
        }

        public int NoOfBidsFor(Guid autionId)
        {                     
            return _auctionExampleContext.Bids.Count(x => x.AuctionId == autionId);                                        
        }

        public void Add(Bid bid)        
        {
            var bidDTO = new BidDTO();

            bidDTO.AuctionId = bid.AuctionId;
            bidDTO.Bid = bid.AmountBid.GetSnapshot().Value;
            bidDTO.BidderId = bid.Bidder;
            bidDTO.TimeOfBid = bid.TimeOfBid;

            bidDTO.Id = Guid.NewGuid();

            _auctionExampleContext.Bids.Add(bidDTO); 
        }

        public BidHistory FindBy(Guid auctionId)
        {
            var bidDTOs = _auctionExampleContext.Bids.Where<BidDTO>(x => x.AuctionId == auctionId).ToList();
            var bids = new List<Bid>();

            foreach (var bidDTO in bidDTOs)
            {
                bids.Add(new Bid(bidDTO.AuctionId, bidDTO.BidderId, new Money(bidDTO.Bid), bidDTO.TimeOfBid));
            }

            return new BidHistory(bids);
        }
    }
}
