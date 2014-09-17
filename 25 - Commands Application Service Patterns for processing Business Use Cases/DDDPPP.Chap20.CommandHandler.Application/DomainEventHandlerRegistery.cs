using Agathas.Storefront.Infrastructure;
using StructureMap;

namespace Agathas.Storefront.Application
{
    public class DomainEventHandlerRegistery : IDomainEventHandlerRegistery
    {        
        public void handle<TEvent>(TEvent domain_event) where TEvent : IDomainEvent
        {
            var handler = ObjectFactory.TryGetInstance<IDomainEventHandler<TEvent>>();

            handler.action(domain_event);
        }
    }
}