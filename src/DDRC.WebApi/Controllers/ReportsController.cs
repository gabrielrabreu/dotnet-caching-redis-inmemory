using DDRC.WebApi.Caches;
using DDRC.WebApi.Contracts;
using DDRC.WebApi.Reports;
using DDRC.WebApi.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace DDRC.WebApi.Controllers
{
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IVideoStoreReport _report;
        private readonly IDistributedCache _distributedCache;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(IVideoStoreReport report,
                                 IDistributedCache distributedCache,
                                 IMemoryCache memoryCache,
                                 ILogger<ReportsController> logger)
        {
            _distributedCache = distributedCache;
            _memoryCache = memoryCache;
            _report = report;
            _logger = logger;
        }

        [HttpGet("api/reports:video-store/refresh")]
        public async Task<IActionResult> Refresh()
        {
            var cacheType = CacheType.None;

            if (Request.Headers.TryGetValue("Cache-Type", out StringValues cacheTypeHeader))
            {
                Enum.TryParse(cacheTypeHeader.Single(), true, out cacheType);
            }

            switch (cacheType)
            {
                case CacheType.InMemory:
                    _memoryCache.Remove(CacheKeys.VideoStoreReportCacheKey);
                    break;
                case CacheType.Redis:
                    await _distributedCache.RemoveAsync(CacheKeys.VideoStoreReportCacheKey);
                    break;
                case CacheType.None:
                default:
                    break;
            }

            return Ok();
        }

        [HttpGet("api/reports:video-store/{videoStoreName}")]
        public async Task<IActionResult> Report(string videoStoreName)
        {
            var cacheType = CacheType.None;

            if (Request.Headers.TryGetValue("Cache-Type", out StringValues cacheTypeHeader))
            {
                Enum.TryParse(cacheTypeHeader.Single(), true, out cacheType);
            }

            VideoStoreReportsDto? result = cacheType switch
            {
                CacheType.InMemory => GetFromMemory(),
                CacheType.Redis => await GetFromRedis(),
                _ => _report.Generate(),
            };

            var resultFromVideoStore = result?.VideoStores.Where(x => x.VideoStore == videoStoreName);

            return Ok(resultFromVideoStore);
        }

        private async Task<VideoStoreReportsDto?> GetFromRedis()
        {
            _logger.LogInformation("Trying to fetch the report from redis cache.");

            var key = CacheKeys.VideoStoreReportCacheKey;

            if (_distributedCache.TryGetValue(key, out VideoStoreReportsDto? result))
            {
                _logger.LogInformation("Report found in redis cache.");
            }
            else
            {
                _logger.LogInformation("Report not found in redis cache. Generating from database.");

                result = _report.Generate();

                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(600))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));

                await _distributedCache.SetAsync(key, result, cacheEntryOptions);
            }

            return result;
        }

        private  VideoStoreReportsDto? GetFromMemory()
        {
            _logger.LogInformation("Trying to fetch the report from memory cache.");

            var key = CacheKeys.VideoStoreReportCacheKey;

            if (_memoryCache.TryGetValue(key, out VideoStoreReportsDto? result))
            {
                _logger.LogInformation("Report found in memory cache.");
            }
            else
            {
                _logger.LogInformation("Report not found in memory cache. Generating from database.");

                result = _report.Generate();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(600))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));

                _memoryCache.Set(key, result, cacheEntryOptions);
            }

            return result;
        }
    }
}
