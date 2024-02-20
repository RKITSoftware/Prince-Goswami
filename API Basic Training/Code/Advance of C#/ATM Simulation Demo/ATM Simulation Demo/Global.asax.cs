using Serilog;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            // Database connection using connection string and orm lite tool.
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            var dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

            // Storing OrmLiteConnectionFactory instance for further usage in any other component.
            Application["DbFactory"] = dbFactory;

            //q1UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .CreateLogger();

            
        }
    }
}
