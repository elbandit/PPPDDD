
namespace OnlineTakeawayStore.NServiceBusApplication
{
    using NServiceBus;
using OnlineTakeawayStore.Domain;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, IWantCustomInitialization,
        AsA_Publisher, AsA_Server
    {
        public void Init()
        {
            Configure.With()
                    .DefiningEventsAs(t => t.Namespace != null
                          && t.Namespace.Contains("Events"));

        }

    }

    public class DependencyRegistration : INeedInitialization
    {
        public void Init()
        {
            Configure.Instance.Configurer.ConfigureComponent<DummyBehaviorChecker>(DependencyLifecycle.SingleInstance);
        }
    }

    public class DummyBehaviorChecker : ICustomerBehaviorChecker
    {
        public bool IsBlacklisted(int customerId)
        {
            return true;
        }
    }


}
