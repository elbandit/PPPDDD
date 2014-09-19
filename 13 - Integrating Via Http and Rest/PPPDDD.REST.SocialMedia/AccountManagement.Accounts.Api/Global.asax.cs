using System;
using System.Web.Http;
using WebApi.Hal;

namespace AccountManagement.Accounts.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // default media type (HAL JSON)
            GlobalConfiguration.Configuration.Formatters.Insert(
                0, new JsonHalMediaTypeFormatter()
            );

            // alternative media type (HAL XML)
            // accept header must equal application/hal+xml
            GlobalConfiguration.Configuration.Formatters.Insert(
                1, new XmlHalMediaTypeFormatter()
            );
        }
    }
}