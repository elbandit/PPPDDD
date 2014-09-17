using System;
using PPPDDDChap19.eBidder.Listings.Model.Auctions;
using PPPDDDChap19.eBidder.Listings.Model;
using PPPDDDChap19.eBidder.Listings.Model.Items;

namespace PPPDDDChap19.eBidder.Listings.Application.Application.BusinessUseCases
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

            _auctions.Add(new PPPDDDChap19.eBidder.Listings.Model.Auctions.Auction(auctionId, command.SellerId, startingPrice, command.EndsAt, item));

            // _unitOfWork.SaveChanges();

            return auctionId;
        }
    }
}