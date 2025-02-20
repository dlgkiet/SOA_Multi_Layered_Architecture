using Microsoft.EntityFrameworkCore;
using SOA_Layered_Arch.CoreLayer.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.DataAccessLayer.Repositories
{
    public class MovieRepository
    {
        private readonly AppDbContext _context;

        // Constructor để khởi tạo _context
        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách tất cả phim
        public async Task<List<Movie>> GetAllMoviesAsync()
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
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        }

        // Thêm phim mới
        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        // Cập nhật phim
        public async Task<Movie> UpdateMovieAsync(Movie movie)
        {
            var existingMovie = await _context.Movies.FindAsync(movie.Id);
            if (existingMovie == null)
                return null;

            _context.Entry(existingMovie).CurrentValues.SetValues(movie);
            await _context.SaveChangesAsync();
            return existingMovie;
        }

        // Xóa phim theo ID
        public async Task<bool> DeleteMovieAsync(int id)
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
