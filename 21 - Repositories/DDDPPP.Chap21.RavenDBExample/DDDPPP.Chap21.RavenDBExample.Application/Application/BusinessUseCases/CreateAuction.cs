using System;
using DDDPPP.Chap21.RavenDBExample.Application.Model.Auction;
using DDDPPP.Chap21.RavenDBExample.Application.Model.BidHistory;
using Raven.Client;
using DDDPPP.Chap21.RavenDBExample.Application.Infrastructure;

namespace DDDPPP.Chap21.RavenDBExample.Application.Application.BusinessUseCases
{
    public class CreateAuction
    {
        private IAuctionRepository _auctions;
        private IDocumentSession _unitOfWork;

        public CreateAuction(IAuctionRepository auctions, IDocumentSession unitOfWork)
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
