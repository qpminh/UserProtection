using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Infrastructure.Repositories.Core
{
    public class UserRepository : IUserRepository
    {
        private readonly UserProtectionContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(UserProtectionContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> GetById(String id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task Add(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<SignInResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            return result;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
