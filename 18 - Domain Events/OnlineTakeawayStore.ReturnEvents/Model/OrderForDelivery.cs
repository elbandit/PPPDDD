using OnlineTakeawayStore.ReturnEvents.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.ReturnEvents.Model
{
    public class OrderForDelivery
    {
        public Guid Id { get; private set; }
        private DateTime TimeOfOrderBeingPlaced { get; set; }
        private DateTime TimeThatPizzaWasDelivered { get; set; }
        private FoodDeliveryOrderSteps Status { get; set; }
        private IDeliveryGuaranteeOffer DeliveryGuaranteeOffer { get; set; }

        public OrderForDelivery(Guid id, Guid customerId, Guid restuarantId, List<int> menuItemIds, DateTime timeOfOrderBeingPlaced,
            IDeliveryGuaranteeOffer deliveryGuaranteeOffer)
        {
            Id = id;
            TimeOfOrderBeingPlaced = timeOfOrderBeingPlaced;
            Status = FoodDeliveryOrderSteps.Pending;
            RecordedEvents = new List<Object>();
            DeliveryGuaranteeOffer = deliveryGuaranteeOffer;
        }

        public List<Object> RecordedEvents { get; private set; }

        public void ConfirmReceipt(DateTime timeThatPizzaWasDelivered)
        {
            if (Status != FoodDeliveryOrderSteps.Delivered)
            {
                TimeThatPizzaWasDelivered = timeThatPizzaWasDelivered;
                Status = FoodDeliveryOrderSteps.Delivered;
                if (DeliveryGuaranteeOffer.IsNotSatisfiedBy(TimeOfOrderBeingPlaced, TimeThatPizzaWasDelivered))
                {
                     RecordedEvents.Add(new DeliveryGuaranteeFailed(this));
                }
            }
        }
    }
}
