using System;

namespace DDDPPP.Chap19.NHibernateExample.Application.Model.Auction
{
    public interface IAuctionRepository
    {
        void Add(Auction auction);
        Auction FindBy(Guid Id);
    }
}
