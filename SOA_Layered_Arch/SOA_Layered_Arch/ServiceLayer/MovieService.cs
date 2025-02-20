using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;


namespace SOA_Layered_Arch.ServiceLayer
{
    public class MovieService
    {
        private readonly MovieRepository _movierepositories; // Sử dụng class Repositories thay vì Interface

        // Constructor - Dependency Injection
        public MovieService(MovieRepository repositories)
        {
            _movierepositories = repositories;
        }

        // Lấy danh sách tất cả phim
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _movierepositories.GetAllMoviesAsync();
        }

        // Lấy danh sách phim có đánh giá cao nhất (Stored Procedure)
        public async Task<IEnumerable<Movie>> GetTopRatedMoviesWithSpAsync(int topCount)
        {
            return await _movierepositories.GetTopRatedMoviesWithSpAsync(topCount);
        }
    }
}
