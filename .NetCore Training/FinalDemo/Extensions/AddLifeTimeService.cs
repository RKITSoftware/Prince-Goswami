using FinalDemo.BL.Interface.Repository;
using FinalDemo.BL.Interface.Service;
using FinalDemo.BL.Services;
using FinalDemo.DAL;
using FinalDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace FinalDemo.Extension
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
            #region Register Repository
            services.AddTransient<IVEH01_DAL, VEH01_DAL>();
            services.AddTransient<ISAL01_DAL, SAL01_DAL>();
            services.AddTransient<ICTG01_DAL, CTG01_DAL>();
            services.AddTransient<ICUS01_DAL, CUS01_DAL>();
            services.AddTransient<ICUS02_DAL, CUS02_DAL>();
            services.AddTransient<IUSR01_DAL, USR01_DAL>();
            #endregion

            #region Register services
            services.AddTransient<IBLVEH01, BLVEH01>();
            services.AddTransient<IBLSAL01, BLSAL01>();
            services.AddTransient<IBLCTG01, BLCTG01>();
            services.AddTransient<IBLCUS01, BLCUS01>();
            services.AddTransient<IBLCUS02, BLCUS02>();
            services.AddTransient<IBLUSR01, BLUSR01>();

            #endregion


            // Register DALHelper
            services.AddSingleton<DALHelper>();
            
            return services;
        }
    }
}
