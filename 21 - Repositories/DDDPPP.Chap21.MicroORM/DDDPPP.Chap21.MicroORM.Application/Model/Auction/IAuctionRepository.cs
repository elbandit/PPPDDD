using System;
using DDDPPP.Chap21.MicroORM.Application.Application;

namespace DDDPPP.Chap21.MicroORM.Application.Model.Auction
{
    public interface IAuctionRepository
    {
        void Add(Auction auction);
        void Save(Auction auction);
        Auction FindBy(Guid Id);
    }
}
