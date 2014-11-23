using OnlineTakeawayStore.Application.EventHandlers;
using OnlineTakeawayStore.Application.Handlers;
using OnlineTakeawayStore.Domain;
using OnlineTakeawayStore.StaticDomainEvents.Infrastructure;
using OnlineTakeawayStore.StaticDomainEvents.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.Application
{
    // Wires up event handlers that live in the service layer that
    // carry out infrastructural concerns like sending emails
    public static class ServiceLayerHandlersRegister
    {
        public static void WireUpDomainEventHandlers(IEmailer emailer)
        {
            DomainEvents.Register<FoodDeliveryOrderConfirmed>(new SendConfirmationEmailOnOrderConfirmed(emailer));
            DomainEvents.Register<FoodDeliveryOrderValidated>(new SendEmailAcknowledgementOnOrderValidation(emailer));
            DomainEvents.Register<FoodDeliveryOrderInvalidatedDueToBlacklistedCustomer>(new EmailBlacklistedCustomerOnOrderInvalidation(emailer));
        }
    }
}
