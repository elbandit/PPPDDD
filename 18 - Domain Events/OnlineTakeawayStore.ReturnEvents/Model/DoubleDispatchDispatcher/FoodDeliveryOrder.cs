using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTakeawayStore.Domain.DispatcherVersion.Events;

namespace OnlineTakeawayStore.Domain.DispatcherVersion.DoubleDispatchVersion
{
    public class FoodDeliveryOrder
    {
        public FoodDeliveryOrder(int id, int customerId, int restuarantId, List<int> menuItemIds,
            DateTime deliveryTime, IEventDispatcher dispatcher)
        {
            this.Id = id;
            this.CustomerId = customerId;
            this.RestaurantId = restuarantId;
            this.MenuItemIds = menuItemIds;
            this.RequestedDeliveryTime = deliveryTime;
            this.RecordedEvents = new List<Object>();
            Status = FoodDeliveryOrderSteps.Pending;

            dispatcher.RecordEvent(new FoodDeliveryOrderCreated(this));
        }

        public int Id { get; private set; }

        public FoodDeliveryOrderSteps Status { get; private set; }

        public int CustomerId { get; private set; }

        public int RestaurantId { get; private set; }

        public List<int> MenuItemIds { get; private set; }

        public DateTime RequestedDeliveryTime { get; private set; }

        public List<Object> RecordedEvents { get; private set; }

        public void Confirm(IEventDispatcher dispatcher)
        {
            Status = FoodDeliveryOrderSteps.Confirmed;

            dispatcher.RecordEvent(new FoodDeliveryOrderConfirmed(this));
        }

        public void Invalidate()
        {
            Status = FoodDeliveryOrderSteps.Invalidated;
        }
    }

    public interface IEventDispatcher
    {
        void RecordEvent(Object @event);
    }

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
