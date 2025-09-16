using UserProtection.Application.Dtos.Tenant;

namespace UserProtection.Application.Interfaces.Tenants
{
    public interface ITenantUserAccessService
    {
        Task<TenantUserAccessDto?> GetByIdAsync(int accessId);
        Task<IEnumerable<TenantUserAccessDto>> GetByTenantAsync(int tenantId);
        Task<IEnumerable<TenantUserAccessDto>> GetByUserAsync(string userId);
        Task<TenantUserAccessDto> CreateAsync(TenantUserAccessCreateDto dto);
        Task<TenantUserAccessDto?> UpdateAsync(int accessId, TenantUserAccessUpdateDto dto);
        Task<bool> DeleteAsync(int accessId);
    }
}
