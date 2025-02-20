using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.ServiceLayer
{
    public class MovieSeriesTagService
    {
        private readonly IRepository<MovieSeriesTag> _movieSeriesTagRepository; // ✅ Sử dụng Interface thay vì Repository cụ thể

        // Constructor - Dependency Injection
        public MovieSeriesTagService(IRepository<MovieSeriesTag> movieSeriesTagRepository) // ✅ Inject Interface
        {
            _movieSeriesTagRepository = movieSeriesTagRepository ?? throw new ArgumentNullException(nameof(movieSeriesTagRepository));
        }

        // ✅ Lấy tất cả MovieSeriesTags
        public async Task<IEnumerable<MovieSeriesTag>> GetAllMovieSeriesTagsAsync(CancellationToken cancellationToken = default)
        {
            return await _movieSeriesTagRepository.GetAllAsync(cancellationToken);
        }

        // ✅ Lấy danh sách Tags theo MovieSeriesId
        public async Task<IEnumerable<MovieSeriesTag>> GetTagsByMovieSeriesIdAsync(int movieSeriesId, CancellationToken cancellationToken = default)
        {
            if (movieSeriesId <= 0)
                throw new ArgumentException("MovieSeriesId must be greater than zero.", nameof(movieSeriesId));

            var allTags = await _movieSeriesTagRepository.GetAllAsync(cancellationToken);
            return allTags.Where(tag => tag.MovieSeriesId == movieSeriesId);
        }

        // ✅ Thêm một MovieSeriesTag
        public async Task<MovieSeriesTag> AddMovieSeriesTagAsync(MovieSeriesTag movieSeriesTag, CancellationToken cancellationToken = default)
        {
            if (movieSeriesTag == null)
                throw new ArgumentNullException(nameof(movieSeriesTag));

            return await _movieSeriesTagRepository.AddAsync(movieSeriesTag, cancellationToken);
        }

        // ✅ Xóa tất cả tags của một `MovieSeriesId`
        public async Task<bool> DeleteTagsByMovieSeriesIdAsync(int movieSeriesId, CancellationToken cancellationToken = default)
        {
            if (movieSeriesId <= 0)
                throw new ArgumentException("MovieSeriesId must be greater than zero.", nameof(movieSeriesId));

            var tags = await GetTagsByMovieSeriesIdAsync(movieSeriesId, cancellationToken);
            if (!tags.Any())
                return false;

            foreach (var tag in tags)
            {
                await _movieSeriesTagRepository.DeleteAsync(tag.MovieSeriesId * 10000 + tag.TagId, cancellationToken);
            }

            return true;
        }
    }
}
