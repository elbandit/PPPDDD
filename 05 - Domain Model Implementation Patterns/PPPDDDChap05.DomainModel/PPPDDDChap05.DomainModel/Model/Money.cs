using System;
using System.Collections.Generic;

namespace PPPDDDChap05.DomainModel.Model
{
    public class Money 
    {
        protected decimal Value { get; set; }

        public Money()
            : this(0m)
        {
        }

        public Money(decimal value)
        {
            ThrowExceptionIfNotValid(value);

            Value = value;
        }

        private void ThrowExceptionIfNotValid(decimal value)
        {
            if (value % 0.01m != 0)
                throw new InvalidOperationException("There cannot be more than two decimal places."); 

            if(value < 0)
                throw new InvalidOperationException("Money cannot be a negative value.");
        }

        public Money Add(Money money)
        {
            return new Money(Value + money.Value);
        }

        public bool IsGreaterThan(Money money)
        {
            return this.Value > money.Value;
        }

        public bool IsGreaterThanOrEqualTo(Money money)
        {
            return this.Value > money.Value || this.Equals(money);
        }

        public bool IsLessThanOrEqualTo(Money money)
        {
            return this.Value < money.Value || this.Equals(money);
        }

        public override string ToString()
        {
            return string.Format("{0}", Value);
        }
    }
}
