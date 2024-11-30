using HAN.OOSE.Movies.BusinessLayer.Contracts;
using HAN.OOSE.Movies.BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HAN.OOSE.Movies.Services.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IMovieService movieService, ILogger<MovieController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        [HttpGet("/movies")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Dit is het movie endpoint");
            var movies = await _movieService.GetMovies();
            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovieDTO movie)
        {
           movie = await _movieService.CreateMovie(movie);
           return Ok(movie);
        }
    }
}
