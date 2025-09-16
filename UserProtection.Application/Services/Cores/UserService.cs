using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserProtection.Application.Dtos.Core;
using UserProtection.Application.Interfaces.Cores;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Core;

namespace UserProtection.Application.Services.Cores
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserService(
            IUserRepository userRepository,
            IAuditLogRepository auditLogRepository,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper,
            IConfiguration config)
        {
            _userRepository = userRepository;
            _auditLogRepository = auditLogRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
        }

        public async Task Add(User user, string password)
        {
            await _userRepository.Add(user, password);
        }

        public async Task Update(User user)
        {
            await _userRepository.Update(user);
        }

        public async Task Delete(string id)
        {
            await _userRepository.Delete(id);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<User> GetById(string id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<IdentityResult> Register(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await LogAction(user.Id, user.TenantId, "User Registered");
            }
            return result;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new InvalidOperationException("User not found.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded) throw new InvalidOperationException("Invalid credentials.");

            await LogAction(user.Id, user.TenantId, "User Logged In");

            return GenerateJwtToken(user);
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task LogAction(string userId, int? tenantId, string action, string? metadata = null)
        {
            await _auditLogRepository.AddLog(new AuditLog
            {
                UserId = userId,
                TenantId = tenantId,
                Action = action,
                Metadata = metadata
            });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim("tenantId", user.TenantId?.ToString() ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["Jwt:ExpireMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
