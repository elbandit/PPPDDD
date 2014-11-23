using NServiceBus;

namespace Sales.Orders.OrderCreated
{
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization, AsA_Publisher
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
