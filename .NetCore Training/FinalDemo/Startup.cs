using FinalDemo.BL.Interface.Service;
using FinalDemo.BL.Services;
using FinalDemo.DAL;
using FinalDemo.Extension;
using FinalDemo.Middleware;
using FinalDemo.Middlewares;
using Microsoft.OpenApi.Models;
using NLog;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace FinalDemo
{
    /// <summary>
    /// Startup class for the configure the project.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Access to appsettings.json file.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initialize the <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        /// <summary>
        /// Configure the services that used in project.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            //register services and repositories
            services.AddServices();
            services.AddTransient<IDbConnectionFactory>(sp =>
            {
                return new OrmLiteConnectionFactory();
            });
            services.AddLogging(logging =>
            {
                logging.AddEventLog();
                //logging.AddEventLog(options =>
                //{
                //    options.LogName = "Application";
                //    options.SourceName = "Logging";
                //});
            });

            services.AddScoped<BasicAuthenticationMiddleware>();
            // Swagger Service
            services.AddSwaggerGen(c =>

                // Adds basic authentication security definition to Swagger.
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header"
                })
                );
        }


        /// <summary>
        /// Configure the web app.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/> instance.</param>
        /// <param name="env"><see cref="IWebHostEnvironment"/> specifies which environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseDeveloperExceptionPage();
            }

            var loggerFactory = LoggerFactory.Create(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            });

            // Create a logger
            var logger = loggerFactory.CreateLogger<Startup>();

            app.UseExceptionMiddleware();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseBasicAuthenticationMiddleware();
            // Enable authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization(); 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}