using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;
using PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.ApplicationTests
{
    [TestClass]
    public class PayAsYouGoAccount_Tests
    {
        static PayAsYouGoAccount _account;
        static Money _fiveDollars = new Money(5);
        static PayAsYouGoInclusiveMinutesOffer _free90MinsWith10DollarTopUp = new PayAsYouGoInclusiveMinutesOffer(new Money(10), new Minutes(90));

        [ClassInitialize] // runs first
        public static void When_applying_a_top_up_that_does_not_qualify_for_inclusive_minutes(TestContext ctx)
        {
            _account = new PayAsYouGoAccount(Guid.NewGuid(), new Money(0));
            // remove the AccountCreated event that is not relevant to this test
            _account.Changes.Clear();

            _account.AddInclusiveMinutesOffer(_free90MinsWith10DollarTopUp);
            _account.TopUp(_fiveDollars, new SystemClock()); // $5 top up does not meet $10 threshold for free minutes
        }

        [TestMethod]
        public void The_account_will_be_credited_with_the_top_up_amount_but_no_free_minutes()
        {
            var lastEvent = _account.Changes.SingleOrDefault() as CreditAdded;
            Assert.IsNotNull(lastEvent);
            Assert.AreEqual(_fiveDollars, lastEvent.Credit);
            Assert.AreEqual(5, _account.GetPayAsYouGoAccountSnapshot().Credit);
        }

    }
}
