using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Application.Dtos.Security;

namespace UserProtection.Application.Interfaces
{
    public interface ISuspiciousLinkService
    {
        Task<SuspiciousLinkDto> Report(SuspiciousLinkDto dto, string? currentUserId);
        Task<IEnumerable<SuspiciousLinkDto>> GetByUser(string userId);
        Task<IEnumerable<SuspiciousLinkDto>> GetRecent(int limit = 100);
    }
}
