using Microsoft.EntityFrameworkCore;
using SOA_Layered_Arch.CoreLayer.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.DataAccessLayer.Repositories
{
    public class MovieRepository : IRepository<Movie>
    {
        private readonly AppDbContext _context;

        // Constructor để khởi tạo _context
        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách tất cả phim
<<<<<<< HEAD
        public async Task<List<Movie>> GetAllMoviesAsync()
=======
        public async Task<IEnumerable<Movie>> GetAllAsync()
>>>>>>> c6d24fa7f5782800b887a61144a5933b7a05568f
        {
            return await _context.Movies.ToListAsync();
        }

        // Lấy danh sách phim có đánh giá cao nhất (Stored Procedure)
        public async Task<List<Movie>> GetTopRatedMoviesWithSpAsync(int topCount)
        {
            return await _context.Movies
                .FromSqlRaw("EXEC GetTopRatedMovies @top_count = {0}", topCount)
                .ToListAsync();
        }

        // Lấy phim theo ID
        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        }

        // Thêm phim mới
        public async Task<Movie> AddAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        // Cập nhật phim
        public async Task<Movie> UpdateAsync(Movie movie)
        {
            var existingMovie = await _context.Movies.FindAsync(movie.Id);
            if (existingMovie == null)
                return null;

            _context.Entry(existingMovie).CurrentValues.SetValues(movie);
            await _context.SaveChangesAsync();
            return existingMovie;
        }

        // Xóa phim theo ID
        public async Task<bool> DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return false;

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

