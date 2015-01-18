using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.StaticDomainEvents.Model
{
    public interface IDeliveryGuaranteeOffer
    {
        bool IsNotSatisfiedBy(DateTime timeOfOrderBeingPlaced, DateTime timeThatPizzaWasDelivered);
    }

    public class ThirtyMinuteDeliveryGuaranteeOffer : IDeliveryGuaranteeOffer
    {
        public bool IsNotSatisfiedBy(DateTime timeOfOrderBeingPlaced, DateTime timeThatPizzaWasDelivered)
        {
            return (timeThatPizzaWasDelivered - timeOfOrderBeingPlaced) > TimeSpan.FromMinutes(30);
        }
    }

}
