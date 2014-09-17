using System;
using DDDPPP.Chap19.MicroORM.Application.Application;

namespace DDDPPP.Chap19.MicroORM.Application.Model.Auction
{
    public interface IAuctionRepository
    {
        void Add(Auction auction);
        void Save(Auction auction);
        Auction FindBy(Guid Id);
    }
}
