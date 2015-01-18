using NServiceBus;
using OnlineTakeawayStore.StaticDomainEvents.Application.Events;
using OnlineTakeawayStore.StaticDomainEvents.Infrastructure;
using OnlineTakeawayStore.StaticDomainEvents.Model;
using OnlineTakeawayStore.StaticDomainEvents.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.StaticDomainEvents.Application
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
            using(DomainEvents.Register<DeliveryGuaranteeFailed>(onDeliveryFailure))
            {
                var order = orderRepository.FindBy(orderId);
                order.ConfirmReceipt(timeThatPizzaWasDelivered);
            }
        }

        private void onDeliveryFailure(DeliveryGuaranteeFailed evnt)
        {
            // handle internal event and publish external event to other bounded contexts
            bus.Send(new RefundDueToLateDelivery() { OrderId = evnt.Order.Id });
        }
    }
}
