using NServiceBus;
using OnlineTakeawayStore.NativeEvents.Application.Events;
using OnlineTakeawayStore.NativeEvents.Model;
using OnlineTakeawayStore.NativeEvents.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.NativeEvents.Application
{
    public class ConfirmDeliveryOfOrder
    {
        private IOrderRepository orderRepository;
        private IBus bus;

        public ConfirmDeliveryOfOrder(IOrderRepository orderRepository, IBus bus)
        {
            this.orderRepository = orderRepository;
            this.bus = bus;
        }

        public void Confirm(DateTime timeThatPizzaWasDelivered, Guid orderId)
        {
            var order = orderRepository.FindBy(orderId);
            order.DeliveryGuaranteeFailed += onDeliveryGuaranteeFailed;
            order.ConfirmReceipt(timeThatPizzaWasDelivered);
        }

        private void onDeliveryGuaranteeFailed(DeliveryGuaranteeFailed evnt)
        {
            // handle internal event and publish external event to other bounded contexts
            bus.Send(new RefundDueToLateDelivery() { OrderId = evnt.Order.Id });
        }
    }
}
