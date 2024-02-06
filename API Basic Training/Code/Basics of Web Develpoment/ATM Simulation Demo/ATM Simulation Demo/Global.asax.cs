using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;

namespace ATM_Simulation_Demo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            //q1UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .CreateLogger();

            
        }
    }
}
