using UserProtection.Domain.Entities;

namespace UserProtection.Application.Interfaces.Security
{
    public interface IUserDomainEntriesService
    {
        Task<IEnumerable<UserDomainEntry?>> GetByUserId(string userId);

        Task<IEnumerable<UserDomainEntry?>> GetByUserId(string userId, string status);

        Task<IEnumerable<UserDomainEntry?>> GetAll();

        Task<UserDomainEntry> Add(UserDomainEntry entity);

        Task Update(UserDomainEntry entity);
    }
}
