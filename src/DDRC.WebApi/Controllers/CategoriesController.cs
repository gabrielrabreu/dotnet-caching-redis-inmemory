using DDRC.WebApi.Contracts;
using DDRC.WebApi.Data;
using DDRC.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DDRC.WebApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CategoriesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _dataContext.Query<CategoryModel>();

            var result = model.Select(x => new CategoryForViewDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public IActionResult Details(Guid id)
        {
            var model = _dataContext.Query<CategoryModel>()
                .SingleOrDefault(x => x.Id == id);

            if (model == null) return NoContent();

            var dto = new CategoryForViewDto
            {
                Id = model.Id,
                Name = model.Name
            };

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CategoryDto dto)
        {
            var model = new CategoryModel
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };

            _dataContext.AddData(model);
            _dataContext.CommitChanges();

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public IActionResult Edit(Guid id, [FromBody] CategoryDto dto)
        {
            var model = _dataContext.Query<CategoryModel>()
                .SingleOrDefault(x => x.Id == id);

            if (model == null) return BadRequest();

            model.Name = dto.Name;

            _dataContext.UpdateData(model);
            _dataContext.CommitChanges();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var model = _dataContext.Query<CategoryModel>()
                .SingleOrDefault(x => x.Id == id);

            if (model == null) return NoContent();

            _dataContext.DeleteData(model);
            _dataContext.CommitChanges();

            return NoContent();
        }
    }
}
