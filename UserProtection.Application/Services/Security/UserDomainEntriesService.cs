using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Application.Interfaces.Security;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Security;

namespace UserProtection.Application.Services.Security
{
    public class UserDomainEntriesService : IUserDomainEntriesService
    {
        private readonly IUserDomainEntriesRepository _repository;

        public UserDomainEntriesService(IUserDomainEntriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDomainEntry> Add(UserDomainEntry entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDomainEntry?>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<UserDomainEntry?>> GetByUserId(string userId)
        {
            return await _repository.GetByUserId(userId);
        }

        public async Task<IEnumerable<UserDomainEntry?>> GetByUserId(string userId, string status)
        {
            return await _repository.GetByUserId(userId, status);
        }

        public async Task Update(UserDomainEntry entity)
        {
            throw new NotImplementedException();
        }
    }
}
