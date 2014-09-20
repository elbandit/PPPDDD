using PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Infrastructure
{
    public class PasAsYouGoAccountSnapshotJob
    {
        private IDocumentStore documentStore;

        public PasAsYouGoAccountSnapshotJob(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        public void Run()
        {
            while(true)
            {
                foreach (var id in GetIds())
                {
                    using (var session = documentStore.OpenSession())
                    {
                        var repository = new PayAsYouGoAccountRepository(new EventStore(session));
                        var account = repository.FindBy(Guid.Parse(id));
                        var snapshot = account.GetPayAsYouGoAccountSnapshot();
                        repository.SaveSnapshot(snapshot, account);
                    }
                }

                // Create a new snapshot for each Aggregate every 12 hours
                Thread.Sleep(TimeSpan.FromHours(12));
            }
        }

        private IEnumerable<string> GetIds()
        {
            using (var session = documentStore.OpenSession())
            {
                return session.Query<EventStream>()
                              .Select(x => x.Id)
                              .ToList();
            }
        }
    }
}
