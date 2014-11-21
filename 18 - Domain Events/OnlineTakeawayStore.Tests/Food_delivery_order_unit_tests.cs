using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineTakeawayStore.Domain;
using OnlineTakeawayStore.Domain.Events;
using System.Collections.Generic;

namespace OnlineTakeawayStore.Tests
{
    [TestClass]
    public class Food_delivery_order_unit_tests
    {
        [TestMethod]
        public void A_food_delivery_order_created_event_will_be_raised_on_creation()
        {
            DomainEvents.ClearAll();

            var eventWasRaised = false;
            DomainEvents.Register<FoodDeliveryOrderCreated>(order =>
            {
                eventWasRaised = true;
            });
            new FoodDeliveryOrder(1, 1, 1, new List<int>(), DateTime.Now);

            Assert.AreEqual(eventWasRaised, true);
            // Also may want to verify that the properties were correctly set
            // on the order
        }
    }
}
