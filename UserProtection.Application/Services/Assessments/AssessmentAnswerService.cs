using AutoMapper;
using UserProtection.Application.Dtos.Assessments;
using UserProtection.Application.Interfaces.Assessments;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Assessments;

namespace UserProtection.Application.Services.Assessments
{
    public class AssessmentAnswerService : IAssessmentAnswerService
    {
        private readonly IAssessmentAnswerRepository _repo;
        private readonly IMapper _mapper;

        public AssessmentAnswerService(IAssessmentAnswerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AssessmentAnswerDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<AssessmentAnswerDto?>(entity);
        }

        public async Task<IEnumerable<AssessmentAnswerDto>> GetByAttemptAsync(int attemptId)
        {
            var entities = await _repo.GetByAttemptAsync(attemptId);
            return _mapper.Map<IEnumerable<AssessmentAnswerDto>>(entities);
        }

        public async Task<AssessmentAnswerDto> CreateAsync(AssessmentAnswerDto dto)
        {
            var entity = _mapper.Map<AssessmentAnswer>(dto);
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return _mapper.Map<AssessmentAnswerDto>(entity);
        }

        public async Task<AssessmentAnswerDto> UpdateAsync(int id, AssessmentAnswerDto dto)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Answer not found");

            _mapper.Map(dto, entity);
            _repo.Update(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<AssessmentAnswerDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Answer not found");
            _repo.Delete(entity);
            await _repo.SaveChangesAsync();
        }
    }
}
