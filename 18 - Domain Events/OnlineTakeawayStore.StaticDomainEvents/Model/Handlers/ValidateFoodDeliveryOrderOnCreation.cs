using OnlineTakeawayStore.StaticDomainEvents.Infrastructure;
using OnlineTakeawayStore.StaticDomainEvents.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.StaticDomainEvents.Model.EventHandlers
{
    public class ValidateFoodDeliveryOrderOnCreation : IHandleEvents<FoodDeliveryOrderCreated>
    {
        private ICustomerBehaviorChecker checker;

        public ValidateFoodDeliveryOrderOnCreation(ICustomerBehaviorChecker checker)
        {
            this.checker = checker;
        }

        public void Handle(FoodDeliveryOrderCreated @event)
        {
            if (checker.IsBlacklisted(@event.Order.CustomerId))
            {
                @event.Order.Reject();
                DomainEvents.Raise(new FoodDeliveryOrderRejectedDueToBlacklistedCustomer(@event.Order));
            }
            else
                DomainEvents.Raise(new FoodDeliveryOrderValidated(@event.Order));

        }
    }
}
