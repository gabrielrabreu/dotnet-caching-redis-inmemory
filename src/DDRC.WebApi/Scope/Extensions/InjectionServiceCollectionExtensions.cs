using DDRC.WebApi.Data;
using DDRC.WebApi.Reports;
using DDRC.WebApi.Settings;
using Microsoft.EntityFrameworkCore;

namespace DDRC.WebApi.Scope.Extensions
{
    public static class InjectionServiceCollectionExtensions
    {
        public static void AddDDRCServices(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettingsSection = configuration.GetSection("DatabaseSettings");
            services.Configure<DatabaseSettings>(databaseSettingsSection);

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer("name=ConnectionStrings:SqlServer"));

            services.AddScoped<IVideoStoreReport, VideoStoreReport>();
        }
    }
}
