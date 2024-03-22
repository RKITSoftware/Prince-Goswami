using Logging;
using Microsoft.AspNetCore.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Creating a startup object
        Startup startup = new Startup(builder.Configuration);

        // Configuring Services
        startup.ConfigureServices(builder.Services);

        // Configure NLog
        builder.Services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Trace);
        });

        // Building App and Starting.
        WebApplication app = builder.Build();
        startup.Configure(app, builder.Environment);
    }
}