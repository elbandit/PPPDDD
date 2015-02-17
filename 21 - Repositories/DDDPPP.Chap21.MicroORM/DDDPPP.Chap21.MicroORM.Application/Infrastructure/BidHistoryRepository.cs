using System;
using System.Collections.Generic;
using System.Linq;
using DDDPPP.Chap21.MicroORM.Application.Model.BidHistory;
using DDDPPP.Chap21.MicroORM.Application.Model.Auction;
using DDDPPP.Chap21.MicroORM.Application.Infrastructure.DataModel;
using Dapper;
using System.Data.SqlClient;

namespace DDDPPP.Chap21.MicroORM.Application.Infrastructure
{
    public class BidHistoryRepository : IBidHistoryRepository, IUnitOfWorkRepository
    {
        private IUnitOfWork _unitOfWork;

        public BidHistoryRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int NoOfBidsFor(Guid auctionId)
        {                        
            int count;

            using (var connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AuctionDB"].ConnectionString))
            {
                var count1 = connection.Query<int>("Select Count(*) From BidHistory Where AuctionId = @Id", new { Id = auctionId }).FirstOrDefault();

                count = count1 != null ? count1 : 1;
            }
                                     
            return count;
        }

        public void Add(HistoricalBid bid)        
        {
            var bidHistoryDTO = new BidDTO();

            bidHistoryDTO.AuctionId = bid.AuctionId;
            bidHistoryDTO.Bid = bid.AmountBid.GetSnapshot().Value;
            bidHistoryDTO.BidderId = bid.Bidder;
            bidHistoryDTO.TimeOfBid = bid.TimeOfBid;

            bidHistoryDTO.Id = Guid.NewGuid();

            _unitOfWork.RegisterNew(bidHistoryDTO, this);
        }

        public BidHistory FindBy(Guid auctionId)
        {
            IEnumerable<BidDTO> bidDTOs;

            using (var connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AuctionDB"].ConnectionString))
            {
                bidDTOs = connection.Query<BidDTO>("Select * From BidHistory Where AuctionId = @Id", new { Id = auctionId });
            }
  
            var bids = new List<HistoricalBid>();

            foreach (var bid in bidDTOs)
            { 
                bids.Add(new HistoricalBid(bid.AuctionId, bid.BidderId, new Money(bid.Bid), bid.TimeOfBid));
            }

            return new BidHistory(bids);
        }

        public void PersistCreationOf(IAggregateDataModel entity)
        {
            var bidHistoryDTO = (BidDTO)entity;

            using (var connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AuctionDB"].ConnectionString))
            {
                connection.Execute(@"                
                    INSERT INTO [dbo].[BidHistory]
                           ([AuctionId]
                           ,[BidderId]
                           ,[Bid]
                           ,[TimeOfBid]
                           ,[Id])
                     VALUES
                           (@AuctionId
                           ,@BidderId
                           ,@Bid
                           ,@TimeOfBid
                           ,@Id)"
                    , new
                    {
                        Id = bidHistoryDTO.Id,
                        BidderId = bidHistoryDTO.BidderId,
                        Bid = bidHistoryDTO.Bid,
                        TimeOfBid = bidHistoryDTO.TimeOfBid,
                        AuctionId = bidHistoryDTO.AuctionId 
                    });            
            }
        }

        public void PersistUpdateOf(IAggregateDataModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
