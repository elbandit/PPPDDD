using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class PhoneCallCharged : DomainEvent
    {
        public PhoneCallCharged(Guid aggregateId, PhoneCall phoneCall, Money costOfCall, Minutes coveredByAllowance) : base (aggregateId)
        {
            PhoneCall = phoneCall;
            CostOfCall = costOfCall;
            CoveredByAllowance = coveredByAllowance;
        }

        public PhoneCall PhoneCall { get; private set; }

        public Money CostOfCall { get; private set; }

        public Minutes CoveredByAllowance { get; private set; }
    }
}
