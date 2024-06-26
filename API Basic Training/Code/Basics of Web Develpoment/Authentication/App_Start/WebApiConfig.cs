﻿using System.Web.Http;
using System.Web.Http.Cors;

namespace Authentication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //// Enable CORS with policy allowing requests from http://example.com
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
