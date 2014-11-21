using NServiceBus;
using OnlineTakeawayStore.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTakeawayStore.Domain.InMemoryBus.Events;
using FoodDeliveryOrderCreated = OnlineTakeawayStore.Domain.InMemoryBus.Events.FoodDeliveryOrderCreated;

namespace OnlineTakeawayStore.Domain.InMemoryBus
{
    public class ValidateFoodDeliveryOrderOnCreation : IHandleMessages<FoodDeliveryOrderCreated>
    {
        private ICustomerBehaviorChecker checker;

        public ValidateFoodDeliveryOrderOnCreation(ICustomerBehaviorChecker checker)
        {
            this.checker = checker;
        }

        public void Handle(FoodDeliveryOrderCreated @event)
        {
            // Appears in NServiceBus confirmation it is working
            Console.WriteLine("Validating food delivery order: " + @event.Order.Id);

            if (checker.IsBlacklisted(@event.Order.CustomerId))
            {
                @event.Order.Invalidate();
            }
        }
    }
}
