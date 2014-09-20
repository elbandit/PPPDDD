using System;

namespace Sales.Messages.events
{
    public class OrderCreated_V2 : OrderCreated
    {
        public string AddressId { get; set; }
    }
}
