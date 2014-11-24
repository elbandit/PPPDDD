using OnlineTakeawayStore.StaticDomainEvents.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.StaticDomainEvents.Model.Events
{
    // ------------ domain events - present in Ubiquitous Language ------------//
    
    public class FoodDeliveryOrderCreated
    {
        public FoodDeliveryOrderCreated(FoodDeliveryOrder order)
        {
            this.Order = order;
        }

        public FoodDeliveryOrder Order { get; private set; }
    }

    public class FoodDeliveryOrderValidated
    {
        public FoodDeliveryOrderValidated(FoodDeliveryOrder order)
        {
            this.Order = order;
        }

        public FoodDeliveryOrder Order { get; private set; }
    }

    public class FoodDeliveryOrderRejectedDueToBlacklistedCustomer
    {
        public FoodDeliveryOrderRejectedDueToBlacklistedCustomer(FoodDeliveryOrder order)
        {
            this.Order = order;
        }

        public FoodDeliveryOrder Order { get; private set; }
    }

    // events are immutable
    public class FoodDeliveryOrderConfirmed
    {
        public FoodDeliveryOrderConfirmed(FoodDeliveryOrder order)
        {
            this.Order = order;
        }

        public FoodDeliveryOrder Order { get; private set; }
    }

    public class FoodDeliveryOrderCooked
    {
        public FoodDeliveryOrderCooked(FoodDeliveryOrder order)
        {
            this.Order = order;
        }

        public FoodDeliveryOrder Order { get; private set; }
    }

    public class FoodDeliveryOrderDespatched
    {
        public FoodDeliveryOrderDespatched(FoodDeliveryOrder order)
        {
            this.Order = order;
        }

        public FoodDeliveryOrder Order { get; private set; }
    }
}
