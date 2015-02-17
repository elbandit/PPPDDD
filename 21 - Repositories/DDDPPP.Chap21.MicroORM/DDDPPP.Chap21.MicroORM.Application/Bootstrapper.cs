using System;
using StructureMap;
using DDDPPP.Chap21.MicroORM.Application.Infrastructure;
using DDDPPP.Chap21.MicroORM.Application.Model.Auction;
using DDDPPP.Chap21.MicroORM.Application.Model.BidHistory;

namespace DDDPPP.Chap21.MicroORM.Application
{
    public static class Bootstrapper
    {
        public static void Startup()
        {                      
            ObjectFactory.Initialize(config =>
            {
                config.For<IAuctionRepository>().Use<AuctionRepository>();
                config.For<IBidHistoryRepository>().Use<BidHistoryRepository>();
                config.For<IClock>().Use<SystemClock>();

                config.For<IUnitOfWork>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use(x =>
                    {
                        var uow = new UnitOfWork();
                        return uow;
                    });              
            });

        }
    }
}
