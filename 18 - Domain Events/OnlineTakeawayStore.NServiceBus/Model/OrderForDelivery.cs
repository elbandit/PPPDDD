using NServiceBus;
using OnlineTakeawayStore.NServiceBus.Model;
using OnlineTakeawayStore.NServiceBus.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.NServiceBus.Model
{
    public class OrderForDelivery
    {
        public Guid Id { get; private set; }
        private DateTime TimeOfOrderBeingPlaced { get; set; }
        private DateTime TimeThatPizzaWasDelivered { get; set; }
        private FoodDeliveryOrderSteps Status { get; set; }
        private IDeliveryGuaranteeOffer DeliveryGuaranteeOffer { get; set; }
        private IBus Bus { get; set; }

        public OrderForDelivery(Guid id, Guid customerId, Guid restuarantId, List<int> menuItemIds, DateTime timeOfOrderBeingPlaced, IBus bus)
        {
            Id = id;
            TimeOfOrderBeingPlaced = timeOfOrderBeingPlaced;
            Status = FoodDeliveryOrderSteps.Pending;
            Bus = bus;
        }

        public void ConfirmReceipt(DateTime timeThatPizzaWasDelivered)
        {
            if (Status != FoodDeliveryOrderSteps.Delivered)
            {
                TimeThatPizzaWasDelivered = timeThatPizzaWasDelivered;
                Status = FoodDeliveryOrderSteps.Delivered;
                if (DeliveryGuaranteeOffer.IsNotSatisfiedBy(TimeOfOrderBeingPlaced, TimeThatPizzaWasDelivered))
                {
                    Bus.InMemory.Raise(new DeliveryGuaranteeFailed(this));
                }
            }
        }
    }
}
