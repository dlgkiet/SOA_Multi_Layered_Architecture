using Microsoft.EntityFrameworkCore;
using SOA_Layered_Arch.CoreLayer.Entities;

namespace SOA_Layered_Arch.DataAccessLayer
{
    public class Repositories
    {
        private readonly AppDbContext _context;

        // Constructor để khởi tạo _context
        public Repositories(AppDbContext context)
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
    }
}
