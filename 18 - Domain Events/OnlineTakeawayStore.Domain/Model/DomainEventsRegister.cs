using OnlineTakeawayStore.Domain.EventHandlers;
using OnlineTakeawayStore.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.Domain
{
    public static class DomainHandlersRegister
    {
        // Just one possible way to do this. A  number of alternative approaches discussed in the chapter
        public static void WireUpDomainEventHandlers(IFoodDeliveryOrderRepository repository, ICustomerBehaviorChecker checker)
        {
            DomainEvents.Register<FoodDeliveryOrderCreated>(new ValidateFoodDeliveryOrderOnCreation(checker));
            DomainEvents.Register<FoodDeliveryOrderInvalidatedDueToBlacklistedCustomer>(new SaveFoodDeliveryOrder(repository));
        }
    }
}
