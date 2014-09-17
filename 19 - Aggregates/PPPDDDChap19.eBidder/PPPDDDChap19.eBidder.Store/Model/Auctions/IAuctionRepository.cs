using System;

namespace PPPDDDChap19.eBidder.Store.Application.Model.Auctions
{
    public interface IAuctionRepository
    {
        void Add(Auction item);
        Auction FindBy(Guid Id);
    }
}
