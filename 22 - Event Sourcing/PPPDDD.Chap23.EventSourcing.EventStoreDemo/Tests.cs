using EventStore.ClientAPI;
using EventStore.ClientAPI.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PPPDDD.Chap23.EventSourcing.EventStoreDemo
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Events_are_persisted_and_can_be_retrieved()
        {
            NewConnection(con =>
            {
                var eventStore = new GetEventStore(con);
                var streamName = CreateDisposableStreamName();
                eventStore.AppendEventsToStream(streamName, testEvents, null);

                var fromEs = eventStore.GetStream(streamName, 0, Int32.MaxValue - 1);

                Assert.AreEqual(fromEs.Count(), testEvents.Count());
                foreach (var e in testEvents)
                {
                    Assert.IsTrue(fromEs.Contains(e));
                }
            });
        }

        [TestMethod]
        public void A_subset_of_events_can_be_retrieved_by_querying_on_version_numbers()
        {
            NewConnection(con =>
            {
                var eventStore = new GetEventStore(con);
                var streamName = CreateDisposableStreamName();
                eventStore.AppendEventsToStream(streamName, testEvents, null);

                var fromEs = eventStore.GetStream(streamName, 1, 1);

                Assert.AreEqual(1, fromEs.Count());
                Assert.AreEqual("Trevor", ((TestEvent)fromEs.Single()).Handle);
            });
        }

        [TestMethod]
        public void Snapshots_are_persisted_and_the_latest_one_is_always_returned()
        {
            NewConnection(con =>
            {
                var eventStore = new GetEventStore(con);
                var streamName = CreateDisposableStreamName();

                var snapshot1 = new TestSnapshot { Version = 1 };
                var snapshot2 = new TestSnapshot { Version = 2 };
                var snapshot3 = new TestSnapshot { Version = 3 };

                eventStore.AddSnapshot<TestSnapshot>(streamName, snapshot1);
                eventStore.AddSnapshot<TestSnapshot>(streamName, snapshot2);
                eventStore.AddSnapshot<TestSnapshot>(streamName, snapshot3);

                var fromEs = eventStore.GetLatestSnapshot<TestSnapshot>(streamName);
                
                Assert.AreEqual(snapshot3, fromEs);
            });
        }

        [TestMethod]
        public void Optimistic_concurrency_is_supported()
        {
            NewConnection(con =>
            {
                var eventStore = new GetEventStore(con);
                var streamName = CreateDisposableStreamName();

                // version number will be 2 (3 events, 0 based)
                eventStore.AppendEventsToStream(streamName, testEvents, null);

                var newEvnt = new TestEvent(Guid.NewGuid())
                {
                    Handle = "New",
                    Name = "Newnew"
                };

                try
                {
                    var expectedVersion = 1; // we know the version is already 2
                    eventStore.AppendEventsToStream(streamName, new List<TestEvent>{ newEvnt }, expectedVersion);
                }
                catch (AggregateException e)
                {
                    var extype = e.InnerExceptions.First().GetType();
                    Assert.AreEqual(typeof(WrongExpectedVersionException), extype);
                    return; 
                }

                Assert.Fail("Optimistic concurrency violation was not detected");
            });
        }

        private string CreateDisposableStreamName()
        {
            // ES will not allow deleted streams to be re-created
            // for more info: https://groups.google.com/forum/#!msg/event-store/HnJcubREozE/shw_qCwvJ5IJ
            return "teststream" + Guid.NewGuid();
        }

        private void NewConnection(Action<IEventStoreConnection> action)
        {
            using(var con = EventStoreConnection.Create(endpoint))
            {
                con.Connect();
                action(con);
            }
        }

        List<TestEvent> testEvents = new List<TestEvent>
        {
            new TestEvent(Guid.NewGuid())
            {
                Name = "Name 0",
                Handle = "Jimmy"
            },
            new TestEvent(Guid.NewGuid())
            {
                Name = "Name 1",
                Handle = "Trevor",
            },
            new TestEvent(Guid.NewGuid())
            {
                Name = "Name 3",
                Handle = "Suzie"
            }
        };

        // ***** These tests require event store to be running on port configured here *****
        //       1113 is the default port, but change as necessary
        IPEndPoint endpoint = new IPEndPoint(IPAddress.Loopback, 1113);
    }

    public class TestEvent : DomainEvent
    {
        public TestEvent(Guid id): base(id) { }

        public string Name { get; set; }

        public string Handle { get; set; }

        public override bool Equals(object obj)
        {
            var te = obj as TestEvent;
            if (te == null) return false;

            return te.Id == this.Id &&
            te.Name == this.Name &&
            te.Handle == this.Handle;
        }
    }

    public class TestSnapshot
    {
        public int Version { get; set; }

        public override bool Equals(object obj)
        {
            var s = obj as TestSnapshot;
            if (s == null) return false;

            return s.Version == this.Version;
        }
    }
}
