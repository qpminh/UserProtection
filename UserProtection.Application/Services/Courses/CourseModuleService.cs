using AutoMapper;
using UserProtection.Application.Dtos.Courses;
using UserProtection.Application.Interfaces.Courses;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Courses;

namespace UserProtection.Application.Services.Courses
{
    public class CourseModuleService : ICourseModuleService
    {
        private readonly ICourseModuleRepository _repo;
        private readonly IMapper _mapper;

        public CourseModuleService(ICourseModuleRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseModuleDto>> GetByCourseAsync(int courseId) =>
            _mapper.Map<IEnumerable<CourseModuleDto>>(await _repo.GetByCourseAsync(courseId));

        public async Task<CourseModuleDto?> GetByIdAsync(int id) =>
            _mapper.Map<CourseModuleDto?>(await _repo.GetByIdAsync(id));

        public async Task<CourseModuleDto> CreateAsync(int courseId, CreateCourseModuleRequest request)
        {
            var module = _mapper.Map<CourseModule>(request);
            module.CourseId = courseId;
            module.CreatedAt = DateTime.UtcNow;

            await _repo.AddAsync(module);
            await _repo.SaveChangesAsync();

            return _mapper.Map<CourseModuleDto>(module);
        }

        public async Task UpdateAsync(int id, UpdateCourseModuleRequest request)
        {
            var module = await _repo.GetByIdAsync(id) ?? throw new Exception("Module not found");
            _mapper.Map(request, module);
            await _repo.UpdateAsync(module);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var module = await _repo.GetByIdAsync(id) ?? throw new Exception("Module not found");
            await _repo.DeleteAsync(module);
            await _repo.SaveChangesAsync();
        }
    }
}
