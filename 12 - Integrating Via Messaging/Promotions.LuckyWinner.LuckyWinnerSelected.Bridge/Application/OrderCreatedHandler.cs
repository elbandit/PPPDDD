using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using System.Messaging;
using Sales.Messages.Events;
using System.Xml.Linq;
using System.Xml;

namespace Promotions.LuckyWinner.LuckyWinnerSelected.Bridge
{
    public class OrderCreatedHandler : IHandleMessages<OrderCreated_V2>
    {

        public void Handle(OrderCreated_V2 message)
        {
            Console.WriteLine(
                "Bridge received order: {0}. " +
                "About to push it onto Mass Transit's queue",
                message.OrderId
            );
            var massMsg = ConvertToMassTransitXmlMessageFormat(message);
            var msmqMsg = new Message 
            {
                Body = XDocument.Parse(massMsg).Root,
                Extension = Encoding.UTF8.GetBytes("{\"Content-Type\":\"application/vnd.masstransit+xml\"}")
            };
            var queue = new MessageQueue(".\\Private$\\promotions.ordercreated", QueueAccessMode.Send);
            queue.Send(msmqMsg);
        }

        // use a more robust strategy in production
        // this approach is used to highligh format mass transit needs
        private string ConvertToMassTransitXmlMessageFormat(OrderCreated_V2 message)
        {
            return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
             "<envelope>" +
                "<headers />" +
                "<destinationAddress>msmq://localhost/promotions.ordercreated?tx=false&amp;recoverable=true</destinationAddress>" +
                 "<message>" +
                     "<orderId>" + message.OrderId + "</orderId>" +
                     "<userId>" + message.UserId + "</userId>" +
                     GenerateProductIdsXml(message.ProductIds) +
                     "<shippingTypeId>" + message.ShippingTypeId + "</shippingTypeId>" +
                     "<amount>" + message.Amount + "</amount>" +
                     "<timestamp>" + XmlConvert.ToString(message.TimeStamp, XmlDateTimeSerializationMode.Utc) + "</timestamp>" +
                 "</message>" +
                 "<messageType>urn:message:Promotions.LuckyWinner.LuckyWinnerSelected:OrderCreated</messageType>" +
              "</envelope>";
        }

         
        private string GenerateProductIdsXml(IEnumerable<string> productIds)
        {
            return String.Join("", productIds.Select(p => "<productIds>" + p + "</productIds>"));
        }
    }
}
