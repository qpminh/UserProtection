using System;
using System.Collections.Generic;
using UserProtection.Domain.Entities;
namespace UserProtection.Infrastructure.Interfaces;

public interface IFeatureRepository
{
    Task<IEnumerable<Feature>> GetAllAsync();
    Task<Feature?> GetByIdAsync(int id);
    Task AddAsync(Feature entity);
    Task UpdateAsync(Feature entity);
    Task DeleteAsync(Feature entity);
    Task SaveChangesAsync();
}