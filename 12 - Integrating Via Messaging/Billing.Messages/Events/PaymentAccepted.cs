using System;

namespace Billing.Messages.Events
{
    public class PaymentAccepted
    {
        public string OrderId { get; set; }
    }
}
