using UserProtection.Application.Dtos.Feature;

namespace UserProtection.Application.Interfaces.Features
{
    public interface IFeatureService
    {
        Task<IEnumerable<FeatureDto>> GetAllAsync();
        Task<FeatureDto?> GetByIdAsync(int id);
        Task<FeatureDto> CreateAsync(CreateFeatureRequest request);
        Task UpdateAsync(int id, CreateFeatureRequest request);
        Task DeleteAsync(int id);
    }
}
