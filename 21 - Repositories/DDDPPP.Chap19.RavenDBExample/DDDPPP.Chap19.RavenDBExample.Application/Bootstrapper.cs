using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;
using Raven.Client.Indexes;
using StructureMap;
using DDDPPP.Chap19.RavenDBExample.Application.Infrastructure;
using DDDPPP.Chap19.RavenDBExample.Application.Model.Auction;
using DDDPPP.Chap19.RavenDBExample.Application.Model.BidHistory;

namespace DDDPPP.Chap19.RavenDBExample.Application
{
    public static class Bootstrapper
    {
        public static void Startup()
        {           
            var documentStore = new DocumentStore
            {
                ConnectionStringName = "RavenDB"
            }.Initialize();

            documentStore.DatabaseCommands.EnsureDatabaseExists("RepositoryExample");

            ObjectFactory.Initialize(config =>
            {
                config.For<IAuctionRepository>().Use<AuctionRepository>();
                config.For<IBidHistoryRepository>().Use<Infrastructure.BidHistoryRepository>();
                config.For<IClock>().Use<SystemClock>();

                config.For<IDocumentStore>().Use(documentStore);
                config.For<IDocumentSession>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use(x =>
                    {
                        var store = x.GetInstance<IDocumentStore>();
                        var session = store.OpenSession();
                        session.Advanced.UseOptimisticConcurrency = true;
                        return session;
                    });

                IndexCreation.CreateIndexes(typeof(BidHistory_NumberOfBids).Assembly, documentStore);
            });
        }
    }
}
