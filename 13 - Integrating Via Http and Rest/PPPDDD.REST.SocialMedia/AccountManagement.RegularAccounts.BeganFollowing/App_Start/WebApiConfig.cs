using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AccountManagement.RegularAccounts.BeganFollowing
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Began Following",
                routeTemplate: "accountmanagement/beganfollowing",
                defaults: new { controller = "BeganFollowing", action = "Feed" }
            );
        }
    }
}
