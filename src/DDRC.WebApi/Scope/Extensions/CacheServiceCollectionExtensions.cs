namespace DDRC.WebApi.Scope.Extensions
{
    public static class CacheServiceCollectionExtensions
    {
        public static void AddDDRCCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["ConnectionStrings:Redis"];
            });
        }
    }
}
