using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Versioning
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
          
            // Versioning using URI
            config.Routes.MapHttpRoute(
                name: "VersionedApi",
                routeTemplate: "api/v{version}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
