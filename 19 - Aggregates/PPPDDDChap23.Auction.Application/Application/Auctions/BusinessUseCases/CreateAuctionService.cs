using System;
using PPPDDDChap23.Auction.Application.Model.Auctions;
using PPPDDDChap23.Auction.Application.Model;
using PPPDDDChap23.Auction.Application.Model.Items;

namespace PPPDDDChap23.Auction.Application.Application.BusinessUseCases
{
    public class CreateAuctionService
    {
        private IAuctionRepository _auctions;
        // private IDocumentSession _unitOfWork;

        public CreateAuctionService(IAuctionRepository auctions)
        {
            _auctions = auctions;
            // _unitOfWork = unitOfWork;
        }

        public Guid Create(AuctionCreation command)
        {
            var auctionId = Guid.NewGuid();
            var startingPrice = new Money(command.StartingPrice);

            var item = new Item();

            // Find Seller Id

            _auctions.Add(new PPPDDDChap23.Auction.Application.Model.Auctions.Auction(auctionId, command.SellerId, startingPrice, command.EndsAt, item));

            // _unitOfWork.SaveChanges();

            return auctionId;
        }
    }
}