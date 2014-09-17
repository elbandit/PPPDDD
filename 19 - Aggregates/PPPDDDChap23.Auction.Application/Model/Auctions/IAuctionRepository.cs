using System;

namespace PPPDDDChap23.Auction.Application.Model.Auctions
{
    public interface IAuctionRepository
    {
        void Add(Auction item);
        Auction FindBy(Guid Id);
    }
}
