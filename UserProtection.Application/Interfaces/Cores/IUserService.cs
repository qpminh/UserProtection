using Microsoft.AspNetCore.Identity;
using UserProtection.Application.Dtos.Cores;
using UserProtection.Domain.Entities;

namespace UserProtection.Application.Interfaces.Cores
{
    public interface IUserService
    {
        Task<User> GetById(string id);
        Task<IEnumerable<UserDto>> GetAll();
        Task Add(User user, string password);
        Task Update(User user);
        Task Delete(string id);
        Task<IdentityResult> Register(User user, string password);
        Task<string> Login(string email, string password);
        Task Logout();
    }
}
