using DDRC.WebApi.Data;
using DDRC.WebApi.Reports;
using Microsoft.EntityFrameworkCore;

namespace DDRC.WebApi.Scope.Extensions
{
    public static class InjectionServiceCollectionExtensions
    {
        public static void AddDDRCServices(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer("name=ConnectionStrings:SqlServer"));

            services.AddScoped<IVideoStoreReport, VideoStoreReport>();
        }
    }
}
