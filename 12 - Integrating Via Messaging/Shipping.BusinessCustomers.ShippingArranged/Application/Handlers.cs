using Billing.Messages.Events;
using NServiceBus;
using Sales.Messages.Events;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Shipping.BusinessCustomers.ShippingArranged
{
    // update inherited interface with V2
    public class OrderCreatedHandler : IHandleMessages<OrderCreated_V2>
    {
        // dependency injected by NServiceBus
        public IBus Bus { get; set; }

        // updated Handle accepting a V2
        public void Handle(OrderCreated_V2 message)
        {
            Console.WriteLine(
                "Shipping BC storing: Order: {0} User: {1} Shipping Type: {2}",
                message.OrderId, message.UserId, message.ShippingTypeId, message.AddressId
            );
            var order = new ShippingOrder
            {
                UserId = message.UserId,
                OrderId = message.OrderId,
                AddressId = message.AddressId,
                ShippingTypeId = message.ShippingTypeId
            };
            ShippingDatabase.AddOrderDetails(order);
        }
    }

    public class PaymentAcceptedHandler : IHandleMessages<PaymentAccepted>
   { 
        // dependency injected by NServiceBus
        public IBus Bus { get; set; }

        public void Handle(PaymentAccepted message)
        {
            var address = ShippingDatabase.GetCustomerAddress(message.OrderId);
            var confirmation = ShippingProvider.ArrangeShippingFor(address, message.OrderId);
            if (confirmation.Status == ShippingStatus.Success)
            {
                var evnt = new Shipping.Messages.Events.ShippingArranged
                {
                    OrderId = message.OrderId
                };
                Bus.Publish(evnt);
                Console.WriteLine(
                    "Shipping BC arranged shipping for Order: {0}",
                    message.OrderId, address
                );
            }
            else
            {
                // .. notify failed shipping instead
            }
        }
    }

    public static class ShippingDatabase
    {
        private static List<ShippingOrder> Orders = new List<ShippingOrder>();

        public static void AddOrderDetails(ShippingOrder order)
        {
            Orders.Add(order);
        }

        public static string GetCustomerAddress(string orderId)
        {
            var order = Orders
                        .Single(o => o.OrderId == orderId);

            return string.Format(
                "{0}, Address ID: {1}", 
                order.UserId, order.AddressId
            );
        }
    }

    public class ShippingOrder
    {
        public string UserId { get; set; }

        public string OrderId { get; set; }

        public string ShippingTypeId { get; set; }

        public string AddressId { get; set; }
    }

    public static class ShippingProvider
    {
        public static ShippingConfirmation ArrangeShippingFor(string address, string referenceCode)
        {
            return new ShippingConfirmation { Status = ShippingStatus.Success };
        }
    }
    public class ShippingConfirmation
    {
        public ShippingStatus Status { get; set; }
    }
    public enum ShippingStatus
    {
        Success,
        Failure
    }
}

