using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTakeawayStore.Domain.DispatcherVersion.Events;

namespace OnlineTakeawayStore.Domain.DispatcherVersion
{
    public class FoodDeliveryOrder
    {
        public FoodDeliveryOrder(Guid id, Guid customerId, Guid restuarantId, List<int> menuItemIds,
            DateTime deliveryTime)
        {
            this.Id = id;
            this.CustomerId = customerId;
            this.RestaurantId = restuarantId;
            this.MenuItemIds = menuItemIds;
            this.RequestedDeliveryTime = deliveryTime;
            this.RecordedEvents = new List<Object>();
            Status = FoodDeliveryOrderSteps.Pending;

            RecordedEvents.Add(new FoodDeliveryOrderCreated(this));
        }

        public Guid Id { get; private set; }

        public FoodDeliveryOrderSteps Status { get; private set; }

        public Guid CustomerId { get; private set; }

        public Guid RestaurantId { get; private set; }

        public List<int> MenuItemIds { get; private set; }

        public DateTime RequestedDeliveryTime { get; private set; }

        public List<Object> RecordedEvents { get; private set; }

        public void Confirm()
        {
            Status = FoodDeliveryOrderSteps.Confirmed;

            RecordedEvents.Add(new FoodDeliveryOrderConfirmed(this));
        }

        public void Reject()
        {
            Status = FoodDeliveryOrderSteps.Rejected;
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
        Rejected
    }
}
