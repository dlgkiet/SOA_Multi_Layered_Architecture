using Microsoft.AspNetCore.Mvc;
using SOA_Layered_Arch.ServiceLayer;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;

        // Constructor - Dependency Injection
        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        // Lấy phim theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }

        // Lấy danh sách phim có đánh giá cao nhất
        [HttpGet("top-rated/{count}")]
        public async Task<IActionResult> GetTopRatedMovies(int count)
        {
            var movies = await _movieService.GetTopRatedMoviesWithSpAsync(count);
            return Ok(movies);
        }
    }
}
