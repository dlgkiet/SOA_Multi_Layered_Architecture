using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.ServiceLayer
{
    public class RatingService
    {
        private readonly IRepository<Rating> _ratingRepository; // ✅ Sử dụng Interface để dễ dàng Mock/Test

        // Constructor - Dependency Injection
        public RatingService(IRepository<Rating> ratingRepository)
        {
            _ratingRepository = ratingRepository ?? throw new ArgumentNullException(nameof(ratingRepository));
        }

        // Lấy danh sách tất cả đánh giá
        public async Task<IEnumerable<Rating>> GetAllRatingsAsync(CancellationToken cancellationToken = default)
        {
            return await _ratingRepository.GetAllAsync(cancellationToken);
        }

        // Lấy đánh giá theo ID
        public async Task<Rating?> GetRatingByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            return await _ratingRepository.GetByIdAsync(id, cancellationToken);
        }

        // Thêm một đánh giá mới
        public async Task<Rating> AddRatingAsync(Rating rating, CancellationToken cancellationToken = default)
        {
            if (rating == null)
                throw new ArgumentNullException(nameof(rating));

            if (rating.RatingValue < 0 || rating.RatingValue > 10)
                throw new ArgumentOutOfRangeException(nameof(rating.RatingValue), "Rating value must be between 0 and 10.");

            return await _ratingRepository.AddAsync(rating, cancellationToken);
        }

        // Cập nhật thông tin đánh giá
        public async Task<Rating?> UpdateRatingAsync(Rating rating, CancellationToken cancellationToken = default)
        {
            if (rating == null)
                throw new ArgumentNullException(nameof(rating));

            return await _ratingRepository.UpdateAsync(rating, cancellationToken);
        }

        // Xóa một đánh giá theo ID
        public async Task<bool> DeleteRatingAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            return await _ratingRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
