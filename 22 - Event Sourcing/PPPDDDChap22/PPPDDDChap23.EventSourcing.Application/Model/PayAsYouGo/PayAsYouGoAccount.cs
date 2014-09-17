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
        private PayAsYouGoInclusiveMinutesOffer _InclusiveMinutesOffer = new PayAsYouGoInclusiveMinutesOffer();

        public PayAsYouGoAccount()
        { }

        public PayAsYouGoAccount(Guid id, Money credit)
        {
            Causes(new AccountCreated(id, credit));          
        }

        public PayAsYouGoAccount(PayAsYouGoAccountSnapshot snapShot)
        {
            // Restore all state
            Version = snapShot.Version;
        }

        public override void Apply(DomainEvent @event)
        {            
            When((dynamic)@event);  
            Version = Version ++;
        }

        public PayAsYouGoAccountSnapshot GetPayAsYouGoAccountSnapShot()
        { 
            var snapshot = new PayAsYouGoAccountSnapshot();

            // Save all state

            snapshot.Version = Version;

            return snapshot;
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
            if (_InclusiveMinutesOffer.IsSatisfiedBy(credit))
                Causes(new CreditSatisfiesFreeCallAllowanceOffer(this.Id, clock.Time(), _InclusiveMinutesOffer.FreeMinutes));            

            Causes(new CreditAdded(this.Id, credit));
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
