using EventStore.ClientAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;
using PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDD.Chap23.EventSourcing.EventStoreDemo
{
    [TestClass]
    public class ImportTestData
    {
        // ***** These tests require event store to be running on port configured here *****
        //       1113 is the default port, but change as necessary
        IPEndPoint endpoint = new IPEndPoint(IPAddress.Loopback, 1113);

        PayAsYouGoAccount account1;
        PayAsYouGoAccount account2;
        PayAsYouGoAccount account3;
        PayAsYouGoAccount account4;
        PayAsYouGoAccount account5;

        /*
         * After running these tests the events will exist inside Event Store. You can then
         * access the Web Admin UI and run queries, projections etc.
         * 
         */
        [TestMethod]
        public void Import_test_data_for_temporal_queries_and_projections_example_in_the_book()
        {
            using (var con = EventStoreConnection.Create(endpoint))
            {
                con.Connect();
                var es = new GetEventStore(con);
                var repo = new PayAsYouGoAccountRepository(es);

                Create5EmptyAccounts();

                SimulateCustomerActivityFor3rdJune();
                SimulateCustomerActivityFor4thJune();
                SimulateCustomerActivityFor5thJune();

                PersistAllUncommittedEvents(repo);
            }
        }

        private void Create5EmptyAccounts()
        {
            var account1Id = Guid.NewGuid();
            account1 = new PayAsYouGoAccount(account1Id, new Money(0));

            var account2Id = Guid.NewGuid();
            account2 = new PayAsYouGoAccount(account2Id, new Money(0));

            var account3Id = Guid.NewGuid();
            account3 = new PayAsYouGoAccount(account3Id, new Money(0));

            var account4Id = Guid.NewGuid();
            account4 = new PayAsYouGoAccount(account4Id, new Money(0));

            var account5Id = Guid.NewGuid();
            account5 = new PayAsYouGoAccount(account5Id, new Money(0));
        }

        private void SimulateCustomerActivityFor3rdJune()
        {
            // quiet normal day

            var startOfDay = new DateTime(2014, 06, 03);

            account1.TopUp(new Money(5), TestClock(startOfDay.AddHours(10)));
            account1.Record(PhoneCall(startOfDay.AddHours(10.5), 5), Cost(), null);
            account1.Record(PhoneCall(startOfDay.AddHours(20), 10), Cost(), null);

            account4.TopUp(new Money(10), TestClock(startOfDay.AddHours(18)));
            account4.Record(PhoneCall(startOfDay.AddHours(18.1), 7), Cost(), null);
        }

        private void SimulateCustomerActivityFor4thJune()
        {
            // Day of the big marketing promotion

            var startOfDay = new DateTime(2014, 06, 04);

            var freeCalls = new FreePhoneCallCosting();

            account1.TopUp(new Money(20), TestClock(startOfDay.AddHours(9.5)));
            account1.Record(PhoneCall(startOfDay.AddHours(10), 22), freeCalls, null);
            account1.Record(PhoneCall(startOfDay.AddHours(10.5), 15), freeCalls, null);
            account1.Record(PhoneCall(startOfDay.AddHours(12.75), 45), freeCalls, null);
            account1.Record(PhoneCall(startOfDay.AddHours(18.75), 5), freeCalls, null);
            account1.Record(PhoneCall(startOfDay.AddHours(19.0), 7), freeCalls, null);

            account2.TopUp(new Money(20), TestClock(startOfDay.AddHours(6.5)));
            account2.Record(PhoneCall(startOfDay.AddHours(19), 120), freeCalls, null);
            
            account3.TopUp(new Money(20), TestClock(startOfDay.AddHours(21.25)));
            account3.Record(PhoneCall(startOfDay.AddHours(21.25), 24), freeCalls, null);
            account3.Record(PhoneCall(startOfDay.AddHours(23.5), 28), freeCalls, null);
            
            account4.TopUp(new Money(20), TestClock(startOfDay.AddHours(18.75)));
            account4.Record(PhoneCall(startOfDay.AddHours(19.0), 13), freeCalls, null);
            account4.Record(PhoneCall(startOfDay.AddHours(19.25), 19), freeCalls, null);
            account4.Record(PhoneCall(startOfDay.AddHours(20), 7), freeCalls, null);
            account4.Record(PhoneCall(startOfDay.AddHours(19.0), 13), freeCalls, null);
            
            account5.TopUp(new Money(20), TestClock(startOfDay.AddHours(23)));
            account5.Record(PhoneCall(startOfDay.AddHours(23.1), 35), freeCalls, null);
        }

        private void SimulateCustomerActivityFor5thJune()
        {
            var startOfDay = new DateTime(2014, 06, 05);

            account1.Record(PhoneCall(startOfDay.AddHours(7.5), 3), Cost(), null);
            account1.Record(PhoneCall(startOfDay.AddHours(14.4), 6), Cost(), null);
            account1.Record(PhoneCall(startOfDay.AddHours(19.75), 2), Cost(), null);

            account2.Record(PhoneCall(startOfDay.AddHours(19), 5), Cost(), null);

            account4.Record(PhoneCall(startOfDay.AddHours(15.75), 9), Cost(), null);
            account4.Record(PhoneCall(startOfDay.AddHours(23.6), 4), Cost(), null);
        }

        private IClock TestClock(DateTime date)
        {
            return new TestClockThatReturnsFixedDate(date);
        }

        private PhoneCall PhoneCall(DateTime start, int minutes)
        {
            return new PhoneCall(new PhoneNumber("11111111111"), start, new Minutes(minutes));
        }

        private PhoneCallCosting Cost()
        {
            return new PhoneCallCosting();
        }

        private void PersistAllUncommittedEvents(PayAsYouGoAccountRepository repo)
        {
            repo.Save(account1);
            repo.Save(account2);
            repo.Save(account3);
            repo.Save(account4);
            repo.Save(account5);
        }
    }

    public class TestClockThatReturnsFixedDate : IClock
    {
        DateTime stubbedDate;

        public TestClockThatReturnsFixedDate(DateTime date)
        {
            stubbedDate = date;
        }

        public DateTime Time()
        {
            return stubbedDate;
        }
    }

    public class FreePhoneCallCosting : PhoneCallCosting
    {
        public override Money DetermineCostOfCall(Minutes minutes)
        {
            return new Money(0);
        }
    }
}
