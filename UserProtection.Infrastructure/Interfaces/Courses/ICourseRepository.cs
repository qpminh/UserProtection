namespace UserProtection.Infrastructure.Interfaces.Courses
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Domain.Entities.Course>> GetAllAsync();
        Task<Domain.Entities.Course?> GetByIdAsync(int id);
        Task AddAsync(Domain.Entities.Course entity);
        Task UpdateAsync(Domain.Entities.Course entity);
        Task DeleteAsync(Domain.Entities.Course entity);
        Task SaveChangesAsync();
        Task<IEnumerable<Domain.Entities.Course>> GetCoursesBySubscriptionKeyAsync(string keyValue);
    }
}