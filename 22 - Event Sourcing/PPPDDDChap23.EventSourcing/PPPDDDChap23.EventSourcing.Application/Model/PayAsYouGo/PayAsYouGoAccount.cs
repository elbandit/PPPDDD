using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class PayAsYouGoAccount : EventSourcedAggregate
    {
        private FreeCallAllowance _freeCallAllowance;
        private Money _credit;
        private PayAsYouGoInclusiveMinutesOffer _inclusiveMinutesOffer = new PayAsYouGoInclusiveMinutesOffer(new Money(100000), new Minutes(0));

        public int InitialVersion { get; private set; }

        public PayAsYouGoAccount()
        { }

        public PayAsYouGoAccount(Guid id, Money credit)
        {
            Causes(new AccountCreated(id, credit));          
        }

        public PayAsYouGoAccount(PayAsYouGoAccountSnapshot snapshot)
        {
            Version = snapshot.Version;
            InitialVersion = snapshot.Version;
            _credit = new Money(snapshot.Credit);
        }

        public override void Apply(DomainEvent @event)
        {            
            When((dynamic)@event);  
            Version = Version ++;
        }

        public PayAsYouGoAccountSnapshot GetPayAsYouGoAccountSnapshot()
        {
            return new PayAsYouGoAccountSnapshot
            {
                Version = Version,
                Credit = _credit.Amount
            };
        }

        public void Record(PhoneCall phoneCall, PhoneCallCosting phoneCallCosting, IClock clock) 
        {
            var numberOfMinutesCoveredByAllowance = new Minutes();
      
            if (_freeCallAllowance != null)
                numberOfMinutesCoveredByAllowance = _freeCallAllowance.MinutesWhichCanCover(phoneCall, clock);

            var numberOfMinutesToChargeFor = phoneCall.Minutes.Subtract(numberOfMinutesCoveredByAllowance);

            var costOfCall = phoneCallCosting.DetermineCostOfCall(numberOfMinutesToChargeFor);

            Causes(new PhoneCallCharged(this.Id, phoneCall, costOfCall, numberOfMinutesCoveredByAllowance));
        }

        public void TopUp(Money credit, IClock clock)
        {
            if (_inclusiveMinutesOffer.IsSatisfiedBy(credit))
                Causes(new CreditSatisfiesFreeCallAllowanceOffer(this.Id, clock.Time(), _inclusiveMinutesOffer.FreeMinutes));            

            Causes(new CreditAdded(this.Id, credit));
        }

        public void AddInclusiveMinutesOffer(PayAsYouGoInclusiveMinutesOffer offer)
        {
            _inclusiveMinutesOffer = offer;
        }

        private void Causes(DomainEvent @event)
        {            
            Changes.Add(@event);
            Apply(@event);
        }

        private void When(CreditAdded creditAdded)
        {
            _credit = _credit.Add(creditAdded.Credit);
        }

        private void When(CreditSatisfiesFreeCallAllowanceOffer creditSatisfiesFreeCallAllowanceOffer)
        {
            _freeCallAllowance = new FreeCallAllowance(creditSatisfiesFreeCallAllowanceOffer.FreeMinutes, creditSatisfiesFreeCallAllowanceOffer.OfferSatisfied);
        }

        private void When(PhoneCallCharged phoneCallCharged)
        {
            _credit = _credit.Subtract(phoneCallCharged.CostOfCall);

            if (_freeCallAllowance != null)
                _freeCallAllowance.Subtract(phoneCallCharged.CoveredByAllowance);
        }

        private void When(AccountCreated accountCreated)
        {
            Id = accountCreated.Id;
            _credit = accountCreated.Credit;
        }
    }
}
