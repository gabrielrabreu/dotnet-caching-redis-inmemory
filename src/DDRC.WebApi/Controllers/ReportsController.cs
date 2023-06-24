using DDRC.WebApi.Adapters;
using DDRC.WebApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace DDRC.WebApi.Controllers
{
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ReportsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("api/reports:video-store/{videoStoreName}")]
        public IActionResult VideoStoreSales(string videoStoreName)
        {
            var adapter = new VideoStoreReportAdapter(_dataContext);
            return Ok(adapter.Transform(videoStoreName));
        }
    }
}
