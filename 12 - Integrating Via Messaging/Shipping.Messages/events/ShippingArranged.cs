using System;

namespace Shipping.Messages.events
{
    public class ShippingArranged  
    {
        public string OrderId { get; set; }

        /*
         * Other fields, such as date/date range 
         * could be added here depending on your 
         * shipping provider(s) API
         */
    }
}
