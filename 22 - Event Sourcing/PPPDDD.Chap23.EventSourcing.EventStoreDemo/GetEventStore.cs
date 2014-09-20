using EventStore.ClientAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDD.Chap23.EventSourcing.EventStoreDemo
{
    public class GetEventStore : IEventStore
    {
        private IEventStoreConnection esConn;

        private const string EventClrTypeHeader = "EventClrTypeName";

        public GetEventStore(IEventStoreConnection esConn)
        {
            this.esConn = esConn;
        }

        public void CreateNewStream(string streamName, IEnumerable<DomainEvent> domainEvents)
        {
            // ES will automatically create a stream when events are added to it
            AppendEventsToStream(streamName, domainEvents, null);
        }

        public void AppendEventsToStream(string streamName, IEnumerable<DomainEvent> domainEvents, int? expectedVersion)
        {
            var commitId = Guid.NewGuid();
            var eventsInStorageFormat = domainEvents.Select(e => MapToEventStoreStorageFormat(e, commitId, e.Id));
            esConn.AppendToStream(StreamName(streamName), expectedVersion ?? ExpectedVersion.Any, eventsInStorageFormat);
        }

        private EventData MapToEventStoreStorageFormat(object evnt, Guid commitId, Guid eventId)
        {
            var headers = new Dictionary<string, object>
            {
                // each event will be associated with the same commit
                {"CommitId", commitId}, 
                
                // store type of class so event can be rebuilt when the event is loaded
                {EventClrTypeHeader, evnt.GetType().AssemblyQualifiedName}
            };

            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evnt));
            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(headers));
            var isJson = true;

            return new EventData(eventId, evnt.GetType().Name, isJson, data, metadata);
        }

        public IEnumerable<DomainEvent> GetStream(string streamName, int fromVersion, int toVersion)
        {
            // ES wants the number of events to retrieve not highest version
            var amount = (toVersion - fromVersion) + 1;
            Console.WriteLine("Amount: " + amount);
            var events = esConn.ReadStreamEventsForward(StreamName(streamName), fromVersion, amount, false); // last param not important here

            // map events back from JSON string to DomainEvent. Header indicates the type
            return events.Events.Select(e => (DomainEvent)RebuildEvent(e));
        }

        private object RebuildEvent(ResolvedEvent eventStoreEvent)
        {
            var metadata = eventStoreEvent.OriginalEvent.Metadata;
            var data = eventStoreEvent.OriginalEvent.Data;
            var typeOfDomainEvent = JObject.Parse(Encoding.UTF8.GetString(metadata)).Property(EventClrTypeHeader).Value;
            var rebuiltEvent = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), Type.GetType((string)typeOfDomainEvent));
            return rebuiltEvent;
        }

        // snapshots in Event Store are just events in dedicated snapshot streams
        // explained: http://stackoverflow.com/questions/16359330/is-snapshot-supported-from-greg-young-eventstore
        public void AddSnapshot<T>(string streamName, T snapshot)
        {
            var stream = SnapshotStreamNameFor(streamName);
            var snapshotAsEvent = MapToEventStoreStorageFormat(snapshot, Guid.NewGuid(), Guid.NewGuid());
            esConn.AppendToStream(stream, ExpectedVersion.Any, snapshotAsEvent);
        }

        public T GetLatestSnapshot<T>(string streamName) where T : class
        {
            var stream = SnapshotStreamNameFor(streamName);
            var amountToFetch = 1; // just the latest one
            var ev = esConn.ReadStreamEventsBackward(stream, StreamPosition.End, amountToFetch, false);
            
            if (ev.Events.Any())
                return (T)RebuildEvent(ev.Events.Single());
            else
                return null;
        }

        private string SnapshotStreamNameFor(string streamName)
        {
            // snapshots are just events in separate streams
            return StreamName(streamName) + "-snapshots";
        }

        private string StreamName(string streamName)
        {
            // Get Event Store projections require only a single hypen ("-")
            // see: https://groups.google.com/forum/#!msg/event-store/D477bKLcdI8/62iFGhHdMMIJ
            var sp = streamName.Split(new []{ '-' }, 2);
            return sp[0] + "-" + sp[1].Replace("-", "");
        }
    }
}
