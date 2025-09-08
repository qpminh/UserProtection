using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Domain.Entities;

namespace UserProtection.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetById(User id);
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> GetAllUsers();
        Task Add(User user);
        Task Update(User user);
        Task Delete(Guid id);
    }
}
