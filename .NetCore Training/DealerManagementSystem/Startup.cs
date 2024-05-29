using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.BL.Services;
using DealerManagementSystem.DAL;
using DealerManagementSystem.Middleware;
using DealerManagementSystem.Middlewares;
using Microsoft.OpenApi.Models;
using NLog;

namespace DealerManagementSystem
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

            #region Register Repository
            services.AddTransient<IVEH01_DAL, VEH01_DAL>();
            services.AddTransient<ISAL01_DAL, SAL01_DAL>();
            services.AddTransient<ICTG01_DAL, CTG01_DAL>();
            services.AddTransient<IDEA01_DAL, DEA01_DAL>();
            services.AddTransient<IDEA02_DAL, DEA02_DAL>();
            services.AddTransient<ICUS01_DAL, CUS01_DAL>();
            services.AddTransient<ICUS02_DAL, CUS02_DAL>();
            services.AddTransient<IUSR01_DAL, USR01_DAL>();
            #endregion

            #region Register services
            services.AddTransient<IBLVEH01, BLVEH01>();
            services.AddTransient<IBLSAL01, BLSAL01>();
            services.AddTransient<IBLCTG01, BLCTG01>();
            services.AddTransient<IBLDEA01, BLDEA01>();
            services.AddTransient<IBLDEA02, BLDEA02>();
            services.AddTransient<IBLCUS01, BLCUS01>();
            services.AddTransient<IBLCUS02, BLCUS02>();
            services.AddTransient<IBLUSR01, BLUSR01>();
            #endregion

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
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}