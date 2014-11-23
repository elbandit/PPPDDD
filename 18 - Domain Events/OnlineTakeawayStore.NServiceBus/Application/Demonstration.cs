using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.NServiceBus.Application
{
    public class Demonstration : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            var service = new FoodDeliveryOrderService(Bus);
            var request = new PlaceFoodDeliveryOrderRequest
            {
                CustomerId = 8989,
                RestaurantId = 333,
                DeliveryTime = DateTime.Now.AddHours(1),
                MenuItemIds = new List<int> { 1, 2, 3}
            };
            service.PlaceFoodDeliveryOrder(request);
        }

        public void Stop()
        {
        }
    }
}
