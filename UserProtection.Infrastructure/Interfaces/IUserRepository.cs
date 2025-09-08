using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetById(String id);
        Task<IEnumerable<User>> GetAll();
        Task Add(User user, string password);
        Task Update(User user);
        Task Delete(Guid id);
    }
}
