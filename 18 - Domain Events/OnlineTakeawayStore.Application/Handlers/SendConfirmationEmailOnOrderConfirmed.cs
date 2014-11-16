using OnlineTakeawayStore.Domain;
using OnlineTakeawayStore.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.Application.Handlers
{
    public class SendConfirmationEmailOnOrderConfirmed : IHandleEvents<FoodDeliveryOrderConfirmed>
    {
        private IEmailer emailer;

        public SendConfirmationEmailOnOrderConfirmed(IEmailer emailer)
        {
            this.emailer = emailer;
        }

        public void Handle(FoodDeliveryOrderConfirmed @event)
        {
            emailer.SendFoodDeliveryOrderConfirmation(@event.Order.CustomerId);
        }
    }
}
