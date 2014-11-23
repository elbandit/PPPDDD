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
        public FoodDeliveryOrder(int id, int customerId, int restuarantId, List<int> menuItemIds, DateTime deliveryTime)
        {
            this.Id = id;
            this.CustomerId = customerId;
            this.RestaurantId = restuarantId;
            this.MenuItemIds = menuItemIds;
            this.RequestedDeliveryTime = deliveryTime;

            Status = FoodDeliveryOrderSteps.Pending;

            DomainEvents.Raise(new FoodDeliveryOrderCreated(this));
        }

        public int Id { get; private set; }

        public FoodDeliveryOrderSteps Status { get; private set; }

        public int CustomerId { get; private set; }

        public int RestaurantId { get; private set; }

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
