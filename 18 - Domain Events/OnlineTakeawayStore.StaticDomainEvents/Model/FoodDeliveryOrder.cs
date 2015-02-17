using OnlineTakeawayStore.StaticDomainEvents.Model.Events;
using OnlineTakeawayStore.StaticDomainEvents.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.StaticDomainEvents.Model
{
    public class FoodDeliveryOrder
    {
        public FoodDeliveryOrder(Guid id, Guid customerId, Guid restuarantId, List<int> menuItemIds, DateTime deliveryTime)
        {
            this.Id = id;
            this.CustomerId = customerId;
            this.RestaurantId = restuarantId;
            this.MenuItemIds = menuItemIds;
            this.RequestedDeliveryTime = deliveryTime;

            Status = FoodDeliveryOrderSteps.Pending;

            DomainEvents.Raise(new FoodDeliveryOrderCreated(this));
        }

        public Guid Id { get; private set; }

        public FoodDeliveryOrderSteps Status { get; private set; }

        public Guid CustomerId { get; private set; }

        public Guid RestaurantId { get; private set; }

        public List<int> MenuItemIds { get; private set; }

        public DateTime RequestedDeliveryTime { get; private set; }

        public void Confirm()
        {
            Status = FoodDeliveryOrderSteps.Confirmed;

            DomainEvents.Raise(new FoodDeliveryOrderConfirmed(this));
        }

        public void Reject()
        {
            Status = FoodDeliveryOrderSteps.Rejected;
        }

        public void CompleteCurrentStep()
        {
            var nextStep = GetNextStep();
            var ev = GetEventFor(nextStep);
            DomainEvents.Raise(ev);
        }

        private FoodDeliveryOrderSteps GetNextStep()
        {
            switch (Status)
            {
                case FoodDeliveryOrderSteps.Confirmed:
                    return FoodDeliveryOrderSteps.Cooked;

                case FoodDeliveryOrderSteps.Cooked:
                    return FoodDeliveryOrderSteps.Despatched;

                default:
                    throw new UnexpectedFoodDeliveryStep();
            }

        }

        private object GetEventFor(FoodDeliveryOrderSteps step)
        {
            switch (step)
            {
                case FoodDeliveryOrderSteps.Confirmed:
                    return new FoodDeliveryOrderConfirmed(this);

                case FoodDeliveryOrderSteps.Cooked:
                    return new FoodDeliveryOrderCooked(this);

                case FoodDeliveryOrderSteps.Despatched:
                    return new FoodDeliveryOrderDespatched(this);

                default:
                    throw new UnexpectedFoodDeliveryStep();
            }
        }
    }

    // also from the Ubiquitous Language
    public enum FoodDeliveryOrderSteps
    {
        Pending,
        Validated,
        Confirmed,
        Cooked,
        Despatched,
        Rejected,
    }

    public class UnexpectedFoodDeliveryStep : Exception { }
}



namespace OnlineTakeawayStore.Model
{
    public class OrderForDelivery
    {
        private DateTime TimeOfOrderBeingPlaced { get; set; }
        private DateTime TimeThatPizzaWasDelivered { get; set; }

        public OrderForDelivery(Guid id, Guid customerId, Guid restuarantId, List<int> menuItemIds, DateTime timeOfOrderBeingPlaced)
        {
            // ....
            TimeOfOrderBeingPlaced = timeOfOrderBeingPlaced;
            Status = FoodDeliveryOrderSteps.Pending;
        }

        public void ConfirmReceipt(DateTime timeThatPizzaWasDelivered)
        {
            if (Status != FoodDeliveryOrderSteps.Delivered)
            {
                TimeThatPizzaWasDelivered = timeThatPizzaWasDelivered;
                Status = FoodDeliveryOrderSteps.Delivered;

                if(DeliveryGuaranteeOffer.IsNotSatisifiedBy(timeOfOrderBeingPlaced, timeThatPizzaWasDelivered);
                {
                    DomainEvents.Raise(new DeliveryGuaranteeFailed(this));
                }
            }
        }
    }
}

namespace OnlineTakeawayStore.ApplicationServiceLayer
{
    public class ConfirmDeliveryOfOrder
    {
        //.....

        public void Confirm(DateTime timeThatPizzaWasDelivered, int orderId)
        {
            var order = _orderRepository.FindBy(orderId);

            using (DomainEvents.Register(onDeliveryGuaranteeFailed()))
            {
                order.ConfirmReceipt(timeThatPizzaWasDelivered);
            }
        }

        private Action<DeliveryGuaranteeFailed> onDeliveryGuaranteeFailed()
        {

            return (DeliveryGuaranteeFailed e) => _bus.Send(new RefundDueToLateDelivery() { RentalRequestId = e.OrderId });
        }
    }


    public class 
}
