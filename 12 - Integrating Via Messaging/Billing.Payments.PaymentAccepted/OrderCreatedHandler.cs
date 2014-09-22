using Billing.Messages.Commands;
using NServiceBus;
using Sales.Messages.Events;
using System;

namespace Billing.Payments.PaymentAccepted
{
    public class OrderCreatedHandler : IHandleMessages<OrderCreated>
    {
         // dependency injected by NServiceBus
         public IBus Bus { get; set; }

         public void Handle(OrderCreated message)
         {
             Console.WriteLine("Received order created event: OrderId: {0}", message.OrderId);
             var cardDetails = Database.GetCardDetailsFor(message.UserId);
             var confirmation = PaymentProvider.ChargeCreditCard(cardDetails, message.Amount);
             var command = new RecordPaymentAttempt
             {
                 OrderId = message.OrderId,
                 Status = confirmation.Status
             };
             Bus.SendLocal(command);
         }
    }

    public static class PaymentProvider
    {
        private static int Attempts = 0;

        public static PaymentConfirmation ChargeCreditCard(CardDetails details, double amount)
        {
            if (Attempts < 2)
            {
                Attempts++;
                throw new Exception("Service unavailable. Down for maintenance.");
            }
            return new PaymentConfirmation { Status = PaymentStatus.Accepted };
        }
    }

    public class PaymentConfirmation
    {
        public PaymentStatus Status { get; set; }
    }

    public static class Database
    {
        public static CardDetails GetCardDetailsFor(string userId)
        {
            return new CardDetails();
        }
    }

    public class CardDetails
    {
        // ...
    }
}
