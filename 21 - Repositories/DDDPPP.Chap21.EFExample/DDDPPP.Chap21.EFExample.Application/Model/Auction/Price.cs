using System;
using System.Collections.Generic;
using System.Linq;
using DDDPPP.Chap21.EFExample.Application.Infrastructure;

namespace DDDPPP.Chap21.EFExample.Application.Model.Auction
{
    public class Price : ValueObject<Price>
    {
        private Price()
        { }

        public Price(Money amount)
        {
            if (amount == null)
                throw new ArgumentNullException("Amount cannot be null");

            Amount = amount;
        }

        public Money Amount { get; private set; }

        public Money BidIncrement()
        {
            if (Amount.IsGreaterThanOrEqualTo(new Money(0.01m)) && Amount.IsLessThanOrEqualTo(new Money(0.99m)))
                return Amount.Add(new Money(0.05m));

            if (Amount.IsGreaterThanOrEqualTo(new Money(1.00m)) && Amount.IsLessThanOrEqualTo(new Money(4.99m)))
                return Amount.Add(new Money(0.20m));

            if (Amount.IsGreaterThanOrEqualTo(new Money(5.00m)) && Amount.IsLessThanOrEqualTo(new Money(14.99m)))
                return Amount.Add(new Money(0.50m));

            return Amount.Add(new Money(1.00m));

        }

        public bool CanBeExceededBy(Money offer)
        {
            return offer.IsGreaterThanOrEqualTo(BidIncrement());
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object>() { Amount };
        }
    }
}
