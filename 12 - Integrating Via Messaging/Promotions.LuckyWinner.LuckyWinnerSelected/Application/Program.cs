using MassTransit;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using MassTransit.Context;
using System.Text;

namespace Promotions.LuckyWinner.LuckyWinnerSelected
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Bus.Initialize(config =>
            {
                config.UseMsmq();
                // look on this queue for order placed event messages
                config.ReceiveFrom("msmq://localhost/promotions.ordercreated");
                // subscribe to order placed events
                config.Subscribe(sub =>
                {
                    // handle order placed events like this
                    sub.Handler<OrderCreated>(msg => new OrderCreatedHandler().Handle(msg));
                });

            });

           // keep the application running so it can listen for messages
            while(true)
            {
                Thread.Sleep(1000);
            }
        }
    }

    public class OrderCreated
    {
        public string OrderId { get; set; }

        public string UserId { get; set; }

        public List<string> ProductIds { get; set; }

        public string ShippingTypeId { get; set; }

        public DateTime TimeStamp { get; set; }

        public double Amount { get; set; }
    }
   
    public class OrderCreatedHandler
    {
        public void Handle(OrderCreated message)
        {
            Console.WriteLine(
                "Mass Transit handling order placed event: Order: {0} for User: {1}", 
                message.OrderId, message.UserId);
        }
    }

}
