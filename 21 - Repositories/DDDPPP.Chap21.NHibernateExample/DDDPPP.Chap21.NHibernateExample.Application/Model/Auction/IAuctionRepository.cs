using System;

namespace DDDPPP.Chap21.NHibernateExample.Application.Model.Auction
{
    public interface IAuctionRepository
    {
        void Add(Auction auction);
        Auction FindBy(Guid Id);
    }
}
