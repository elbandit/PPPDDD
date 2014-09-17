using System;
using System.Collections.Generic;
using DDDPPP.Chap19.EFExample.Application.Model.Auction;
using DDDPPP.Chap19.EFExample.Application.Application;
using DDDPPP.Chap19.EFExample.Application.Infrastructure.DataModel;

namespace DDDPPP.Chap19.EFExample.Application.Infrastructure
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly AuctionDatabaseContext _auctionExampleContext;

        public AuctionRepository(AuctionDatabaseContext auctionExampleContext)
        {
            _auctionExampleContext = auctionExampleContext;
        }

        public void Add(Auction auction)
        {            
            var auctionDTO = new AuctionDTO();

            Map(auctionDTO, auction.GetSnapshot());
            
            _auctionExampleContext.Auctions.Add(auctionDTO); 
        }

        public void Save(Auction auction)
        {
            var auctionDTO = _auctionExampleContext.Auctions.Find(auction.Id);

            Map(auctionDTO, auction.GetSnapshot());                      
        }

        public Auction FindBy(Guid Id)
        {
            var auctionDTO = _auctionExampleContext.Auctions.Find(Id);
            var auctionSnapshot = new AuctionSnapshot();

            auctionSnapshot.Id = auctionDTO.Id;
            auctionSnapshot.EndsAt = auctionDTO.AuctionEnds;
            auctionSnapshot.StartingPrice = auctionDTO.StartingPrice;
            auctionSnapshot.Version = auctionDTO.Version;

            if (auctionDTO.BidderMemberId.HasValue)
            {
                var bidSnapshot = new WinningBidSnapshot();

                bidSnapshot.BiddersMaximumBid = auctionDTO.MaximumBid.Value;
                bidSnapshot.CurrentPrice = auctionDTO.CurrentPrice.Value;
                bidSnapshot.BiddersId = auctionDTO.BidderMemberId.Value;
                bidSnapshot.TimeOfBid = auctionDTO.TimeOfBid.Value;
                auctionSnapshot.WinningBid = bidSnapshot;
            }
           
            return Auction.CreateFrom(auctionSnapshot);
        }

        public void Map(AuctionDTO auctionDTO, AuctionSnapshot snapshot)
        {                        
            auctionDTO.Id = snapshot.Id;
            auctionDTO.StartingPrice = snapshot.StartingPrice;
            auctionDTO.AuctionEnds = snapshot.EndsAt;
            auctionDTO.Version = snapshot.Version;

            if (snapshot.WinningBid != null)
            {
                auctionDTO.BidderMemberId = snapshot.WinningBid.BiddersId;
                auctionDTO.CurrentPrice = snapshot.WinningBid.CurrentPrice;
                auctionDTO.MaximumBid = snapshot.WinningBid.BiddersMaximumBid;
                auctionDTO.TimeOfBid = snapshot.WinningBid.TimeOfBid;
            }
        }
    }
}
