using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.StaticDomainEvents.Model
{
    public interface IFoodDeliveryOrderRepository
    {
        void Save(FoodDeliveryOrder order);
    }
}
