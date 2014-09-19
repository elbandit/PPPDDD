using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AccountManagement.Accounts.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Accounts Collection",
                routeTemplate: "accountmanagement/accounts",
                defaults: new { controller = "Accounts", action = "Index" }
            );

            config.Routes.MapHttpRoute(
                name: "Individual Account",
                routeTemplate: "accountmanagement/accounts/{accountId}",
                defaults: new { controller = "Accounts", action = "Account" }
            );

            config.Routes.MapHttpRoute(
               name: "Account Followers",
               routeTemplate: "accountmanagement/accounts/{accountId}/followers",
               defaults: new { controller = "Followers", action = "Index" }
            );
        }
    }
}
