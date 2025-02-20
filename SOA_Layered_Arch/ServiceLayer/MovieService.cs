using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.ServiceLayer
{
    public class MovieService
    {
        private readonly IRepository<Movie> _movieRepository; // ✅ Sử dụng Interface

        // Constructor - Dependency Injection
        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // Lấy danh sách tất cả phim
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _movieRepository.GetAllAsync();
        }

        // Lấy danh sách phim có đánh giá cao nhất (Stored Procedure)
        public async Task<IEnumerable<Movie>> GetTopRatedMoviesWithSpAsync(int topCount)
        {
            return await _movieRepository.GetTopRatedMoviesWithSpAsync(topCount);
        }

        // Lấy phim theo ID
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _movieRepository.GetByIdAsync(id);
        }
    }
}
