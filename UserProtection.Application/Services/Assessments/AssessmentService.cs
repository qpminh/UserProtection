using AutoMapper;
using UserProtection.Application.Dtos.Assessments;
using UserProtection.Application.Interfaces.Assessments;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Assessments;

namespace UserProtection.Application.Services.Assessments
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IAssessmentRepository _repo;
        private readonly IMapper _mapper;

        public AssessmentService(IAssessmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AssessmentDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<AssessmentDto?>(entity);
        }

        public async Task<IEnumerable<AssessmentDto>> GetByCourseAsync(int courseId)
        {
            var entities = await _repo.GetByCourseAsync(courseId);
            return _mapper.Map<IEnumerable<AssessmentDto>>(entities);
        }

        public async Task<AssessmentDto> CreateAsync(CreateAssessmentDto dto)
        {
            var entity = _mapper.Map<Assessment>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.Status = "Draft";

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<AssessmentDto>(entity);
        }

        public async Task<AssessmentDto> UpdateAsync(int id, UpdateAssessmentDto dto)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Assessment not found");
            _mapper.Map(dto, entity);

            _repo.Update(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<AssessmentDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Assessment not found");
            _repo.Delete(entity);
            await _repo.SaveChangesAsync();
        }
    }
}
