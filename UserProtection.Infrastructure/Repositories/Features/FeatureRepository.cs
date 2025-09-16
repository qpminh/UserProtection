using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Features;

namespace UserProtection.Infrastructure.Repositories.Features
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly UserProtectionContext _context;
        public FeatureRepository(UserProtectionContext context) => _context = context;

        public async Task<IEnumerable<Domain.Entities.Feature>> GetAllAsync() =>
            await _context.Features.ToListAsync();

        public async Task<Domain.Entities.Feature?> GetByIdAsync(int id) =>
            await _context.Features.FindAsync(id);

        public async Task AddAsync(Domain.Entities.Feature entity) =>
            await _context.Features.AddAsync(entity);

        public Task UpdateAsync(Domain.Entities.Feature entity)
        {
            _context.Features.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Domain.Entities.Feature entity)
        {
            _context.Features.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}