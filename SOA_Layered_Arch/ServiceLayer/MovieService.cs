using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.ServiceLayer
{
    public class MovieService
    {
        private readonly IRepository<Movie> _movieRepository; // ✅ Sử dụng Interface để dễ dàng Mock/Test

        // Constructor - Dependency Injection
        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        // Lấy danh sách tất cả phim
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync(CancellationToken cancellationToken = default)
        {
            return await _movieRepository.GetAllAsync(cancellationToken);
        }

        // Lấy phim theo ID
        public async Task<Movie?> GetMovieByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            return await _movieRepository.GetByIdAsync(id, cancellationToken);
        }

        // Thêm một phim mới
        public async Task<Movie> AddMovieAsync(Movie movie, CancellationToken cancellationToken = default)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));

            return await _movieRepository.AddAsync(movie, cancellationToken);
        }

        // Cập nhật thông tin phim
        public async Task<Movie?> UpdateMovieAsync(Movie movie, CancellationToken cancellationToken = default)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));

            return await _movieRepository.UpdateAsync(movie, cancellationToken);
        }

        // Xóa một phim theo ID
        public async Task<bool> DeleteMovieAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            return await _movieRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
