using System;
using Billing.Messages.Commands;
using NServiceBus;

namespace Billing.Payments.PaymentAccepted
{
    public class RecordPaymentAttemptHandler : IHandleMessages<RecordPaymentAttempt>
    {
        // dependency injected by NServiceBus
        public IBus Bus { get; set; }

        public void Handle(RecordPaymentAttempt message)
        {
            Database.SavePaymentAttempt(message.OrderId, message.Status);
            if (message.Status == PaymentStatus.Accepted)
            {
                var evnt = new Billing.Messages.Events.PaymentAccepted 
                { 
                    OrderId = message.OrderId
                };
                Bus.Publish(evnt);
                Console.WriteLine(
                    "Recevied payment accepted notification for Order: {0}. Published PaymentAccepted event",
                    message.OrderId
                );
            }
            else
            {
                // publish a payment rejected event
            }
        }

        public  static class Database
        {
            public static void SavePaymentAttempt(string orderId, PaymentStatus status)
            {
                // .. save it to your favorite database
            }
        }
    }
}
