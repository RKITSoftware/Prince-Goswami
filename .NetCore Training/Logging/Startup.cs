using NLog;
using NLog.Extensions.Logging;
/// <summary>
/// Represents the startup configuration for the application.
/// </summary>
namespace Logging
{
    public class Startup
    {
        /// <summary>
        /// configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configures the services used by the application.
        /// </summary>
        /// <param name="services">The collection of services to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Controller Service
            services.AddControllers();

            // Endpoint Service
            services.AddEndpointsApiExplorer();

            // Swagger Service
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configures the HTTP request pipeline for the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="environment">The hosting environment.</param>
        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            // Configure the HTTP request pipeline.
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //        var config = new ConfigurationBuilder()
            //.SetBasePath(System.IO.Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //.Build();
            //        var loggerFromJSON = LogManager.Setup()
            //                               .LoadConfigurationFromSection(config);
            
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
