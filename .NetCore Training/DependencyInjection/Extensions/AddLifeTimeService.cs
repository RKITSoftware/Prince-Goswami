using DependencyInjection.BL.Interface;
using DependencyInjection.BL.Service;

namespace DependencyInjection.Extension
{
    /// <summary>
    /// class for manage the extension method
    /// </summary>
    public static class AddLifeTimeService
    {
        /// <summary>
        /// Extension method for adding the services into start-up class
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Register Products services
            services.AddSingleton<IProductService, ProductService>();
            // Register ShoppingCart services
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            // Register OrderProcessing services
            services.AddTransient<IOrderProcessingService, OrderProcessingService>();

            return services;
        }
    }
}
