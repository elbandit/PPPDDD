using System;

namespace PPPDDDChap19.eBidder.Listings.Model.Auctions
{
    public interface IAuctionRepository
    {
        void Add(Auction item);
        Auction FindBy(Guid Id);
    }
}
