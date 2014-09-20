using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Infrastructure
{
    public class EventWrapper
    {
        public string Id { get; private set; }
        public DomainEvent Event { get; private set; }
        public string EventStreamId { get; private set; }
        public int EventNumber { get; private set; }

        public EventWrapper(DomainEvent @event, int eventNumber, string streamStateId)
        {
            Event = @event;     
            EventNumber = eventNumber;
            EventStreamId = streamStateId;
            Id = string.Format("{0}-{1}", streamStateId, EventNumber);
        }
    }
}
