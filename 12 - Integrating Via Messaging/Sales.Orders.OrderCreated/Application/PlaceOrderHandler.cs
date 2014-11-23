using System;
using NServiceBus;
using Sales.Messages.Commands;
using System.Collections.Generic;

namespace Sales.Orders.OrderCreated
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        public IBus Bus { get; set; }

        public void Handle(PlaceOrder message)
        {
            var orderId = Database.SaveOrder(message.ProductIds, message.UserId, message.ShippingTypeId);

            Console.WriteLine(
                @"Created order #{3} : Products:{0} with shipping: {1} made by user: {2}", 
                String.Join(",", message.ProductIds), message.ShippingTypeId, message.UserId, orderId
            ); 

            // sending a V2 message now
            var orderCreatedEvent = new Sales.Messages.Events.OrderCreated_V2
            {
                OrderId = orderId,
                UserId = message.UserId,
                ProductIds = message.ProductIds,
                ShippingTypeId = message.ShippingTypeId,
                TimeStamp = DateTime.Now,
                Amount = CalculateCostOf(message.ProductIds),
                /*
                 * add a new field to the form and the PlaceOrder command
                 * if you don't want to hard-code the value
                 */
                AddressId = "AddressID123"
            };

            Bus.Publish(orderCreatedEvent);
        }

        private double CalculateCostOf(IEnumerable<string> productIds)
        {
            // database lookup, etc
            return 1000.00;
        }
    }

    // This could be any database technology. It can differ between Business Components
    public static class Database
    {
        private static int Id = 0;

        public static string SaveOrder(IEnumerable<string> productIds, string userId, string shippingTypeId)
        {
            var nextOrderId = Id++;
            return nextOrderId.ToString();
        }
    }
}


