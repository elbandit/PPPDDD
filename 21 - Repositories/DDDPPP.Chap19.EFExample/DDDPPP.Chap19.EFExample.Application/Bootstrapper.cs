using System;
using StructureMap;
using DDDPPP.Chap19.EFExample.Application.Infrastructure;
using DDDPPP.Chap19.EFExample.Application.Model.Auction;
using DDDPPP.Chap19.EFExample.Application.Model.BidHistory;

namespace DDDPPP.Chap19.EFExample.Application
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
