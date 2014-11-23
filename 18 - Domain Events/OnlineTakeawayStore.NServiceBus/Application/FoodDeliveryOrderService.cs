using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using OnlineTakeawayStore.NServiceBus.Model;

namespace OnlineTakeawayStore.NServiceBus.Application
{
    public class FoodDeliveryOrderService 
    {
        public FoodDeliveryOrderService(IBus bus)
        {
            this.Bus = bus;
        }
        
        private IBus Bus { get; set; }

        public void PlaceFoodDeliveryOrder(PlaceFoodDeliveryOrderRequest request)
        {
            var order = new FoodDeliveryOrder(
                Guid.NewGuid(), request.CustomerId, request.RestaurantId, request.MenuItemIds,
                request.DeliveryTime, Bus
            );
        }
    }

    public class PlaceFoodDeliveryOrderRequest
    {
        public Guid CustomerId { get; set; }

        public Guid RestaurantId { get; set; }

        public List<int> MenuItemIds { get; set; }

        public DateTime DeliveryTime { get; set; }
    }

    public class MenuItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
    }

}
