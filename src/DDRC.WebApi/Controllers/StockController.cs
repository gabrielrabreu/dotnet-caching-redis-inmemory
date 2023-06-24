using DDRC.WebApi.Caches;
using DDRC.WebApi.Contracts;
using DDRC.WebApi.Data;
using DDRC.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace DDRC.WebApi.Controllers
{
    [ApiController]
    [Route("api/stocks")]
    public class StockController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IMemoryCache _memoryCache;
        private readonly DataContext _dataContext;

        public StockController(IDistributedCache distributedCache,
                               IMemoryCache memoryCache,
                               DataContext dataContext)
        {
            _distributedCache = distributedCache;
            _memoryCache = memoryCache;
            _dataContext = dataContext;
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import([FromBody] List<StockDto> dtos)
        {
            if (dtos.Any(x => x.Date != DateTime.UtcNow.Date)) return BadRequest();

            var hasAdded = false;

            var movies = _dataContext.Query<MovieModel>().ToList();

            foreach (var dto in dtos)
            {
                var movie = movies.SingleOrDefault(x => x.Title == dto.Movie);

                if (movie == null) return BadRequest();

                var stock = new StockModel
                {
                    Id = Guid.NewGuid(),
                    Date = dto.Date,
                    Amount = dto.Amount,
                    Movie = movie
                };

                _dataContext.AddData(stock);

                hasAdded = true;
            }

            if (hasAdded)
            {
                _dataContext.CommitChanges();
                await _distributedCache.RemoveAsync(CacheKeys.VideoStoreReportCacheKey);
                _memoryCache.Remove(CacheKeys.VideoStoreReportCacheKey);
            }

            return NoContent();
        }
    }
}
