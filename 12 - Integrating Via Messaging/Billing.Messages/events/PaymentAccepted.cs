using System;

namespace Billing.Messages.events
{
    public class PaymentAccepted
    {
        public string OrderId { get; set; }
    }
}
