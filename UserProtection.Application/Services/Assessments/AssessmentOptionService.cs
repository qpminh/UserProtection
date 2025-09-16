using AutoMapper;
using UserProtection.Application.Dtos.Assessments;
using UserProtection.Application.Interfaces.Assessments;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Assessments;

namespace UserProtection.Application.Services.Assessments
{
    public class AssessmentOptionService : IAssessmentOptionService
    {
        private readonly IAssessmentOptionRepository _repo;
        private readonly IMapper _mapper;

        public AssessmentOptionService(IAssessmentOptionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AssessmentOptionDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<AssessmentOptionDto?>(entity);
        }

        public async Task<IEnumerable<AssessmentOptionDto>> GetByQuestionAsync(int questionId)
        {
            var entities = await _repo.GetByQuestionAsync(questionId);
            return _mapper.Map<IEnumerable<AssessmentOptionDto>>(entities);
        }

        public async Task<AssessmentOptionDto> CreateAsync(CreateAssessmentOptionDto dto)
        {
            var entity = _mapper.Map<AssessmentOption>(dto);

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<AssessmentOptionDto>(entity);
        }

        public async Task<AssessmentOptionDto> UpdateAsync(int id, UpdateAssessmentOptionDto dto)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Option not found");
            _mapper.Map(dto, entity);

            _repo.Update(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<AssessmentOptionDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Option not found");
            _repo.Delete(entity);
            await _repo.SaveChangesAsync();
        }
    }
}
