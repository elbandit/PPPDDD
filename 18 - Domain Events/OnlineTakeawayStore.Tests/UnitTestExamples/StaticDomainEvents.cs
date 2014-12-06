using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineTakeawayStore.StaticDomainEvents.Model;
using System.Collections.Generic;
using Rhino.Mocks;
using OnlineTakeawayStore.StaticDomainEvents.Infrastructure;
using OnlineTakeawayStore.StaticDomainEvents.Model.Events;

namespace OnlineTakeawayStore.Tests.UnitTestExamples
{
    [TestClass]
    public class Delivery_guarantee_events_are_raised_on_guarantee_offer_failure
    {
        public bool eventWasRaised = false;
        public Guid id = Guid.NewGuid();
        public Guid customerId = Guid.NewGuid();
        public Guid restaurantId = Guid.NewGuid();
        public List<int> menuItemIds = new List<int>();
        public DateTime timeOrderWasPlaced = DateTime.Now.AddMinutes(-30);
        public DateTime timePizzaDelivered = DateTime.Now;
        public IDeliveryGuaranteeOffer offer = MockRepository.GenerateStub<IDeliveryGuaranteeOffer>();

        [TestInitialize]
        public void When_confirming_an_order_that_is_late()
        {
            offer.Stub(x => x.IsNotSatisfiedBy(timeOrderWasPlaced, timePizzaDelivered)).Return(true);
            var order = new OrderForDelivery(id, customerId, restaurantId, menuItemIds, timeOrderWasPlaced, offer);
            
            using (DomainEvents.Register<DeliveryGuaranteeFailed>(setTestFlag))
            {
                order.ConfirmReceipt(timePizzaDelivered);
            }
        }

        private void setTestFlag(DeliveryGuaranteeFailed obj)
        {
            eventWasRaised = true;
        }

        [TestMethod]
        public void A_delivery_guarantee_failed_event_will_be_raised()
        {
            Assert.IsTrue(eventWasRaised);
        }
    }
 }
