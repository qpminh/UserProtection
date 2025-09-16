using UserProtection.Application.Dtos.Security;

namespace UserProtection.Application.Interfaces.Security
{
    public interface ITrustedLinkService
    {
        Task<IEnumerable<TrustedLinkDto>> GetAll(int? tenantId = null);
        Task<TrustedLinkDto?> GetById(int id);
        Task<TrustedLinkDto> Create(TrustedLinkDto dto, string? currentUserId);
        Task<TrustedLinkDto?> Update(int id, TrustedLinkDto dto, string? currentUserId);
        Task<bool> Delete(int id, string? currentUserId);
        Task<bool> IsTrusted(string url); // helper for extension
    }
}
