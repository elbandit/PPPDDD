using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using NServiceBus;
using NServiceBus.Installation.Environments;

namespace DDDesign.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IBus bus;

        public static IBus Bus { get { return bus; } }

        protected void Application_Start()
        {
            Configure.Serialization.Xml();
            bus = Configure.With()
                           .DefaultBuilder()
                           .DefiningCommandsAs(t => t.Namespace != null 
                               && t.Namespace.Contains("Commands"))
                           .UseTransport<Msmq>()
                           .UnicastBus()
                           .SendOnly();

            // following code was provided by default and should remain un-modified
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}