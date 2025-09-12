using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetById(String id);
        Task<IEnumerable<User>> GetAll();
        Task Add(User user, string password);
        Task Update(User user);
        Task Delete(String id);
    }
}
