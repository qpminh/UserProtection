using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Course;
using UserProtection.Application.Interfaces.Courses;

namespace UserProtection.API.Controllers.Courses
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCourseProgressController : ControllerBase
    {
        private readonly IUserCourseProgressService _service;

        public UserCourseProgressController(IUserCourseProgressService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<UserCourseProgressDto>>> GetUserProgress(string userId)
        {
            var result = await _service.GetUserProgressAsync(userId);
            return Ok(result);
        }

        [HttpGet("{userId}/course/{courseId}")]
        public async Task<ActionResult<UserCourseProgressDto>> GetProgress(string userId, int courseId)
        {
            var result = await _service.GetProgressAsync(userId, courseId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<UserCourseProgressDto>> Create([FromBody] CreateUserCourseProgressDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{userId}/course/{courseId}")]
        public async Task<ActionResult<UserCourseProgressDto>> Update(
            string userId, int courseId, [FromBody] UpdateUserCourseProgressDto dto)
        {
            var result = await _service.UpdateAsync(userId, courseId, dto);
            return Ok(result);
        }
    }
}
