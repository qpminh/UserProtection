using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Infrastructure.Repositories;

public class FeatureRepository : IFeatureRepository
{
    private readonly UserProtectionContext _context;
    public FeatureRepository(UserProtectionContext context) => _context = context;

    public async Task<IEnumerable<Feature>> GetAllAsync() =>
        await _context.Features.ToListAsync();

    public async Task<Feature?> GetByIdAsync(int id) =>
        await _context.Features.FindAsync(id);

    public async Task AddAsync(Feature entity) =>
        await _context.Features.AddAsync(entity);

    public Task UpdateAsync(Feature entity)
    {
        _context.Features.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Feature entity)
    {
        _context.Features.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}