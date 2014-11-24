using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using OnlineTakeawayStore.Application;
using OnlineTakeawayStore.Domain;
using OnlineTakeawayStore.StaticDomainEvents.Model;
using OnlineTakeawayStore.StaticDomainEvents.Infrastructure;

namespace OnlineTakeawayStore.Tests
{
    [TestClass]
    public class Food_delivery_order_validation
    {
        // collaborators
        static INotificationChannel clientChannel = MockRepository.GenerateStub<INotificationChannel>();
        static IRestaurantConnector connector = MockRepository.GenerateStub<IRestaurantConnector>();

        // services used in domain event handlers
        static ICustomerBehaviorChecker checker = MockRepository.GenerateStub<ICustomerBehaviorChecker>();
        static IEmailer emailer = MockRepository.GenerateStub<IEmailer>();
        static IFoodDeliveryOrderRepository repository = MockRepository.GenerateStub<IFoodDeliveryOrderRepository>();

        // test data
        static Guid blacklistedCustomerId = Guid.NewGuid();
        static List<int> menuItemIds = new List<int> { 333, 164, 990 };
        static PlaceFoodDeliveryOrderRequest request = new PlaceFoodDeliveryOrderRequest
        {
            CustomerId = blacklistedCustomerId,
            DeliveryTime = DateTime.Now.AddHours(1),
            MenuItemIds = menuItemIds,
            RestaurantId = Guid.NewGuid()
        };
        
        [ClassInitialize]
        public static void When_a_food_delivery_order_is_placed_for_the_blacklisted_customer(TestContext ctx)
        {
            // simulate a blacklisted customer
            checker.Stub(c => c.IsBlacklisted(blacklistedCustomerId)).Return(true);

            DomainHandlersRegister.WireUpDomainEventHandlers(repository, checker);
            ServiceLayerHandlersRegister.WireUpDomainEventHandlers(emailer);

            var service = new FoodDeliveryOrderService(clientChannel, connector);
            service.PlaceFoodDeliveryOrder(request);
        }

        [TestMethod]
        public void A_notification_will_be_sent_to_customer_indicating_they_are_blacklisted()
        {
            clientChannel.AssertWasCalled(c => c.Publish(""), x => x.IgnoreArguments());
            var arg = clientChannel.GetArgumentsForCallsMadeOn(c => c.Publish(""))[0][0];
            Assert.IsTrue(arg.ToString().StartsWith("ORDER_REJECTED_BLACKLISTED_CUSTOMER_"));
        }

        [TestMethod]
        public void An_email_will_be_sent_to_the_customer_indicating_they_are_blacklisted()
        {
            emailer.AssertWasCalled(e => e.NotifyBlacklistedCustomerRejection(blacklistedCustomerId));
        }

        [TestMethod]
        public void The_order_will_be_saved_in_the_rejected_state()
        {
            FoodDeliveryOrder savedOrder = (FoodDeliveryOrder)repository.GetArgumentsForCallsMadeOn(r => r.Save(null), x => x.IgnoreArguments())[0][0];

            Assert.AreEqual(blacklistedCustomerId, savedOrder.CustomerId);
            Assert.AreEqual(FoodDeliveryOrderSteps.Rejected, savedOrder.Status);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            //unregister all handlers to not interfere with other tests
            DomainEvents.ClearAll();
        }
    }

}
