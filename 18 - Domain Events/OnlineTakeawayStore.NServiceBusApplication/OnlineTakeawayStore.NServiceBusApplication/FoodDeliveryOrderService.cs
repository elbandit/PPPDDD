using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDeliveryOrder = OnlineTakeawayStore.Domain.InMemoryBus.FoodDeliveryOrder;
using OnlineTakeawayStore.Domain.InMemoryBus;
using NServiceBus;

namespace OnlineTakeawayStore.NServiceBusApplication
{
    public class FoodDeliveryOrderService 
    {
        public FoodDeliveryOrderService(IBus bus)
        {
            this.Bus = bus;
        }
        
        private IBus Bus { get; set; }

        private static int Id = 0;

        public void PlaceFoodDeliveryOrder(PlaceFoodDeliveryOrderRequest request)
        {
            var order = new FoodDeliveryOrder(
                Id++, request.CustomerId, request.RestaurantId, request.MenuItemIds,
                request.DeliveryTime, Bus
            );
        }
    }

    public class PlaceFoodDeliveryOrderRequest
    {
        public int CustomerId { get; set; }

        public int RestaurantId { get; set; }

        public List<int> MenuItemIds { get; set; }

        public DateTime DeliveryTime { get; set; }
    }

    public class MenuItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
    }

}
