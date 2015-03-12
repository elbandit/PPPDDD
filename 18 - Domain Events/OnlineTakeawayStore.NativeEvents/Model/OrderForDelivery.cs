using OnlineTakeawayStore.NativeEvents.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.NativeEvents.Model
{
    public class OrderForDelivery
    {
        public Guid Id { get; private set; }
        private DateTime TimeOfOrderBeingPlaced { get; set; }
        private DateTime TimeThatPizzaWasDelivered { get; set; }
        private FoodDeliveryOrderSteps Status { get; set; }
        private IDeliveryGuaranteeOffer DeliveryGuaranteeOffer { get; set; }

        public delegate void DeliveryGuaranteeFailedHandler(DeliveryGuaranteeFailed evnt);
        public event DeliveryGuaranteeFailedHandler DeliveryGuaranteeFailed;

        public OrderForDelivery(Guid id, Guid customerId, Guid restuarantId, List<int> menuItemIds, DateTime timeOfOrderBeingPlaced)
        {
            Id = id;
            TimeOfOrderBeingPlaced = timeOfOrderBeingPlaced;
            Status = FoodDeliveryOrderSteps.Pending;
        }

        public void ConfirmReceipt(DateTime timeThatPizzaWasDelivered)
        {
            if (Status != FoodDeliveryOrderSteps.Delivered)
            {
                TimeThatPizzaWasDelivered = timeThatPizzaWasDelivered;
                Status = FoodDeliveryOrderSteps.Delivered;
                if(DeliveryGuaranteeOffer.IsNotSatisfiedBy(TimeOfOrderBeingPlaced, TimeThatPizzaWasDelivered))
                {
                    if (DeliveryGuaranteeFailed !=  null)
                        DeliveryGuaranteeFailed(new DeliveryGuaranteeFailed(this));
                }
            }
        }
    }

 }
