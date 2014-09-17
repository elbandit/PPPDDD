using System;
using System.Collections.Generic;
using System.Linq;
using DDDPPP.Chap19.MicroORM.Application.Model.Auction;
using DDDPPP.Chap19.MicroORM.Application.Application;
using DDDPPP.Chap19.MicroORM.Application.Infrastructure.DataModel;
using System.Data.SqlClient;
using Dapper;

namespace DDDPPP.Chap19.MicroORM.Application.Infrastructure
{
    public class AuctionRepository : IAuctionRepository, IUnitOfWorkRepository
    {
        private IUnitOfWork _unitOfWork;

        public AuctionRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Auction auction)
        {
            var snapshot = auction.GetSnapshot();
            var auctionDTO = new AuctionDTO();

            Map(auctionDTO, snapshot);
                        
            _unitOfWork.RegisterNew(auctionDTO, this);
        }

        public void Save(Auction auction)
        {
            var snapshot = auction.GetSnapshot();
            var auctionDTO = new AuctionDTO(); 
           
            Map(auctionDTO, snapshot);

            _unitOfWork.RegisterAmended(auctionDTO, this);
        }

        public Auction FindBy(Guid Id)
        {
            AuctionDTO auctionDTO;

            using (var connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AuctionDB"].ConnectionString))
            {
                auctionDTO = connection.Query<AuctionDTO>("Select * From Auctions Where Id = CAST(@Id AS uniqueidentifier)", new { Id = Id }).FirstOrDefault();
            }

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

        public void PersistCreationOf(IAggregateDataModel entity)
        {
            var auctionDTO = (AuctionDTO)entity;

            using (var connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AuctionDB"].ConnectionString))
            {
                var recordsAdded = connection.Execute(@"                
                    INSERT INTO [AuctionExample].[dbo].[Auctions]
                           ([Id]
                           ,[StartingPrice]
                           ,[BidderMemberId]
                           ,[TimeOfBid]
                           ,[MaximumBid]
                           ,[CurrentPrice]
                           ,[AuctionEnds]
                           ,[Version])
                     VALUES
                           (@Id, @StartingPrice, @BidderMemberId, @TimeOfBid, @MaximumBid, @CurrentPrice, @AuctionEnds, @Version)"
                    , new { Id = auctionDTO.Id, StartingPrice = auctionDTO.StartingPrice, BidderMemberId = auctionDTO.BidderMemberId, 
                            TimeOfBid = auctionDTO.TimeOfBid, MaximumBid = auctionDTO.MaximumBid, CurrentPrice = auctionDTO.CurrentPrice,
                            AuctionEnds = auctionDTO.AuctionEnds, Version = auctionDTO.Version });
            }
        }

        public void PersistUpdateOf(IAggregateDataModel  entity)
        {
            var auctionDTO = (AuctionDTO)entity;

            using (var connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AuctionDB"].ConnectionString))
            {
                var recordsUpdated = connection.Execute(@"                
                    UPDATE 
                        [AuctionExample].[dbo].[Auctions]
                    SET 
                        [Id] = @Id
                       ,[StartingPrice] = @StartingPrice
                       ,[BidderMemberId] = @BidderMemberId
                       ,[TimeOfBid] = @TimeOfBid
                       ,[MaximumBid] = @MaximumBid
                       ,[CurrentPrice] = @CurrentPrice
                       ,[AuctionEnds] = @AuctionEnds
                       ,[Version] = @Version
                    WHERE
                        Id = @Id AND Version = @PreviousVersion"
                    , new
                    {
                        Id = auctionDTO.Id,
                        StartingPrice = auctionDTO.StartingPrice,
                        BidderMemberId = auctionDTO.BidderMemberId,
                        TimeOfBid = auctionDTO.TimeOfBid,
                        MaximumBid = auctionDTO.MaximumBid,
                        CurrentPrice = auctionDTO.CurrentPrice,
                        AuctionEnds = auctionDTO.AuctionEnds,
                        Version = auctionDTO.Version + 1,
                        PreviousVersion = auctionDTO.Version
                    });

                if (!recordsUpdated.Equals(1))
                {
                    throw new ConcurrencyException();
                }  
            }
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
