using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTakeawayStore.StaticDomainEvents.Model.EventHandlers;
using OnlineTakeawayStore.StaticDomainEvents.Model.Events;
using OnlineTakeawayStore.Domain;
using OnlineTakeawayStore.StaticDomainEvents.Infrastructure;
using OnlineTakeawayStore.StaticDomainEvents.Model;

namespace OnlineTakeawayStore.Application
{
    public class FoodDeliveryOrderService
    {
        private static int Id = 0;

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
            
            // This has to be registered in-line to keep the "state" in scope
            // The state is the client channel tha keeps a connection to the specific customer making this order request
            // Alternative is to use threadstatic to clear callbacks
            Action<FoodDeliveryOrderCreated> publishOrderAcknowledgement = e =>
            {
                if (e.Order.Id == id) // filter out events for other customers' orders
                    clientChannel.Publish("ORDER_ACKNOWLEDGED");
            };
            DomainEvents.Register<FoodDeliveryOrderCreated>(publishOrderAcknowledgement);

            Action<FoodDeliveryOrderInvalidatedDueToBlacklistedCustomer> publishBlacklistNotification = e =>
            {
                if (e.Order.Id == id)
                    clientChannel.Publish("ORDER_INVALIDATED_BLACKLISTED_CUSTOMER");
            };
            DomainEvents.Register<FoodDeliveryOrderInvalidatedDueToBlacklistedCustomer>(publishBlacklistNotification);

            var order = new FoodDeliveryOrder(id, request.CustomerId, request.RestaurantId, request.MenuItemIds, request.DeliveryTime);

            // Unregister the callback to prevent the list of handlers constantly increasing
            // drawback to using this approach is to remember to do this
            DomainEvents.UnRegister(publishOrderAcknowledgement);
            DomainEvents.UnRegister(publishBlacklistNotification);
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

