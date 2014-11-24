using NServiceBus;

namespace Shipping.BusinessCustomers.ShippingArranged
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                     .DefiningCommandsAs(t => t.Namespace != null 
                         && t.Namespace.Contains("Commands"))
                     .DefiningEventsAs(t => t.Namespace != null 
                         && t.Namespace.Contains("Events"));
        }
    }
}
