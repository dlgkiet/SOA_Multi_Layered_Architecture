using Microsoft.AspNetCore.Mvc;
using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Threading;
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
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
        }

        // ✅ Lấy tất cả phim
        [HttpGet]
        public async Task<IActionResult> GetAllMovies(CancellationToken cancellationToken)
        {
            var movies = await _movieService.GetAllMoviesAsync(cancellationToken);
            return Ok(movies);
        }

        // ✅ Lấy phim theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("ID must be greater than zero.");

            var movie = await _movieService.GetMovieByIdAsync(id, cancellationToken);
            if (movie == null) return NotFound();

            return Ok(movie);
        }

        // ✅ Thêm một phim mới
        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] Movie movie, CancellationToken cancellationToken)
        {
            if (movie == null)
                return BadRequest("Movie data is required.");

            var createdMovie = await _movieService.AddMovieAsync(movie, cancellationToken);
            return CreatedAtAction(nameof(GetMovie), new { id = createdMovie.Id }, createdMovie);
        }

        // ✅ Cập nhật phim
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie movie, CancellationToken cancellationToken)
        {
            if (id <= 0 || movie == null || id != movie.Id)
                return BadRequest("Invalid movie data.");

            var updatedMovie = await _movieService.UpdateMovieAsync(movie, cancellationToken);
            if (updatedMovie == null) return NotFound();

            return Ok(updatedMovie);
        }

        // ✅ Xóa một phim
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("ID must be greater than zero.");

            var deleted = await _movieService.DeleteMovieAsync(id, cancellationToken);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
