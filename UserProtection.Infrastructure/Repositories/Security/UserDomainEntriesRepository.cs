using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Security;

namespace UserProtection.Infrastructure.Repositories.Security
{
    public class UserDomainEntriesRepository : IUserDomainEntriesRepository
    {
        private readonly UserProtectionContext _context;
        public UserDomainEntriesRepository(UserProtectionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDomainEntry?>> GetByUserId(string userId)
        {
            return await _context.UserDomainEntries.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<UserDomainEntry?>> GetByUserId(string userId, string status)
        {
            return await _context.UserDomainEntries
                .Where(x => x.UserId == userId && x.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<UserDomainEntry?>> GetAll()
        {
            return await _context.UserDomainEntries.ToListAsync();
        }

        public async Task<UserDomainEntry> Add(UserDomainEntry entity)
        {
            _context.UserDomainEntries.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(UserDomainEntry entity)
        {
            _context.UserDomainEntries.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
