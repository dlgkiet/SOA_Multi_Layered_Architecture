using Microsoft.AspNetCore.Mvc;
using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.APILayer.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
        }

        // ✅ Lấy tất cả Review
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews(CancellationToken cancellationToken)
        {
            try
            {
                var reviews = await _reviewService.GetAllReviewsAsync(cancellationToken);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
            }
        }

        // ✅ Lấy Review theo ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Review>> GetReviewById(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest(new { message = "Review ID must be greater than zero." });

            try
            {
                var review = await _reviewService.GetReviewByIdAsync(id, cancellationToken);
                if (review == null)
                    return NotFound(new { message = "Review not found." });

                return Ok(review);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
            }
        }

        // ✅ Thêm một Review mới
        [HttpPost]
        public async Task<ActionResult<Review>> AddReview([FromBody] Review review, CancellationToken cancellationToken)
        {
            if (review == null)
                return BadRequest(new { message = "Review data is required." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdReview = await _reviewService.AddReviewAsync(review, cancellationToken);
                return CreatedAtAction(nameof(GetReviewById), new { id = createdReview.ReviewId }, createdReview);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to add review.", details = ex.Message });
            }
        }

        // ✅ Cập nhật một Review
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Review>> UpdateReview(int id, [FromBody] Review review, CancellationToken cancellationToken)
        {
            if (id <= 0 || review == null || id != review.ReviewId)
                return BadRequest(new { message = "Invalid review data." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedReview = await _reviewService.UpdateReviewAsync(review, cancellationToken);
                if (updatedReview == null)
                    return NotFound(new { message = "Review not found or update failed." });

                return Ok(updatedReview);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to update review.", details = ex.Message });
            }
        }

        // ✅ Xóa một Review theo ID
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteReview(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest(new { message = "Review ID must be greater than zero." });

            try
            {
                var deleted = await _reviewService.DeleteReviewAsync(id, cancellationToken);
                if (!deleted)
                    return NotFound(new { message = "Review not found." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to delete review.", details = ex.Message });
            }
        }
    }
}
