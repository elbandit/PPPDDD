using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.Domain
{
    public interface ICustomerBehaviourChecker
    {
        // did they place an order and not pay etc
        bool IsBlacklisted(int customerId);
    }
}
