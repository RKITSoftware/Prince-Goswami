using Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Creating a startup object
        var startup = new Startup(builder.Configuration);

        // Configuring Services
        startup.ConfigureServices(builder.Services);

        // Configure NLog
        builder.Services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Trace);
        });

        // Building App and Starting.
        var app = builder.Build();
        startup.Configure(app, builder.Environment);

        app.Run();
    }
}
