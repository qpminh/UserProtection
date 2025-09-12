using AutoMapper;
using UserProtection.Application.Dtos;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Application.Services;

public class FeatureService
{
    private readonly IFeatureRepository _repo;
    private readonly IMapper _mapper;

    public FeatureService(IFeatureRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FeatureDto>> GetAllAsync()
    {
        var features = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<FeatureDto>>(features);
    }

    public async Task<FeatureDto?> GetByIdAsync(int id)
    {
        var f = await _repo.GetByIdAsync(id);
        return _mapper.Map<FeatureDto?>(f);
    }

    public async Task<FeatureDto> CreateAsync(CreateFeatureRequest request)
    {
        var entity = _mapper.Map<Feature>(request);
        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();
        return _mapper.Map<FeatureDto>(entity);
    }

    public async Task UpdateAsync(int id, CreateFeatureRequest request)
    {
        var feature = await _repo.GetByIdAsync(id);
        if (feature == null) throw new Exception("Feature not found");

        feature.Name = request.Name;
        feature.Description = request.Description;
        await _repo.UpdateAsync(feature);
        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var feature = await _repo.GetByIdAsync(id);
        if (feature == null) throw new Exception("Feature not found");

        await _repo.DeleteAsync(feature);
        await _repo.SaveChangesAsync();
    }
}