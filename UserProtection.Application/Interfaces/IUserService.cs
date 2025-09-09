using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Application.Dtos.Core;
using UserProtection.Domain.Entities;

namespace UserProtection.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetById(String id);
        Task<IEnumerable<UserDto>> GetAll();
        Task Add(User user, string password);
        Task Update(User user);
        Task Delete(String id);
        Task<IdentityResult> Register(User user, string password);
        Task<SignInResult> Login(string username, string password);
        Task Logout();
    }
}
