using AutoMapper;
using UserProtection.Application.Dtos.Courses;
using UserProtection.Application.Interfaces.Courses;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Courses;

namespace UserProtection.Application.Services.Courses
{
    public class UserCourseProgressService : IUserCourseProgressService
    {
        private readonly IUserCourseProgressRepository _progressRepo;
        private readonly IEnrollmentRepository _enrollmentRepo;
        private readonly IMapper _mapper;

        public UserCourseProgressService(
            IUserCourseProgressRepository progressRepo,
            IEnrollmentRepository enrollmentRepo,
            IMapper mapper)
        {
            _progressRepo = progressRepo;
            _enrollmentRepo = enrollmentRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserCourseProgressDto>> GetUserProgressAsync(string userId)
        {
            var entities = await _progressRepo.GetByUserAsync(userId);
            return _mapper.Map<IEnumerable<UserCourseProgressDto>>(entities);
        }

        public async Task<UserCourseProgressDto?> GetProgressAsync(string userId, int courseId)
        {
            var entity = await _progressRepo.GetByUserAndCourseAsync(userId, courseId);
            return _mapper.Map<UserCourseProgressDto?>(entity);
        }

        public async Task<UserCourseProgressDto> CreateAsync(CreateUserCourseProgressDto dto)
        {
            var enrollment = await _enrollmentRepo.GetByUserAndCourseAsync(dto.UserId, dto.CourseId);
            if (enrollment == null)
                throw new InvalidOperationException("User chưa enroll course này.");

            var entity = _mapper.Map<UserCourseProgress>(dto);
            entity.LastAccessedAt = DateTime.UtcNow;
            entity.CompletedAt = dto.Progress >= 100 ? DateTime.UtcNow : null;

            await _progressRepo.AddAsync(entity);

            return _mapper.Map<UserCourseProgressDto>(entity);
        }

        public async Task<UserCourseProgressDto> UpdateAsync(string userId, int courseId, UpdateUserCourseProgressDto dto)
        {
            var enrollment = await _enrollmentRepo.GetByUserAndCourseAsync(userId, courseId);
            if (enrollment == null)
                throw new InvalidOperationException("User chưa enroll course này.");

            var existing = await _progressRepo.GetByUserAndCourseAsync(userId, courseId);
            if (existing == null)
                throw new KeyNotFoundException("Progress chưa tồn tại.");

            _mapper.Map(dto, existing);

            existing.LastAccessedAt = DateTime.UtcNow;
            if (dto.Progress >= 100)
                existing.CompletedAt = DateTime.UtcNow;

            await _progressRepo.UpdateAsync(existing);

            return _mapper.Map<UserCourseProgressDto>(existing);
        }
    }
}
