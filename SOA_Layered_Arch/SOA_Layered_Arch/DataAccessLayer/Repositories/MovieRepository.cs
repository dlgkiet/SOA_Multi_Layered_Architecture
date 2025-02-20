using Microsoft.EntityFrameworkCore;
using SOA_Layered_Arch.CoreLayer.Entities;

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

        // Phương thức CRUD
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        // Phương thức gọi Stored Procedure
        public async Task<IEnumerable<Movie>> GetTopRatedMoviesWithSpAsync(int topCount)
        {
            return await _context.Movies
                .FromSqlRaw("EXEC GetTopRatedMovies @top_count = {0}", topCount)
                .ToListAsync();
        }
        // Lấy phim theo ID
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _context.Movies.FindAsync(id);
        }
    }
}
