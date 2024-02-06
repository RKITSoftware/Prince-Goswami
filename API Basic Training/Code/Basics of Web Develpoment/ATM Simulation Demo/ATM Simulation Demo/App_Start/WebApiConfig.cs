using Microsoft.AspNetCore.Cors;
using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ATM_Simulation_Demo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ATMApi",
                routeTemplate: "api/V1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional , Controllers = ("CLAccountController", "CLTransactionController")  }
            );
            config.Routes.MapHttpRoute(
                name: "ATMApiV2",
                routeTemplate: "api/V2/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional , Controllers = ("CLAccountV2Controller", "CLTransactionV2Controller") }
            );
           
        }
    }
}
