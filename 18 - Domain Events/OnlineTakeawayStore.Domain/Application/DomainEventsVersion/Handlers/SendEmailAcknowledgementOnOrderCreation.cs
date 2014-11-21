using OnlineTakeawayStore.Domain;
using OnlineTakeawayStore.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.Application.Handlers
{
    public class SendEmailAcknowledgementOnOrderValidation : IHandleEvents<FoodDeliveryOrderValidated>
    {
        private IEmailer emailer;

        public SendEmailAcknowledgementOnOrderValidation(IEmailer emailer)
        {
            this.emailer = emailer;
        }

        public void Handle(FoodDeliveryOrderValidated @event)
        {
            emailer.SendFoodDeliveryOrderAcknowledgement(@event.Order.CustomerId);
        }
    }
}
