namespace DDRC.WebApi.Scope.Extensions
{
    public static class DistributedCacheServiceCollectionExtensions
    {
        public static void AddDDRCCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["ConnectionStrings:Redis"];
            });
        }
    }
}
