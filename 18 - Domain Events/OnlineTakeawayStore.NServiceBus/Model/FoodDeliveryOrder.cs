using NServiceBus;
using OnlineTakeawayStore.NServiceBus.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.NServiceBus.Model
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

    // also from the Ubiquitous Language
    public enum FoodDeliveryOrderSteps
    {
        Pending,
        Validated,
        Confirmed,
        Cooked,
        Despatched,
        Rejected,
        Invalidated
    }
}
