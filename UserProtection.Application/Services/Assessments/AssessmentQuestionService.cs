using AutoMapper;
using UserProtection.Application.Dtos.Assessments;
using UserProtection.Application.Interfaces.Assessments;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Assessments;

namespace UserProtection.Application.Services.Assessments
{
    public class AssessmentQuestionService : IAssessmentQuestionService
    {
        private readonly IAssessmentQuestionRepository _repo;
        private readonly IMapper _mapper;

        public AssessmentQuestionService(IAssessmentQuestionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AssessmentQuestionDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<AssessmentQuestionDto?>(entity);
        }

        public async Task<IEnumerable<AssessmentQuestionDto>> GetByAssessmentAsync(int assessmentId)
        {
            var entities = await _repo.GetByAssessmentAsync(assessmentId);
            return _mapper.Map<IEnumerable<AssessmentQuestionDto>>(entities);
        }

        public async Task<AssessmentQuestionDto> CreateAsync(CreateAssessmentQuestionDto dto)
        {
            var entity = _mapper.Map<AssessmentQuestion>(dto);

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<AssessmentQuestionDto>(entity);
        }

        public async Task<AssessmentQuestionDto> UpdateAsync(int id, UpdateAssessmentQuestionDto dto)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Question not found");
            _mapper.Map(dto, entity);

            _repo.Update(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<AssessmentQuestionDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Question not found");
            _repo.Delete(entity);
            await _repo.SaveChangesAsync();
        }
    }
}
