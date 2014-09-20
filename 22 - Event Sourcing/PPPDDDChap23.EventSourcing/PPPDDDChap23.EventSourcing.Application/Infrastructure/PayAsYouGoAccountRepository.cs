using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo;

namespace PPPDDDChap23.EventSourcing.Application.Infrastructure
{
    public class PayAsYouGoAccountRepository : IPayAsYouGoAccountRepository
    {
        private readonly IEventStore _eventStore;

        public PayAsYouGoAccountRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public PayAsYouGoAccount FindBy(Guid id)
        {
            var streamName = StreamNameFor(id);
            
            var fromEventNumber = 0;
            var toEventNumber = int.MaxValue ;

            var snapshot = _eventStore.GetLatestSnapshot<PayAsYouGoAccountSnapshot>(streamName);
            if (snapshot != null)
            {
                fromEventNumber = snapshot.Version + 1; // load only events after snapshot
            }
            
            var stream = _eventStore.GetStream(streamName, fromEventNumber, toEventNumber);

            PayAsYouGoAccount payAsYouGoAccount = null;
            if (snapshot != null)
            {
                payAsYouGoAccount = new PayAsYouGoAccount(snapshot);
            }
            else
            {
                payAsYouGoAccount = new PayAsYouGoAccount();
            }


            foreach(var @event in stream)
            {
                payAsYouGoAccount.Apply(@event);
            }

            return payAsYouGoAccount;            
        }
               

        public void Add(PayAsYouGoAccount payAsYouGoAccount)
        {
            var streamName = StreamNameFor(payAsYouGoAccount.Id);

            _eventStore.CreateNewStream(streamName, payAsYouGoAccount.Changes);
        }

        public void Save(PayAsYouGoAccount payAsYouGoAccount)
        {
            var streamName = StreamNameFor(payAsYouGoAccount.Id);

            var expectedVersion = GetExpectedVersion(payAsYouGoAccount.InitialVersion);
            _eventStore.AppendEventsToStream(streamName, payAsYouGoAccount.Changes, expectedVersion);                      
        }

        private int? GetExpectedVersion(int expectedVersion)
        {
            if (expectedVersion == 0)
            {
                // first time the aggregate is stored there is no expected version
                return null;
            }
            else 
            {
                return expectedVersion;
            }
        }

        public void SaveSnapshot(PayAsYouGoAccountSnapshot snapshot, PayAsYouGoAccount payAsYouGoAccount)
        {
            var streamName = StreamNameFor(payAsYouGoAccount.Id);

            _eventStore.AddSnapshot<PayAsYouGoAccountSnapshot>(streamName, snapshot);
        }

        private string StreamNameFor(Guid id)
        {
            // Stream per-aggregate: {AggregateType}-{AggregateId}
            return string.Format("{0}-{1}", typeof(PayAsYouGoAccount).Name, id);
        }
    }
}
