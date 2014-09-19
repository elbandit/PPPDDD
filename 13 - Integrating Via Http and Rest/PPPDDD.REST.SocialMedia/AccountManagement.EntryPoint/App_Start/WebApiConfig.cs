using System;
using System.Web.Http;

namespace AccountManagement.EntryPoint.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Entry Point",
                routeTemplate: "accountmanagement",
                defaults: new { controller = "EntryPoint", action = "Get" }
            );
        }
    }
}
