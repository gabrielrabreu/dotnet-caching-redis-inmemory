using DDRC.WebApi.Caches;
using DDRC.WebApi.Contracts;
using DDRC.WebApi.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace DDRC.WebApi.Controllers
{
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(IDistributedCache cache, 
                                 ILogger<ReportsController> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        [HttpGet("api/reports:video-store/{videoStoreName}")]
        public async Task<IActionResult> ReportVideoStore([FromServices] IVideoStoreReport report, string videoStoreName)
        {
            _logger.LogInformation("Trying to fetch the report from cache.");

            var key = CacheKeys.VideoStoreReportCacheKey;

            if (_cache.TryGetValue(key, out VideoStoreReportsDto? result))
            {
                _logger.LogInformation("Report found in cache.");
            }
            else
            {
                _logger.LogInformation("Report not found in cache. Generating from database.");

                result = report.Generate();

                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(600))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));

                await _cache.SetAsync(key, result, cacheEntryOptions);
            }

            var resultFromVideoStore = result?.VideoStores.Where(x => x.VideoStore == videoStoreName);

            return Ok(resultFromVideoStore);
        }
    }
}
