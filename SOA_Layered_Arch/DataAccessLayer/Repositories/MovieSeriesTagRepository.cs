using Microsoft.EntityFrameworkCore;
using SOA_Layered_Arch.CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.DataAccessLayer.Repositories
{
    public class MovieSeriesTagRepository : IRepository<MovieSeriesTag>
    {
        private readonly AppDbContext _context;

        public MovieSeriesTagRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // 📌 Lấy tất cả MovieSeriesTags
        public async Task<IEnumerable<MovieSeriesTag>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.MovieSeriesTags.AsNoTracking().ToListAsync(cancellationToken);
        }

        // 📌 Chuyển đổi (MovieSeriesId, TagId) -> Id duy nhất
        private int ConvertCompositeKeyToId(int movieSeriesId, int tagId)
        {
            return movieSeriesId * 10000 + tagId; // ✅ Giả sử `TagId` nhỏ hơn 10000
        }

        // 📌 Tách Id duy nhất -> (MovieSeriesId, TagId)
        private (int movieSeriesId, int tagId) ConvertIdToCompositeKey(int id)
        {
            int movieSeriesId = id / 10000;
            int tagId = id % 10000;
            return (movieSeriesId, tagId);
        }

        // 📌 Lấy một MovieSeriesTag theo "Id"
        public async Task<MovieSeriesTag?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var (movieSeriesId, tagId) = ConvertIdToCompositeKey(id);

            return await _context.MovieSeriesTags
                .FindAsync(new object[] { movieSeriesId, tagId }, cancellationToken);
        }

        // 📌 Thêm một MovieSeriesTag mới
        public async Task<MovieSeriesTag> AddAsync(MovieSeriesTag movieSeriesTag, CancellationToken cancellationToken = default)
        {
            if (movieSeriesTag == null)
                throw new ArgumentNullException(nameof(movieSeriesTag));

            await _context.MovieSeriesTags.AddAsync(movieSeriesTag, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return movieSeriesTag;
        }

        // 📌 Cập nhật MovieSeriesTag (không thực sự cần vì chỉ có 2 khóa chính, thường chỉ thêm/xóa)
        public async Task<MovieSeriesTag?> UpdateAsync(MovieSeriesTag movieSeriesTag, CancellationToken cancellationToken = default)
        {
            if (movieSeriesTag == null)
                throw new ArgumentNullException(nameof(movieSeriesTag));

            var existingEntity = await GetByIdAsync(ConvertCompositeKeyToId(movieSeriesTag.MovieSeriesId, movieSeriesTag.TagId), cancellationToken);
            if (existingEntity == null) return null;

            _context.Entry(existingEntity).CurrentValues.SetValues(movieSeriesTag);
            await _context.SaveChangesAsync(cancellationToken);
            return existingEntity;
        }

        // 📌 Xóa một MovieSeriesTag bằng "Id"
        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var (movieSeriesId, tagId) = ConvertIdToCompositeKey(id);

            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity == null)
                return false;

            _context.MovieSeriesTags.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
