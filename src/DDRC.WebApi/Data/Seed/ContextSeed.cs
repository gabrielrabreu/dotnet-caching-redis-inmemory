using DDRC.WebApi.Settings;
using Microsoft.Extensions.Options;

namespace DDRC.WebApi.Data.Seed
{
    public static class ContextSeed
    {
        public static void InitializeDatabase(this IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var databaseSettings = services.GetRequiredService<IOptions<DatabaseSettings>>().Value;

            if (databaseSettings != null && databaseSettings.RecreateDatabase)
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        public static void SeedData(this IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var databaseSettings = services.GetRequiredService<IOptions<DatabaseSettings>>().Value;

            if (databaseSettings != null && databaseSettings.SeedDatabase)
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                ActorsSeed.SeedData(context);
                CategoriesSeed.SeedData(context);
                MoviesSeed.SeedData(context);
                VideoStoresSeed.SeedData(context);
                StocksSeed.SeedData(context);
                FulfilledSaleSeed.SeedData(context);
                ExpectedSaleSeed.SeedData(context);
            }
        }
    }
}
