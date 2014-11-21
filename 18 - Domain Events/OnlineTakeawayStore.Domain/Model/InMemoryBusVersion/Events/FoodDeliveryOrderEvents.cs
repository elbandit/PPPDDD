using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTakeawayStore.Domain.InMemoryBus;

namespace OnlineTakeawayStore.Domain.InMemoryBus.Events
{
    public class FoodDeliveryOrderCreated
    {
        public FoodDeliveryOrderCreated(FoodDeliveryOrder order)
        {
            this.Order = order;
        }

        public FoodDeliveryOrder Order { get; private set; }
    }

    public class FoodDeliveryOrderConfirmed
    {
        public FoodDeliveryOrderConfirmed(FoodDeliveryOrder order)
        {
            this.Order = order;
        }

        public FoodDeliveryOrder Order { get; private set; }
    }
}
