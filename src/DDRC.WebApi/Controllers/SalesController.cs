﻿using DDRC.WebApi.Contracts;
using DDRC.WebApi.Data;
using DDRC.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DDRC.WebApi.Controllers
{
    [ApiController]
    [Route("api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public SalesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost("fulfilled:import")]
        public ActionResult ImportFulfilled([FromBody] List<FulfilledSaleDto> dtos)
        {
            if (dtos.Any(x => x.Date >= DateTime.UtcNow.Date)) return BadRequest();

            var hasAdded = false;

            var videoStores = _dataContext.Query<VideoStoreModel>().ToList();
            var movies = _dataContext.Query<MovieModel>().ToList();

            foreach (var dto in dtos)
            {
                var videoStore = videoStores.SingleOrDefault(x => x.Name == dto.VideoStore);
                var movie = movies.SingleOrDefault(x => x.Title == dto.Movie);

                if (videoStore == null || movie == null) return BadRequest();

                var sale = new FulfilledSaleModel
                {
                    Id = Guid.NewGuid(),
                    Date = dto.Date,
                    VideoStore = videoStore,
                    Movie = movie
                };

                _dataContext.AddData(sale);

                hasAdded = true;
            }

            if (hasAdded)
                _dataContext.CommitChanges();

            return NoContent();
        }

        [HttpPost("expected:import")]
        public ActionResult ImportExpected([FromBody] List<ExpectedSaleDto> dtos)
        {
            if (dtos.Any(x => x.Date < DateTime.UtcNow.Date)) return BadRequest();

            var hasAdded = false;

            var videoStores = _dataContext.Query<VideoStoreModel>().ToList();
            var movies = _dataContext.Query<MovieModel>().ToList();

            foreach (var dto in dtos)
            {
                var videoStore = videoStores.SingleOrDefault(x => x.Name == dto.VideoStore);
                var movie = movies.SingleOrDefault(x => x.Title == dto.Movie);

                if (videoStore == null || movie == null) return BadRequest();

                var sale = new ExpectedSaleModel
                {
                    Id = Guid.NewGuid(),
                    Date = dto.Date,
                    Amount = dto.Amount,
                    VideoStore = videoStore,
                    Movie = movie
                };

                _dataContext.AddData(sale);

                hasAdded = true;
            }

            if (hasAdded)
                _dataContext.CommitChanges();

            return NoContent();
        }
    }
}
