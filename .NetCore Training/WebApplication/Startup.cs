﻿/// <summary>
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

        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}