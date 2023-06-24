using Microsoft.OpenApi.Models;

namespace DDRC.WebApi.Scope.Extensions
{
    public static class SwaggerServiceCollectionExtensions
    {
        public static void AddDDRCSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DDRC"
                });
            });
        }

        public static void UseDDRCSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
