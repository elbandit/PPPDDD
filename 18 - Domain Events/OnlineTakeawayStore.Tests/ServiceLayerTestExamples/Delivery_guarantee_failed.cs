using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using OnlineTakeawayStore.StaticDomainEvents.Model;
using OnlineTakeawayStore.StaticDomainEvents.Application;
using OnlineTakeawayStore.StaticDomainEvents.Application.Events;
using NServiceBus;
using System.Collections.Generic;

namespace OnlineTakeawayStore.Tests.ServiceLayerTestExamples
{
    [TestClass]
    public class Delivery_guarantee_failed
    {
        IBus bus = MockRepository.GenerateStub<IBus>();
        // when testing at service layer may want to use concrete repository
        IOrderRepository repo = MockRepository.GenerateStub<IOrderRepository>();
        Guid orderId = Guid.NewGuid();
        Guid customerId = Guid.NewGuid();
        Guid restaurantId = Guid.NewGuid();
        List<int> itemIds = new List<int> { 123, 456, 789 };
        DateTime timeOrderWasPlaced = new DateTime(2015, 03, 01, 20, 15, 0);


        [TestInitialize]
        public void If_an_order_is_not_delivered_within_the_agreed_upon_timeframe()
        {
            var offer = new ThirtyMinuteDeliveryGuaranteeOffer();
            
            // took longer than 30 minutes - failing the delivery guarantee
            var timeOrderWasReceived = timeOrderWasPlaced.AddMinutes(31);

            var order = new OrderForDelivery(
                orderId, customerId, restaurantId, itemIds, timeOrderWasPlaced, offer
            );

            repo.Stub(r => r.FindBy(orderId)).Return(order);
            var service = new ConfirmDeliveryOfOrder(repo, bus);
            service.Confirm(timeOrderWasReceived, orderId);
        }

        [TestMethod]
        public void An_external_refund_due_to_late_delivery_instruction_will_be_published()
        {
            // get first message published during execution of this use case
            var message = bus.GetArgumentsForCallsMadeOn(
                b => b.Send(new RefundDueToLateDelivery()), x => x.IgnoreArguments()
            )[0][0];
            var refund = message as RefundDueToLateDelivery;
            
            Assert.IsNotNull(refund);
            Assert.AreEqual(refund.OrderId, orderId);
        }
    }
}
