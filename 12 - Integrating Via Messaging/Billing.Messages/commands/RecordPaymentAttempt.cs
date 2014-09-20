using System;

namespace Billing.Messages.commands
{
    public class RecordPaymentAttempt
    {
        public string OrderId { get; set; }
        public PaymentStatus Status { get; set; }
    }

    public enum PaymentStatus
    {
        Accepted,
        Rejected
    }
}
