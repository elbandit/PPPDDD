using System;

namespace Sales.Messages.Events
{
    public class OrderCreated_V2 : OrderCreated
    {
        public string AddressId { get; set; }
    }
}
