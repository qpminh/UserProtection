using System;
using System.Collections.Generic;
using UserProtection.Domain.Entities;
namespace UserProtection.Infrastructure.Interfaces;

public interface IFeatureRepository
{
    Task<IEnumerable<Feature>> GetAllAsync();
    Task<Feature?> GetByIdAsync(int id);
    Task<Feature> AddAsync(Feature feature);
    Task UpdateAsync(Feature feature);
    Task DeleteAsync(Feature feature);
    Task SaveChangesAsync();
}