using System;
using System.Collections.Generic;
using System.Linq;
using DDDPPP.Chap19.EFExample.Application.Model.Auction;
using DDDPPP.Chap19.EFExample.Application.Model.BidHistory;
using DDDPPP.Chap19.EFExample.Application.Infrastructure;

namespace DDDPPP.Chap19.EFExample.Application.Application.BusinessUseCases
{
    public class CreateAuction
    {
        private IAuctionRepository _auctions;
        private AuctionDatabaseContext _unitOfWork;

        public CreateAuction(IAuctionRepository auctions, AuctionDatabaseContext unitOfWork)
        {
            _auctions = auctions;            
            _unitOfWork = unitOfWork;
        }

        public Guid Create(NewAuctionRequest command)
        {
            var auctionId = Guid.NewGuid();
            var startingPrice = new Money(command.StartingPrice);
           
            _auctions.Add(new Auction(auctionId, startingPrice, command.EndsAt));

            _unitOfWork.SaveChanges();
            
            return auctionId;
        }
    }
}
