using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Application.Dtos.Course;

namespace UserProtection.Application.Interfaces.Courses
{
    public interface IUserCourseProgressService
    {
        Task<IEnumerable<UserCourseProgressDto>> GetUserProgressAsync(string userId);
        Task<UserCourseProgressDto?> GetProgressAsync(string userId, int courseId);
        Task<UserCourseProgressDto> CreateAsync(CreateUserCourseProgressDto dto);
        Task<UserCourseProgressDto> UpdateAsync(string userId, int courseId, UpdateUserCourseProgressDto dto);
    }
}
