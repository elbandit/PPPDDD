using System;
using PPPDDDChap19.eBidder.Listings.Model.Auctions;
using PPPDDDChap19.eBidder.Listings.Model;
using PPPDDDChap19.eBidder.Listings.Model.Sellers;
using PPPDDDChap19.eBidder.Listings.Model.Listings;

namespace PPPDDDChap19.eBidder.Listings.Application.Application.BusinessUseCases
{
    public class CreateAuctionService
    {
        private IAuctionRepository _auctions;
        private ISellerService _sellerService;
        private IListingRepository _listings;
        // private IDocumentSession _unitOfWork;

        public CreateAuctionService(IAuctionRepository auctions, IListingRepository listing, ISellerService sellerService)
        {
            _auctions = auctions;
            _sellerService = sellerService;
            _listings = listing;
            // _unitOfWork = unitOfWork;
        }

        public Guid Create(AuctionCreation command)
        {
            var auctionId = Guid.NewGuid();
            var listingId = Guid.NewGuid();
            var startingPrice = new Money(command.StartingPrice);

            var listing = new Listing(listingId, command.SellerId, new ListingFormat(auctionId, FormatType.Auction));

            var auction = new Auction(auctionId, listingId, startingPrice, command.EndsAt); 

            // Can seller list
            var seller = _sellerService.GetSeller(command.SellerId);

            if (seller != null && seller.CanList)
            {
                _listings.Add(listing);
                _auctions.Add(auction);  
            }
       
            // _unitOfWork.SaveChanges();

            return auctionId;
        }
    }
}