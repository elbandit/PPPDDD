using NServiceBus;
using OnlineTakeawayStore.NativeEvents.Application.Events;
using OnlineTakeawayStore.NServiceBus.Model;
using OnlineTakeawayStore.NServiceBus.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.NServiceBusApplication.Application
{
    public class ConfirmDeliveryOfOrder
    {
        private IOrderRepository orderRepository;

        public ConfirmDeliveryOfOrder(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public void Confirm(DateTime timeThatPizzaWasDelivered, Guid orderId)
        {
            var order = orderRepository.FindBy(orderId);
            order.ConfirmReceipt(timeThatPizzaWasDelivered);
        }
    }

    // Automatically found by NServiceBus (due to inheriting IHandleMessages<T>
    public class RefundOnDeliveryGuaranteeFailureHandler : IHandleMessages<DeliveryGuaranteeFailed>
    {
        // Injected by NServiceBus
        public IBus Bus { get; set; }

        public void Handle(DeliveryGuaranteeFailed message)
        {
            // handle internal event and publish external event to other bounded contexts
            Bus.Send(new RefundDueToLateDelivery() { OrderId = message.Order.Id });
        }
    }

}
