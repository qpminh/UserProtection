using AutoMapper;
using UserProtection.Application.Dtos.Assessments;
using UserProtection.Application.Interfaces.Assessments;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Assessments;
using UserProtection.Infrastructure.Interfaces.Courses;

namespace UserProtection.Application.Services.Assessments
{
    public class AssessmentSubmissionService : IAssessmentSubmissionService
    {
        private readonly IAssessmentSubmissionRepository _repo;
        private readonly IAssessmentRepository _assessmentRepo;
        private readonly IEnrollmentRepository _enrollmentRepo;
        private readonly IMapper _mapper;

        public AssessmentSubmissionService(
            IAssessmentSubmissionRepository repo,
            IAssessmentRepository assessmentRepo,
            IEnrollmentRepository enrollmentRepo,
            IMapper mapper)
        {
            _repo = repo;
            _assessmentRepo = assessmentRepo;
            _enrollmentRepo = enrollmentRepo;
            _mapper = mapper;
        }

        public async Task<AssessmentSubmissionDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<AssessmentSubmissionDto?>(entity);
        }

        public async Task<IEnumerable<AssessmentSubmissionDto>> GetByAssessmentAsync(int assessmentId)
        {
            var entities = await _repo.GetByAssessmentAsync(assessmentId);
            return _mapper.Map<IEnumerable<AssessmentSubmissionDto>>(entities);
        }

        public async Task<IEnumerable<AssessmentSubmissionDto>> GetByUserAsync(string userId)
        {
            var entities = await _repo.GetByUserAsync(userId);
            return _mapper.Map<IEnumerable<AssessmentSubmissionDto>>(entities);
        }

        public async Task<AssessmentSubmissionDto> CreateAsync(CreateAssessmentSubmissionDto dto)
        {
            // Rule 1: Check assessment còn hạn
            var assessment = await _assessmentRepo.GetByIdAsync(dto.AssessmentId)
                ?? throw new KeyNotFoundException("Assessment not found");

            if (assessment.DueDate.HasValue && assessment.DueDate.Value < DateTime.UtcNow)
                throw new InvalidOperationException("Assessment đã hết hạn nộp.");

            // Rule 2: Check user đã enroll khóa học
            var enrollment = await _enrollmentRepo.GetByUserAndCourseAsync(dto.UserId, assessment.CourseId);
            if (enrollment == null)
                throw new InvalidOperationException("User chưa enroll course này, không thể nộp bài.");

            // Rule 3: 1 user chỉ được nộp 1 lần/assessment
            var existing = (await _repo.GetByUserAsync(dto.UserId))
                .FirstOrDefault(s => s.AssessmentId == dto.AssessmentId);

            if (existing != null)
            {
                existing.Content = dto.Content;
                existing.SubmittedAt = DateTime.UtcNow;

                _repo.Update(existing);
                await _repo.SaveChangesAsync();

                return _mapper.Map<AssessmentSubmissionDto>(existing);
            }

            // nếu chưa có thì tạo mới
            var entity = _mapper.Map<AssessmentSubmission>(dto);
            entity.SubmittedAt = DateTime.UtcNow;

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<AssessmentSubmissionDto>(entity);
        }

        public async Task<AssessmentSubmissionDto> UpdateAsync(int id, UpdateAssessmentSubmissionDto dto)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Submission not found");

            _mapper.Map(dto, entity);

            _repo.Update(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<AssessmentSubmissionDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Submission not found");
            _repo.Delete(entity);
            await _repo.SaveChangesAsync();
        }
    }
}
