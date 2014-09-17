using System;
using System.Collections.Generic;
using StructureMap;
using DDDPPP.Chap19.NHibernateExample.Application.Infrastructure;
using DDDPPP.Chap19.NHibernateExample.Application.Model.Auction;
using DDDPPP.Chap19.NHibernateExample.Application.Model.BidHistory;
using NHibernate;
using NHibernate.Cfg;

namespace DDDPPP.Chap19.NHibernateExample.Application
{
    public static class Bootstrapper
    {
        public static void Startup()
        {
            Configuration config = new Configuration();
                       
            config.Configure();
            config.AddAssembly("DDDPPP.Chap19.NHibernateExample.Application");

            var sessionFactory = config.BuildSessionFactory();

            ObjectFactory.Initialize(structureMapConfig =>
            {
                structureMapConfig.For<IAuctionRepository>().Use<AuctionRepository>();
                structureMapConfig.For<IBidHistoryRepository>().Use<Infrastructure.BidHistoryRepository>();
                structureMapConfig.For<IClock>().Use<SystemClock>();

                structureMapConfig.For<ISessionFactory>().Use(sessionFactory);
                structureMapConfig.For<ISession>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use(x =>
                    {
                        var factory = x.GetInstance<ISessionFactory>();
                        return factory.OpenSession();
                    });
            });

        }
    }
}
