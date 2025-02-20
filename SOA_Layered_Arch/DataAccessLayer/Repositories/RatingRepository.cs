using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;

namespace SOA_Layered_Arch.DataAccessLayer.Repositories
{
    public class RatingRepository : IRepository<Rating>
    {
        private readonly AppDbContext _context;

        public RatingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rating>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Ratings.ToListAsync(cancellationToken);
        }

        public async Task<Rating?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Ratings.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<Rating> AddAsync(Rating entity, CancellationToken cancellationToken = default)
        {
            await _context.Ratings.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<Rating?> UpdateAsync(Rating entity, CancellationToken cancellationToken = default)
        {
            var existingRating = await _context.Ratings.FindAsync(new object[] { entity.RatingId }, cancellationToken);
            if (existingRating == null)
            {
                return null;
            }

            _context.Entry(existingRating).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return existingRating;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var rating = await _context.Ratings.FindAsync(new object[] { id }, cancellationToken);
            if (rating == null)
            {
                return false;
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
