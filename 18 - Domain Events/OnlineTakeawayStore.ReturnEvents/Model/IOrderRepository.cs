using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.ReturnEvents.Model
{
    public interface IOrderRepository
    {
        OrderForDelivery FindBy(Guid id);
    }
}
