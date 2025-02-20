using Microsoft.EntityFrameworkCore;
using SOA_Layered_Arch.CoreLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.DataAccessLayer.Repositories
{
    public class TagRepository : IRepository<Tag>
    {
        private readonly AppDbContext _context;

        public TagRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Tags.ToListAsync(cancellationToken);
        }

        public async Task<Tag?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Tags.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<Tag> AddAsync(Tag entity, CancellationToken cancellationToken = default)
        {
            _context.Tags.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<Tag?> UpdateAsync(Tag entity, CancellationToken cancellationToken = default)
        {
            var existingTag = await _context.Tags.FindAsync(new object[] { entity.TagId }, cancellationToken);
            if (existingTag == null)
                return null;

            _context.Entry(existingTag).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return existingTag;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var tag = await _context.Tags.FindAsync(new object[] { id }, cancellationToken);
            if (tag == null)
                return false;

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
