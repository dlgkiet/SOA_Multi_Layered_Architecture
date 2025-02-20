using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;

namespace SOA_Layered_Arch.DataAccessLayer.Repositories
{
    public class ReviewRepository : IRepository<Review>
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Lấy tất cả Review
        public async Task<IEnumerable<Review>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Reviews.ToListAsync(cancellationToken);
        }

        // ✅ Lấy Review theo ID
        public async Task<Review?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Reviews.FindAsync(new object[] { id }, cancellationToken);
        }

        // ✅ Thêm Review mới
        public async Task<Review> AddAsync(Review entity, CancellationToken cancellationToken = default)
        {
            await _context.Reviews.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        // ✅ Cập nhật Review
        public async Task<Review?> UpdateAsync(Review entity, CancellationToken cancellationToken = default)
        {
            var existingReview = await _context.Reviews.FindAsync(new object[] { entity.ReviewId }, cancellationToken);
            if (existingReview == null)
            {
                return null;
            }

            _context.Entry(existingReview).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return existingReview;
        }

        // ✅ Xóa Review theo ID
        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var review = await _context.Reviews.FindAsync(new object[] { id }, cancellationToken);
            if (review == null)
            {
                return false;
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
