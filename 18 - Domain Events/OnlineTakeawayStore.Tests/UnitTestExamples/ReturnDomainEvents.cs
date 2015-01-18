using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineTakeawayStore.ReturnEvents.Model;
using OnlineTakeawayStore.ReturnEvents.Model.Events;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.Tests.UnitTestExamples
{
    [TestClass]
    public class Delivery_guarantee_events_are_recorded_on_guarantee_offer_failure
    {
        public bool eventWasRaised = false;
        public Guid id = Guid.NewGuid();
        public Guid customerId = Guid.NewGuid();
        public Guid restaurantId = Guid.NewGuid();
        public List<int> menuItemIds = new List<int>();
        public DateTime timeOrderWasPlaced = DateTime.Now.AddMinutes(-30);
        public DateTime timePizzaDelivered = DateTime.Now;
        public IDeliveryGuaranteeOffer offer = MockRepository.GenerateStub<IDeliveryGuaranteeOffer>();
        public OrderForDelivery order = null;

        [TestInitialize]
        public void When_confirming_an_order_that_is_late()
        {
            offer.Stub(x => x.IsNotSatisfiedBy(timeOrderWasPlaced, timePizzaDelivered)).Return(true);
            order = new OrderForDelivery(id, customerId, restaurantId, menuItemIds, timeOrderWasPlaced, offer);
            order.ConfirmReceipt(timePizzaDelivered);
        }

        [TestMethod]
        public void A_delivery_guarantee_failed_event_will_be_recorded()
        {
            var wasRecorded = order.RecordedEvents.OfType<DeliveryGuaranteeFailed>().Count() == 1;
            Assert.IsTrue(wasRecorded);
        }
    }
}
