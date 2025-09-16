using AutoMapper;
using UserProtection.Application.Dtos.Tenant;
using UserProtection.Application.Interfaces.Tenants;
using UserProtection.Infrastructure.Interfaces.Tenants;

namespace UserProtection.Application.Services.Tenants
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _repository;
        private readonly IMapper _mapper;

        public TenantService(ITenantRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TenantDto?> GetByIdAsync(int tenantId)
        {
            var tenant = await _repository.GetByIdAsync(tenantId);
            return tenant == null ? null : _mapper.Map<TenantDto>(tenant);
        }

        public async Task<IEnumerable<TenantDto>> GetAllAsync()
        {
            var tenants = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TenantDto>>(tenants);
        }

        public async Task<TenantDto> CreateAsync(TenantCreateDto dto)
        {
            var tenant = _mapper.Map<Domain.Entities.Tenant>(dto);
            await _repository.AddAsync(tenant);
            await _repository.SaveChangesAsync();
            return _mapper.Map<TenantDto>(tenant);
        }

        public async Task<TenantDto?> UpdateAsync(int tenantId, TenantUpdateDto dto)
        {
            var tenant = await _repository.GetByIdAsync(tenantId);
            if (tenant == null) return null;

            _mapper.Map(dto, tenant);
            _repository.Update(tenant);
            await _repository.SaveChangesAsync();

            return _mapper.Map<TenantDto>(tenant);
        }

        public async Task<bool> DeleteAsync(int tenantId)
        {
            var tenant = await _repository.GetByIdAsync(tenantId);
            if (tenant == null) return false;

            _repository.Delete(tenant);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
