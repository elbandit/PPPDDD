using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class Minutes
    {
        public int Number { get; private set; }

        public Minutes() : this(0) { }

        public Minutes(int number)
        {
            // Can't be nagative
            Number = number;
        }

        public Minutes Subtract(Minutes minutes)
        {            
            return new Minutes(Number - minutes.Number);
        }

        public Money CostAt(Money chargePerMinute)
        {
            return chargePerMinute.MultiplyBy(Number);
        }

        public bool IsGreaterOrEqualTo(Minutes minutes)
        {
            return this.Number >= minutes.Number;
        }
    }
}
