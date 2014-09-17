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
        private readonly EventStore _eventStore;

        public PayAsYouGoAccountRepository(EventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public PayAsYouGoAccount FindBy(Guid id)
        {
            var streamName = string.Format("{0}-{1}", typeof(PayAsYouGoAccount).Name, id.ToString());

            // Check for snapshots

            var fromEventNumber = 0;
            var toEventNumber = int.MaxValue ;

            // pull back all events from snapshot
            var stream = _eventStore.GetStream(streamName, fromEventNumber, toEventNumber);

            var payAsYouGoAccount = new PayAsYouGoAccount();

            foreach(var @event in stream)
            {
                payAsYouGoAccount.Apply(@event);
            }

            return payAsYouGoAccount;            
        }

        public void Add(PayAsYouGoAccount payAsYouGoAccount)
        {
            var streamName = string.Format("{0}-{1}", typeof(PayAsYouGoAccount).Name, payAsYouGoAccount.Id.ToString());

            _eventStore.CreateNewStream(streamName, payAsYouGoAccount.Changes);
        }

        public void Save(PayAsYouGoAccount payAsYouGoAccount)
        {
            var streamName = string.Format("{0}-{1}", typeof(PayAsYouGoAccount).Name, payAsYouGoAccount.Id.ToString());

            _eventStore.AppendEventsToStream(streamName, payAsYouGoAccount.Changes);                      
        }

    }
}
