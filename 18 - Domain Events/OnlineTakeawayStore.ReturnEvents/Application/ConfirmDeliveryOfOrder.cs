using OnlineTakeawayStore.ReturnEvents.Infrastructure;
using OnlineTakeawayStore.ReturnEvents.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.ReturnEvents.Application
{
    public class ConfirmDeliveryOfOrder
    {
        private IOrderRepository orderRepository;
        private IEventDispatcher dispatcher;

        public ConfirmDeliveryOfOrder(IOrderRepository orderRepository, IEventDispatcher dispatcher)
        {
            this.orderRepository = orderRepository;
            this.dispatcher = dispatcher;
        }

        public void Confirm(DateTime timeThatPizzaWasDelivered, Guid orderId)
        {
            var order = orderRepository.FindBy(orderId);
            order.ConfirmReceipt(timeThatPizzaWasDelivered);

            foreach (var evnt in order.RecordedEvents)
            {
                dispatcher.Dispatch(evnt);
            }
        }
    }
}
