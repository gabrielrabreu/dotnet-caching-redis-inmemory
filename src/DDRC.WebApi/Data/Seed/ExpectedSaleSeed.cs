using Bogus;
using DDRC.WebApi.Models;

namespace DDRC.WebApi.Data.Seed
{
    public static class ExpectedSaleSeed
    {
        public static void SeedData(DataContext context)
        {
            var videoStores = context.Query<VideoStoreModel>().ToList();
            var movies = context.Query<MovieModel>().ToList();

            var currentDate = DateTime.UtcNow.Date;

            foreach (var videoStore in videoStores)
            {
                foreach (var movie in movies)
                {
                    for (var date = currentDate; date < currentDate.AddYears(1); date = date.AddDays(1))
                    {
                        var model = new ExpectedSaleModel()
                        {
                            Id = Guid.NewGuid(),
                            Date = date,
                            Amount = new Faker("en").Random.Int(0, 100),
                            VideoStore = videoStore,
                            Movie = movie
                        };

                        context.AddData(model);
                    }
                }
            }

            context.CommitChanges();
        }
    }
}
