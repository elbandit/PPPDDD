using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTakeawayStore.Domain.EventHandlers;
using OnlineTakeawayStore.Domain.Events;
using OnlineTakeawayStore.Domain;
using FoodDeliveryOrder = OnlineTakeawayStore.Domain.EventsKeyword.FoodDeliveryOrder;
using OnlineTakeawayStore.Domain.EventsKeyword;

namespace OnlineTakeawayStore.Application.EventsKeywordVersion
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
    
 }

