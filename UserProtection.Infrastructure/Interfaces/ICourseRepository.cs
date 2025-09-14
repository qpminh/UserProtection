using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task AddAsync(Course entity);
    Task UpdateAsync(Course entity);
    Task DeleteAsync(Course entity);
    Task SaveChangesAsync();
}