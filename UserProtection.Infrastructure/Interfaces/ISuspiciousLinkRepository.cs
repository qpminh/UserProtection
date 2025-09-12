using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces
{
    public interface ISuspiciousLinkRepository
    {
        Task<SuspiciousLink> Add(SuspiciousLink entity);
        Task<IEnumerable<SuspiciousLink>> GetByUser(string userId);
        Task<IEnumerable<SuspiciousLink>> GetRecent(int limit = 100);
    }
}
