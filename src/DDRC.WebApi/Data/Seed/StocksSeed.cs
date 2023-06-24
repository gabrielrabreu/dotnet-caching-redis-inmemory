using Bogus;
using DDRC.WebApi.Models;

namespace DDRC.WebApi.Data.Seed
{
    public static class StocksSeed
    {
        public static void SeedData(DataContext context)
        {
            var movies = context.Query<MovieModel>().ToList();

            var currentDate = DateTime.UtcNow.Date;

            foreach (var movie in movies)
            {
                for (var date = currentDate.AddYears(-1); date <= currentDate; date = date.AddDays(1))
                {
                    var model = new StockModel()
                    {
                        Id = Guid.NewGuid(),
                        Date = date,
                        Amount = new Faker("en").Random.Int(0, 100),
                        Movie = movie
                    };

                    context.AddData(model);
                }
            }

            context.CommitChanges();
        }
    }
}
