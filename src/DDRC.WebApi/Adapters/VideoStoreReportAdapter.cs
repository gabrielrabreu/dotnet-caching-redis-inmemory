using DDRC.WebApi.Contracts;
using DDRC.WebApi.Data;
using DDRC.WebApi.Models;

namespace DDRC.WebApi.Adapters
{
    public class VideoStoreReportAdapter
    {
        private const int MIN_DAYS_RANGE = 5;
        private const int MAX_DAYS_RANGE = 5;

        private readonly List<VideoStoreModel> _videoStores;
        private readonly List<MovieModel> _movies;
        private readonly List<FulfilledSaleModel> _fulfilledSales;
        private readonly List<ExpectedSaleModel> _expectedSales;
        private readonly List<StockModel> _stocks;

        private VideoStoreModel? _currentVideoStore;
        private DateTimeOffset _currentDateTime;
        private DateTimeOffset _initialDateTime;
        private DateTimeOffset _endDateTime;

        public VideoStoreReportAdapter(DataContext dataContext)
        {
            _videoStores = dataContext.Query<VideoStoreModel>().ToList();
            _movies = dataContext.Query<MovieModel>().ToList();
            _fulfilledSales = dataContext.Query<FulfilledSaleModel>().ToList();
            _expectedSales = dataContext.Query<ExpectedSaleModel>().ToList();
            _stocks = dataContext.Query<StockModel>().ToList();

            _currentDateTime = DateTime.UtcNow.Date;
            _initialDateTime = _currentDateTime.AddDays(-MIN_DAYS_RANGE);
            _endDateTime = _currentDateTime.AddDays(MAX_DAYS_RANGE);
        }

        public VideoStoreReportDto? Transform(string videoStoreName)
        {
            _currentVideoStore = _videoStores.SingleOrDefault(x => x.Name == videoStoreName);

            if (_currentVideoStore == null) return null;

            return new VideoStoreReportDto
            {
                VideoStore = _currentVideoStore.Name,
                Movies = MapMovies()
            };
        }

        private List<MovieSalesReportDto> MapMovies()
        {
            var result = new List<MovieSalesReportDto>();

            foreach (var movie in _movies)
            {
                result.Add(MapMovie(movie));
            }

            return result;
        }

        private MovieSalesReportDto MapMovie(MovieModel movie)
        {
            var result = new MovieSalesReportDto()
            {
                Movie = movie.Title,
                Days = MapMovieDays(movie),
                RetroactiveDays = MapMovieRetroactiveDays(movie)
            };

            return result;
        }

        private List<DayMovieSalesReportDto> MapMovieDays(MovieModel movie)
        {
            var result = new List<DayMovieSalesReportDto>();

            if (_currentVideoStore == null) return result;

            var stockOnDay = _stocks
                .SingleOrDefault(x => x.Movie.Id == movie.Id
                                   && x.Date == _currentDateTime)?.Amount ?? 0;

            for (DateTimeOffset date = _currentDateTime; date < _endDateTime; date = date.AddDays(1))
            {
                var allMovieSalesOnDay = _expectedSales
                    .Where(x => x.Movie.Id == movie.Id
                             && x.Date == date);

                var movieSalesOnDayAndVideoStore = _expectedSales
                    .SingleOrDefault(x => x.Movie.Id == movie.Id
                                       && x.VideoStore.Id == _currentVideoStore.Id
                                       && x.Date == date);

                result.Add(new DayMovieSalesReportDto()
                {
                    Date = date,
                    Stock = stockOnDay,
                    SalesOnAllVideoStores = allMovieSalesOnDay.Sum(x => x.Amount),
                    SalesOnCurrentVideoStore = movieSalesOnDayAndVideoStore?.Amount ?? 0
                });

                stockOnDay -= allMovieSalesOnDay.Sum(x => x.Amount);
            }

            return result;
        }

        private List<DayMovieSalesReportDto> MapMovieRetroactiveDays(MovieModel movie)
        {
            var result = new List<DayMovieSalesReportDto>();

            if (_currentVideoStore == null) return result;

            for (DateTimeOffset date = _initialDateTime; date < _currentDateTime; date = date.AddDays(1))
            {
                var stockOnDay = _stocks
                    .SingleOrDefault(x => x.Movie.Id == movie.Id
                                       && x.Date == date)?.Amount ?? 0;

                var allMovieSalesOnDay = _fulfilledSales
                    .Where(x => x.Movie.Id == movie.Id
                             && x.Date.Date == date.Date);

                var movieSalesOnDayAndVideoStore = _fulfilledSales
                    .Where(x => x.Movie.Id == movie.Id
                             && x.VideoStore.Id == _currentVideoStore.Id
                             && x.Date.Date == date.Date);

                result.Add(new DayMovieSalesReportDto()
                {
                    Date = date,
                    Stock = stockOnDay,
                    SalesOnAllVideoStores = allMovieSalesOnDay.Count(),
                    SalesOnCurrentVideoStore = movieSalesOnDayAndVideoStore.Count()
                });
            }

            return result;
        }
    }
}
