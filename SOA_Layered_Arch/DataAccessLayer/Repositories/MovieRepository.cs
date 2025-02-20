using Microsoft.EntityFrameworkCore;
using SOA_Layered_Arch.CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.DataAccessLayer.Repositories
{
    public class MovieRepository : IRepository<Movie>
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách tất cả Movie
        public async Task<IEnumerable<Movie>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Movies.AsNoTracking().ToListAsync(cancellationToken);
        }

        // Lấy Movie theo ID
        public async Task<Movie?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }

        // Thêm một Movie
        public async Task<Movie> AddAsync(Movie movie, CancellationToken cancellationToken = default)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));

            await _context.Movies.AddAsync(movie, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return movie;
        }

        // Cập nhật thông tin Movie
        public async Task<Movie?> UpdateAsync(Movie movie, CancellationToken cancellationToken = default)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));

            var existingMovie = await _context.Movies.FindAsync(new object[] { movie.Id }, cancellationToken);
            if (existingMovie == null)
                return null;

            _context.Entry(existingMovie).CurrentValues.SetValues(movie);
            await _context.SaveChangesAsync(cancellationToken);
            return existingMovie;
        }

        // Xóa một Movie theo ID
        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var movie = await _context.Movies.FindAsync(new object[] { id }, cancellationToken);
            if (movie == null)
                return false;

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
