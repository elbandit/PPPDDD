using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTakeawayStore.Domain.DispatcherVersion;
using OnlineTakeawayStore.ReturnEvents.Infrastructure;
using OnlineTakeawayStore.Domain.DispatcherVersion.Events;

namespace OnlineTakeawayStore.Application.DispatcherVersion
{
    public class FoodDeliveryOrderService
    {
        private static int Id = 0;

        // sends real-time notifications to browser and restaurant
        private INotificationChannel clientChannel;
        private IRestaurantConnector connector;
        private IEventDispatcher dispatcher;

        public FoodDeliveryOrderService(INotificationChannel clientChannel, IRestaurantConnector connector,
            IEventDispatcher dispatcher)
        {
            this.clientChannel = clientChannel;
            this.connector = connector;
            this.dispatcher = dispatcher;
        }

        public void PlaceFoodDeliveryOrder(PlaceFoodDeliveryOrderRequest request)
        {
            var id = Id++; // for demonstration purposes only

            dispatcher.Register<FoodDeliveryOrderCreated>(e =>
            {
                clientChannel.Publish("ORDER_ACKNOWLEDGED_ " + e.Order.Id);
            });

            var order = new FoodDeliveryOrder(
                id, request.CustomerId, request.RestaurantId, request.MenuItemIds,
                request.DeliveryTime
            );

            foreach (var ev in order.RecordedEvents)
            {
                dispatcher.Dispatch(ev);
            }
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
