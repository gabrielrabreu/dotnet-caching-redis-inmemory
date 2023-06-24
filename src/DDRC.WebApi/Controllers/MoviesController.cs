using DDRC.WebApi.Contracts;
using DDRC.WebApi.Data;
using DDRC.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DDRC.WebApi.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public MoviesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _dataContext.Query<MovieModel>()
                .Include(x => x.Actors)
                .Include(x => x.Categories);

            var dtos = model.Select(x => new MovieForViewDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Actors = x.Actors.Select(x => x.Name).ToList(),
                Categories = x.Categories.Select(x => x.Name).ToList()
            });

            return Ok(dtos);
        }

        [HttpGet("{id:guid}")]
        public IActionResult Details(Guid id)
        {
            var model = _dataContext.Query<MovieModel>()
                .Include(x => x.Actors)
                .Include(x => x.Categories)
                .SingleOrDefault(x => x.Id == id);

            if (model == null) return NoContent();

            var dto = new MovieForViewDto
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Actors = model.Actors.Select(x => x.Name).ToList(),
                Categories = model.Categories.Select(x => x.Name).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult Create([FromBody] MovieDto dto)
        {
            var actors = _dataContext.Query<ActorModel>()
                .Where(x => dto.Actors.Contains(x.Name))
                .ToList();

            var categories = _dataContext.Query<CategoryModel>()
                .Where(x => dto.Categories.Contains(x.Name))
                .ToList();

            var model = new MovieModel
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Actors = actors,
                Categories = categories
            };

            _dataContext.AddData(model);
            _dataContext.CommitChanges();

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public ActionResult Edit(Guid id, [FromBody] MovieDto dto)
        {
            var model = _dataContext.Query<MovieModel>()
                .Include(x => x.Actors)
                .Include(x => x.Categories)
                .SingleOrDefault(x => x.Id == id);

            var actors = _dataContext.Query<ActorModel>()
                .Where(x => dto.Actors.Contains(x.Name))
                .ToList();

            var categories = _dataContext.Query<CategoryModel>()
                .Where(x => dto.Categories.Contains(x.Name))
                .ToList();

            if (model == null) return BadRequest();

            model.Title = dto.Title;
            model.Description = dto.Description;
            model.Actors = actors;
            model.Categories = categories;

            _dataContext.UpdateData(model);
            _dataContext.CommitChanges();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            var model = _dataContext.Query<MovieModel>()
                .Include(x => x.Actors)
                .Include(x => x.Categories)
                .SingleOrDefault(x => x.Id == id);

            if (model == null) return NoContent();

            _dataContext.DeleteData(model);
            _dataContext.CommitChanges();

            return NoContent();
        }
    }
}
