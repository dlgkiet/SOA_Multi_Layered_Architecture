using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.ServiceLayer
{
    public class ReviewService
    {
        private readonly IRepository<Review> _reviewRepository;

        // Constructor - Dependency Injection
        public ReviewService(IRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
        }

        // ✅ Lấy tất cả Review
        public async Task<IEnumerable<Review>> GetAllReviewsAsync(CancellationToken cancellationToken = default)
        {
            return await _reviewRepository.GetAllAsync(cancellationToken);
        }

        // ✅ Lấy Review theo ID
        public async Task<Review?> GetReviewByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("Review ID must be greater than zero.", nameof(id));

            return await _reviewRepository.GetByIdAsync(id, cancellationToken);
        }

        // ✅ Thêm một Review mới
        public async Task<Review> AddReviewAsync(Review review, CancellationToken cancellationToken = default)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            review.ReviewDate = DateTime.UtcNow; // Đảm bảo thời gian chuẩn UTC

            return await _reviewRepository.AddAsync(review, cancellationToken);
        }

        // ✅ Cập nhật Review
        public async Task<Review?> UpdateReviewAsync(Review review, CancellationToken cancellationToken = default)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            return await _reviewRepository.UpdateAsync(review, cancellationToken);
        }

        // ✅ Xóa Review theo ID
        public async Task<bool> DeleteReviewAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("Review ID must be greater than zero.", nameof(id));

            return await _reviewRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
