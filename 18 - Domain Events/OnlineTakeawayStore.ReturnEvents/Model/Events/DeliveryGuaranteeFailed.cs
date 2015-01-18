using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.ReturnEvents.Model.Events
{
    public class DeliveryGuaranteeFailed
    {
        public DeliveryGuaranteeFailed(OrderForDelivery order)
        {
            Order = order;
        }

        public OrderForDelivery Order { get; private set; }
    }
}
