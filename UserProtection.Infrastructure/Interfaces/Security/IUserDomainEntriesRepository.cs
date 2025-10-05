using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Security
{
    public interface IUserDomainEntriesRepository
    {
        Task<IEnumerable<UserDomainEntry?>> GetByUserId(string userId);

        Task<IEnumerable<UserDomainEntry?>> GetByUserId(string userId, string status);

        Task<IEnumerable<UserDomainEntry?>> GetAll();

        Task<UserDomainEntry> Add(UserDomainEntry entity);

        Task Update(UserDomainEntry entity);
    }
}
