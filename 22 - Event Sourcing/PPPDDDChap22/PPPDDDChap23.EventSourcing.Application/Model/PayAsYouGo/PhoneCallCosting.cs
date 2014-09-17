using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class PhoneCallCosting
    {
        private Money PricePerMinute { get; set; }

        public PhoneCallCosting()
        {
            PricePerMinute = new Money(0.30m); 
        }

        public Money DetermineCostOfCall(Minutes minutes)
        {
            return minutes.CostAt(PricePerMinute);
        }
    }
}
