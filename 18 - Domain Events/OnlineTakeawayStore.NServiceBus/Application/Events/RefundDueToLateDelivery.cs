using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.NativeEvents.Application.Events
{
    public class RefundDueToLateDelivery
    {
        public Guid OrderId { get; set; }
    }
}
