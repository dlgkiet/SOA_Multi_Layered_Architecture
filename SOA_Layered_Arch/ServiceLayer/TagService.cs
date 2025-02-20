using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.ServiceLayer
{
    public class TagService
    {
        private readonly IRepository<Tag> _tagRepository;

        public TagService(IRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        }

        // Lấy danh sách tất cả tag
        public async Task<IEnumerable<Tag>> GetAllTagsAsync(CancellationToken cancellationToken = default)
        {
            return await _tagRepository.GetAllAsync(cancellationToken);
        }

        // Lấy tag theo ID
        public async Task<Tag?> GetTagByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            return await _tagRepository.GetByIdAsync(id, cancellationToken);
        }

        // Thêm một tag mới
        public async Task<Tag> AddTagAsync(Tag tag, CancellationToken cancellationToken = default)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));

            return await _tagRepository.AddAsync(tag, cancellationToken);
        }

        // Cập nhật thông tin tag
        public async Task<Tag?> UpdateTagAsync(Tag tag, CancellationToken cancellationToken = default)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));

            return await _tagRepository.UpdateAsync(tag, cancellationToken);
        }

        // Xóa một tag theo ID
        public async Task<bool> DeleteTagAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            return await _tagRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
