
namespace OnlineTakeawayStore.NServiceBusApplication
{
    using NServiceBus;

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
}
