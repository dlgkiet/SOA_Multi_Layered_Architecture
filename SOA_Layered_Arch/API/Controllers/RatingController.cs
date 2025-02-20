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
    public class RatingController : ControllerBase
    {
        private readonly RatingService _ratingService;

        // Constructor - Dependency Injection
        public RatingController(RatingService ratingService)
        {
            _ratingService = ratingService ?? throw new ArgumentNullException(nameof(ratingService));
        }

        // ✅ Lấy tất cả đánh giá
        [HttpGet]
        public async Task<IActionResult> GetAllRatings(CancellationToken cancellationToken)
        {
            var ratings = await _ratingService.GetAllRatingsAsync(cancellationToken);
            return Ok(ratings);
        }

        // ✅ Lấy đánh giá theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRatingById(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("Rating ID must be greater than zero.");

            var rating = await _ratingService.GetRatingByIdAsync(id, cancellationToken);
            if (rating == null) return NotFound();

            return Ok(rating);
        }

        // ✅ Thêm một đánh giá mới
        [HttpPost]
        public async Task<IActionResult> AddRating([FromBody] Rating rating, CancellationToken cancellationToken)
        {
            if (rating == null)
                return BadRequest("Rating data is required.");

            var createdRating = await _ratingService.AddRatingAsync(rating, cancellationToken);
            return CreatedAtAction(nameof(GetRatingById), new { id = createdRating.RatingId }, createdRating);
        }

        // ✅ Cập nhật một đánh giá
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRating(int id, [FromBody] Rating rating, CancellationToken cancellationToken)
        {
            if (id <= 0 || rating == null || id != rating.RatingId)
                return BadRequest("Invalid rating data.");

            var updatedRating = await _ratingService.UpdateRatingAsync(rating, cancellationToken);
            if (updatedRating == null) return NotFound();

            return Ok(updatedRating);
        }

        // ✅ Xóa một đánh giá theo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("Rating ID must be greater than zero.");

            var deleted = await _ratingService.DeleteRatingAsync(id, cancellationToken);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
