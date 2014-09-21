using System;

namespace Billing.Messages.Commands
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
