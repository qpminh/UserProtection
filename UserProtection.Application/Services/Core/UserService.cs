using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Application.Interfaces;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Application.Services.Core
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task Add(User user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(User id)
        {
            throw new NotImplementedException();
        }

        public Task Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
