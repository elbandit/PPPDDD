using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDeliveryOrderCreated = OnlineTakeawayStore.Domain.InMemoryBus.Events.FoodDeliveryOrderCreated;
using FoodDeliveryOrderConfirmed = OnlineTakeawayStore.Domain.InMemoryBus.Events.FoodDeliveryOrderConfirmed;

namespace OnlineTakeawayStore.Domain.InMemoryBus
{
    public class FoodDeliveryOrder
    {
        public FoodDeliveryOrder(int id, int customerId, int restuarantId, List<int> menuItemIds, 
            DateTime deliveryTime, IBus bus)
        {
            this.Id = id;
            this.CustomerId = customerId;
            this.RestaurantId = restuarantId;
            this.MenuItemIds = menuItemIds;
            this.RequestedDeliveryTime = deliveryTime;
            this.bus = bus;

            Status = FoodDeliveryOrderSteps.Pending;

            bus.InMemory.Raise(new FoodDeliveryOrderCreated(this));
        }

        private IBus bus;

        public int Id { get; private set; }

        public FoodDeliveryOrderSteps Status { get; private set; }

        public int CustomerId { get; private set; }

        public int RestaurantId { get; private set; }

        public List<int> MenuItemIds { get; private set; }

        public DateTime RequestedDeliveryTime { get; private set; }

        public void Confirm()
        {
            Status = FoodDeliveryOrderSteps.Confirmed;

            bus.InMemory.Raise(new FoodDeliveryOrderConfirmed(this));
        }

        public void Invalidate()
        {
            Status = FoodDeliveryOrderSteps.Invalidated;
        }
    }
}
