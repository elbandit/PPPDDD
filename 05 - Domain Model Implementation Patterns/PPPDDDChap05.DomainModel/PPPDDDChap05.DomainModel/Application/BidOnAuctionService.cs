using System;
using PPPDDDChap05.DomainModel.Model;

namespace PPPPDDDChap05.DomainModel.Application
{
    public class BidOnAuctionService
    {
        private IAuctionRepository _auctions;

        public BidOnAuctionService(IAuctionRepository auctions)
        {
            _auctions = auctions;

        }

        public void Bid(Guid auctionId, Guid memberId, decimal amount, DateTime dateOfBid)
        {                                                        
            var auction = _auctions.FindBy(auctionId);

            var bidAmount = new Money(amount);

            var offer = new Bid(memberId, bidAmount, dateOfBid);

            auction.PlaceBidFor(offer, dateOfBid);                                
         }                      
    }
}