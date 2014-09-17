using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class PayAsYouGoInclusiveMinutesOffer
    {
        private Money spendThreshold;

        public PayAsYouGoInclusiveMinutesOffer()
        {
            spendThreshold = new Money(10m);
            FreeMinutes = new Minutes(90);
        }

        public bool IsSatisfiedBy(Money credit)
        {
            return credit.IsGreaterThanOrEqualTo(spendThreshold);
        }

        public Minutes FreeMinutes { get; private set; }
    }
}
