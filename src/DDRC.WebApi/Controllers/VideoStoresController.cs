using DDRC.WebApi.Contracts;
using DDRC.WebApi.Data;
using DDRC.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DDRC.WebApi.Controllers
{
    [ApiController]
    [Route("api/video-stores")]
    public class VideoStoresController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public VideoStoresController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _dataContext.Query<VideoStoreModel>();

            var dtos = model.Select(x => new VideoStoreForViewDto
            {
                Id = x.Id,
                Name = x.Name
            });

            return Ok(dtos);
        }

        [HttpGet("{id:guid}")]
        public IActionResult Details(Guid id)
        {
            var model = _dataContext.Query<VideoStoreModel>()
                .SingleOrDefault(x => x.Id == id);

            if (model == null) return NoContent();

            var dto = new VideoStoreForViewDto
            {
                Id = model.Id,
                Name = model.Name
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult Create([FromBody] VideoStoreDto dto)
        {
            var model = new VideoStoreModel
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };

            _dataContext.AddData(model);
            _dataContext.CommitChanges();

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public ActionResult Edit(Guid id, [FromBody] VideoStoreDto dto)
        {
            var model = _dataContext.Query<VideoStoreModel>()
                .SingleOrDefault(x => x.Id == id);

            if (model == null) return BadRequest();

            model.Name = dto.Name;

            _dataContext.UpdateData(model);
            _dataContext.CommitChanges();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            var model = _dataContext.Query<VideoStoreModel>()
                .SingleOrDefault(x => x.Id == id);

            if (model == null) return NoContent();

            _dataContext.DeleteData(model);
            _dataContext.CommitChanges();

            return NoContent();
        }
    }
}
