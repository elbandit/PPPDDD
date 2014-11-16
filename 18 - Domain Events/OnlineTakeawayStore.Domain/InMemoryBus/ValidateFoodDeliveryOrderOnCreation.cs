using NServiceBus;
using OnlineTakeawayStore.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.Domain.InMemoryBus
{
    public class ValidateFoodDeliveryOrderOnCreation : IHandleMessages<FoodDeliveryOrderCreated>
    {
        private ICustomerBehaviourChecker checker;

        public ValidateFoodDeliveryOrderOnCreation(ICustomerBehaviourChecker checker)
        {
            this.checker = checker;
        }

        public void Handle(OnlineTakeawayStore.Domain.Events.FoodDeliveryOrderCreated @event)
        {
            if (checker.IsBlacklisted(@event.Order.CustomerId))
            {
                @event.Order.Invalidate();
            }
        }
    }
}
