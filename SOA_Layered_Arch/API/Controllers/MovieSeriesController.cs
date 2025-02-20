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
    public class MovieSeriesTagController : ControllerBase
    {
        private readonly MovieSeriesTagService _movieSeriesTagService;

        // Constructor - Dependency Injection
        public MovieSeriesTagController(MovieSeriesTagService movieSeriesTagService)
        {
            _movieSeriesTagService = movieSeriesTagService ?? throw new ArgumentNullException(nameof(movieSeriesTagService));
        }

        // ✅ Lấy tất cả MovieSeriesTags
        [HttpGet]
        public async Task<IActionResult> GetAllMovieSeriesTags(CancellationToken cancellationToken)
        {
            var movieSeriesTags = await _movieSeriesTagService.GetAllMovieSeriesTagsAsync(cancellationToken);
            return Ok(movieSeriesTags);
        }

        // ✅ Lấy danh sách Tags theo MovieSeriesId (Không cần TagId)
        [HttpGet("movieseries/{movieSeriesId}")]
        public async Task<IActionResult> GetTagsByMovieSeriesId(int movieSeriesId, CancellationToken cancellationToken)
        {
            if (movieSeriesId <= 0)
                return BadRequest("MovieSeriesId must be greater than zero.");

            var tags = await _movieSeriesTagService.GetTagsByMovieSeriesIdAsync(movieSeriesId, cancellationToken);
            return Ok(tags);
        }

        // ✅ Thêm một MovieSeriesTag mới
        [HttpPost]
        public async Task<IActionResult> AddMovieSeriesTag([FromBody] MovieSeriesTag movieSeriesTag, CancellationToken cancellationToken)
        {
            if (movieSeriesTag == null)
                return BadRequest("MovieSeriesTag data is required.");

            var createdTag = await _movieSeriesTagService.AddMovieSeriesTagAsync(movieSeriesTag, cancellationToken);
            return CreatedAtAction(nameof(GetTagsByMovieSeriesId),
                new { movieSeriesId = createdTag.MovieSeriesId },
                createdTag);
        }

        // ✅ Xóa tất cả tags của một MovieSeries
        [HttpDelete("movieseries/{movieSeriesId}")]
        public async Task<IActionResult> DeleteMovieSeriesTags(int movieSeriesId, CancellationToken cancellationToken)
        {
            if (movieSeriesId <= 0)
                return BadRequest("MovieSeriesId must be greater than zero.");

            var deleted = await _movieSeriesTagService.DeleteTagsByMovieSeriesIdAsync(movieSeriesId, cancellationToken);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
