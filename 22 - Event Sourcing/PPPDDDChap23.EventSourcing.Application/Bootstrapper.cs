using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;
using Raven.Client.Indexes;
using StructureMap;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;
using PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo;

namespace PPPDDDChap23.EventSourcing.Application
{
    public static class Bootstrapper
    {
        public static void Startup()
        {           
            var documentStore = new DocumentStore
            {
                ConnectionStringName = "RavenDB"
            }.Initialize();

            documentStore.DatabaseCommands.EnsureDatabaseExists("EventSourcingExample");

            ObjectFactory.Initialize(config =>
            {
                config.For<IPayAsYouGoAccountRepository>().Use<PayAsYouGoAccountRepository>();
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
            });
        }
    }
}
