using UserProtection.Application.Dtos.Tenants;

namespace UserProtection.Application.Interfaces.Tenants
{
    public interface ITenantService
    {
        Task<TenantDto?> GetByIdAsync(int tenantId);
        Task<IEnumerable<TenantDto>> GetAllAsync();
        Task<TenantDto> CreateAsync(TenantCreateDto dto);
        Task<TenantDto?> UpdateAsync(int tenantId, TenantUpdateDto dto);
        Task<bool> DeleteAsync(int tenantId);
    }
}
