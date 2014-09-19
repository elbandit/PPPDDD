using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using OnlineTakeawayStore.Application;
using OnlineTakeawayStore.Domain;

namespace OnlineTakeawayStore.Tests
{
    [TestClass]
    public class Placing_a_food_delivery_order
    {
        // application service collaborators
        static INotificationChannel client = MockRepository.GenerateStub<INotificationChannel>();
        static IRestaurantConnector connector = MockRepository.GenerateStub<IRestaurantConnector>();

        // services used in event handlers (previously would have also been collaborators)
        static IEmailer emailer = MockRepository.GenerateStub<IEmailer>();
        static IFoodDeliveryOrderRepository repository = MockRepository.GenerateStub<IFoodDeliveryOrderRepository>();

        // test data
        static int customerId = 9989445;
        static DateTime deliveryTime = DateTime.Now.AddHours(1);
        static List<int> menuItemÌds = new List<int> { 46, 23, 921 };
        static int restaurantId = 422326;

        [ClassInitialize]
        public static void When_placing_a_food_delivery_order_that_is_accepted_by_the_restaurant(TestContext ctx)
        {
            DomainHandlersRegister.WireUpDomainEventHandlers(repository, null);
            ServiceLayerHandlersRegister.WireUpDomainEventHandlers(emailer);

            var service = new FoodDeliveryOrderService(client, connector);
            var request = new PlaceFoodDeliveryOrderRequest
            {
                CustomerId = customerId,
                DeliveryTime = deliveryTime,
                MenuItemIds = menuItemÌds,
                RestaurantId = restaurantId
            };
            service.PlaceFoodDeliveryOrder(request);
        }

        [TestMethod]
        public void The_order_will_be_confirmed_with_an_email()
        {
            emailer.AssertWasCalled(e => e.SendFoodDeliveryOrderConfirmation(customerId));
        }

        [TestMethod]
        public void A_real_time_notification_of_order_completion_will_also_be_sent()
        {
            client.AssertWasCalled(c => c.Publish("ORDER_CONFIRMED"));
        }

        [TestMethod]
        public void A_record_of_the_order_will_be_saved()
        {
            // this is how you get arguments passed into methods with rhino mocks
            // in this case, getting the order to check the repository saved it
            FoodDeliveryOrder savedOrder = (FoodDeliveryOrder)repository.GetArgumentsForCallsMadeOn(r => r.Save(null), x => x.IgnoreArguments())[0][0];

            // the order that was saved must match the details of the customer's order
            Assert.AreEqual(customerId, savedOrder.CustomerId);
            Assert.AreEqual(restaurantId, savedOrder.RestaurantId);
            Assert.AreEqual(menuItemÌds, savedOrder.MenuItemIds);
            Assert.AreEqual(deliveryTime, savedOrder.RequestedDeliveryTime);
        }
    }
   
    [TestClass]
    public class Food_delivery_order_with_real_time_updates
    {
        // application service collaborators
        static INotificationChannel client = MockRepository.GenerateStub<INotificationChannel>();
        static IRestaurantConnector connector = MockRepository.GenerateStub<IRestaurantConnector>();
        static INotificationChannel kitchen = MockRepository.GenerateStub<INotificationChannel>();

        // services used in event handlers (previously would have also been collaborators)
        static IEmailer emailer = MockRepository.GenerateStub<IEmailer>();
        static IFoodDeliveryOrderRepository repository = MockRepository.GenerateStub<IFoodDeliveryOrderRepository>();

        // test data
        static int customerId = 9989445;
        static DateTime deliveryTime = DateTime.Now.AddHours(1);
        static List<int> menuItemÌds = new List<int> { 46, 23, 921 };
        static int restaurantId = 422326;
        static PlaceFoodDeliveryOrderRequest request = new PlaceFoodDeliveryOrderRequest
        {
            CustomerId = customerId,
            RestaurantId = restaurantId,
            MenuItemIds = menuItemÌds,
            DeliveryTime = deliveryTime
        };
        static string orderRequest = "ORDER_REQUEST_" + customerId + "_" + restaurantId + "_" + String.Join(",", menuItemÌds.Select(x => x.ToString()) + "_" + deliveryTime.ToLocalTime());

        [TestMethod]
        public void Each_time_the_kitchen_staff_the_order_to_the_next_step_a_real_time_notification_is_sent_to_the_customer()
        {
            connector.Stub(s => s.ConnectTo(restaurantId)).Return(kitchen);

            // when the order is placed
            var service = new FoodDeliveryOrderService(client, connector);
            service.PlaceFoodDeliveryOrder(request);
                // the order is saved
                   // TODO

            // when the order is validated
                // the order is saved
                // the restaurant kitchen is notified of the order when the kitchen confirms the order
                kitchen.Stub(k => k.Handle(orderRequest)).Return("ORDER_CONFIRMED");

            // then
                // the order is saved
                // the customer is sent a real-time notification
                // the customer is sent a real-time notification

            // Order is cooked
                // triggered by update from restaurant 
                // client notified

            // Order id despatched
                // triggered by update from restaurant
                // client notified

            // Final delivery step
                // order saved 
                // client notified
                // connections closed
        }
    }
}
