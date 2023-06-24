namespace DDRC.WebApi.Scope.Extensions
{
    public static class ControllersServiceCollectionExtensions
    {
        public static void AddDDRCControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
            }).AddNewtonsoftJson();
        }
    }
}
