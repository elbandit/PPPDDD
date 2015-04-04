using System;
using StructureMap;
using DDDPPP.Chap21.EFExample.Application.Infrastructure;
using DDDPPP.Chap21.EFExample.Application.Model.Auction;
using DDDPPP.Chap21.EFExample.Application.Model.BidHistory;

namespace DDDPPP.Chap21.EFExample.Application
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
            });
        }
    }
}
