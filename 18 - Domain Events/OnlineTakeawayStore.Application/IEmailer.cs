using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.Application
{
    public interface IEmailer
    {
        void SendFoodDeliveryOrderConfirmation(int customerId);

        void NotifyBlacklistedCustomerRejection(int customerId);
    }
}
