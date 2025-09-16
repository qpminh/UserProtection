using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Security
{
    public interface ITrustedLinkRepository
    {
        Task<IEnumerable<TrustedLink>> GetAll(int? tenantId = null);
        Task<TrustedLink?> GetById(int id);
        Task<TrustedLink> Add(TrustedLink entity);
        Task Update(TrustedLink entity);
        Task Delete(int id);
        Task<IEnumerable<TrustedLink>> GetByDomain(string domain);
    }
}
