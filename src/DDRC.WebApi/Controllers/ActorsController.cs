using DDRC.WebApi.Contracts;
using DDRC.WebApi.Data;
using DDRC.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DDRC.WebApi.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ActorsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _dataContext.Query<ActorModel>();

            var dtos = model.Select(x => new ActorForViewDto
            {
                Id = x.Id,
                Name = x.Name
            });

            return Ok(dtos);
        }

        [HttpGet("{id:guid}")]
        public IActionResult Details(Guid id)
        {
            var model = _dataContext.Query<ActorModel>()
                .SingleOrDefault(x => x.Id == id);

            if (model == null) return NoContent();

            var dto = new ActorForViewDto
            {
                Id = model.Id,
                Name = model.Name
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult Create([FromBody] ActorDto dto)
        {
            var model = new ActorModel
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };

            _dataContext.AddData(model);
            _dataContext.CommitChanges();

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public ActionResult Edit(Guid id, [FromBody] ActorDto dto)
        {
            var model = _dataContext.Query<ActorModel>()
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
            var model = _dataContext.Query<ActorModel>()
                .SingleOrDefault(x => x.Id == id);

            if (model == null) return NoContent();

            _dataContext.DeleteData(model);
            _dataContext.CommitChanges();

            return NoContent();
        }
    }
}
