using AutoMapper;
using UserProtection.Application.Dtos.Course;
using UserProtection.Application.Interfaces.Courses;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Courses;

namespace UserProtection.Application.Services.Courses
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repo;
        private readonly IMapper _mapper;

        public EnrollmentService(IEnrollmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<EnrollmentDto?> GetByIdAsync(int id) =>
            _mapper.Map<EnrollmentDto?>(await _repo.GetByIdAsync(id));

        public async Task<IEnumerable<EnrollmentDto>> GetByCourseAsync(int courseId) =>
            _mapper.Map<IEnumerable<EnrollmentDto>>(await _repo.GetByCourseAsync(courseId));

        public async Task<IEnumerable<EnrollmentDto>> GetByUserAsync(string userId) =>
            _mapper.Map<IEnumerable<EnrollmentDto>>(await _repo.GetByUserAsync(userId));

        public async Task<EnrollmentDto> CreateAsync(int courseId, CreateEnrollmentRequest request)
        {
            var entity = new Enrollment
            {
                CourseId = courseId,
                UserId = request.UserId,
                EnrolledAt = DateTime.UtcNow,
                Status = request.Status
            };
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return _mapper.Map<EnrollmentDto>(entity);
        }

        public async Task<bool> CancelAsync(int id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return false;
            e.Status = "Cancelled";
            _repo.Update(e);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
