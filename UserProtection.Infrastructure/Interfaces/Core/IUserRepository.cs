using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Core
{
    public interface IUserRepository
    {
        Task<User> GetById(string id);
        Task<IEnumerable<User>> GetAll();
        Task Add(User user, string password);
        Task Update(User user);
        Task Delete(string id);
    }
}
