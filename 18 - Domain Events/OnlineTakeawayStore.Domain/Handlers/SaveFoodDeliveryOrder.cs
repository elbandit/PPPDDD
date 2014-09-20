using OnlineTakeawayStore.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.Domain.EventHandlers
{
    public class SaveFoodDeliveryOrder : IHandleEvents<FoodDeliveryOrderCreated>, IHandleEvents<FoodDeliveryOrderInvalidatedDueToBlacklistedCustomer>
    {
        // repository could be backed by any kind of storage technology
        private IFoodDeliveryOrderRepository repository;

        public SaveFoodDeliveryOrder(IFoodDeliveryOrderRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(FoodDeliveryOrderCreated @event)
        {
            repository.Save(@event.Order);
        }

        public void Handle(FoodDeliveryOrderInvalidatedDueToBlacklistedCustomer @event)
        {
            repository.Save(@event.Order);
        }
    }
}
