 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTakeawayStore.Domain;
using FoodDeliveryOrder = OnlineTakeawayStore.Domain.EventsKeyword.FoodDeliveryOrder;
using OnlineTakeawayStore.Domain.EventsKeyword;

namespace OnlineTakeawayStore.Application.NativeEvents
{
    public class FoodDeliveryOrderService
    {
        private static int Id = 0;

        // sends real-time notifications to browser and restaurant
        private INotificationChannel clientChannel;
        private IRestaurantConnector connector;

        public FoodDeliveryOrderService(INotificationChannel clientChannel, IRestaurantConnector connector)
        {
            this.clientChannel = clientChannel;
            this.connector = connector;
        }

        public void PlaceFoodDeliveryOrder(PlaceFoodDeliveryOrderRequest request)
        {
            var id = Id++; // for demonstration purposes only

            FoodDeliveryOrderCreatedHandler onCreationHandler = (FoodDeliveryOrder o) => 
            {
                clientChannel.Publish("ORDER_ACKNOWLEDGED");
            };
            
            var order = new FoodDeliveryOrder(
                id, request.CustomerId, request.RestaurantId, request.MenuItemIds, 
                request.DeliveryTime, onCreationHandler
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

