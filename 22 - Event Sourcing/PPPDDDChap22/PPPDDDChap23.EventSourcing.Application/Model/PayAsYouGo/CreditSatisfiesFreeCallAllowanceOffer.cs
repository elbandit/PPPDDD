using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class CreditSatisfiesFreeCallAllowanceOffer : DomainEvent
    {
        public CreditSatisfiesFreeCallAllowanceOffer(Guid aggregateId, DateTime offerSatisfied, Minutes freeMinutes)
            : base(aggregateId) 
        {
            OfferSatisfied = offerSatisfied;
            FreeMinutes = freeMinutes;
        }

        public DateTime OfferSatisfied { get; private set; }
        public Minutes FreeMinutes { get; private set; }
    }
}
