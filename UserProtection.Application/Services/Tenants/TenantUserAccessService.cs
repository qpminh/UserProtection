using AutoMapper;
using UserProtection.Application.Dtos.Tenants;
using UserProtection.Application.Interfaces.Tenants;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Tenants;

namespace UserProtection.Application.Services.Tenants
{
    public class TenantUserAccessService : ITenantUserAccessService
    {
        private readonly ITenantUserAccessRepository _repository;
        private readonly IMapper _mapper;

        public TenantUserAccessService(ITenantUserAccessRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TenantUserAccessDto?> GetByIdAsync(int accessId)
        {
            var access = await _repository.GetByIdAsync(accessId);
            return access == null ? null : _mapper.Map<TenantUserAccessDto>(access);
        }

        public async Task<IEnumerable<TenantUserAccessDto>> GetByTenantAsync(int tenantId)
        {
            var list = await _repository.GetByTenantAsync(tenantId);
            return _mapper.Map<IEnumerable<TenantUserAccessDto>>(list);
        }

        public async Task<IEnumerable<TenantUserAccessDto>> GetByUserAsync(string userId)
        {
            var list = await _repository.GetByUserAsync(userId);
            return _mapper.Map<IEnumerable<TenantUserAccessDto>>(list);
        }

        public async Task<TenantUserAccessDto> CreateAsync(TenantUserAccessCreateDto dto)
        {
            var entity = _mapper.Map<TenantUserAccess>(dto);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<TenantUserAccessDto>(entity);
        }

        public async Task<TenantUserAccessDto?> UpdateAsync(int accessId, TenantUserAccessUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(accessId);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<TenantUserAccessDto>(entity);
        }

        public async Task<bool> DeleteAsync(int accessId)
        {
            var entity = await _repository.GetByIdAsync(accessId);
            if (entity == null) return false;

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
