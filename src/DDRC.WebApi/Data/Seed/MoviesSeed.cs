using Bogus;
using DDRC.WebApi.Models;

namespace DDRC.WebApi.Data.Seed
{
    public static class MoviesSeed
    {
        public static void SeedData(DataContext context)
        {
            var categories = context.Query<CategoryModel>().ToList();
            var actors = context.Query<ActorModel>().ToList();

            for (var index = 0; index < 20; index++)
            {
                var model = new MovieModel()
                {
                    Id = Guid.NewGuid(),
                    Title = new Faker("en").Commerce.ProductName(),
                    Description = new Faker("en").Commerce.ProductDescription(),
                    Categories = new Faker("en").PickRandom(categories, new Faker("en").Random.Int(1, 3)).ToList(),
                    Actors = new Faker("en").PickRandom(actors, new Faker("en").Random.Int(2, 5)).ToList()
                };

                context.AddData(model);
            }

            context.CommitChanges();
        }
    }
}
