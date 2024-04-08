using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.DAL;
using DealerManagementSystem.Middlewares;
using Microsoft.OpenApi.Models;

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
            Configuration = configuration;
        }

        /// <summary>
        /// Configure the services that used in project.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();

            #region Register Repository
            services.AddTransient<IVEH01_DAL, VEH01Repository>();
            services.AddTransient<ISAL01_DAL, SAL01Repository>();
            services.AddTransient<ICTG01_DAL, CTG01Repository>();
            services.AddTransient<IDEA01_DAL, DEA01Repository>();
            services.AddTransient<IDEA02_DAL, DEA02Repository>();
            services.AddTransient<ICUS01_DAL, CUS01Repository>();
            services.AddTransient<ICUS02_DAL, CUS02Repository>();
            #endregion

            #region Register services
            services.AddTransient<IBLVEH01, BLVEH01>();
            services.AddTransient<IBLCUS01, BLCUS01>();
            services.AddTransient<IBLSAL01, BLSAL01>();
            services.AddTransient<IBLDEA01, BLDEA01>();
            services.AddTransient<IBLDEA02, BLDEA02>();
            services.AddTransient<IBLCUS02, BLCUS02>();
            #endregion

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