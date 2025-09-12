using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces
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
