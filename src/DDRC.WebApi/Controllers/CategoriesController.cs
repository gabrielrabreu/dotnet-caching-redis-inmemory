using DDRC.WebApi.Caches;
using DDRC.WebApi.Contracts;
using DDRC.WebApi.Data;
using DDRC.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace DDRC.WebApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<CategoriesController> _logger;
        private readonly DataContext _dataContext;

        public CategoriesController(IDistributedCache cache,
                                    ILogger<CategoriesController> logger,
                                    DataContext dataContext)
        {
            _cache = cache;
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await FetchFromCache());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = (await FetchFromCache())
                .SingleOrDefault(x => x.Id == id);

            if (model == null) return NoContent();

            var dto = new CategoryForViewDto
            {
                Id = model.Id,
                Name = model.Name
            };

            return Ok(dto);
        }

        private async Task<List<CategoryForViewDto>> FetchFromCache()
        {
            _logger.LogInformation("Trying to fetch the categories from cache.");

            var key = CacheKeys.CategoriesListCacheKey;

            if (_cache.TryGetValue(key, out List<CategoryForViewDto>? result))
            {
                _logger.LogInformation("Categories found in cache.");
            }
            else
            {
                _logger.LogInformation("Categories not found in cache. Fetching from database.");

                var model = _dataContext.Query<CategoryModel>();

                result = model.Select(x => new CategoryForViewDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(600))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));

                await _cache.SetAsync(key, result, cacheEntryOptions);
            }

            return result ?? new List<CategoryForViewDto>();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto dto)
        {
            var model = new CategoryModel
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };

            _dataContext.AddData(model);
            _dataContext.CommitChanges();

            await _cache.RemoveAsync(CacheKeys.CategoriesListCacheKey);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] CategoryDto dto)
        {
            var model = _dataContext.Query<CategoryModel>()
                .SingleOrDefault(x => x.Id == id);

            if (model == null) return BadRequest();

            model.Name = dto.Name;

            _dataContext.UpdateData(model);
            _dataContext.CommitChanges();

            await _cache.RemoveAsync(CacheKeys.CategoriesListCacheKey);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = _dataContext.Query<CategoryModel>()
                .SingleOrDefault(x => x.Id == id);

            if (model == null) return NoContent();

            _dataContext.DeleteData(model);
            _dataContext.CommitChanges();

            await _cache.RemoveAsync(CacheKeys.CategoriesListCacheKey);

            return NoContent();
        }
    }
}
