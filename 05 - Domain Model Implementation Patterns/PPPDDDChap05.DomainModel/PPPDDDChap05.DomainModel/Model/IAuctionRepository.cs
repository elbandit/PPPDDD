using System;

namespace PPPDDDChap05.DomainModel.Model
{
    public interface IAuctionRepository
    {
        void Add(Auction item);
        Auction FindBy(Guid Id);
    }
}
