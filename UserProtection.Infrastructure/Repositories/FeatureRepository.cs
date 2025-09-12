using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Infrastructure.Repositories;

public class FeatureRepository : IFeatureRepository
{
    private readonly UserProtectionContext _context;
    public FeatureRepository(UserProtectionContext context) => _context = context;

    public async Task<IEnumerable<Feature>> GetAllAsync() => await _context.Features.ToListAsync();
    public async Task<Feature?> GetByIdAsync(int id) => await _context.Features.FindAsync(id);
    public async Task<Feature> AddAsync(Feature feature) { await _context.Features.AddAsync(feature); return feature; }
    public async Task UpdateAsync(Feature feature) { _context.Features.Update(feature); }
    public async Task DeleteAsync(Feature feature) { _context.Features.Remove(feature); }
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
