using AutoMapper;
using UserProtection.Application.Dtos.Course;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Application.Services.Course;

public class CourseService
{
    private readonly ICourseRepository _repo;
    private readonly IMapper _mapper;
    public CourseService(ICourseRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }

    public async Task<IEnumerable<CourseDto>> GetAllAsync() =>
        _mapper.Map<IEnumerable<CourseDto>>(await _repo.GetAllAsync());

    public async Task<CourseDto?> GetByIdAsync(int id) =>
        _mapper.Map<CourseDto?>(await _repo.GetByIdAsync(id));

    public async Task<CourseDto> CreateAsync(CreateCourseRequest request)
    {
        var entity = _mapper.Map<Domain.Entities.Course>(request);
        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();
        return _mapper.Map<CourseDto>(entity);
    }

    public async Task UpdateAsync(int id, CreateCourseRequest request)
    {
        var course = await _repo.GetByIdAsync(id) ?? throw new Exception("Course not found");
        course.Title = request.Title;
        course.Description = request.Description;
        course.Category = request.Category;
        course.Level = request.Level;
        course.Price = request.Price;
        await _repo.UpdateAsync(course);
        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var course = await _repo.GetByIdAsync(id) ?? throw new Exception("Course not found");
        await _repo.DeleteAsync(course);
        await _repo.SaveChangesAsync();
    }
}
