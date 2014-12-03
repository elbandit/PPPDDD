using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.NativeEvents.Model
{
    public interface IDeliveryGuaranteeOffer
    {
        bool IsNotSatisfiedBy(DateTime timeOfOrderBeingPlaced, DateTime timeThatPizzaWasDelivered);
    }
}
